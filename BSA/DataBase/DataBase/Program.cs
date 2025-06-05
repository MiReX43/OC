using System;
using System.Linq;
using System.Windows.Forms;
using System.Security.Principal;
using System.IO;
using Newtonsoft.Json; // Added for JSON deserialization

namespace DataBase
{
    static class Program
    {
        public const string APP_NAME = "BackupSchedulerApp"; // Application name for folder paths

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length > 0 && args[0] == "--schedule-task-elevated")
            {
                if (args.Length > 1)
                {
                    string paramsFilePath = args[1];
                    try
                    {
                        if (File.Exists(paramsFilePath))
                        {
                            string jsonParams = File.ReadAllText(paramsFilePath);
                            ScheduleParameters taskParams = JsonConvert.DeserializeObject<ScheduleParameters>(jsonParams);
                            MainForm.ExecuteSchedulingLogicElevated(taskParams); // Call static method in MainForm
                            File.Delete(paramsFilePath); // Clean up temp file
                        }
                        else
                        {
                            MessageBox.Show($"Parameter file not found: {paramsFilePath}", "Elevated Task Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Environment.ExitCode = 2; // Indicate error
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error processing elevated task: {ex.Message}", "Elevated Task Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.ExitCode = 1; // Indicate error
                    }
                }
                else
                {
                    MessageBox.Show("Parameter file path missing for elevated task.", "Elevated Task Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.ExitCode = 3; // Indicate error
                }
                Application.Exit(); // Exit after performing the elevated task
            }
            else
            {
                Application.Run(new MainForm());
            }
        }
    }
}