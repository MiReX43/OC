namespace DataBase
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.txtLog = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.btnSaveSchedule = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnTestBackup = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.chkCreateRestorePoint = new System.Windows.Forms.CheckBox();
            this.chkIncludeAllCritical = new System.Windows.Forms.CheckBox();
            this.lblSchedule = new System.Windows.Forms.Label();
            this.lblDestinationPath = new System.Windows.Forms.Label();
            this.btnBrowseDestination = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txtDestinationPath = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.cmbFrequency = new Krypton.Toolkit.KryptonComboBox();
            this.dtpTime = new Krypton.Toolkit.KryptonDateTimePicker();
            this.clbDaysOfWeek = new Krypton.Toolkit.KryptonCheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFrequency)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPalette1
            // 
            this.kryptonPalette1.BasePaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Black;
            this.kryptonPalette1.ButtonSpecs.FormClose.Image = global::DataBase.Properties.Resources.icons8_close_up1;
            this.kryptonPalette1.ButtonSpecs.FormClose.ImageStates.ImagePressed = global::DataBase.Properties.Resources.icons8_close_press1;
            this.kryptonPalette1.ButtonSpecs.FormClose.ImageStates.ImageTracking = global::DataBase.Properties.Resources.icons8_close_press;
            this.kryptonPalette1.ButtonSpecs.FormMax.Image = global::DataBase.Properties.Resources.icons8_yellow1;
            this.kryptonPalette1.ButtonSpecs.FormMax.ImageStates.ImagePressed = global::DataBase.Properties.Resources.icons8_yellow1;
            this.kryptonPalette1.ButtonSpecs.FormMax.ImageStates.ImageTracking = global::DataBase.Properties.Resources.icons8_yellow;
            this.kryptonPalette1.ButtonSpecs.FormMin.Image = global::DataBase.Properties.Resources.icons8_green;
            this.kryptonPalette1.ButtonSpecs.FormMin.ImageStates.ImagePressed = global::DataBase.Properties.Resources.icons8_green;
            this.kryptonPalette1.ButtonSpecs.FormMin.ImageStates.ImageTracking = global::DataBase.Properties.Resources.icons8_close_24__3_;
            this.kryptonPalette1.ButtonSpecs.FormRestore.Image = global::DataBase.Properties.Resources.icons8_yellow1;
            this.kryptonPalette1.ButtonSpecs.FormRestore.ImageStates.ImagePressed = global::DataBase.Properties.Resources.icons8_yellow1;
            this.kryptonPalette1.ButtonSpecs.FormRestore.ImageStates.ImageTracking = global::DataBase.Properties.Resources.icons8_yellow1;
            this.kryptonPalette1.ButtonStyles.ButtonForm.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StateNormal.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StateNormal.Border.Width = 2;
            this.kryptonPalette1.ButtonStyles.ButtonForm.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StatePressed.Border.Width = 2;
            this.kryptonPalette1.ButtonStyles.ButtonForm.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StateTracking.Border.Width = -2;
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StateNormal.Border.Width = 2;
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StatePressed.Border.Width = 2;
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StateTracking.Border.Width = -2;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.None;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Rounding = 12;
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.ButtonEdgeInset = 10;
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, -1, -1, -1);
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.LightGray;
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateNormal.Content.ShortText.Color2 = System.Drawing.Color.LightGray;
            this.kryptonPalette1.LabelStyles.LabelBoldPanel.StateCommon.ShortText.Color1 = System.Drawing.Color.LightGray;
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLog.Location = new System.Drawing.Point(11, 403);
            this.txtLog.Name = "txtLog";
            this.txtLog.Palette = this.kryptonPalette1;
            this.txtLog.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.txtLog.Size = new System.Drawing.Size(548, 28);
            this.txtLog.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtLog.StateCommon.Back.Color1 = System.Drawing.Color.Black;
            this.txtLog.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.txtLog.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.txtLog.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtLog.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.txtLog.StateCommon.Border.Rounding = 5;
            this.txtLog.StateCommon.Border.Width = 3;
            this.txtLog.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txtLog.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtLog.StateCommon.Content.Padding = new System.Windows.Forms.Padding(2);
            this.txtLog.StateDisabled.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtLog.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtLog.TabIndex = 58;
            this.txtLog.Tag = "";
            // 
            // btnSaveSchedule
            // 
            this.btnSaveSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveSchedule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveSchedule.Location = new System.Drawing.Point(185, 231);
            this.btnSaveSchedule.Name = "btnSaveSchedule";
            this.btnSaveSchedule.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.btnSaveSchedule.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnSaveSchedule.OverrideDefault.Back.ColorAngle = 45F;
            this.btnSaveSchedule.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.btnSaveSchedule.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnSaveSchedule.OverrideDefault.Border.ColorAngle = 45F;
            this.btnSaveSchedule.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnSaveSchedule.OverrideDefault.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnSaveSchedule.OverrideDefault.Border.Rounding = 5;
            this.btnSaveSchedule.OverrideDefault.Border.Width = 1;
            this.btnSaveSchedule.OverrideDefault.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSaveSchedule.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.btnSaveSchedule.Size = new System.Drawing.Size(374, 28);
            this.btnSaveSchedule.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.btnSaveSchedule.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnSaveSchedule.StateCommon.Back.ColorAngle = 45F;
            this.btnSaveSchedule.StateCommon.Back.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnSaveSchedule.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.btnSaveSchedule.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnSaveSchedule.StateCommon.Border.ColorAngle = 45F;
            this.btnSaveSchedule.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnSaveSchedule.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnSaveSchedule.StateCommon.Border.Rounding = 5;
            this.btnSaveSchedule.StateCommon.Border.Width = 1;
            this.btnSaveSchedule.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.LightGray;
            this.btnSaveSchedule.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.LightGray;
            this.btnSaveSchedule.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSaveSchedule.StateDisabled.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnSaveSchedule.StateDisabled.Border.Rounding = 5;
            this.btnSaveSchedule.StateDisabled.Border.Width = 1;
            this.btnSaveSchedule.StateDisabled.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSaveSchedule.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnSaveSchedule.StateNormal.Border.Rounding = 5;
            this.btnSaveSchedule.StateNormal.Border.Width = 1;
            this.btnSaveSchedule.StateNormal.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSaveSchedule.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(41)))), ((int)(((byte)(128)))));
            this.btnSaveSchedule.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(35)))), ((int)(((byte)(110)))));
            this.btnSaveSchedule.StatePressed.Back.ColorAngle = 135F;
            this.btnSaveSchedule.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(41)))), ((int)(((byte)(128)))));
            this.btnSaveSchedule.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(35)))), ((int)(((byte)(110)))));
            this.btnSaveSchedule.StatePressed.Border.ColorAngle = 135F;
            this.btnSaveSchedule.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnSaveSchedule.StatePressed.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnSaveSchedule.StatePressed.Border.Rounding = 5;
            this.btnSaveSchedule.StatePressed.Border.Width = 1;
            this.btnSaveSchedule.StatePressed.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSaveSchedule.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnSaveSchedule.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.btnSaveSchedule.StateTracking.Back.ColorAngle = 45F;
            this.btnSaveSchedule.StateTracking.Back.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnSaveSchedule.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnSaveSchedule.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.btnSaveSchedule.StateTracking.Border.ColorAngle = 45F;
            this.btnSaveSchedule.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnSaveSchedule.StateTracking.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnSaveSchedule.StateTracking.Border.Rounding = 5;
            this.btnSaveSchedule.StateTracking.Border.Width = 1;
            this.btnSaveSchedule.StateTracking.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSaveSchedule.TabIndex = 57;
            this.btnSaveSchedule.Values.Text = "Сохранить и запланировать резервное копирование";
            this.btnSaveSchedule.Click += new System.EventHandler(this.btnSaveSchedule_Click);
            // 
            // btnTestBackup
            // 
            this.btnTestBackup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestBackup.Location = new System.Drawing.Point(15, 231);
            this.btnTestBackup.Name = "btnTestBackup";
            this.btnTestBackup.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.btnTestBackup.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnTestBackup.OverrideDefault.Back.ColorAngle = 45F;
            this.btnTestBackup.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.btnTestBackup.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnTestBackup.OverrideDefault.Border.ColorAngle = 45F;
            this.btnTestBackup.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnTestBackup.OverrideDefault.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnTestBackup.OverrideDefault.Border.Rounding = 5;
            this.btnTestBackup.OverrideDefault.Border.Width = 1;
            this.btnTestBackup.OverrideDefault.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnTestBackup.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.btnTestBackup.Size = new System.Drawing.Size(146, 28);
            this.btnTestBackup.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.btnTestBackup.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnTestBackup.StateCommon.Back.ColorAngle = 45F;
            this.btnTestBackup.StateCommon.Back.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnTestBackup.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.btnTestBackup.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnTestBackup.StateCommon.Border.ColorAngle = 45F;
            this.btnTestBackup.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnTestBackup.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnTestBackup.StateCommon.Border.Rounding = 5;
            this.btnTestBackup.StateCommon.Border.Width = 1;
            this.btnTestBackup.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.LightGray;
            this.btnTestBackup.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.LightGray;
            this.btnTestBackup.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnTestBackup.StateDisabled.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnTestBackup.StateDisabled.Border.Rounding = 5;
            this.btnTestBackup.StateDisabled.Border.Width = 1;
            this.btnTestBackup.StateDisabled.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnTestBackup.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnTestBackup.StateNormal.Border.Rounding = 5;
            this.btnTestBackup.StateNormal.Border.Width = 1;
            this.btnTestBackup.StateNormal.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnTestBackup.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(41)))), ((int)(((byte)(128)))));
            this.btnTestBackup.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(35)))), ((int)(((byte)(110)))));
            this.btnTestBackup.StatePressed.Back.ColorAngle = 135F;
            this.btnTestBackup.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(41)))), ((int)(((byte)(128)))));
            this.btnTestBackup.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(35)))), ((int)(((byte)(110)))));
            this.btnTestBackup.StatePressed.Border.ColorAngle = 135F;
            this.btnTestBackup.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnTestBackup.StatePressed.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnTestBackup.StatePressed.Border.Rounding = 5;
            this.btnTestBackup.StatePressed.Border.Width = 1;
            this.btnTestBackup.StatePressed.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnTestBackup.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnTestBackup.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.btnTestBackup.StateTracking.Back.ColorAngle = 45F;
            this.btnTestBackup.StateTracking.Back.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnTestBackup.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnTestBackup.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.btnTestBackup.StateTracking.Border.ColorAngle = 45F;
            this.btnTestBackup.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnTestBackup.StateTracking.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnTestBackup.StateTracking.Border.Rounding = 5;
            this.btnTestBackup.StateTracking.Border.Width = 1;
            this.btnTestBackup.StateTracking.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnTestBackup.TabIndex = 56;
            this.btnTestBackup.Values.Text = "Выполнить сейчас";
            this.btnTestBackup.Click += new System.EventHandler(this.btnTestBackup_Click);
            // 
            // chkCreateRestorePoint
            // 
            this.chkCreateRestorePoint.AutoSize = true;
            this.chkCreateRestorePoint.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chkCreateRestorePoint.ForeColor = System.Drawing.Color.LightGray;
            this.chkCreateRestorePoint.Location = new System.Drawing.Point(15, 181);
            this.chkCreateRestorePoint.Name = "chkCreateRestorePoint";
            this.chkCreateRestorePoint.Size = new System.Drawing.Size(467, 19);
            this.chkCreateRestorePoint.TabIndex = 55;
            this.chkCreateRestorePoint.Text = "Создание точки восстановления системы перед резервным копированием";
            this.chkCreateRestorePoint.UseVisualStyleBackColor = true;
            // 
            // chkIncludeAllCritical
            // 
            this.chkIncludeAllCritical.AutoSize = true;
            this.chkIncludeAllCritical.Checked = true;
            this.chkIncludeAllCritical.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeAllCritical.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chkIncludeAllCritical.ForeColor = System.Drawing.Color.LightGray;
            this.chkIncludeAllCritical.Location = new System.Drawing.Point(15, 156);
            this.chkIncludeAllCritical.Name = "chkIncludeAllCritical";
            this.chkIncludeAllCritical.Size = new System.Drawing.Size(548, 19);
            this.chkIncludeAllCritical.TabIndex = 54;
            this.chkIncludeAllCritical.Text = "Включить все критические тома (рекомендуется для резервного копирования системы)";
            this.chkIncludeAllCritical.UseVisualStyleBackColor = true;
            // 
            // lblSchedule
            // 
            this.lblSchedule.AutoSize = true;
            this.lblSchedule.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSchedule.ForeColor = System.Drawing.Color.LightGray;
            this.lblSchedule.Location = new System.Drawing.Point(12, 82);
            this.lblSchedule.Name = "lblSchedule";
            this.lblSchedule.Size = new System.Drawing.Size(226, 15);
            this.lblSchedule.TabIndex = 53;
            this.lblSchedule.Text = "Расписание резервного копирования";
            // 
            // lblDestinationPath
            // 
            this.lblDestinationPath.AutoSize = true;
            this.lblDestinationPath.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDestinationPath.ForeColor = System.Drawing.Color.LightGray;
            this.lblDestinationPath.Location = new System.Drawing.Point(12, 9);
            this.lblDestinationPath.Name = "lblDestinationPath";
            this.lblDestinationPath.Size = new System.Drawing.Size(408, 15);
            this.lblDestinationPath.TabIndex = 52;
            this.lblDestinationPath.Text = "Место назначения резервного копирования (диск или сетевой путь)";
            // 
            // btnBrowseDestination
            // 
            this.btnBrowseDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseDestination.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowseDestination.Location = new System.Drawing.Point(437, 38);
            this.btnBrowseDestination.Name = "btnBrowseDestination";
            this.btnBrowseDestination.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.btnBrowseDestination.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnBrowseDestination.OverrideDefault.Back.ColorAngle = 45F;
            this.btnBrowseDestination.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.btnBrowseDestination.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnBrowseDestination.OverrideDefault.Border.ColorAngle = 45F;
            this.btnBrowseDestination.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnBrowseDestination.OverrideDefault.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnBrowseDestination.OverrideDefault.Border.Rounding = 5;
            this.btnBrowseDestination.OverrideDefault.Border.Width = 1;
            this.btnBrowseDestination.OverrideDefault.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnBrowseDestination.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.btnBrowseDestination.Size = new System.Drawing.Size(122, 28);
            this.btnBrowseDestination.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.btnBrowseDestination.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnBrowseDestination.StateCommon.Back.ColorAngle = 45F;
            this.btnBrowseDestination.StateCommon.Back.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnBrowseDestination.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.btnBrowseDestination.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnBrowseDestination.StateCommon.Border.ColorAngle = 45F;
            this.btnBrowseDestination.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnBrowseDestination.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnBrowseDestination.StateCommon.Border.Rounding = 5;
            this.btnBrowseDestination.StateCommon.Border.Width = 1;
            this.btnBrowseDestination.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.LightGray;
            this.btnBrowseDestination.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.LightGray;
            this.btnBrowseDestination.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnBrowseDestination.StateDisabled.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnBrowseDestination.StateDisabled.Border.Rounding = 5;
            this.btnBrowseDestination.StateDisabled.Border.Width = 1;
            this.btnBrowseDestination.StateDisabled.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnBrowseDestination.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnBrowseDestination.StateNormal.Border.Rounding = 5;
            this.btnBrowseDestination.StateNormal.Border.Width = 1;
            this.btnBrowseDestination.StateNormal.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnBrowseDestination.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(41)))), ((int)(((byte)(128)))));
            this.btnBrowseDestination.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(35)))), ((int)(((byte)(110)))));
            this.btnBrowseDestination.StatePressed.Back.ColorAngle = 135F;
            this.btnBrowseDestination.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(41)))), ((int)(((byte)(128)))));
            this.btnBrowseDestination.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(35)))), ((int)(((byte)(110)))));
            this.btnBrowseDestination.StatePressed.Border.ColorAngle = 135F;
            this.btnBrowseDestination.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnBrowseDestination.StatePressed.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnBrowseDestination.StatePressed.Border.Rounding = 5;
            this.btnBrowseDestination.StatePressed.Border.Width = 1;
            this.btnBrowseDestination.StatePressed.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnBrowseDestination.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnBrowseDestination.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.btnBrowseDestination.StateTracking.Back.ColorAngle = 45F;
            this.btnBrowseDestination.StateTracking.Back.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnBrowseDestination.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnBrowseDestination.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.btnBrowseDestination.StateTracking.Border.ColorAngle = 45F;
            this.btnBrowseDestination.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnBrowseDestination.StateTracking.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnBrowseDestination.StateTracking.Border.Rounding = 5;
            this.btnBrowseDestination.StateTracking.Border.Width = 1;
            this.btnBrowseDestination.StateTracking.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnBrowseDestination.TabIndex = 51;
            this.btnBrowseDestination.Values.Text = "Обзор";
            this.btnBrowseDestination.Click += new System.EventHandler(this.btnBrowseDestination_Click);
            // 
            // txtDestinationPath
            // 
            this.txtDestinationPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDestinationPath.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDestinationPath.Location = new System.Drawing.Point(11, 38);
            this.txtDestinationPath.Name = "txtDestinationPath";
            this.txtDestinationPath.Palette = this.kryptonPalette1;
            this.txtDestinationPath.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.txtDestinationPath.Size = new System.Drawing.Size(405, 28);
            this.txtDestinationPath.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtDestinationPath.StateCommon.Back.Color1 = System.Drawing.Color.WhiteSmoke;
            this.txtDestinationPath.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.txtDestinationPath.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.txtDestinationPath.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtDestinationPath.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.txtDestinationPath.StateCommon.Border.Rounding = 5;
            this.txtDestinationPath.StateCommon.Border.Width = 3;
            this.txtDestinationPath.StateCommon.Content.Color1 = System.Drawing.Color.Gray;
            this.txtDestinationPath.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDestinationPath.StateCommon.Content.Padding = new System.Windows.Forms.Padding(2);
            this.txtDestinationPath.StateDisabled.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtDestinationPath.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtDestinationPath.TabIndex = 50;
            this.txtDestinationPath.Tag = "";
            // 
            // cmbFrequency
            // 
            this.cmbFrequency.DropButtonStyle = Krypton.Toolkit.ButtonStyle.Form;
            this.cmbFrequency.DropDownWidth = 75;
            this.cmbFrequency.ItemStyle = Krypton.Toolkit.ButtonStyle.Form;
            this.cmbFrequency.Location = new System.Drawing.Point(11, 110);
            this.cmbFrequency.Name = "cmbFrequency";
            this.cmbFrequency.Size = new System.Drawing.Size(191, 28);
            this.cmbFrequency.StateCommon.ComboBox.Back.Color1 = System.Drawing.Color.WhiteSmoke;
            this.cmbFrequency.StateCommon.ComboBox.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.cmbFrequency.StateCommon.ComboBox.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.cmbFrequency.StateCommon.ComboBox.Border.Rounding = 5F;
            this.cmbFrequency.StateCommon.ComboBox.Border.Width = 3;
            this.cmbFrequency.StateCommon.ComboBox.Content.Padding = new System.Windows.Forms.Padding(2);
            this.cmbFrequency.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbFrequency.StateCommon.Item.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cmbFrequency.StateCommon.Item.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cmbFrequency.StateCommon.Item.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.cmbFrequency.StateCommon.Item.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.cmbFrequency.StateCommon.Item.Border.Rounding = 5F;
            this.cmbFrequency.StateCommon.Item.Border.Width = 2;
            this.cmbFrequency.StateCommon.Item.Content.LongText.Color1 = System.Drawing.Color.Gray;
            this.cmbFrequency.StateCommon.Item.Content.LongText.Color2 = System.Drawing.Color.Gray;
            this.cmbFrequency.StateCommon.Item.Content.LongText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbFrequency.StateCommon.Item.Content.Padding = new System.Windows.Forms.Padding(2);
            this.cmbFrequency.StateCommon.Item.Content.ShortText.Color1 = System.Drawing.Color.Gray;
            this.cmbFrequency.StateCommon.Item.Content.ShortText.Color2 = System.Drawing.Color.Gray;
            this.cmbFrequency.StateCommon.Item.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbFrequency.TabIndex = 59;
            this.cmbFrequency.SelectedIndexChanged += new System.EventHandler(this.cmbFrequency_SelectedIndexChanged);
            // 
            // dtpTime
            // 
            this.dtpTime.CalendarTodayDate = new System.DateTime(2025, 5, 17, 0, 0, 0, 0);
            this.dtpTime.DropButtonStyle = Krypton.Toolkit.ButtonStyle.Form;
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime.Location = new System.Drawing.Point(225, 110);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.Size = new System.Drawing.Size(191, 27);
            this.dtpTime.StateCommon.Back.Color1 = System.Drawing.Color.WhiteSmoke;
            this.dtpTime.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.dtpTime.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.dtpTime.StateCommon.Border.Rounding = 5F;
            this.dtpTime.StateCommon.Border.Width = 3;
            this.dtpTime.StateCommon.Content.Color1 = System.Drawing.Color.Gray;
            this.dtpTime.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpTime.StateCommon.Content.Padding = new System.Windows.Forms.Padding(2);
            this.dtpTime.TabIndex = 60;
            // 
            // clbDaysOfWeek
            // 
            this.clbDaysOfWeek.ItemStyle = Krypton.Toolkit.ButtonStyle.Form;
            this.clbDaysOfWeek.Location = new System.Drawing.Point(15, 265);
            this.clbDaysOfWeek.Name = "clbDaysOfWeek";
            this.clbDaysOfWeek.Size = new System.Drawing.Size(223, 96);
            this.clbDaysOfWeek.StateCommon.Back.Color1 = System.Drawing.Color.WhiteSmoke;
            this.clbDaysOfWeek.StateCommon.Back.Color2 = System.Drawing.Color.WhiteSmoke;
            this.clbDaysOfWeek.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.clbDaysOfWeek.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.clbDaysOfWeek.StateCommon.Border.Rounding = 5F;
            this.clbDaysOfWeek.StateCommon.Border.Width = 3;
            this.clbDaysOfWeek.StateCommon.Item.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.clbDaysOfWeek.StateCommon.Item.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.clbDaysOfWeek.StateCommon.Item.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.clbDaysOfWeek.StateCommon.Item.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(89)))), ((int)(((byte)(201)))));
            this.clbDaysOfWeek.StateCommon.Item.Border.Rounding = 5F;
            this.clbDaysOfWeek.StateCommon.Item.Border.Width = 2;
            this.clbDaysOfWeek.StateCommon.Item.Content.LongText.Color1 = System.Drawing.Color.Gray;
            this.clbDaysOfWeek.StateCommon.Item.Content.LongText.Color2 = System.Drawing.Color.Gray;
            this.clbDaysOfWeek.StateCommon.Item.Content.Padding = new System.Windows.Forms.Padding(2);
            this.clbDaysOfWeek.StateCommon.Item.Content.ShortText.Color1 = System.Drawing.Color.Gray;
            this.clbDaysOfWeek.StateCommon.Item.Content.ShortText.Color2 = System.Drawing.Color.Gray;
            this.clbDaysOfWeek.StateCommon.Item.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clbDaysOfWeek.TabIndex = 61;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.ClientSize = new System.Drawing.Size(567, 571);
            this.Controls.Add(this.clbDaysOfWeek);
            this.Controls.Add(this.dtpTime);
            this.Controls.Add(this.cmbFrequency);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnSaveSchedule);
            this.Controls.Add(this.btnTestBackup);
            this.Controls.Add(this.chkCreateRestorePoint);
            this.Controls.Add(this.chkIncludeAllCritical);
            this.Controls.Add(this.lblSchedule);
            this.Controls.Add(this.lblDestinationPath);
            this.Controls.Add(this.btnBrowseDestination);
            this.Controls.Add(this.txtDestinationPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Main";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Windows System Backup Scheduler";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbFrequency)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtLog;
        public ComponentFactory.Krypton.Toolkit.KryptonButton btnSaveSchedule;
        public ComponentFactory.Krypton.Toolkit.KryptonButton btnTestBackup;
        private System.Windows.Forms.CheckBox chkCreateRestorePoint;
        private System.Windows.Forms.CheckBox chkIncludeAllCritical;
        private System.Windows.Forms.Label lblSchedule;
        private System.Windows.Forms.Label lblDestinationPath;
        public ComponentFactory.Krypton.Toolkit.KryptonButton btnBrowseDestination;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDestinationPath;
        private Krypton.Toolkit.KryptonComboBox cmbFrequency;
        private Krypton.Toolkit.KryptonDateTimePicker dtpTime;
        private Krypton.Toolkit.KryptonCheckedListBox clbDaysOfWeek;
    }
}