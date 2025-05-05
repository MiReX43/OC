using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BackupSchedulerApp
{
    public partial class BackupSchedulerForm : Form
    {
        public BackupSchedulerForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private Label lblSourceFolder;
        private TextBox txtSourceFolder;
        private Button btnBrowseSource;

        private Label lblDestinationFolder;
        private TextBox txtDestinationFolder;
        private Button btnBrowseDestination;

        private Label lblSchedule;
        private ComboBox cmbFrequency;
        private DateTimePicker dtpTime;

        private CheckedListBox clbDaysOfWeek;

        private CheckBox chkCreateRestorePoint;

        private Button btnTestBackup;
        private Button btnSaveSchedule;
        private Button btnCancel;

        private TextBox txtLog;

        private void InitializeCustomComponents()
        {
            this.Text = "Backup Scheduler";
            this.ClientSize = new System.Drawing.Size(600, 550);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Source Folder
            lblSourceFolder = new Label() { Text = "Source Folder:", Top = 20, Left = 10, Width = 100 };
            txtSourceFolder = new TextBox() { Top = 40, Left = 10, Width = 450, ReadOnly = true };
            btnBrowseSource = new Button() { Text = "Browse...", Top = 38, Left = 470, Width = 100 };
            btnBrowseSource.Click += BtnBrowseSource_Click;

            // Destination Folder
            lblDestinationFolder = new Label() { Text = "Destination Folder:", Top = 80, Left = 10, Width = 110 };
            txtDestinationFolder = new TextBox() { Top = 100, Left = 10, Width = 450, ReadOnly = true };
            btnBrowseDestination = new Button() { Text = "Browse...", Top = 98, Left = 470, Width = 100 };
            btnBrowseDestination.Click += BtnBrowseDestination_Click;

            // Schedule label
            lblSchedule = new Label() { Text = "Backup Schedule:", Top = 140, Left = 10, Width = 110 };

            // Frequency ComboBox
            cmbFrequency = new ComboBox() { Top = 160, Left = 10, Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbFrequency.Items.AddRange(new string[] { "Daily", "Weekly", "Monthly" });
            cmbFrequency.SelectedIndex = 0;
            cmbFrequency.SelectedIndexChanged += CmbFrequency_SelectedIndexChanged;

            // Time picker
            dtpTime = new DateTimePicker() { Top = 160, Left = 180, Width = 150, Format = DateTimePickerFormat.Time, ShowUpDown = true };

            // Days of week CheckedListBox (for weekly schedule)
            clbDaysOfWeek = new CheckedListBox()
            {
                Top = 200,
                Left = 10,
                Width = 250,
                Height = 100,
                Visible = false,
                CheckOnClick = true
            };
            clbDaysOfWeek.Items.AddRange(new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" });

            // Create Restore Point checkbox
            chkCreateRestorePoint = new CheckBox() { Text = "Create System Restore Point Before Backup", Top = 320, Left = 10, Width = 400 };

            // Buttons
            btnTestBackup = new Button() { Text = "Test Backup Now", Top = 360, Left = 10, Width = 150 };
            btnTestBackup.Click += BtnTestBackup_Click;

            btnSaveSchedule = new Button() { Text = "Save && Schedule Backup", Top = 360, Left = 180, Width = 190 };
            btnSaveSchedule.Click += BtnSaveSchedule_Click;

            btnCancel = new Button() { Text = "Cancel", Top = 360, Left = 390, Width = 100 };
            btnCancel.Click += (s, e) => this.Close();

            // Log TextBox
            txtLog = new TextBox()
            {
                Top = 410,
                Left = 10,
                Width = 560,
                Height = 120,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = System.Drawing.Color.Black,
                ForeColor = System.Drawing.Color.LightGreen,
                Font = new System.Drawing.Font("Consolas", 9)
            };

            // Add controls to form
            this.Controls.Add(lblSourceFolder);
            this.Controls.Add(txtSourceFolder);
            this.Controls.Add(btnBrowseSource);

            this.Controls.Add(lblDestinationFolder);
            this.Controls.Add(txtDestinationFolder);
            this.Controls.Add(btnBrowseDestination);

            this.Controls.Add(lblSchedule);
            this.Controls.Add(cmbFrequency);
            this.Controls.Add(dtpTime);

            this.Controls.Add(clbDaysOfWeek);

            this.Controls.Add(chkCreateRestorePoint);

            this.Controls.Add(btnTestBackup);
            this.Controls.Add(btnSaveSchedule);
            this.Controls.Add(btnCancel);

            this.Controls.Add(txtLog);
        }

        private void BtnBrowseSource_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select Source Folder to Backup";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtSourceFolder.Text = fbd.SelectedPath;
                }
            }
        }

        private void BtnBrowseDestination_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select Destination Folder for Backup";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtDestinationFolder.Text = fbd.SelectedPath;
                }
            }
        }

        private void CmbFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            string freq = cmbFrequency.SelectedItem.ToString();
            if (freq == "Weekly")
            {
                clbDaysOfWeek.Visible = true;
            }
            else
            {
                clbDaysOfWeek.Visible = false;
            }
        }

        private void BtnTestBackup_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
            if (!ValidatePaths()) return;
            RunBackupScript(txtSourceFolder.Text, txtDestinationFolder.Text, chkCreateRestorePoint.Checked);
        }

        private void BtnSaveSchedule_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
            if (!ValidatePaths()) return;

            string taskName = "ScheduledBackupTask";

            string psScriptPath = PreparePowerShellScript();

            // Build scheduled task command string
            string frequency = cmbFrequency.SelectedItem.ToString();
            TimeSpan time = dtpTime.Value.TimeOfDay;

            string scheduleCmd = BuildScheduleCommand(taskName, psScriptPath, txtSourceFolder.Text,
                txtDestinationFolder.Text, chkCreateRestorePoint.Checked, frequency, time);

            try
            {
                ExecuteCommand("schtasks", scheduleCmd);
                txtLog.AppendText("Scheduled Task created or updated successfully.\r\n");
            }
            catch (Exception ex)
            {
                txtLog.AppendText("ERROR creating scheduled task: " + ex.Message + "\r\n");
            }
        }

        private bool ValidatePaths()
        {
            if (string.IsNullOrWhiteSpace(txtSourceFolder.Text) || !Directory.Exists(txtSourceFolder.Text))
            {
                MessageBox.Show("Please select a valid existing Source folder.", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDestinationFolder.Text))
            {
                MessageBox.Show("Please select a Destination folder.", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Directory.Exists(txtDestinationFolder.Text))
            {
                try
                {
                    Directory.CreateDirectory(txtDestinationFolder.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot create destination folder: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            // If weekly schedule, at least one day must be selected
            if (cmbFrequency.SelectedItem.ToString() == "Weekly")
            {
                if (clbDaysOfWeek.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Please select at least one day of the week for scheduling.", "Schedule Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        private string PreparePowerShellScript()
        {
            // Save powershell backup script to app directory
            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            string psScriptPath = Path.Combine(appDir, "backup.ps1");

            string scriptContent = @"
param(
    [string]$SourceFolder,
    [string]$DestinationFolder,
    [switch]$CreateRestorePoint
)

function Create-RestorePoint {
    Write-Output 'Creating system restore point...'
    # Create restore point, requires admin rights!
    Checkpoint-Computer -Description 'Scheduled Backup Restore Point' -RestorePointType 'MODIFY_SETTINGS'
}

try {
    if ($CreateRestorePoint) {
        Create-RestorePoint
    }

    Write-Output ""Starting backup from $SourceFolder to $DestinationFolder""
    # Use robocopy for mirroring backup
    robocopy $SourceFolder $DestinationFolder /MIR /Z /XA:H /W:5 /R:3

    if ($LASTEXITCODE -le 3) {
        Write-Output 'Backup completed successfully.'
        exit 0
    }
    else {
        Write-Output ""Backup failed with code $LASTEXITCODE""
        exit 1
    }
}
catch {
    Write-Error $_.Exception.Message
    exit 1
}
";

            File.WriteAllText(psScriptPath, scriptContent, Encoding.UTF8);
            return psScriptPath;
        }

        private string BuildScheduleCommand(string taskName, string psScriptPath, string sourceFolder, string destFolder, bool createRestorePoint, string frequency, TimeSpan time)
        {
            // Build the scheduled task schtasks.exe arguments
            // Build PowerShell argument string
            StringBuilder psArgs = new StringBuilder();
            psArgs.Append("-ExecutionPolicy Bypass -File \"");
            psArgs.Append(psScriptPath);
            psArgs.Append("\"");
            psArgs.Append(" -SourceFolder \"").Append(sourceFolder).Append("\"");
            psArgs.Append(" -DestinationFolder \"").Append(destFolder).Append("\"");

            if (createRestorePoint)
            {
                psArgs.Append(" -CreateRestorePoint");
            }

            string taskArguments = $" /TN \"{taskName}\" /TR \"powershell.exe {psArgs.ToString()}\" ";

            // Format time as HH:mm 24-hour
            DateTime scheduledTime = DateTime.Today.Add(time);
            string startTime = scheduledTime.ToString("HH:mm");

            // Frequency converts:
            // /SC DAILY, /SC WEEKLY, /SC MONTHLY

            // For weekly, add days
            string days = "";
            if (frequency == "Weekly")
            {
                // Convert selected days to short names for schtasks:
                // Sunday=SUN, Monday=MON, Tuesday=TUE, Wednesday=WED etc.
                string[] dayNamesShort = new string[] { "SUN", "MON", "TUE", "WED", "THU", "FRI", "SAT" };

                var selectedDays = clbDaysOfWeek.CheckedIndices.Cast<int>()
                    .Select(i => dayNamesShort[i]).ToArray();

                days = string.Join(",", selectedDays);

                if (days.Length == 0)
                    throw new Exception("At least one day of week must be selected for weekly schedule.");

                return $"{taskArguments} /SC WEEKLY /D {days} /ST {startTime} /F";
            }
            else if (frequency == "Daily")
            {
                return $"{taskArguments} /SC DAILY /ST {startTime} /F";
            }
            else if (frequency == "Monthly")
            {
                // Monthly support: schedule on day 1 monthly (simplify)
                return $"{taskArguments} /SC MONTHLY /D 1 /ST {startTime} /F";
            }
            else
            {
                throw new Exception("Unsupported schedule frequency.");
            }
        }

        private void RunBackupScript(string sourceFolder, string destFolder, bool createRestorePoint)
        {
            string psScriptPath = PreparePowerShellScript();

            StringBuilder args = new StringBuilder();
            args.Append("-ExecutionPolicy Bypass -File \"").Append(psScriptPath).Append("\"");
            args.Append(" -SourceFolder \"").Append(sourceFolder).Append("\"");
            args.Append(" -DestinationFolder \"").Append(destFolder).Append("\"");

            if (createRestorePoint)
            {
                args.Append(" -CreateRestorePoint");
            }

            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = args.ToString(),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            try
            {
                using (Process p = Process.Start(psi))
                {
                    string output = p.StandardOutput.ReadToEnd();
                    string error = p.StandardError.ReadToEnd();
                    p.WaitForExit();

                    txtLog.AppendText(output);
                    if (!string.IsNullOrWhiteSpace(error))
                    {
                        txtLog.AppendText("\r\nErrors:\r\n" + error);
                    }
                }
            }
            catch (Exception ex)
            {
                txtLog.AppendText("Exception running backup script: " + ex.Message);
            }
        }

        private void ExecuteCommand(string fileName, string arguments)
        {
            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = fileName,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            using (Process p = Process.Start(psi))
            {
                string output = p.StandardOutput.ReadToEnd();
                string error = p.StandardError.ReadToEnd();
                p.WaitForExit();

                txtLog.AppendText(output);
                if (!string.IsNullOrWhiteSpace(error))
                {
                    txtLog.AppendText("\r\nErrors:\r\n" + error);
                }
            }
        }

        private void BackupSchedulerForm_Load(object sender, EventArgs e)
        {

        }
    }
}
