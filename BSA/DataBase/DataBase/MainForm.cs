using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using System.Threading.Tasks;
using ComponentFactory.Krypton.Toolkit;
// using ComponentFactory.Krypton.Navigator; // This using seems unused in the provided code snippet
using Newtonsoft.Json; // Added for JSON serialization

namespace DataBase
{
    public partial class MainForm : KryptonForm
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private const string TASK_NAME = "WindowsSystemBackupScheduledTask";
        // APP_NAME is now in Program.cs to be accessible by both.
        private string psScriptPath; // Path to PowerShell script, typically in LocalAppData for manual runs

        private string GetScriptPath(bool forSystemTask)
        {
            string baseDir;
            if (forSystemTask)
            {
                // CommonApplicationData is typically C:\ProgramData
                baseDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Program.APP_NAME);
            }
            else
            {
                // LocalApplicationData is typically C:\Users\<username>\AppData\Local
                baseDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Program.APP_NAME);
            }
            Directory.CreateDirectory(baseDir); // Ensure directory exists (might need admin for CommonApplicationData first time)
            return Path.Combine(baseDir, "SystemBackup.ps1");
        }

        private string PreparePowerShellScript(bool forSystemTask)
        {
            string scriptPathToPrepare = GetScriptPath(forSystemTask);
            string scriptContent = @"
param(
    [string]$DestinationPath,
    [switch]$CreateRestorePoint,
    [switch]$IncludeAllCritical,
    [string]$LogFile = (Join-Path $env:TEMP ""SystemBackupLog.txt"") # Default if not provided, script should create/append
)

function Write-Log {
    param ([string]$Message, [string]$Type = ""INFO"")
    $Timestamp = Get-Date -Format ""yyyy-MM-dd HH:mm:ss""
    $LogEntry = ""[$Timestamp] [$Type] $Message""
    Write-Output $LogEntry # Keep for direct console output if any (e.g. manual PS run)
    try {
        Out-File -FilePath $LogFile -Append -InputObject $LogEntry -Encoding UTF8 -ErrorAction Stop
    } catch {
        Write-Warning ""Failed to write to log file $LogFile. Error: $($_.Exception.Message)""
    }
}

function Create-SystemRestorePointUserContext {
    Write-Log ""Attempting to create system restore point...""
    try {
        Checkpoint-Computer -Description ""Pre-SystemBackup Restore Point (BackupSchedulerApp)"" -RestorePointType ""MODIFY_SETTINGS""
        Write-Log ""System restore point creation initiated.""
    }
    catch {
        Write-Log ""ERROR creating system restore point: $($_.Exception.Message)"" -Type ""ERROR""
        Write-Log ""Details: $($_.ToString())"" -Type ""DEBUG""
    }
}

# --- Main Script ---
Write-Log ""System Backup script started.""
Write-Log ""LogFile path: $LogFile""
Write-Log ""Current User: $($env:USERNAME), IsAdmin: $([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)""

if ($CreateRestorePoint) {
    Create-SystemRestorePointUserContext
}

if (-not $DestinationPath) {
    Write-Log ""DestinationPath parameter is missing."" -Type ""ERROR""
    exit 1
}

$isNetworkPath = $DestinationPath.StartsWith('\\')
$targetLocation = $DestinationPath

if (!$isNetworkPath) {
    try {
        $driveLetter = ([System.IO.DirectoryInfo]$DestinationPath).Root.FullName.TrimEnd('\')
        $systemDriveLetter = $env:SystemDrive.TrimEnd('\')
        if ($driveLetter -eq $systemDriveLetter) {
            Write-Log ""Destination for system backup ('$DestinationPath') resolves to the system drive ('$systemDriveLetter'). This is not allowed for wbadmin. Please choose a different drive."" -Type ""ERROR""
            exit 1
        }
        if (-not (Test-Path -Path $DestinationPath -PathType Container)) {
            Write-Log ""Destination folder '$DestinationPath' does not exist. Attempting to create.""
            try {
                New-Item -ItemType Directory -Path $DestinationPath -Force -ErrorAction Stop | Out-Null
                Write-Log ""Successfully created destination folder '$DestinationPath'.""
            } catch {
                Write-Log ""Failed to create destination folder '$DestinationPath'. Error: $($_.Exception.Message)"" -Type ""ERROR""
                exit 1
            }
        }
        $targetLocation = $driveLetter 
        Write-Log ""Local destination path identified. WBAdmin will target drive: $targetLocation (WindowsImageBackup folder will be created there).""
    } catch {
        Write-Log ""Error processing local destination path '$DestinationPath': $($_.Exception.Message)"" -Type ""ERROR""
        exit 1
    }
} else {
     Write-Log ""Network destination path identified: $targetLocation""
}

Write-Log ""Starting Windows System Backup using wbadmin...""
Write-Log ""Target location for wbadmin: $targetLocation""

$wbadminArgs = @(
    ""start"",
    ""backup"",
    ""-backupTarget:$targetLocation"",
    ""-quiet"" 
)

if ($IncludeAllCritical) {
    $wbadminArgs += ""-allCritical"" 
    Write-Log ""Including all critical system volumes.""
} else {
    $systemDrive = $env:SystemDrive
    $wbadminArgs += ""-include:$systemDrive""
    Write-Log ""Including only system drive: $systemDrive (Warning: For full system recovery, using '-allCritical' is highly recommended)."" -Type ""WARN""
}

Write-Log ""Executing: wbadmin.exe $($wbadminArgs -join ' ')""

try {
    # Using Start-Process for wbadmin.exe
    $process = Start-Process ""wbadmin.exe"" -ArgumentList $wbadminArgs -Wait -PassThru -ErrorAction Stop
    
    if ($process.ExitCode -eq 0) {
        Write-Log ""Windows System Backup completed successfully.""
        exit 0
    }
    else {
        Write-Log ""Windows System Backup failed. wbadmin.exe exit code: $($process.ExitCode)."" -Type ""ERROR""
        Write-Log ""Common issues: Insufficient permissions (ensure task runs as SYSTEM or Admin), destination not a valid target (must be a drive letter like D: or a UNC path \\server\share, not a subfolder on the system drive for -backupTarget), insufficient space, VSS (Volume Shadow Copy Service) errors."" -Type ""INFO""
        Write-Log ""Check Windows Event Logs (Application and System logs, source: 'Backup', 'wbengine', 'SPP', 'VSS') for detailed error messages."" -Type ""INFO""
        exit $process.ExitCode
    }
}
catch {
    Write-Log ""An exception occurred while trying to run wbadmin.exe: $($_.Exception.ToString())"" -Type ""ERROR""
    if ($_.Exception.InnerException) {
        Write-Log ""Inner Exception: $($_.Exception.InnerException.ToString())"" -Type ""ERROR""
    }
    exit 1
}

Write-Log ""System Backup script finished.""
";
            try
            {
                File.WriteAllText(scriptPathToPrepare, scriptContent, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                // This might be called from an elevated context (ExecuteSchedulingLogicElevated) or non-elevated.
                // If non-elevated and trying to write to CommonApplicationData, it might fail if permissions aren't set.
                string logMessage = $"ОШИБКА написание файла скрипта PowerShell '{scriptPathToPrepare}': {ex.Message}\r\n";
                if (IsElevated())
                { // Simple check if we are in an elevated context for message box
                    MessageBox.Show(logMessage, "Ошибка скрипта (повышенный уровень)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    AppendLogTextSafe(logMessage); // Log to GUI if possible
                }
                throw new IOException($"Не удалось написать скрипт PowerShell: {ex.Message}", ex);
            }
            return scriptPathToPrepare;
        }


        private void InitializeCustomComponents()
        {
            cmbFrequency.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFrequency.Items.AddRange(new string[] { "Ежедневно", "Еженедельно", "Ежемесячно" });
            cmbFrequency.SelectedIndex = 0;

            dtpTime.Format = DateTimePickerFormat.Time;
            dtpTime.ShowUpDown = true;

            clbDaysOfWeek.Visible = false;
            clbDaysOfWeek.CheckOnClick = true;
            clbDaysOfWeek.Items.AddRange(new string[] { "Воскресенье", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" });

            txtLog.Multiline = true;
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.BackColor = System.Drawing.Color.Black;
            txtLog.ForeColor = System.Drawing.Color.LightGreen;
            txtLog.Font = new System.Drawing.Font("Consolas", 9);

            try
            {
                // Prepare script for manual run in LocalAppData
                psScriptPath = PreparePowerShellScript(false);
                AppendLogTextSafe($"Скрипт PowerShell для ручного запуска подготовлен по адресу: {psScriptPath}\r\n");
            }
            catch (Exception ex)
            {
                AppendLogTextSafe($"ФАТАЛЬНАЯ ОШИБКА при подготовке скрипта PowerShell: {ex.Message}\r\nПриложение может работать некорректно.\r\n");
                MessageBox.Show(this, $"ФАТАЛЬНАЯ ОШИБКА при подготовке скрипта PowerShell: {ex.Message}\r\nПриложение может работать некорректно.", "Script Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnTestBackup.Enabled = false;
                btnSaveSchedule.Enabled = false;
            }
            cmbFrequency_SelectedIndexChanged(null, null);
        }

        void Main_Load(object sender, EventArgs e)
        {
            cmbFrequency_SelectedIndexChanged(null, null);
            // Check if this instance is the elevated one for scheduling and exit if so,
            // this is handled by Program.cs now.
        }

        private void btnBrowseDestination_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Выберите диск(отличный от системного диска (C:\\)) или папку назначения резервного копирования(пр., D:\\Backup, \\\\Server\\Share).";
                fbd.ShowNewFolderButton = true;
                if (fbd.ShowDialog(this) == DialogResult.OK)
                {
                    txtDestinationPath.Text = fbd.SelectedPath;
                }
            }
        }

        private void cmbFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            string freq = cmbFrequency.SelectedItem?.ToString() ?? "Ежедневно";
            clbDaysOfWeek.Visible = (freq == "Еженедельно");
            UpdateLayoutAfterFrequencyChange();
        }

        private void UpdateLayoutAfterFrequencyChange()
        {
            int top = cmbFrequency.Top + cmbFrequency.Height + 10;
            if (clbDaysOfWeek.Visible)
            {
                clbDaysOfWeek.Top = top;
                top += clbDaysOfWeek.Height + 10;
            }
            chkIncludeAllCritical.Top = top;
            top += chkIncludeAllCritical.Height + 10;
            chkCreateRestorePoint.Top = top;
            top += chkCreateRestorePoint.Height + 20;
            btnTestBackup.Top = top;
            btnSaveSchedule.Top = top;
            top += btnTestBackup.Height + 20;
            txtLog.Top = top;
            txtLog.Height = this.ClientSize.Height - top - 20;
        }

        private async void btnTestBackup_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
            if (!ValidateDestinationPath()) return;

            try // Re-prepare script for manual run in case it was deleted or is outdated
            {
                psScriptPath = PreparePowerShellScript(false);
                if (string.IsNullOrEmpty(psScriptPath) || !File.Exists(psScriptPath))
                {
                    AppendLogTextSafe("ОШИБКА: Путь к скрипту PowerShell не задан или файл сценария не найден. Невозможно запустить резервное копирование.\r\n");
                    return;
                }
            }
            catch (Exception ex)
            {
                AppendLogTextSafe($"ОШИБКА при подготовке скрипта PowerShell для ручного запуска: {ex.Message}\r\n");
                MessageBox.Show(this, $"Ошибка при подготовке скрипта: {ex.Message}", "Script Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            AppendLogTextSafe("Запуск резервного копирования вручную...\r\n");
            btnTestBackup.Enabled = false;
            btnSaveSchedule.Enabled = false;

            try
            {
                await RunBackupProcess(txtDestinationPath.Text, chkCreateRestorePoint.Checked, chkIncludeAllCritical.Checked);
                // Log appending is now handled within RunBackupProcess after reading the PS log file
            }
            catch (Exception ex)
            {
                AppendLogTextSafe($"Ошибка при попытке запустить процесс резервного копирования: {ex.Message}\r\n");
            }
            finally
            {
                btnTestBackup.Enabled = true;
                btnSaveSchedule.Enabled = true;
            }
        }

        private async System.Threading.Tasks.Task RunBackupProcess(string destinationPath, bool createRestorePoint, bool includeAllCritical)
        {
            // psScriptPath should be set by InitializeCustomComponents or btnTestBackup_Click to the LocalAppData path
            if (string.IsNullOrEmpty(psScriptPath) || !File.Exists(psScriptPath))
            {
                AppendLogTextSafe("ОШИБКА: Сценарий резервного копирования PowerShell не найден или не подготовлен в RunBackupProcess.\r\n");
                return;
            }

            string logFilePath = Path.Combine(Path.GetTempPath(), $"SystemBackupLog_Manual_{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid().ToString().Substring(0, 8)}.txt");

            StringBuilder args = new StringBuilder();
            args.Append($"-ExecutionPolicy Bypass -NoProfile -File \"{psScriptPath}\"");
            args.Append($" -DestinationPath \"{destinationPath.Trim()}\"");
            if (createRestorePoint) args.Append(" -CreateRestorePoint");
            if (includeAllCritical) args.Append(" -IncludeAllCritical");
            args.Append($" -LogFile \"{logFilePath}\"");

            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = args.ToString(),
                UseShellExecute = true,
                Verb = "runas", // UAC Ppompt
                CreateNoWindow = true, // Attempt to hide, might flash
            };

            AppendLogTextSafe($"Попытка запуска с повышенными привилегиями: powershell.exe {args.ToString()}\r\n");
            AppendLogTextSafe($"Журнал скрипта PowerShell будет находиться по адресу: {logFilePath}\r\n");

            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo = psi;
                    if (process.Start())
                    {
                        AppendLogTextSafe("Запущен процесс PowerShell. Ожидание завершения...\r\n");
                        await System.Threading.Tasks.Task.Run(() => process.WaitForExit()); // Keep UI responsive

                        if (process.ExitCode == 0)
                        {
                            AppendLogTextSafe($"Процесс резервного копирования успешно завершен (Код выхода: {process.ExitCode}).\r\n");
                        }
                        else
                        {
                            AppendLogTextSafe($"Процесс резервного копирования завершен с помощью кода: {process.ExitCode}.\r\n");
                        }
                    }
                    else
                    {
                        AppendLogTextSafe("Не удалось запустить процесс PowerShell (отказ UAC или другая проблема).\r\n");
                    }
                }
            }
            catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode == 1223) // ERROR_CANCELLED (UAC)
            {
                AppendLogTextSafe("Операция отменена пользователем (запрос UAC отклонен) или права администратора PowerShell не предоставлены.\r\n");
            }
            catch (Exception ex)
            {
                AppendLogTextSafe($"Исключение во время выполнения процесса резервного копирования: {ex.ToString()}\r\n");
            }
            finally
            {
                if (File.Exists(logFilePath))
                {
                    try
                    {
                        string scriptOutput = File.ReadAllText(logFilePath);
                        AppendLogTextSafe("--- Вывод журнала скрипта PowerShell ---\r\n");
                        AppendLogTextSafe(scriptOutput + "\r\n");
                        AppendLogTextSafe("--- Конец журнала скрипта PowerShell ---\r\n");
                        // File.Delete(logFilePath); // Optionally delete
                    }
                    catch (Exception ex)
                    {
                        AppendLogTextSafe($"Ошибка при чтении файла журнала PowerShell '{logFilePath}': {ex.Message}\r\n");
                    }
                }
                else
                {
                    AppendLogTextSafe($"Файл журнала PowerShell не найден: {logFilePath}\r\n");
                }
            }
        }

        private static bool IsElevated() // Helper to check if current process is elevated
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        private void btnSaveSchedule_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
            if (!ValidateDestinationPath()) return;

            string scriptPathForSystemTask;
            try
            {
                // Prepare/ensure script exists in CommonApplicationData for SYSTEM execution
                scriptPathForSystemTask = PreparePowerShellScript(true); // true = forSystemTask
                if (string.IsNullOrEmpty(scriptPathForSystemTask) || !File.Exists(scriptPathForSystemTask))
                {
                    AppendLogTextSafe("ОШИБКА: Не удалось подготовить скрипт PowerShell для запланированного задания в CommonApplicationData.\r\nПроверьте права доступа и попробуйте запустить приложение от имени администратора, чтобы создать папку, если это происходит впервые.\r\n");
                    MessageBox.Show(this, "Не удалось подготовить сценарий к планированию в ProgramData. Возможно, вам потребуется запустить это приложение от имени администратора, чтобы создать необходимые каталоги в ProgramData, или проверить права доступа.", "Script Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                AppendLogTextSafe($"Скрипт PowerShell для запланированного задания находится по адресу: {scriptPathForSystemTask}\r\n");
            }
            catch (Exception ex)
            {
                AppendLogTextSafe($"ОШИБКА: Не удалось подготовить скрипт PowerShell для запланированного задания: {ex.Message}\r\nДля этого обычно требуются права администратора, если {Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Program.APP_NAME)} doesn't exist or has wrong permissions.\r\n");
                MessageBox.Show(this, $"Не удалось подготовить скрипт к планированию: {ex.Message}\nПопробуйте запустить это приложение от имени администратора один раз.", "Script Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DaysOfTheWeek selectedDays = 0;
            if (cmbFrequency.SelectedItem.ToString() == "Еженедельно")
            {
                if (clbDaysOfWeek.CheckedItems.Count == 0) { /* Handled by ExecuteSchedulingLogicElevated or its caller */ }
                if (clbDaysOfWeek.GetItemChecked(0)) selectedDays |= DaysOfTheWeek.Sunday;
                if (clbDaysOfWeek.GetItemChecked(1)) selectedDays |= DaysOfTheWeek.Monday;
                // ... (rest of days)
                if (clbDaysOfWeek.GetItemChecked(2)) selectedDays |= DaysOfTheWeek.Tuesday;
                if (clbDaysOfWeek.GetItemChecked(3)) selectedDays |= DaysOfTheWeek.Wednesday;
                if (clbDaysOfWeek.GetItemChecked(4)) selectedDays |= DaysOfTheWeek.Thursday;
                if (clbDaysOfWeek.GetItemChecked(5)) selectedDays |= DaysOfTheWeek.Friday;
                if (clbDaysOfWeek.GetItemChecked(6)) selectedDays |= DaysOfTheWeek.Saturday;
            }

            ScheduleParameters parameters = new ScheduleParameters
            {
                DestinationPath = txtDestinationPath.Text.Trim(),
                Frequency = cmbFrequency.SelectedItem.ToString(),
                TimeOfDay = dtpTime.Value, // This is DateTime, ExecuteSchedulingLogicElevated will use .TimeOfDay
                DaysOfWeek = selectedDays,
                CreateRestorePoint = chkCreateRestorePoint.Checked,
                IncludeAllCritical = chkIncludeAllCritical.Checked,
                PsScriptPathForSystemTask = scriptPathForSystemTask
            };

            if (!IsElevated())
            {
                AppendLogTextSafe("Для составления расписания требуются права администратора. Запрос повышения...\r\n");
                string tempParamsFile = Path.Combine(Path.GetTempPath(), $"schedule_params_{Guid.NewGuid()}.json");
                try
                {
                    string jsonParams = JsonConvert.SerializeObject(parameters);
                    File.WriteAllText(tempParamsFile, jsonParams);

                    ProcessStartInfo proc = new ProcessStartInfo
                    {
                        UseShellExecute = true,
                        WorkingDirectory = Environment.CurrentDirectory,
                        FileName = Application.ExecutablePath,
                        Verb = "runas",
                        Arguments = $"--schedule-task-elevated \"{tempParamsFile}\""
                    };
                    Process.Start(proc); // This will start a new elevated process
                    AppendLogTextSafe("Отправлен запрос на повышение. В случае одобрения задание будет запланировано отдельным процессом.\r\n");
                }
                catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode == 1223) // ERROR_CANCELLED (UAC)
                {
                    AppendLogTextSafe("Операция отменена пользователем (запрос UAC отклонен).\r\n");
                    MessageBox.Show(this, "Планирование отменено или права администратора не предоставлены.", "Операция отменена", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (File.Exists(tempParamsFile)) try { File.Delete(tempParamsFile); } catch { }
                }
                catch (Exception ex)
                {
                    AppendLogTextSafe($"Ошибка при попытке перезапуска с повышенными привилегиями: {ex.Message}\r\n");
                    MessageBox.Show(this, $"Ошибка при попытке поднятия: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (File.Exists(tempParamsFile)) try { File.Delete(tempParamsFile); } catch { }
                }
            }
            else // Already admin (this branch typically won't be hit by user GUI directly after changes)
            {
                AppendLogTextSafe("Непосредственное выполнение логики планирования (уже повышено)...\r\n");
                ExecuteSchedulingLogicElevated(parameters);
            }
        }


        public static void ExecuteSchedulingLogicElevated(ScheduleParameters taskParams)
        {
            if (taskParams == null)
            {
                MessageBox.Show("Параметры задачи отсутствуют.", "Ошибка планировщика (повышенный уровень)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(taskParams.PsScriptPathForSystemTask) || !File.Exists(taskParams.PsScriptPathForSystemTask))
            {
                MessageBox.Show($"Скрипт PowerShell для запланированной задачи не найден по адресу {taskParams.PsScriptPathForSystemTask}. Невозможно запланировать задание.", "Ошибка планировщика (Elevated)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (taskParams.Frequency == "Еженедельно" && taskParams.DaysOfWeek == 0)
            {
                MessageBox.Show("Для еженедельного расписания необходимо выбрать хотя бы один день.", "Ошибка ввода (Elevated)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            try
            {
                using (TaskService ts = new TaskService())
                {
                    ts.RootFolder.DeleteTask(TASK_NAME, false);

                    TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = "Резервное копирование системы Windows по расписанию с помощью BackupSchedulerApp";
                    td.RegistrationInfo.Author = WindowsIdentity.GetCurrent().Name;
                    td.Principal.RunLevel = TaskRunLevel.Highest;

                    DateTime triggerStartTime = DateTime.Today.Date + taskParams.TimeOfDay.TimeOfDay;
                    // If the calculated start time is in the past for today, schedule for the next occurrence.
                    if (triggerStartTime < DateTime.Now)
                    {
                        triggerStartTime = triggerStartTime.AddDays(1); // For daily, this pushes to tomorrow. Weekly/Monthly logic below handles it.
                    }

                    Trigger trigger;
                    switch (taskParams.Frequency)
                    {
                        case "Ежедневно":
                            trigger = new DailyTrigger { StartBoundary = triggerStartTime, DaysInterval = 1 };
                            break;
                        case "Еженедельно":
                            // Adjust startBoundary to be the next valid selected weekday if today + time is passed or not a selected day
                            trigger = new WeeklyTrigger { StartBoundary = triggerStartTime, WeeksInterval = 1, DaysOfWeek = taskParams.DaysOfWeek };
                            break;
                        case "Ежемесячно":
                            int dayOfMonth = 1; // Or make this configurable via ScheduleParameters
                            DateTime firstPossibleMonthlyRun = new DateTime(DateTime.Today.Year, DateTime.Today.Month, dayOfMonth) + taskParams.TimeOfDay.TimeOfDay;
                            if (firstPossibleMonthlyRun < DateTime.Now)
                            {
                                firstPossibleMonthlyRun = firstPossibleMonthlyRun.AddMonths(1);
                            }
                            trigger = new MonthlyTrigger { StartBoundary = firstPossibleMonthlyRun, DaysOfMonth = new[] { dayOfMonth }, MonthsOfYear = MonthsOfTheYear.AllMonths };
                            break;
                        default:
                            MessageBox.Show("Выбрана недопустимая частота.", "Ошибка планировщика (Elevated)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }
                    td.Triggers.Add(trigger);

                    string scheduledTaskLogDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Program.APP_NAME, "Logs");
                    Directory.CreateDirectory(scheduledTaskLogDir);
                    string scheduledTaskLogFile = Path.Combine(scheduledTaskLogDir, "ScheduledSystemBackupLog.txt");

                    StringBuilder psArgs = new StringBuilder();
                    psArgs.Append($"-ExecutionPolicy Bypass -NoProfile -File \"{taskParams.PsScriptPathForSystemTask}\"");
                    psArgs.Append($" -DestinationPath \"{taskParams.DestinationPath}\"");
                    if (taskParams.CreateRestorePoint) psArgs.Append(" -CreateRestorePoint");
                    if (taskParams.IncludeAllCritical) psArgs.Append(" -IncludeAllCritical");
                    psArgs.Append($" -LogFile \"{scheduledTaskLogFile}\"");

                    td.Actions.Add(new ExecAction("powershell.exe", psArgs.ToString(), Path.GetDirectoryName(taskParams.PsScriptPathForSystemTask)));
                    td.Settings.MultipleInstances = TaskInstancesPolicy.IgnoreNew;
                    td.Settings.DisallowStartIfOnBatteries = false;
                    td.Settings.StopIfGoingOnBatteries = false;
                    td.Settings.AllowHardTerminate = true;
                    td.Settings.StartWhenAvailable = true;
                    td.Settings.ExecutionTimeLimit = TimeSpan.Zero;
                    td.Settings.Enabled = true;

                    ts.RootFolder.RegisterTaskDefinition(TASK_NAME, td, TaskCreation.CreateOrUpdate, "SYSTEM", null, TaskLogonType.ServiceAccount);
                    MessageBox.Show($"Запланированное задание '{TASK_NAME}' успешно создан/обновлен для запуска от имени SYSTEM.\nTrigger: {trigger.ToString()}", "Task Scheduled (Elevated)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании/обновлении запланированной задачи: {ex.Message}\n{ex.StackTrace}", "Ошибка планировщика (Elevated)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool ValidateDestinationPath()
        {
            string destPath = txtDestinationPath.Text.Trim();
            if (string.IsNullOrWhiteSpace(destPath))
            {
                MessageBox.Show(this, "Укажите путь назначения (e.g., D:\\SystemBackup or \\\\ServerName\\Share).", "Неверный путь", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDestinationPath.Focus();
                return false;
            }
            try
            {
                if (Path.IsPathRooted(destPath) && !destPath.StartsWith(@"\\")) // Local path
                {
                    DirectoryInfo destDirInfo = new DirectoryInfo(destPath);
                    string destRoot = Path.GetPathRoot(destDirInfo.FullName);
                    string systemRoot = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));

                    if (string.Equals(destRoot, systemRoot, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show(this, "Место назначения резервного копирования не может находиться на системном диске (e.g., C:\\). Выберите другой диск или сетевое расположение.", "Неверный пункт назначения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (!Directory.Exists(destPath))
                    {
                        try
                        {
                            Directory.CreateDirectory(destPath); // This might require admin if in protected location, but typically user chooses writable location.
                            AppendLogTextSafe($"Создан каталог назначения: {destPath}\r\n");
                        }
                        catch (UnauthorizedAccessException uae)
                        {
                            MessageBox.Show(this, $"Невозможно создать каталог назначения: {uae.Message}\nУ вас нет прав на запись в это место. Попробуйте запустить приложение от имени администратора, если путь корректен, но защищен, или выберите другой путь.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(this, $"Невозможно создать каталог назначения: {ex.Message}\nУбедитесь, что путь действителен и у вас есть права доступа.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
                else if (destPath.StartsWith(@"\\")) // UNC path
                {
                    if (!Uri.TryCreate(destPath, UriKind.Absolute, out Uri uri) || !uri.IsUnc)
                    {
                        MessageBox.Show(this, "Сетевой путь к пункту назначения кажется недействительным. Пожалуйста, используйте такой формат, как \\\\ServerName\\ShareName.", "Недопустимый сетевой путь", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(this, "Путь назначения не распознается как путь к локальному диску или сетевой путь UNC.", "Неверный формат пути", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Ошибка при проверке пути назначения: {ex.Message}", "Ошибка пути", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void AppendLogTextSafe(string text)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action<string>(AppendLogTextSafe), text);
            }
            else
            {
                if (txtLog.TextLength > 30000) { txtLog.Text = txtLog.Text.Substring(txtLog.TextLength - 15000); } // Log trimming
                txtLog.AppendText(text);
                txtLog.ScrollToCaret();
            }
        }
    }

    public class ScheduleParameters
    {
        public string DestinationPath { get; set; }
        public string Frequency { get; set; }
        public DateTime TimeOfDay { get; set; }
        public DaysOfTheWeek DaysOfWeek { get; set; } // For weekly
        public bool CreateRestorePoint { get; set; }
        public bool IncludeAllCritical { get; set; }
        public string PsScriptPathForSystemTask { get; set; } // Path to script in CommonApplicationData
    }
}