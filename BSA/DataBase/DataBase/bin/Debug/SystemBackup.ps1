
param(
    [string]$DestinationPath,
    [switch]$CreateRestorePoint,
    [switch]$IncludeAllCritical
)

function Write-Log {
    param ([string]$Message, [string]$Type = "INFO")
    $Timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    # Эта строка выводит в стандартный поток вывода, который будет перехвачен C#
    Write-Output "[$Timestamp] [$Type] $Message" 
    # Дополнительно можно писать в файл лога из PowerShell, если требуется отдельный лог скрипта
    # Add-Content -Path (Join-Path $PSScriptRoot "BackupScript.log") -Value "[$Timestamp] [$Type] $Message"
}

function Create-SystemRestorePointElevated {
    Write-Log "Attempting to create system restore point (elevated)..."
    try {
        # Эта команда требует запуска PowerShell от имени администратора.
        Checkpoint-Computer -Description "Pre-SystemBackup Restore Point (BackupSchedulerApp)" -RestorePointType "MODIFY_SETTINGS"
        Write-Log "System restore point creation initiated."
    }
    catch {
        Write-Log "ERROR creating system restore point: $($_.Exception.Message)" -Type "ERROR"
        Write-Log "Details: $($_.ToString())" -Type "DEBUG"
    }
}

# --- Main Script ---
Write-Log "System Backup script started."
Write-Log "Script Path: $PSScriptRoot"
Write-Log "Current User: $($env:USERNAME), IsAdmin: $([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)"

if ($CreateRestorePoint) {
    Create-SystemRestorePointElevated
}

if (-not $DestinationPath) {
    Write-Log "DestinationPath parameter is missing." -Type "ERROR"
    exit 1 # Важно использовать exit коды для C#
}

$isNetworkPath = $DestinationPath.StartsWith('\\')
$targetLocationForWbadmin = $DestinationPath # Изначально предполагаем, что путь уже корректен для wbadmin

if (!$isNetworkPath) {
    try {
        # Проверяем, существует ли указанный локальный путь (папка)
        if (-not (Test-Path -Path $DestinationPath -PathType Container)) {
            Write-Log "Destination folder '$DestinationPath' does not exist. Attempting to create."
            try {
                New-Item -ItemType Directory -Path $DestinationPath -Force -ErrorAction Stop | Out-Null
                Write-Log "Successfully created destination folder '$DestinationPath'."
            } catch {
                Write-Log "Failed to create destination folder '$DestinationPath'. Error: $($_.Exception.Message)" -Type "ERROR"
                exit 1
            }
        }
        
        # Для wbadmin -backupTarget должен быть буквой диска (например, D:) или UNC-путем.
        # Он не может быть путем к папке типа D:\Backups. wbadmin сам создаст WindowsImageBackup в корне диска.
        $driveLetter = ([System.IO.DirectoryInfo]$DestinationPath).Root.FullName.TrimEnd('\')
        $systemDriveLetter = $env:SystemDrive.TrimEnd('\')
        
        if ($driveLetter -eq $systemDriveLetter) {
            Write-Log "Destination for system backup ('$DestinationPath') resolves to the system drive ('$systemDriveLetter'). This is not allowed for wbadmin's -backupTarget. Please choose a different drive." -Type "ERROR"
            exit 1
        }
        $targetLocationForWbadmin = $driveLetter # Используем корень диска для -backupTarget
        Write-Log "Local destination path identified. User specified: '$DestinationPath'. WBAdmin will target drive: $targetLocationForWbadmin (A 'WindowsImageBackup' folder will be created there)."
    } catch {
        Write-Log "Error processing local destination path '$DestinationPath': $($_.Exception.Message)" -Type "ERROR"
        exit 1
    }
} else {
     Write-Log "Network destination path identified: $targetLocationForWbadmin"
     # Для сетевых путей wbadmin использует UNC путь как есть.
     # Убедитесь, что учетная запись SYSTEM (для запланированных задач) или администратор (для ручного теста)
     # имеет права на запись в эту сетевую папку.
}


Write-Log "Starting Windows System Backup using wbadmin..."
Write-Log "Target location for wbadmin: $targetLocationForWbadmin"

$wbadminArgs = @(
    "start",
    "backup",
    "-backupTarget:$targetLocationForWbadmin", # Используем обработанный targetLocationForWbadmin
    "-quiet" # Выполнение без интерактивных запросов
)

if ($IncludeAllCritical) {
    $wbadminArgs += "-allCritical" # Включает все тома, необходимые для восстановления ОС
    Write-Log "Including all critical system volumes."
} else {
    # Если -allCritical не выбран, wbadmin по умолчанию попытается включить системный диск.
    # Можно явно указать системный диск, но -allCritical предпочтительнее для полного бэкапа.
    $systemDrive = $env:SystemDrive
    $wbadminArgs += "-include:$systemDrive"
    Write-Log "Including only system drive: $systemDrive (Warning: For full system recovery, using '-allCritical' is highly recommended)." -Type "WARN"
}

Write-Log "Executing: wbadmin.exe $($wbadminArgs -join ' ')"

try {
    # -NoNewWindow предотвращает мелькание окна PowerShell, если скрипт запускается интерактивно
    $process = Start-Process "wbadmin.exe" -ArgumentList $wbadminArgs -Wait -PassThru -NoNewWindow 
    
    # Проверка стандартного потока ошибок wbadmin (хотя -quiet должен подавлять большинство выводов)
    # Эта часть может быть избыточной, если C# уже перехватывает stderr.
    # if ($process.StandardError) { 
    #    $errorOutput = $process.StandardError | ForEach-Object {$_.ToString()}
    #    if ($errorOutput) {
    #        Write-Log "wbadmin stderr: $errorOutput" -Type "DEBUG"
    #    }
    # }

    if ($process.ExitCode -eq 0) {
        Write-Log "Windows System Backup completed successfully."
        # Можно добавить проверку наличия каталога бэкапа, если необходимо
        # Get-ChildItem -Path (Join-Path $targetLocationForWbadmin "WindowsImageBackup\\$($env:COMPUTERNAME)\\Catalog\\BackupGlobalCatalog")
        exit 0 # Успешное завершение
    }
    else {
        Write-Log "Windows System Backup failed. wbadmin.exe exit code: $($process.ExitCode)." -Type "ERROR"
        Write-Log "Common issues: Insufficient permissions (ensure task runs as SYSTEM or Admin), destination not a valid target (must be a drive letter like D: or a UNC path \\server\share, not a subfolder on the system drive for -backupTarget), insufficient space, VSS (Volume Shadow Copy Service) errors." -Type "INFO"
        Write-Log "Check Windows Event Logs (Application and System logs, source: 'Backup', 'wbengine', 'SPP', 'VSS') for detailed error messages." -Type "INFO"
        exit $process.ExitCode # Возвращаем код ошибки wbadmin
    }
}
catch {
    Write-Log "An exception occurred while trying to run wbadmin.exe: $($_.Exception.ToString())" -Type "ERROR"
    if ($_.Exception.InnerException) {
        Write-Log "Inner Exception: $($_.Exception.InnerException.ToString())" -Type "ERROR"
    }
    exit 1 # Общий код ошибки для исключений в скрипте
}

Write-Log "System Backup script finished."
