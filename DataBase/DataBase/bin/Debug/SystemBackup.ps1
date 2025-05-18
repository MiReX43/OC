
param(
    [string]$DestinationPath,
    [switch]$CreateRestorePoint,
    [switch]$IncludeAllCritical
)

function Write-Log {
    param ([string]$Message, [string]$Type = "INFO")
    $Timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    Write-Output "[$Timestamp] [$Type] $Message"
}

function Create-SystemRestorePointUserContext {
    Write-Log "Attempting to create system restore point..."
    try {
        # Эта команда может требовать запуска PowerShell от имени администратора.
        # В контексте задачи, выполняемой от SYSTEM, это должно сработать.
        Checkpoint-Computer -Description "Pre-SystemBackup Restore Point (BackupSchedulerApp)" -RestorePointType "MODIFY_SETTINGS"
        Write-Log "System restore point creation initiated." # Не всегда сразу видно результат
    }
    catch {
        Write-Log "ERROR creating system restore point: $($_.Exception.Message)" -Type "ERROR"
        Write-Log "Details: $($_.ToString())" -Type "DEBUG"
    }
}

# --- Main Script ---
Write-Log "System Backup script started."
Write-Log "Current User: $($env:USERNAME), IsAdmin: $([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)"

if ($CreateRestorePoint) {
    Create-SystemRestorePointUserContext
}

if (-not $DestinationPath) {
    Write-Log "DestinationPath parameter is missing." -Type "ERROR"
    exit 1
}

# Валидация пути назначения для wbadmin
# wbadmin требует, чтобы -backupTarget был либо буквой диска (D:), либо UNC-путем (\\server\share)
# Если указана папка на диске, wbadmin сам создаст подпапку WindowsImageBackup в КОРНЕ этого диска.
# Например, если DestinationPath = "D:\MyBackups", wbadmin будет пытаться писать в D:\WindowsImageBackup
# Поэтому, если это локальный путь, он должен быть просто буквой диска или путем к папке, но wbadmin использует корень этого диска.
# Для большей предсказуемости, если это локальный путь, лучше указывать корень диска.
# Однако, wbadmin также может принимать путь к папке на другом томе.

$isNetworkPath = $DestinationPath.StartsWith('\\')
$targetLocation = $DestinationPath

if (!$isNetworkPath) {
    try {
        $driveLetter = ([System.IO.DirectoryInfo]$DestinationPath).Root.FullName.TrimEnd('\')
        $systemDriveLetter = $env:SystemDrive.TrimEnd('\')
        if ($driveLetter -eq $systemDriveLetter) {
            Write-Log "Destination for system backup ('$DestinationPath') resolves to the system drive ('$systemDriveLetter'). This is not allowed for wbadmin. Please choose a different drive." -Type "ERROR"
            exit 1
        }
        # Если указана папка, а не корень диска, wbadmin все равно создаст WindowsImageBackup в корне диска,
        # на котором находится эта папка. Убедимся, что папка существует.
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
        $targetLocation = $driveLetter # Используем корень диска для -backupTarget если локальный путь
        Write-Log "Local destination path identified. WBAdmin will target drive: $targetLocation (WindowsImageBackup folder will be created there)."
    } catch {
        Write-Log "Error processing local destination path '$DestinationPath': $($_.Exception.Message)" -Type "ERROR"
        exit 1
    }
} else {
     Write-Log "Network destination path identified: $targetLocation"
}


Write-Log "Starting Windows System Backup using wbadmin..."
Write-Log "Target location for wbadmin: $targetLocation"

$wbadminArgs = @(
    "start",
    "backup",
    "-backupTarget:$targetLocation", # Используем обработанный targetLocation
    "-quiet" # Выполнение без интерактивных запросов
)

if ($IncludeAllCritical) {
    $wbadminArgs += "-allCritical" # Включает все тома, необходимые для восстановления ОС
    Write-Log "Including all critical system volumes."
} else {
    # Обычно для системного бэкапа -allCritical является предпочтительным.
    # Если пользователь не выбрал, можно добавить системный диск, но это не гарантирует полное восстановление.
    $systemDrive = $env:SystemDrive
    $wbadminArgs += "-include:$systemDrive"
    Write-Log "Including only system drive: $systemDrive (Warning: For full system recovery, using '-allCritical' is highly recommended)." -Type "WARN"
}

Write-Log "Executing: wbadmin.exe $($wbadminArgs -join ' ')"

try {
    $process = Start-Process "wbadmin.exe" -ArgumentList $wbadminArgs -Wait -PassThru 
    
    if ($process.ExitCode -eq 0) {
        Write-Log "Windows System Backup completed successfully."
        # Дополнительно можно проверить наличие файла каталога бэкапа
        # Get-ChildItem -Path (Join-Path $targetLocation "WindowsImageBackup\\$($env:COMPUTERNAME)\\Catalog\\BackupGlobalCatalog")
        exit 0
    }
    else {
        Write-Log "Windows System Backup failed. wbadmin.exe exit code: $($process.ExitCode)." -Type "ERROR"
        Write-Log "Common issues: Insufficient permissions (ensure task runs as SYSTEM or Admin), destination not a valid target (must be a drive letter like D: or a UNC path \\server\share, not a subfolder on the system drive for -backupTarget), insufficient space, VSS (Volume Shadow Copy Service) errors." -Type "INFO"
        Write-Log "Check Windows Event Logs (Application and System logs, source: 'Backup', 'wbengine', 'SPP', 'VSS') for detailed error messages." -Type "INFO"
        exit $process.ExitCode
    }
}
catch {
    Write-Log "An exception occurred while trying to run wbadmin.exe: $($_.Exception.ToString())" -Type "ERROR"
    if ($_.Exception.InnerException) {
        Write-Log "Inner Exception: $($_.Exception.InnerException.ToString())" -Type "ERROR"
    }
    exit 1
}

Write-Log "System Backup script finished."
