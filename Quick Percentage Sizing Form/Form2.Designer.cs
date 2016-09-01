namespace Quick_Percentage_Sizing_Form
{
    partial class OptionsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.SymbolsGrid = new MetroFramework.Controls.MetroGrid();
            this.AddSymbolTextBox = new MetroFramework.Controls.MetroTextBox();
            this.DeleteSymbolsButton = new MetroFramework.Controls.MetroButton();
            this.AddButton = new MetroFramework.Controls.MetroButton();
            this.MoveDownButton = new MetroFramework.Controls.MetroButton();
            this.SLLabel = new MetroFramework.Controls.MetroLabel();
            this.TPLabel = new MetroFramework.Controls.MetroLabel();
            this.PipsAmountLabel = new MetroFramework.Controls.MetroLabel();
            this.FixedPipsBasedLabel = new MetroFramework.Controls.MetroLabel();
            this.FixedPipsTPComboBox = new MetroFramework.Controls.MetroComboBox();
            this.FixedPipsSLComboBox = new MetroFramework.Controls.MetroComboBox();
            this.ATRBasedLabel = new MetroFramework.Controls.MetroLabel();
            this.ATRTPComboBox = new MetroFramework.Controls.MetroComboBox();
            this.ATRSLComboBox = new MetroFramework.Controls.MetroComboBox();
            this.ATRMultiplierLabel = new MetroFramework.Controls.MetroLabel();
            this.SaveButton = new MetroFramework.Controls.MetroButton();
            this.MoveUpButton = new MetroFramework.Controls.MetroButton();
            this.ATRPeriodsLabel = new MetroFramework.Controls.MetroLabel();
            this.OptionsProgressBar = new MetroFramework.Controls.MetroProgressBar();
            this.OnCandleCloseLabel = new MetroFramework.Controls.MetroLabel();
            this.OnCandleCloseComboBox = new MetroFramework.Controls.MetroComboBox();
            this.OneOpenPositionSymbolComboBox = new MetroFramework.Controls.MetroComboBox();
            this.OneOpenPositionSymbolLabel = new MetroFramework.Controls.MetroLabel();
            this.OptionsTabControl = new MetroFramework.Controls.MetroTabControl();
            this.SymbolsTab = new MetroFramework.Controls.MetroTabPage();
            this.SLTPTab = new MetroFramework.Controls.MetroTabPage();
            this.EntryTab = new MetroFramework.Controls.MetroTabPage();
            this.NumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SymbolColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PipsSLNumber = new System.Windows.Forms.NumericUpDown();
            this.ATRPeriodsSLNumber = new System.Windows.Forms.NumericUpDown();
            this.ATRPeriodsTPNumber = new System.Windows.Forms.NumericUpDown();
            this.PipsTPNumber = new System.Windows.Forms.NumericUpDown();
            this.ATRMultiplierTPNumber = new System.Windows.Forms.NumericUpDown();
            this.ATRMultiplierSLNumber = new System.Windows.Forms.NumericUpDown();
            this.OneOpenPositionCurrencyComboBox = new MetroFramework.Controls.MetroComboBox();
            this.OneOpenPositionCurrencyLabel = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.SymbolsGrid)).BeginInit();
            this.OptionsTabControl.SuspendLayout();
            this.SymbolsTab.SuspendLayout();
            this.SLTPTab.SuspendLayout();
            this.EntryTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PipsSLNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ATRPeriodsSLNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ATRPeriodsTPNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PipsTPNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ATRMultiplierTPNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ATRMultiplierSLNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // SymbolsGrid
            // 
            this.SymbolsGrid.AllowUserToAddRows = false;
            this.SymbolsGrid.AllowUserToDeleteRows = false;
            this.SymbolsGrid.AllowUserToResizeColumns = false;
            this.SymbolsGrid.AllowUserToResizeRows = false;
            this.SymbolsGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SymbolsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SymbolsGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.SymbolsGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SymbolsGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.SymbolsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SymbolsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumberColumn,
            this.SymbolColumn});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SymbolsGrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.SymbolsGrid.EnableHeadersVisualStyles = false;
            this.SymbolsGrid.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.SymbolsGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SymbolsGrid.Location = new System.Drawing.Point(12, 14);
            this.SymbolsGrid.Name = "SymbolsGrid";
            this.SymbolsGrid.ReadOnly = true;
            this.SymbolsGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SymbolsGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.SymbolsGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.SymbolsGrid.RowTemplate.Height = 24;
            this.SymbolsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SymbolsGrid.Size = new System.Drawing.Size(282, 270);
            this.SymbolsGrid.TabIndex = 0;
            this.SymbolsGrid.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // AddSymbolTextBox
            // 
            // 
            // 
            // 
            this.AddSymbolTextBox.CustomButton.Image = null;
            this.AddSymbolTextBox.CustomButton.Location = new System.Drawing.Point(121, 1);
            this.AddSymbolTextBox.CustomButton.Name = "";
            this.AddSymbolTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.AddSymbolTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.AddSymbolTextBox.CustomButton.TabIndex = 1;
            this.AddSymbolTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.AddSymbolTextBox.CustomButton.UseSelectable = true;
            this.AddSymbolTextBox.CustomButton.Visible = false;
            this.AddSymbolTextBox.Lines = new string[] {
        "Enter Symbol Code"};
            this.AddSymbolTextBox.Location = new System.Drawing.Point(307, 14);
            this.AddSymbolTextBox.MaxLength = 32767;
            this.AddSymbolTextBox.Name = "AddSymbolTextBox";
            this.AddSymbolTextBox.PasswordChar = '\0';
            this.AddSymbolTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.AddSymbolTextBox.SelectedText = "";
            this.AddSymbolTextBox.SelectionLength = 0;
            this.AddSymbolTextBox.SelectionStart = 0;
            this.AddSymbolTextBox.ShortcutsEnabled = true;
            this.AddSymbolTextBox.Size = new System.Drawing.Size(143, 23);
            this.AddSymbolTextBox.TabIndex = 2;
            this.AddSymbolTextBox.Text = "Enter Symbol Code";
            this.AddSymbolTextBox.UseSelectable = true;
            this.AddSymbolTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.AddSymbolTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // DeleteSymbolsButton
            // 
            this.DeleteSymbolsButton.Location = new System.Drawing.Point(81, 305);
            this.DeleteSymbolsButton.Name = "DeleteSymbolsButton";
            this.DeleteSymbolsButton.Size = new System.Drawing.Size(162, 23);
            this.DeleteSymbolsButton.TabIndex = 3;
            this.DeleteSymbolsButton.Text = "Delete Selected Symbols";
            this.DeleteSymbolsButton.UseSelectable = true;
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(465, 14);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(76, 23);
            this.AddButton.TabIndex = 4;
            this.AddButton.Text = "Add";
            this.AddButton.UseSelectable = true;
            // 
            // MoveDownButton
            // 
            this.MoveDownButton.Location = new System.Drawing.Point(422, 73);
            this.MoveDownButton.Name = "MoveDownButton";
            this.MoveDownButton.Size = new System.Drawing.Size(96, 23);
            this.MoveDownButton.TabIndex = 5;
            this.MoveDownButton.Text = "Move Down";
            this.MoveDownButton.UseSelectable = true;
            // 
            // SLLabel
            // 
            this.SLLabel.AutoSize = true;
            this.SLLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.SLLabel.Location = new System.Drawing.Point(205, 22);
            this.SLLabel.Name = "SLLabel";
            this.SLLabel.Size = new System.Drawing.Size(76, 20);
            this.SLLabel.TabIndex = 8;
            this.SLLabel.Text = "Stop Loss";
            this.SLLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SLLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // TPLabel
            // 
            this.TPLabel.AutoSize = true;
            this.TPLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.TPLabel.Location = new System.Drawing.Point(376, 22);
            this.TPLabel.Name = "TPLabel";
            this.TPLabel.Size = new System.Drawing.Size(85, 20);
            this.TPLabel.TabIndex = 9;
            this.TPLabel.Text = "Take Profit";
            this.TPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TPLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // PipsAmountLabel
            // 
            this.PipsAmountLabel.AutoSize = true;
            this.PipsAmountLabel.Location = new System.Drawing.Point(20, 102);
            this.PipsAmountLabel.Name = "PipsAmountLabel";
            this.PipsAmountLabel.Size = new System.Drawing.Size(98, 20);
            this.PipsAmountLabel.TabIndex = 12;
            this.PipsAmountLabel.Text = "Pips Amount : ";
            this.PipsAmountLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // FixedPipsBasedLabel
            // 
            this.FixedPipsBasedLabel.AutoSize = true;
            this.FixedPipsBasedLabel.Location = new System.Drawing.Point(19, 56);
            this.FixedPipsBasedLabel.Name = "FixedPipsBasedLabel";
            this.FixedPipsBasedLabel.Size = new System.Drawing.Size(117, 20);
            this.FixedPipsBasedLabel.TabIndex = 13;
            this.FixedPipsBasedLabel.Text = "Fixed Pips Based?";
            this.FixedPipsBasedLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // FixedPipsTPComboBox
            // 
            this.FixedPipsTPComboBox.FormattingEnabled = true;
            this.FixedPipsTPComboBox.ItemHeight = 24;
            this.FixedPipsTPComboBox.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.FixedPipsTPComboBox.Location = new System.Drawing.Point(358, 56);
            this.FixedPipsTPComboBox.Name = "FixedPipsTPComboBox";
            this.FixedPipsTPComboBox.Size = new System.Drawing.Size(121, 30);
            this.FixedPipsTPComboBox.TabIndex = 16;
            this.FixedPipsTPComboBox.UseSelectable = true;
            // 
            // FixedPipsSLComboBox
            // 
            this.FixedPipsSLComboBox.FormattingEnabled = true;
            this.FixedPipsSLComboBox.ItemHeight = 24;
            this.FixedPipsSLComboBox.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.FixedPipsSLComboBox.Location = new System.Drawing.Point(181, 56);
            this.FixedPipsSLComboBox.Name = "FixedPipsSLComboBox";
            this.FixedPipsSLComboBox.Size = new System.Drawing.Size(121, 30);
            this.FixedPipsSLComboBox.TabIndex = 17;
            this.FixedPipsSLComboBox.UseSelectable = true;
            // 
            // ATRBasedLabel
            // 
            this.ATRBasedLabel.AutoSize = true;
            this.ATRBasedLabel.Location = new System.Drawing.Point(19, 147);
            this.ATRBasedLabel.Name = "ATRBasedLabel";
            this.ATRBasedLabel.Size = new System.Drawing.Size(79, 20);
            this.ATRBasedLabel.TabIndex = 18;
            this.ATRBasedLabel.Text = "ATR Based?";
            this.ATRBasedLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // ATRTPComboBox
            // 
            this.ATRTPComboBox.FormattingEnabled = true;
            this.ATRTPComboBox.ItemHeight = 24;
            this.ATRTPComboBox.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.ATRTPComboBox.Location = new System.Drawing.Point(358, 137);
            this.ATRTPComboBox.Name = "ATRTPComboBox";
            this.ATRTPComboBox.Size = new System.Drawing.Size(121, 30);
            this.ATRTPComboBox.TabIndex = 19;
            this.ATRTPComboBox.UseSelectable = true;
            // 
            // ATRSLComboBox
            // 
            this.ATRSLComboBox.FormattingEnabled = true;
            this.ATRSLComboBox.ItemHeight = 24;
            this.ATRSLComboBox.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.ATRSLComboBox.Location = new System.Drawing.Point(181, 137);
            this.ATRSLComboBox.Name = "ATRSLComboBox";
            this.ATRSLComboBox.Size = new System.Drawing.Size(121, 30);
            this.ATRSLComboBox.TabIndex = 20;
            this.ATRSLComboBox.UseSelectable = true;
            // 
            // ATRMultiplierLabel
            // 
            this.ATRMultiplierLabel.AutoSize = true;
            this.ATRMultiplierLabel.Location = new System.Drawing.Point(19, 237);
            this.ATRMultiplierLabel.Name = "ATRMultiplierLabel";
            this.ATRMultiplierLabel.Size = new System.Drawing.Size(104, 20);
            this.ATRMultiplierLabel.TabIndex = 21;
            this.ATRMultiplierLabel.Text = "ATR Multiplier : ";
            this.ATRMultiplierLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(271, 493);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 24;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseSelectable = true;
            // 
            // MoveUpButton
            // 
            this.MoveUpButton.Location = new System.Drawing.Point(307, 73);
            this.MoveUpButton.Name = "MoveUpButton";
            this.MoveUpButton.Size = new System.Drawing.Size(96, 23);
            this.MoveUpButton.TabIndex = 25;
            this.MoveUpButton.Text = "Move Up";
            this.MoveUpButton.UseSelectable = true;
            // 
            // ATRPeriodsLabel
            // 
            this.ATRPeriodsLabel.AutoSize = true;
            this.ATRPeriodsLabel.Location = new System.Drawing.Point(20, 192);
            this.ATRPeriodsLabel.Name = "ATRPeriodsLabel";
            this.ATRPeriodsLabel.Size = new System.Drawing.Size(92, 20);
            this.ATRPeriodsLabel.TabIndex = 26;
            this.ATRPeriodsLabel.Text = "ATR Periods : ";
            this.ATRPeriodsLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // OptionsProgressBar
            // 
            this.OptionsProgressBar.Location = new System.Drawing.Point(23, 493);
            this.OptionsProgressBar.Name = "OptionsProgressBar";
            this.OptionsProgressBar.Size = new System.Drawing.Size(186, 23);
            this.OptionsProgressBar.TabIndex = 29;
            // 
            // OnCandleCloseLabel
            // 
            this.OnCandleCloseLabel.AutoSize = true;
            this.OnCandleCloseLabel.Location = new System.Drawing.Point(21, 33);
            this.OnCandleCloseLabel.Name = "OnCandleCloseLabel";
            this.OnCandleCloseLabel.Size = new System.Drawing.Size(116, 20);
            this.OnCandleCloseLabel.TabIndex = 30;
            this.OnCandleCloseLabel.Text = "On Candle Close:";
            this.OnCandleCloseLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // OnCandleCloseComboBox
            // 
            this.OnCandleCloseComboBox.FormattingEnabled = true;
            this.OnCandleCloseComboBox.ItemHeight = 24;
            this.OnCandleCloseComboBox.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.OnCandleCloseComboBox.Location = new System.Drawing.Point(285, 23);
            this.OnCandleCloseComboBox.Name = "OnCandleCloseComboBox";
            this.OnCandleCloseComboBox.Size = new System.Drawing.Size(121, 30);
            this.OnCandleCloseComboBox.TabIndex = 31;
            this.OnCandleCloseComboBox.UseSelectable = true;
            // 
            // OneOpenPositionSymbolComboBox
            // 
            this.OneOpenPositionSymbolComboBox.FormattingEnabled = true;
            this.OneOpenPositionSymbolComboBox.ItemHeight = 24;
            this.OneOpenPositionSymbolComboBox.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.OneOpenPositionSymbolComboBox.Location = new System.Drawing.Point(285, 74);
            this.OneOpenPositionSymbolComboBox.Name = "OneOpenPositionSymbolComboBox";
            this.OneOpenPositionSymbolComboBox.Size = new System.Drawing.Size(121, 30);
            this.OneOpenPositionSymbolComboBox.TabIndex = 34;
            this.OneOpenPositionSymbolComboBox.UseSelectable = true;
            // 
            // OneOpenPositionSymbolLabel
            // 
            this.OneOpenPositionSymbolLabel.AutoSize = true;
            this.OneOpenPositionSymbolLabel.Location = new System.Drawing.Point(21, 84);
            this.OneOpenPositionSymbolLabel.Name = "OneOpenPositionSymbolLabel";
            this.OneOpenPositionSymbolLabel.Size = new System.Drawing.Size(235, 20);
            this.OneOpenPositionSymbolLabel.TabIndex = 33;
            this.OneOpenPositionSymbolLabel.Text = "Only One Open Position Per Symbol:";
            this.OneOpenPositionSymbolLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // OptionsTabControl
            // 
            this.OptionsTabControl.Controls.Add(this.SymbolsTab);
            this.OptionsTabControl.Controls.Add(this.SLTPTab);
            this.OptionsTabControl.Controls.Add(this.EntryTab);
            this.OptionsTabControl.Location = new System.Drawing.Point(23, 63);
            this.OptionsTabControl.Name = "OptionsTabControl";
            this.OptionsTabControl.SelectedIndex = 2;
            this.OptionsTabControl.Size = new System.Drawing.Size(570, 401);
            this.OptionsTabControl.TabIndex = 35;
            this.OptionsTabControl.UseSelectable = true;
            // 
            // SymbolsTab
            // 
            this.SymbolsTab.Controls.Add(this.SymbolsGrid);
            this.SymbolsTab.Controls.Add(this.DeleteSymbolsButton);
            this.SymbolsTab.Controls.Add(this.AddSymbolTextBox);
            this.SymbolsTab.Controls.Add(this.AddButton);
            this.SymbolsTab.Controls.Add(this.MoveUpButton);
            this.SymbolsTab.Controls.Add(this.MoveDownButton);
            this.SymbolsTab.HorizontalScrollbarBarColor = true;
            this.SymbolsTab.HorizontalScrollbarHighlightOnWheel = false;
            this.SymbolsTab.HorizontalScrollbarSize = 10;
            this.SymbolsTab.Location = new System.Drawing.Point(4, 38);
            this.SymbolsTab.Name = "SymbolsTab";
            this.SymbolsTab.Size = new System.Drawing.Size(562, 359);
            this.SymbolsTab.TabIndex = 0;
            this.SymbolsTab.Text = "Symbols";
            this.SymbolsTab.VerticalScrollbarBarColor = true;
            this.SymbolsTab.VerticalScrollbarHighlightOnWheel = false;
            this.SymbolsTab.VerticalScrollbarSize = 10;
            // 
            // SLTPTab
            // 
            this.SLTPTab.Controls.Add(this.ATRMultiplierSLNumber);
            this.SLTPTab.Controls.Add(this.ATRMultiplierTPNumber);
            this.SLTPTab.Controls.Add(this.PipsTPNumber);
            this.SLTPTab.Controls.Add(this.ATRPeriodsTPNumber);
            this.SLTPTab.Controls.Add(this.ATRPeriodsSLNumber);
            this.SLTPTab.Controls.Add(this.PipsSLNumber);
            this.SLTPTab.Controls.Add(this.SLLabel);
            this.SLTPTab.Controls.Add(this.TPLabel);
            this.SLTPTab.Controls.Add(this.PipsAmountLabel);
            this.SLTPTab.Controls.Add(this.FixedPipsBasedLabel);
            this.SLTPTab.Controls.Add(this.FixedPipsTPComboBox);
            this.SLTPTab.Controls.Add(this.ATRPeriodsLabel);
            this.SLTPTab.Controls.Add(this.FixedPipsSLComboBox);
            this.SLTPTab.Controls.Add(this.ATRBasedLabel);
            this.SLTPTab.Controls.Add(this.ATRTPComboBox);
            this.SLTPTab.Controls.Add(this.ATRSLComboBox);
            this.SLTPTab.Controls.Add(this.ATRMultiplierLabel);
            this.SLTPTab.HorizontalScrollbarBarColor = true;
            this.SLTPTab.HorizontalScrollbarHighlightOnWheel = false;
            this.SLTPTab.HorizontalScrollbarSize = 10;
            this.SLTPTab.Location = new System.Drawing.Point(4, 38);
            this.SLTPTab.Name = "SLTPTab";
            this.SLTPTab.Size = new System.Drawing.Size(562, 359);
            this.SLTPTab.TabIndex = 1;
            this.SLTPTab.Text = "SL And TP";
            this.SLTPTab.VerticalScrollbarBarColor = true;
            this.SLTPTab.VerticalScrollbarHighlightOnWheel = false;
            this.SLTPTab.VerticalScrollbarSize = 10;
            // 
            // EntryTab
            // 
            this.EntryTab.Controls.Add(this.OneOpenPositionCurrencyComboBox);
            this.EntryTab.Controls.Add(this.OneOpenPositionCurrencyLabel);
            this.EntryTab.Controls.Add(this.OneOpenPositionSymbolComboBox);
            this.EntryTab.Controls.Add(this.OneOpenPositionSymbolLabel);
            this.EntryTab.Controls.Add(this.OnCandleCloseComboBox);
            this.EntryTab.Controls.Add(this.OnCandleCloseLabel);
            this.EntryTab.HorizontalScrollbarBarColor = true;
            this.EntryTab.HorizontalScrollbarHighlightOnWheel = false;
            this.EntryTab.HorizontalScrollbarSize = 10;
            this.EntryTab.Location = new System.Drawing.Point(4, 38);
            this.EntryTab.Name = "EntryTab";
            this.EntryTab.Size = new System.Drawing.Size(562, 359);
            this.EntryTab.TabIndex = 2;
            this.EntryTab.Text = "Entry";
            this.EntryTab.VerticalScrollbarBarColor = true;
            this.EntryTab.VerticalScrollbarHighlightOnWheel = false;
            this.EntryTab.VerticalScrollbarSize = 10;
            // 
            // NumberColumn
            // 
            this.NumberColumn.HeaderText = "Number";
            this.NumberColumn.Name = "NumberColumn";
            this.NumberColumn.ReadOnly = true;
            // 
            // SymbolColumn
            // 
            this.SymbolColumn.HeaderText = "Symbol";
            this.SymbolColumn.Name = "SymbolColumn";
            this.SymbolColumn.ReadOnly = true;
            // 
            // PipsSLNumber
            // 
            this.PipsSLNumber.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.PipsSLNumber.Location = new System.Drawing.Point(181, 100);
            this.PipsSLNumber.Name = "PipsSLNumber";
            this.PipsSLNumber.Size = new System.Drawing.Size(121, 22);
            this.PipsSLNumber.TabIndex = 27;
            this.PipsSLNumber.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // ATRPeriodsSLNumber
            // 
            this.ATRPeriodsSLNumber.Location = new System.Drawing.Point(181, 190);
            this.ATRPeriodsSLNumber.Name = "ATRPeriodsSLNumber";
            this.ATRPeriodsSLNumber.Size = new System.Drawing.Size(121, 22);
            this.ATRPeriodsSLNumber.TabIndex = 28;
            this.ATRPeriodsSLNumber.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // ATRPeriodsTPNumber
            // 
            this.ATRPeriodsTPNumber.Location = new System.Drawing.Point(358, 190);
            this.ATRPeriodsTPNumber.Name = "ATRPeriodsTPNumber";
            this.ATRPeriodsTPNumber.Size = new System.Drawing.Size(121, 22);
            this.ATRPeriodsTPNumber.TabIndex = 29;
            this.ATRPeriodsTPNumber.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // PipsTPNumber
            // 
            this.PipsTPNumber.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.PipsTPNumber.Location = new System.Drawing.Point(358, 100);
            this.PipsTPNumber.Name = "PipsTPNumber";
            this.PipsTPNumber.Size = new System.Drawing.Size(121, 22);
            this.PipsTPNumber.TabIndex = 30;
            this.PipsTPNumber.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // ATRMultiplierTPNumber
            // 
            this.ATRMultiplierTPNumber.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ATRMultiplierTPNumber.Location = new System.Drawing.Point(358, 235);
            this.ATRMultiplierTPNumber.Name = "ATRMultiplierTPNumber";
            this.ATRMultiplierTPNumber.Size = new System.Drawing.Size(121, 22);
            this.ATRMultiplierTPNumber.TabIndex = 31;
            this.ATRMultiplierTPNumber.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // ATRMultiplierSLNumber
            // 
            this.ATRMultiplierSLNumber.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ATRMultiplierSLNumber.Location = new System.Drawing.Point(181, 235);
            this.ATRMultiplierSLNumber.Name = "ATRMultiplierSLNumber";
            this.ATRMultiplierSLNumber.Size = new System.Drawing.Size(121, 22);
            this.ATRMultiplierSLNumber.TabIndex = 32;
            this.ATRMultiplierSLNumber.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // OneOpenPositionCurrencyComboBox
            // 
            this.OneOpenPositionCurrencyComboBox.FormattingEnabled = true;
            this.OneOpenPositionCurrencyComboBox.ItemHeight = 24;
            this.OneOpenPositionCurrencyComboBox.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.OneOpenPositionCurrencyComboBox.Location = new System.Drawing.Point(285, 125);
            this.OneOpenPositionCurrencyComboBox.Name = "OneOpenPositionCurrencyComboBox";
            this.OneOpenPositionCurrencyComboBox.Size = new System.Drawing.Size(121, 30);
            this.OneOpenPositionCurrencyComboBox.TabIndex = 36;
            this.OneOpenPositionCurrencyComboBox.UseSelectable = true;
            // 
            // OneOpenPositionCurrencyLabel
            // 
            this.OneOpenPositionCurrencyLabel.AutoSize = true;
            this.OneOpenPositionCurrencyLabel.Location = new System.Drawing.Point(21, 135);
            this.OneOpenPositionCurrencyLabel.Name = "OneOpenPositionCurrencyLabel";
            this.OneOpenPositionCurrencyLabel.Size = new System.Drawing.Size(247, 20);
            this.OneOpenPositionCurrencyLabel.TabIndex = 35;
            this.OneOpenPositionCurrencyLabel.Text = "Only One Open Position Per Currency:";
            this.OneOpenPositionCurrencyLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 539);
            this.Controls.Add(this.OptionsTabControl);
            this.Controls.Add(this.OptionsProgressBar);
            this.Controls.Add(this.SaveButton);
            this.MaximizeBox = false;
            this.Name = "OptionsForm";
            this.Resizable = false;
            this.ShowIcon = false;
            this.Text = "Options";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SymbolsGrid)).EndInit();
            this.OptionsTabControl.ResumeLayout(false);
            this.SymbolsTab.ResumeLayout(false);
            this.SLTPTab.ResumeLayout(false);
            this.SLTPTab.PerformLayout();
            this.EntryTab.ResumeLayout(false);
            this.EntryTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PipsSLNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ATRPeriodsSLNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ATRPeriodsTPNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PipsTPNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ATRMultiplierTPNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ATRMultiplierSLNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroGrid SymbolsGrid;
        private MetroFramework.Controls.MetroTextBox AddSymbolTextBox;
        private MetroFramework.Controls.MetroButton DeleteSymbolsButton;
        private MetroFramework.Controls.MetroButton AddButton;
        private MetroFramework.Controls.MetroButton MoveDownButton;
        private MetroFramework.Controls.MetroLabel SLLabel;
        private MetroFramework.Controls.MetroLabel TPLabel;
        private MetroFramework.Controls.MetroLabel PipsAmountLabel;
        private MetroFramework.Controls.MetroLabel FixedPipsBasedLabel;
        private MetroFramework.Controls.MetroComboBox FixedPipsTPComboBox;
        private MetroFramework.Controls.MetroComboBox FixedPipsSLComboBox;
        private MetroFramework.Controls.MetroLabel ATRBasedLabel;
        private MetroFramework.Controls.MetroComboBox ATRTPComboBox;
        private MetroFramework.Controls.MetroComboBox ATRSLComboBox;
        private MetroFramework.Controls.MetroLabel ATRMultiplierLabel;
        private MetroFramework.Controls.MetroButton SaveButton;
        private MetroFramework.Controls.MetroButton MoveUpButton;
        private MetroFramework.Controls.MetroLabel ATRPeriodsLabel;
        private MetroFramework.Controls.MetroProgressBar OptionsProgressBar;
        private MetroFramework.Controls.MetroLabel OnCandleCloseLabel;
        private MetroFramework.Controls.MetroComboBox OnCandleCloseComboBox;
        private MetroFramework.Controls.MetroComboBox OneOpenPositionSymbolComboBox;
        private MetroFramework.Controls.MetroLabel OneOpenPositionSymbolLabel;
        private MetroFramework.Controls.MetroTabControl OptionsTabControl;
        private MetroFramework.Controls.MetroTabPage SymbolsTab;
        private MetroFramework.Controls.MetroTabPage SLTPTab;
        private MetroFramework.Controls.MetroTabPage EntryTab;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SymbolColumn;
        private System.Windows.Forms.NumericUpDown ATRMultiplierSLNumber;
        private System.Windows.Forms.NumericUpDown ATRMultiplierTPNumber;
        private System.Windows.Forms.NumericUpDown PipsTPNumber;
        private System.Windows.Forms.NumericUpDown ATRPeriodsTPNumber;
        private System.Windows.Forms.NumericUpDown ATRPeriodsSLNumber;
        private System.Windows.Forms.NumericUpDown PipsSLNumber;
        private MetroFramework.Controls.MetroComboBox OneOpenPositionCurrencyComboBox;
        private MetroFramework.Controls.MetroLabel OneOpenPositionCurrencyLabel;
    }
}