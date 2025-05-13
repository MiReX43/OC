using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal; // Для проверки прав администратора
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler; // NuGet: TaskSchedulerEditor
using System.Threading.Tasks; // Для асинхронного выполнения

namespace BackupSchedulerApp
{
    public partial class BackupSchedulerForm : Form
    {
        public BackupSchedulerForm()
        {
            InitializeComponent(); // Этот метод должен быть в файле BackupSchedulerForm.Designer.cs
            InitializeCustomComponents();
            CheckAdministratorRights(); // Проверка прав при запуске
        }

        // private Label lblSourceFolder; // Больше не нужен для системного бэкапа
        // private TextBox txtSourceFolder; // Больше не нужен
        // private Button btnBrowseSource; // Больше не нужен

        private Label lblDestinationPath;
        private TextBox txtDestinationPath;
        private Button btnBrowseDestination;

        private Label lblSchedule;
        private ComboBox cmbFrequency;
        private DateTimePicker dtpTime;

        private CheckedListBox clbDaysOfWeek;
        private CheckBox chkIncludeAllCritical; // Новый чекбокс

        private CheckBox chkCreateRestorePoint;

        private Button btnTestBackup;
        private Button btnSaveSchedule;
        private Button btnCancel;

        private TextBox txtLog;

        private const string TASK_NAME = "WindowsSystemBackupScheduledTask";
        private string psScriptPath; // Путь к скрипту PowerShell

        private void CheckAdministratorRights()
        {
            bool isAdmin;
            try
            {
                using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
                {
                    WindowsPrincipal principal = new WindowsPrincipal(identity);
                    isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
                }
            }
            catch
            {
                isAdmin = false; // В случае ошибки считаем, что прав нет
            }


            if (!isAdmin)
            {
                AppendLogTextSafe("WARNING: Application is not running with administrator privileges. System backup and task scheduling may fail. Please restart as administrator.\r\n");
                btnTestBackup.Enabled = false;
                btnSaveSchedule.Enabled = false;
                MessageBox.Show(this, "This application requires administrator privileges to function correctly. Please restart it as an administrator.",
                                "Administrator Privileges Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                AppendLogTextSafe("Application running with administrator privileges.\r\n");
            }
        }


        private void InitializeCustomComponents()
        {
            this.Text = "Windows System Backup Scheduler v4";
            this.ClientSize = new System.Drawing.Size(600, 520);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            int currentTop = 20;

            lblDestinationPath = new Label() { Text = "Backup Destination (Drive or Network Path):", Top = currentTop, Left = 10, Width = 250, AutoSize = true };
            currentTop += lblDestinationPath.Height + 5;
            txtDestinationPath = new TextBox() { Top = currentTop, Left = 10, Width = 450 };
            btnBrowseDestination = new Button() { Text = "Browse...", Top = currentTop - 2, Left = 470, Width = 100 };
            btnBrowseDestination.Click += BtnBrowseDestination_Click;

            currentTop += txtDestinationPath.Height + 15;

            lblSchedule = new Label() { Text = "Backup Schedule:", Top = currentTop, Left = 10, Width = 110, AutoSize = true };
            currentTop += lblSchedule.Height + 5;

            cmbFrequency = new ComboBox() { Top = currentTop, Left = 10, Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbFrequency.Items.AddRange(new string[] { "Daily", "Weekly", "Monthly" });
            cmbFrequency.SelectedIndex = 0;
            cmbFrequency.SelectedIndexChanged += CmbFrequency_SelectedIndexChanged;

            dtpTime = new DateTimePicker() { Top = currentTop, Left = 180, Width = 150, Format = DateTimePickerFormat.Time, ShowUpDown = true };

            currentTop += cmbFrequency.Height + 10;

            clbDaysOfWeek = new CheckedListBox()
            {
                Top = currentTop,
                Left = 10,
                Width = 250,
                Height = 140,
                Visible = false,
                CheckOnClick = true
            };
            clbDaysOfWeek.Items.AddRange(new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" });

            // Расположим опции и кнопки после блока расписания
            // Начальная позиция для опций будет зависеть от видимости clbDaysOfWeek
            // Это будет обработано в UpdateLayoutAfterFrequencyChange

            chkIncludeAllCritical = new CheckBox() { Text = "Include all critical volumes (Recommended for system backup)", Left = 10, Width = 450, Checked = true, AutoSize = true };
            chkCreateRestorePoint = new CheckBox() { Text = "Create System Restore Point Before Backup", Left = 10, Width = 400, AutoSize = true };

            btnTestBackup = new Button() { Text = "Test Backup Now", Left = 10, Width = 150 };
            btnTestBackup.Click += BtnTestBackup_Click;

            btnSaveSchedule = new Button() { Text = "Save && Schedule Backup", Left = 180, Width = 190 };
            btnSaveSchedule.Click += BtnSaveSchedule_Click;

            btnCancel = new Button() { Text = "Cancel", Left = 390, Width = 100 };
            btnCancel.Click += (s, e) => this.Close();

            txtLog = new TextBox()
            {
                Left = 10,
                Width = 560,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = System.Drawing.Color.Black,
                ForeColor = System.Drawing.Color.LightGreen,
                Font = new System.Drawing.Font("Consolas", 9)
            };

            this.Controls.Add(lblDestinationPath);
            this.Controls.Add(txtDestinationPath);
            this.Controls.Add(btnBrowseDestination);
            this.Controls.Add(lblSchedule);
            this.Controls.Add(cmbFrequency);
            this.Controls.Add(dtpTime);
            this.Controls.Add(clbDaysOfWeek);
            this.Controls.Add(chkIncludeAllCritical);
            this.Controls.Add(chkCreateRestorePoint);
            this.Controls.Add(btnTestBackup);
            this.Controls.Add(btnSaveSchedule);
            this.Controls.Add(btnCancel);
            this.Controls.Add(txtLog);

            try
            {
                psScriptPath = PreparePowerShellScript();
                AppendLogTextSafe($"PowerShell script prepared at: {psScriptPath}\r\n");
            }
            catch (Exception ex)
            {
                AppendLogTextSafe($"FATAL ERROR preparing PowerShell script: {ex.Message}\r\nApplication might not function correctly.\r\n");
                MessageBox.Show(this, $"FATAL ERROR preparing PowerShell script: {ex.Message}\r\nApplication might not function correctly.", "Script Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnTestBackup.Enabled = false;
                btnSaveSchedule.Enabled = false;
            }
            // Изначальный вызов для правильного расположения
            CmbFrequency_SelectedIndexChanged(null, null);
        }

        private void BtnBrowseDestination_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select Backup Destination Drive or Folder (e.g., D:\\Backup, \\\\Server\\Share). \nFor system backup, this should be a different drive than your system drive (C:\\).";
                fbd.ShowNewFolderButton = true;
                if (fbd.ShowDialog(this) == DialogResult.OK)
                {
                    txtDestinationPath.Text = fbd.SelectedPath;
                }
            }
        }

        private void CmbFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            string freq = cmbFrequency.SelectedItem?.ToString() ?? "Daily";
            clbDaysOfWeek.Visible = (freq == "Weekly");
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
            btnCancel.Top = top;
            top += btnTestBackup.Height + 20;

            txtLog.Top = top;
            txtLog.Height = this.ClientSize.Height - top - 20; // Динамическая высота лога
        }


        private async void BtnTestBackup_Click(object sender, EventArgs e) // Сделали метод async void
        {
            txtLog.Clear();
            if (!ValidateDestinationPath()) return;
            if (string.IsNullOrEmpty(psScriptPath) || !File.Exists(psScriptPath)) // Проверка существования файла скрипта
            {
                AppendLogTextSafe("ERROR: PowerShell script path is not set or script file not found. Cannot run backup.\r\n");
                return;
            }

            AppendLogTextSafe("Starting manual test backup...\r\n");

            // Деактивируем кнопки на время выполнения, чтобы избежать повторных нажатий
            btnTestBackup.Enabled = false;
            btnSaveSchedule.Enabled = false;

            try
            {
                // Вызываем асинхронный метод и ждем его завершения
                await RunBackupProcess(txtDestinationPath.Text, chkCreateRestorePoint.Checked, chkIncludeAllCritical.Checked);
                AppendLogTextSafe("Test backup process has been initiated. Monitor the log for output from the script.\r\n");
            }
            catch (Exception ex)
            {
                AppendLogTextSafe($"An error occurred while trying to start the backup process: {ex.Message}\r\n");
            }
            finally
            {
                // Активируем кнопки обратно после завершения (или ошибки)
                btnTestBackup.Enabled = true;
                btnSaveSchedule.Enabled = true;
            }
        }



        private void BtnSaveSchedule_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
            if (!ValidateDestinationPath()) return;
            if (string.IsNullOrEmpty(psScriptPath))
            {
                AppendLogTextSafe("ERROR: PowerShell script path is not set. Cannot schedule backup.\r\n");
                return;
            }

            AppendLogTextSafe($"Attempting to create/update scheduled task: {TASK_NAME}\r\n");

            try
            {
                using (TaskService ts = new TaskService())
                {
                    ts.RootFolder.DeleteTask(TASK_NAME, false);

                    TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = "Scheduled Windows System Backup via BackupSchedulerApp";
                    td.RegistrationInfo.Author = WindowsIdentity.GetCurrent().Name;
                    td.Principal.RunLevel = TaskRunLevel.Highest; // Запускать с наивысшими правами

                    string frequency = cmbFrequency.SelectedItem.ToString();
                    TimeSpan timeOfDay = dtpTime.Value.TimeOfDay;
                    DateTime startBoundary = DateTime.Today + timeOfDay;

                    Trigger trigger;
                    switch (frequency)
                    {
                        case "Daily":
                            trigger = new DailyTrigger { StartBoundary = startBoundary, DaysInterval = 1 };
                            break;
                        case "Weekly":
                            if (clbDaysOfWeek.CheckedItems.Count == 0)
                            {
                                MessageBox.Show(this, "Please select at least one day for weekly schedule.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            var weeklyTrigger = new WeeklyTrigger { StartBoundary = startBoundary, WeeksInterval = 1 };
                            DaysOfTheWeek days = 0;
                            if (clbDaysOfWeek.GetItemChecked(0)) days |= DaysOfTheWeek.Sunday; // Sunday
                            if (clbDaysOfWeek.GetItemChecked(1)) days |= DaysOfTheWeek.Monday;
                            if (clbDaysOfWeek.GetItemChecked(2)) days |= DaysOfTheWeek.Tuesday;
                            if (clbDaysOfWeek.GetItemChecked(3)) days |= DaysOfTheWeek.Wednesday;
                            if (clbDaysOfWeek.GetItemChecked(4)) days |= DaysOfTheWeek.Thursday;
                            if (clbDaysOfWeek.GetItemChecked(5)) days |= DaysOfTheWeek.Friday;
                            if (clbDaysOfWeek.GetItemChecked(6)) days |= DaysOfTheWeek.Saturday;
                            weeklyTrigger.DaysOfWeek = days;
                            trigger = weeklyTrigger;
                            break;
                        case "Monthly":
                            trigger = new MonthlyTrigger { StartBoundary = startBoundary, DaysOfMonth = new[] { 1 }, MonthsOfYear = MonthsOfTheYear.AllMonths };
                            break;
                        default:
                            AppendLogTextSafe("ERROR: Invalid frequency selected.\r\n");
                            return;
                    }
                    td.Triggers.Add(trigger);

                    StringBuilder psArgs = new StringBuilder();
                    psArgs.Append($"-ExecutionPolicy Bypass -NoProfile -File \"{psScriptPath}\"");
                    psArgs.Append($" -DestinationPath \"{txtDestinationPath.Text.Trim()}\""); // Trim() для удаления случайных пробелов
                    if (chkCreateRestorePoint.Checked) psArgs.Append(" -CreateRestorePoint");
                    if (chkIncludeAllCritical.Checked) psArgs.Append(" -IncludeAllCritical");

                    td.Actions.Add(new ExecAction("powershell.exe", psArgs.ToString(), Path.GetDirectoryName(psScriptPath)));

                    td.Settings.MultipleInstances = TaskInstancesPolicy.IgnoreNew;
                    td.Settings.DisallowStartIfOnBatteries = false;
                    td.Settings.StopIfGoingOnBatteries = false;
                    td.Settings.AllowHardTerminate = true;
                    td.Settings.StartWhenAvailable = true;
                    td.Settings.ExecutionTimeLimit = TimeSpan.Zero; // Без ограничения времени
                    td.Settings.Enabled = true;

                    // Запуск от имени SYSTEM для максимальных прав
                    ts.RootFolder.RegisterTaskDefinition(TASK_NAME, td, TaskCreation.CreateOrUpdate, "SYSTEM", null, TaskLogonType.ServiceAccount);

                    AppendLogTextSafe($"Scheduled task '{TASK_NAME}' created/updated successfully to run as SYSTEM.\r\n");
                    AppendLogTextSafe($"Trigger: {trigger.ToString()}\r\n");
                    AppendLogTextSafe($"Action: powershell.exe {psArgs.ToString()}\r\n");
                }
            }
            catch (Exception ex)
            {
                AppendLogTextSafe($"ERROR creating/updating scheduled task: {ex.Message}\r\n{ex.StackTrace}\r\n");
                MessageBox.Show(this, $"Error creating scheduled task: {ex.Message}", "Task Scheduler Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateDestinationPath()
        {
            string destPath = txtDestinationPath.Text.Trim();
            if (string.IsNullOrWhiteSpace(destPath))
            {
                MessageBox.Show(this, "Please specify a Destination Path (e.g., D:\\SystemBackup or \\\\ServerName\\Share).", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDestinationPath.Focus();
                return false;
            }

            try
            {
                if (Path.IsPathRooted(destPath) && !destPath.StartsWith(@"\\")) // Локальный путь
                {
                    DirectoryInfo destDirInfo = new DirectoryInfo(destPath);
                    string destRoot = Path.GetPathRoot(destDirInfo.FullName);
                    string systemRoot = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)); // Более надежный способ получить системный диск

                    if (string.Equals(destRoot, systemRoot, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show(this, "The backup destination cannot be on the system drive (e.g., C:\\). Please choose a different drive or a network location.", "Invalid Destination", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (!Directory.Exists(destPath))
                    {
                        try
                        {
                            Directory.CreateDirectory(destPath);
                            AppendLogTextSafe($"Destination directory created: {destPath}\r\n");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(this, $"Cannot create destination directory: {ex.Message}\nEnsure the path is valid and you have permissions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
                else if (destPath.StartsWith(@"\\")) // UNC путь
                {
                    if (!Uri.TryCreate(destPath, UriKind.Absolute, out Uri uri) || !uri.IsUnc)
                    {
                        MessageBox.Show(this, "The network path for the destination seems invalid. Please use format like \\\\ServerName\\ShareName.", "Invalid Network Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    // Для wbadmin может потребоваться, чтобы конечная папка на сетевом ресурсе уже существовала,
                    // или чтобы у учетной записи SYSTEM были права на ее создание.
                    // Простая проверка здесь затруднительна, wbadmin сам сообщит об ошибке.
                }
                else
                {
                    MessageBox.Show(this, "Destination path is not recognized as a local drive path or a network UNC path.", "Invalid Path Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Error validating destination path: {ex.Message}", "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private string PreparePowerShellScript()
        {
            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            string scriptFileName = "SystemBackup.ps1";
            string fullPsScriptPath = Path.Combine(appDir, scriptFileName);

            string scriptContent = @"
""[$(Get-Date)] СКРИПТ ЗАПУЩЕН"" | Out-File ""C:\SystemBackupLog.txt"" -Append
param(
    [string]$DestinationPath,
    [switch]$CreateRestorePoint,
    [switch]$IncludeAllCritical
)

function Write-Log {
    param ([string]$Message, [string]$Type = ""INFO"")
    $Timestamp = Get-Date -Format ""yyyy-MM-dd HH:mm:ss""
    Write-Output ""[$Timestamp] [$Type] $Message""
}

function Create-SystemRestorePointUserContext {
    Write-Log ""Attempting to create system restore point...""
    try {
        # Эта команда может требовать запуска PowerShell от имени администратора.
        # В контексте задачи, выполняемой от SYSTEM, это должно сработать.
        Checkpoint-Computer -Description ""Pre-SystemBackup Restore Point (BackupSchedulerApp)"" -RestorePointType ""MODIFY_SETTINGS""
        Write-Log ""System restore point creation initiated."" # Не всегда сразу видно результат
    }
    catch {
        Write-Log ""ERROR creating system restore point: $($_.Exception.Message)"" -Type ""ERROR""
        Write-Log ""Details: $($_.ToString())"" -Type ""DEBUG""
    }
}

# --- Main Script ---
Write-Log ""System Backup script started.""
Write-Log ""Current User: $($env:USERNAME), IsAdmin: $([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)""

if ($CreateRestorePoint) {
    Create-SystemRestorePointUserContext
}

if (-not $DestinationPath) {
    Write-Log ""DestinationPath parameter is missing."" -Type ""ERROR""
    exit 1
}

# Валидация пути назначения для wbadmin
# wbadmin требует, чтобы -backupTarget был либо буквой диска (D:), либо UNC-путем (\\server\share)
# Если указана папка на диске, wbadmin сам создаст подпапку WindowsImageBackup в КОРНЕ этого диска.
# Например, если DestinationPath = ""D:\MyBackups"", wbadmin будет пытаться писать в D:\WindowsImageBackup
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
            Write-Log ""Destination for system backup ('$DestinationPath') resolves to the system drive ('$systemDriveLetter'). This is not allowed for wbadmin. Please choose a different drive."" -Type ""ERROR""
            exit 1
        }
        # Если указана папка, а не корень диска, wbadmin все равно создаст WindowsImageBackup в корне диска,
        # на котором находится эта папка. Убедимся, что папка существует.
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
        $targetLocation = $driveLetter # Используем корень диска для -backupTarget если локальный путь
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
    ""-backupTarget:$targetLocation"", # Используем обработанный targetLocation
    ""-quiet"" # Выполнение без интерактивных запросов
)

if ($IncludeAllCritical) {
    $wbadminArgs += ""-allCritical"" # Включает все тома, необходимые для восстановления ОС
    Write-Log ""Including all critical system volumes.""
} else {
    # Обычно для системного бэкапа -allCritical является предпочтительным.
    # Если пользователь не выбрал, можно добавить системный диск, но это не гарантирует полное восстановление.
    $systemDrive = $env:SystemDrive
    $wbadminArgs += ""-include:$systemDrive""
    Write-Log ""Including only system drive: $systemDrive (Warning: For full system recovery, using '-allCritical' is highly recommended)."" -Type ""WARN""
}

Write-Log ""Executing: wbadmin.exe $($wbadminArgs -join ' ')""

try {
    $process = Start-Process ""wbadmin.exe"" -ArgumentList $wbadminArgs -Wait -PassThru 
    
    if ($process.ExitCode -eq 0) {
        Write-Log ""Windows System Backup completed successfully.""
        # Дополнительно можно проверить наличие файла каталога бэкапа
        # Get-ChildItem -Path (Join-Path $targetLocation ""WindowsImageBackup\\$($env:COMPUTERNAME)\\Catalog\\BackupGlobalCatalog"")
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
""[$(Get-Date)] СКРИПТ ЗАВЕРШЁН"" | Out-File ""C:\SystemBackupLog.txt"" -Append
";
            try
            {
                File.WriteAllText(fullPsScriptPath, scriptContent, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                AppendLogTextSafe($"ERROR writing PowerShell script file '{fullPsScriptPath}': {ex.Message}\r\n");
                throw new IOException($"Failed to write PowerShell script: {ex.Message}", ex);
            }
            return fullPsScriptPath;
        }

        private async System.Threading.Tasks.Task RunBackupProcess(string destinationPath, bool createRestorePoint, bool includeAllCritical) // Указали полный путь для Task
        {
            // Проверка существования файла скрипта (дублируется, но здесь тоже полезна)
            if (string.IsNullOrEmpty(psScriptPath) || !File.Exists(psScriptPath))
            {
                AppendLogTextSafe("ERROR: PowerShell backup script not found or not prepared at RunBackupProcess.\r\n");
                return; // Возвращаем Task.CompletedTask или просто return для async Task void-like методов
            }

            StringBuilder args = new StringBuilder();
            args.Append($"-ExecutionPolicy Bypass -NoProfile -File \"{psScriptPath}\"");
            args.Append($" -DestinationPath \"{destinationPath.Trim()}\"");
            if (createRestorePoint) args.Append(" -CreateRestorePoint");
            if (includeAllCritical) args.Append(" -IncludeAllCritical");

            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = args.ToString(),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                // Verb = "runas" // Оставляем закомментированным, т.к. приложение должно быть уже запущено от администратора
            };

            AppendLogTextSafe($"Executing (async): powershell.exe {args.ToString()}\r\n");

            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo = psi;
                    // Включаем EnableRaisingEvents, чтобы событие Exited сработало
                    process.EnableRaisingEvents = true;

                    // Создаем TaskCompletionSource для асинхронного ожидания завершения процесса
                    var tcs = new System.Threading.Tasks.TaskCompletionSource<int>();
                    process.Exited += (s, e) => tcs.TrySetResult(process.ExitCode); // Устанавливаем результат, когда процесс завершится

                    // Подписываемся на события вывода асинхронно
                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (e.Data != null) AppendLogTextSafe(e.Data + "\r\n");
                    };
                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (e.Data != null) AppendLogTextSafe("ERROR_STREAM: " + e.Data + "\r\n");
                    };

                    if (!process.Start()) // Проверяем, удалось ли запустить процесс
                    {
                        AppendLogTextSafe("Failed to start the PowerShell process.\r\n");
                        tcs.TrySetResult(-1); // Устанавливаем код ошибки, если не удалось запустить
                    }
                    else
                    {
                        process.BeginOutputReadLine(); // Начинаем асинхронное чтение стандартного вывода
                        process.BeginErrorReadLine();  // Начинаем асинхронное чтение стандартного потока ошибок
                    }

                    // Асинхронно ждем завершения задачи, которая завершится при выходе процесса
                    int exitCode = await tcs.Task;

                    if (exitCode == 0)
                    {
                        AppendLogTextSafe($"Backup process completed successfully (PowerShell Exit Code: {exitCode}).\r\n");
                    }
                    else
                    {
                        AppendLogTextSafe($"Backup process exited with code: {exitCode}. Check log for details from PowerShell script.\r\n");
                    }
                }
            }
            catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode == 1223) // ERROR_CANCELLED (UAC)
            {
                AppendLogTextSafe("Operation cancelled by user (UAC prompt denied) or administrator privileges were not granted for PowerShell.\r\n");
                // MessageBox.Show(this, "The backup operation was cancelled or administrator privileges were not granted.", "Operation Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                AppendLogTextSafe($"Exception during backup process execution: {ex.ToString()}\r\n");
            }
        }

        private void AppendLogTextSafe(string text)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action<string>(AppendLogTextSafe), text);
            }
            else
            {
                if (txtLog.TextLength > 20000) { txtLog.Text = txtLog.Text.Substring(txtLog.TextLength - 10000); } // Ограничение размера лога
                txtLog.AppendText(text);
                txtLog.ScrollToCaret();
            }
        }

        private void BackupSchedulerForm_Load(object sender, EventArgs e)
        {
            CmbFrequency_SelectedIndexChanged(null, null);
        }
    }
}