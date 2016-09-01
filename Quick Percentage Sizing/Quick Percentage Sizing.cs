using System;
using System.Linq;
using cAlgo.API;
using cAlgo.API.Indicators;
using cAlgo.API.Internals;
using cAlgo.Indicators;
using MetroFramework.Forms;
using MetroFramework.Controls;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace cAlgo
{
    [Robot(TimeZone = TimeZones.UTC, AccessRights = AccessRights.FullAccess)]
    public class QuickPercentageSizing : Robot
    {
        // Parameters
        [Parameter("Default Symbols", DefaultValue = "EURUSD GBPUSD EURJPY USDJPY AUDUSD USDCHF GBPJPY USDCAD EURGBP EURCHF AUDJPY NZDUSD CHFJPY EURAUD CADJPY GBPAUD EURCAD AUDCAD GBPCAD AUDNZD NZDJPY AUDCHF GBPNZD EURNZD CADCHF NZDCAD NZDCHF GBPCHF")]
        public string defaultSymbols { get; set; }

        // Controls
        // Main Form Controls
        private MetroForm MainForm;
        private MetroButton BuyButton;
        private MetroButton SellButton;
        private MetroButton OptionsButton;
        private MetroLabel PercentLabel;
        private NumericUpDown PercentNumber;
        private MetroComboBox SymbolsComboBox;
        private MetroLabel RiskLabel;
        private MetroLabel SlippageLabel;
        private NumericUpDown SlippageNumber;
        private MetroLabel SlippagePipsLabel;

        // Options Form Controls
        private MetroForm OptionsForm;
        private MetroGrid SymbolsGrid;
        private MetroTextBox AddSymbolTextBox;
        private MetroButton DeleteSymbolsButton;
        private MetroButton AddButton;
        private MetroButton MoveDownButton;
        private MetroLabel SLLabel;
        private MetroLabel TPLabel;
        private MetroLabel PipsAmountLabel;
        private MetroLabel FixedPipsBasedLabel;
        private MetroComboBox FixedPipsTPComboBox;
        private MetroComboBox FixedPipsSLComboBox;
        private MetroLabel ATRBasedLabel;
        private MetroComboBox ATRTPComboBox;
        private MetroComboBox ATRSLComboBox;
        private MetroLabel ATRMultiplierLabel;
        private MetroButton SaveButton;
        private MetroButton MoveUpButton;
        private MetroLabel ATRPeriodsLabel;
        private MetroProgressBar OptionsProgressBar;
        private MetroLabel OnCandleCloseLabel;
        private MetroComboBox OnCandleCloseComboBox;
        private MetroComboBox OneOpenPositionSymbolComboBox;
        private MetroLabel OneOpenPositionSymbolLabel;
        private MetroTabControl OptionsTabControl;
        private MetroTabPage SymbolsTab;
        private MetroTabPage SLTPTab;
        private MetroTabPage EntryTab;
        private DataGridViewTextBoxColumn NumberColumn;
        private DataGridViewTextBoxColumn SymbolColumn;
        private NumericUpDown ATRMultiplierSLNumber;
        private NumericUpDown ATRMultiplierTPNumber;
        private NumericUpDown PipsTPNumber;
        private NumericUpDown ATRPeriodsTPNumber;
        private NumericUpDown ATRPeriodsSLNumber;
        private NumericUpDown PipsSLNumber;
        private MetroComboBox OneOpenPositionCurrencyComboBox;
        private MetroLabel OneOpenPositionCurrencyLabel;



        // Collections
        List<string> SymbolsList = new List<string>();
        List<string> OpenPositionsSymbols = new List<string>();
        List<string> OpenPositionsCurrencies = new List<string>();

        // Options
        bool FixedSL = false;
        bool FixedTP = false;
        double PipsSL = 0;
        double PipsTP = 0;
        bool ATRSL = false;
        bool ATRTP = false;
        double ATRMultiplierSL = 0;
        double ATRMultiplierTP = 0;
        int ATRPeriodsSL = 0;
        int ATRPeriodsTP = 0;
        bool EntryOnCandleClose = false;
        bool OneOpenPositionSymbol = false;
        bool OneOpenPositionCurrency = false;


        // Trades Label
        string label = "QuickTrade";

        // cBot Methods
        protected override void OnStart()
        {
            foreach (string sym in defaultSymbols.Split(' '))
            {
                SymbolsList.Add(sym);
            }

            foreach (Position pos in Positions)
            {
                if (pos.Label == label)
                {
                    if (!OpenPositionsSymbols.Contains(pos.SymbolCode))
                        OpenPositionsSymbols.Add(pos.SymbolCode);
                }
            }

            Positions.Closed += PositionsOnClosed;
            Task showMainForm = Task.Factory.StartNew(() => { MainFormInitializer(); });


        }

        protected override void OnStop()
        {
            MainForm.Close();
        }

        // Main Form Initializer
        private void MainFormInitializer()
        {
            // 
            // MainForm
            //
            MainForm = new MetroForm();
            MainForm.AutoScaleDimensions = new SizeF(8f, 16f);
            MainForm.AutoScaleMode = AutoScaleMode.Font;
            MainForm.ClientSize = new Size(259, 177);
            MainForm.MaximizeBox = false;
            MainForm.Name = "MainForm";
            MainForm.Resizable = false;
            MainForm.ShowIcon = false;
            MainForm.Theme = MetroFramework.MetroThemeStyle.Dark;
            MainForm.Style = MetroFramework.MetroColorStyle.Blue;
            MainForm.Shown += new EventHandler(MainFormShown);
            MainForm.StartPosition = FormStartPosition.CenterScreen;
            MainForm.FormClosed += new FormClosedEventHandler(MainFormClosed);


            // 
            // SymbolsComboBox
            // 

            SymbolsComboBox = new MetroComboBox();
            SymbolsComboBox.Name = "SymbolsComboBox";
            SymbolsComboBox.FormattingEnabled = true;
            SymbolsComboBox.ItemHeight = 24;
            SymbolsComboBox.Location = new Point(23, 18);
            SymbolsComboBox.Size = new Size(139, 30);
            SymbolsComboBox.UseSelectable = true;
            SymbolsComboBox.SelectedIndexChanged += new EventHandler(SymbolsComboBoxSelectionChanged);

            FillSymbolsComboBox();

            // 
            // SellButton
            // 
            SellButton = new MetroButton();
            SellButton.Location = new Point(94, 136);
            SellButton.Name = "SellButton";
            SellButton.Size = new Size(50, 23);
            SellButton.Text = "Sell";
            SellButton.UseSelectable = true;
            SellButton.Click += new EventHandler(ExecuteSellOrder);
            // 
            // BuyButton
            // 
            BuyButton = new MetroButton();
            BuyButton.Location = new Point(23, 136);
            BuyButton.Name = "BuyButton";
            BuyButton.Size = new Size(50, 23);
            BuyButton.Text = "Buy";
            BuyButton.UseSelectable = true;
            BuyButton.Click += new EventHandler(ExecuteBuyOrder);
            // 
            // PercentNumber
            // 
            PercentNumber = new NumericUpDown();
            PercentNumber.DecimalPlaces = 1;
            PercentNumber.Increment = new decimal(new int[]
            {
                1,
                0,
                0,
                65536
            });
            PercentNumber.Location = new Point(112, 61);
            PercentNumber.Name = "PercentNumber";
            PercentNumber.Size = new Size(50, 22);
            PercentNumber.ThousandsSeparator = true;
            PercentNumber.Value = new decimal(new int[]
            {
                1,
                0,
                0,
                0
            });
            // 
            // PercentLabel
            // 
            PercentLabel = new MetroLabel();
            PercentLabel.AutoSize = true;
            PercentLabel.BackColor = Color.Lime;
            PercentLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            PercentLabel.ForeColor = Color.Lime;
            PercentLabel.Location = new Point(181, 60);
            PercentLabel.Name = "PercentLabel";
            PercentLabel.Size = new Size(22, 20);
            PercentLabel.Text = "%";
            PercentLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            PercentLabel.UseCustomForeColor = true;
            // 
            // OptionsButton
            // 
            OptionsButton = new MetroButton();
            OptionsButton.Location = new Point(170, 136);
            OptionsButton.Name = "OptionsButton";
            OptionsButton.Size = new Size(76, 23);
            OptionsButton.Text = "Options";
            OptionsButton.UseSelectable = true;
            OptionsButton.Click += new EventHandler(ShowOptionsForm);
            // 
            // RiskLabel
            // 
            RiskLabel = new MetroLabel();
            RiskLabel.AutoSize = true;
            RiskLabel.Location = new Point(23, 63);
            RiskLabel.Name = "RiskLabel";
            RiskLabel.Size = new Size(36, 20);
            RiskLabel.Text = "Risk:";
            RiskLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // SlippageLabel
            // 
            SlippageLabel = new MetroLabel();
            SlippageLabel.AutoSize = true;
            SlippageLabel.Location = new Point(23, 91);
            SlippageLabel.Name = "SlippageLabel";
            SlippageLabel.Size = new Size(64, 20);
            SlippageLabel.Text = "Slippage:";
            SlippageLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // SlippageNumber
            // 
            SlippageNumber = new NumericUpDown();
            SlippageNumber.DecimalPlaces = 1;
            SlippageNumber.Increment = new decimal(new int[]
            {
                1,
                0,
                0,
                65536
            });
            SlippageNumber.Location = new Point(112, 93);
            SlippageNumber.Minimum = new decimal(new int[]
            {
                1,
                0,
                0,
                0
            });
            SlippageNumber.Name = "SlippageNumber";
            SlippageNumber.Size = new Size(50, 22);
            SlippageNumber.ThousandsSeparator = true;
            SlippageNumber.Value = new decimal(new int[]
            {
                1,
                0,
                0,
                0
            });
            // 
            // SlippagePipsLabel
            // 
            SlippagePipsLabel = new MetroLabel();
            SlippagePipsLabel.AutoSize = true;
            SlippagePipsLabel.BackColor = Color.Lime;
            SlippagePipsLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            SlippagePipsLabel.ForeColor = Color.Lime;
            SlippagePipsLabel.Location = new Point(181, 93);
            SlippagePipsLabel.Name = "SlippagePipsLabel";
            SlippagePipsLabel.Size = new Size(38, 20);
            SlippagePipsLabel.Text = "Pips";
            SlippagePipsLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            SlippagePipsLabel.UseCustomForeColor = true;


            // Adding Controls
            MainForm.Controls.AddRange(new Control[]
            {
                SymbolsComboBox,
                PercentNumber,
                PercentLabel,
                BuyButton,
                SellButton,
                OptionsButton,
                RiskLabel,
                SlippageLabel,
                SlippageNumber,
                SlippagePipsLabel
            });

            Application.Run(MainForm);

        }

        private void OptionsFormInitializer()
        {
            OptionsForm = new MetroForm();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            SymbolsGrid = new MetroGrid();
            AddSymbolTextBox = new MetroTextBox();
            DeleteSymbolsButton = new MetroButton();
            AddButton = new MetroButton();
            MoveDownButton = new MetroButton();
            SLLabel = new MetroLabel();
            TPLabel = new MetroLabel();
            PipsAmountLabel = new MetroLabel();
            FixedPipsBasedLabel = new MetroLabel();
            FixedPipsTPComboBox = new MetroComboBox();
            FixedPipsSLComboBox = new MetroComboBox();
            ATRBasedLabel = new MetroLabel();
            ATRTPComboBox = new MetroComboBox();
            ATRSLComboBox = new MetroComboBox();
            ATRMultiplierLabel = new MetroLabel();
            SaveButton = new MetroButton();
            MoveUpButton = new MetroButton();
            ATRPeriodsLabel = new MetroLabel();
            OptionsProgressBar = new MetroProgressBar();
            OnCandleCloseLabel = new MetroLabel();
            OnCandleCloseComboBox = new MetroComboBox();
            OneOpenPositionSymbolComboBox = new MetroComboBox();
            OneOpenPositionSymbolLabel = new MetroLabel();
            OptionsTabControl = new MetroTabControl();
            SymbolsTab = new MetroTabPage();
            SLTPTab = new MetroTabPage();
            EntryTab = new MetroTabPage();
            NumberColumn = new DataGridViewTextBoxColumn();
            SymbolColumn = new DataGridViewTextBoxColumn();
            PipsSLNumber = new NumericUpDown();
            ATRPeriodsSLNumber = new NumericUpDown();
            ATRPeriodsTPNumber = new NumericUpDown();
            PipsTPNumber = new NumericUpDown();
            ATRMultiplierTPNumber = new NumericUpDown();
            ATRMultiplierSLNumber = new NumericUpDown();
            OneOpenPositionCurrencyComboBox = new MetroComboBox();
            OneOpenPositionCurrencyLabel = new MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(SymbolsGrid)).BeginInit();
            OptionsTabControl.SuspendLayout();
            SymbolsTab.SuspendLayout();
            SLTPTab.SuspendLayout();
            EntryTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(PipsSLNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(ATRPeriodsSLNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(ATRPeriodsTPNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(PipsTPNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(ATRMultiplierTPNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(ATRMultiplierSLNumber)).BeginInit();
            OptionsForm.SuspendLayout();
            // 
            // SymbolsGrid
            // 
            SymbolsGrid.AllowUserToAddRows = false;
            SymbolsGrid.AllowUserToDeleteRows = false;
            SymbolsGrid.AllowUserToResizeColumns = false;
            SymbolsGrid.AllowUserToResizeRows = false;
            SymbolsGrid.BackgroundColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            SymbolsGrid.BorderStyle = BorderStyle.None;
            SymbolsGrid.CellBorderStyle = DataGridViewCellBorderStyle.None;
            SymbolsGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            SymbolsGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            SymbolsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            SymbolsGrid.Columns.AddRange(new DataGridViewColumn[] {
            NumberColumn,
            SymbolColumn});
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            SymbolsGrid.DefaultCellStyle = dataGridViewCellStyle2;
            SymbolsGrid.EnableHeadersVisualStyles = false;
            SymbolsGrid.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
            SymbolsGrid.GridColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            SymbolsGrid.Location = new Point(12, 14);
            SymbolsGrid.Name = "SymbolsGrid";
            SymbolsGrid.ReadOnly = true;
            SymbolsGrid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            SymbolsGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            SymbolsGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            SymbolsGrid.RowTemplate.Height = 24;
            SymbolsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            SymbolsGrid.Size = new Size(282, 270);
            SymbolsGrid.TabIndex = 0;
            SymbolsGrid.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // AddSymbolTextBox
            // 
            // 
            // 
            // 
            AddSymbolTextBox.CustomButton.Image = null;
            AddSymbolTextBox.CustomButton.Location = new Point(121, 1);
            AddSymbolTextBox.CustomButton.Name = "";
            AddSymbolTextBox.CustomButton.Size = new Size(21, 21);
            AddSymbolTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            AddSymbolTextBox.CustomButton.TabIndex = 1;
            AddSymbolTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            AddSymbolTextBox.CustomButton.UseSelectable = true;
            AddSymbolTextBox.CustomButton.Visible = false;
            AddSymbolTextBox.Lines = new string[] {
        "Enter Symbol Code"};
            AddSymbolTextBox.Location = new Point(307, 14);
            AddSymbolTextBox.MaxLength = 32767;
            AddSymbolTextBox.Name = "AddSymbolTextBox";
            AddSymbolTextBox.PasswordChar = '\0';
            AddSymbolTextBox.ScrollBars = ScrollBars.None;
            AddSymbolTextBox.SelectedText = "";
            AddSymbolTextBox.SelectionLength = 0;
            AddSymbolTextBox.SelectionStart = 0;
            AddSymbolTextBox.ShortcutsEnabled = true;
            AddSymbolTextBox.Size = new Size(143, 23);
            AddSymbolTextBox.TabIndex = 2;
            AddSymbolTextBox.Text = "Enter Symbol Code";
            AddSymbolTextBox.UseSelectable = true;
            AddSymbolTextBox.WaterMarkColor = Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            AddSymbolTextBox.WaterMarkFont = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Pixel);
            // 
            // DeleteSymbolsButton
            // 
            DeleteSymbolsButton.Location = new Point(81, 305);
            DeleteSymbolsButton.Name = "DeleteSymbolsButton";
            DeleteSymbolsButton.Size = new Size(162, 23);
            DeleteSymbolsButton.TabIndex = 3;
            DeleteSymbolsButton.Text = "Delete Selected Symbols";
            DeleteSymbolsButton.UseSelectable = true;
            DeleteSymbolsButton.Click += new EventHandler(DeleteSymbols);

            // 
            // AddButton
            // 
            AddButton.Location = new Point(465, 14);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(76, 23);
            AddButton.TabIndex = 4;
            AddButton.Text = "Add";
            AddButton.UseSelectable = true;
            AddButton.Click += new EventHandler(AddSymbol);

            // 
            // MoveDownButton
            // 
            MoveDownButton.Location = new Point(422, 73);
            MoveDownButton.Name = "MoveDownButton";
            MoveDownButton.Size = new Size(96, 23);
            MoveDownButton.TabIndex = 5;
            MoveDownButton.Text = "Move Down";
            MoveDownButton.UseSelectable = true;
            MoveDownButton.Click += new EventHandler(MoveDownSymbols);

            // 
            // SLLabel
            // 
            SLLabel.AutoSize = true;
            SLLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            SLLabel.Location = new Point(205, 22);
            SLLabel.Name = "SLLabel";
            SLLabel.Size = new Size(76, 20);
            SLLabel.TabIndex = 8;
            SLLabel.Text = "Stop Loss";
            SLLabel.TextAlign = ContentAlignment.MiddleCenter;
            SLLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // TPLabel
            // 
            TPLabel.AutoSize = true;
            TPLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            TPLabel.Location = new Point(376, 22);
            TPLabel.Name = "TPLabel";
            TPLabel.Size = new Size(85, 20);
            TPLabel.TabIndex = 9;
            TPLabel.Text = "Take Profit";
            TPLabel.TextAlign = ContentAlignment.MiddleCenter;
            TPLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // PipsAmountLabel
            // 
            PipsAmountLabel.AutoSize = true;
            PipsAmountLabel.Location = new Point(20, 102);
            PipsAmountLabel.Name = "PipsAmountLabel";
            PipsAmountLabel.Size = new Size(98, 20);
            PipsAmountLabel.TabIndex = 12;
            PipsAmountLabel.Text = "Pips Amount : ";
            PipsAmountLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // FixedPipsBasedLabel
            // 
            FixedPipsBasedLabel.AutoSize = true;
            FixedPipsBasedLabel.Location = new Point(19, 56);
            FixedPipsBasedLabel.Name = "FixedPipsBasedLabel";
            FixedPipsBasedLabel.Size = new Size(117, 20);
            FixedPipsBasedLabel.TabIndex = 13;
            FixedPipsBasedLabel.Text = "Fixed Pips Based?";
            FixedPipsBasedLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // FixedPipsTPComboBox
            // 
            FixedPipsTPComboBox.FormattingEnabled = true;
            FixedPipsTPComboBox.ItemHeight = 24;
            FixedPipsTPComboBox.Items.AddRange(new object[] {
            "No",
            "Yes"});
            FixedPipsTPComboBox.Location = new Point(358, 56);
            FixedPipsTPComboBox.Name = "FixedPipsTPComboBox";
            FixedPipsTPComboBox.Size = new Size(121, 30);
            FixedPipsTPComboBox.TabIndex = 16;
            FixedPipsTPComboBox.UseSelectable = true;

            if (FixedTP)
                FixedPipsTPComboBox.SelectedIndex = 1;
            else
                FixedPipsTPComboBox.SelectedIndex = 0;

            // 
            // FixedPipsSLComboBox
            // 
            FixedPipsSLComboBox.FormattingEnabled = true;
            FixedPipsSLComboBox.ItemHeight = 24;
            FixedPipsSLComboBox.Items.AddRange(new object[] {
            "No",
            "Yes"});
            FixedPipsSLComboBox.Location = new Point(181, 56);
            FixedPipsSLComboBox.Name = "FixedPipsSLComboBox";
            FixedPipsSLComboBox.Size = new Size(121, 30);
            FixedPipsSLComboBox.TabIndex = 17;
            FixedPipsSLComboBox.UseSelectable = true;

            if (FixedSL)
                FixedPipsSLComboBox.SelectedIndex = 1;
            else
                FixedPipsSLComboBox.SelectedIndex = 0;
            // 
            // ATRBasedLabel
            // 
            ATRBasedLabel.AutoSize = true;
            ATRBasedLabel.Location = new Point(19, 147);
            ATRBasedLabel.Name = "ATRBasedLabel";
            ATRBasedLabel.Size = new Size(79, 20);
            ATRBasedLabel.TabIndex = 18;
            ATRBasedLabel.Text = "ATR Based?";
            ATRBasedLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // ATRTPComboBox
            // 
            ATRTPComboBox.FormattingEnabled = true;
            ATRTPComboBox.ItemHeight = 24;
            ATRTPComboBox.Items.AddRange(new object[] {
            "No",
            "Yes"});
            ATRTPComboBox.Location = new Point(358, 137);
            ATRTPComboBox.Name = "ATRTPComboBox";
            ATRTPComboBox.Size = new Size(121, 30);
            ATRTPComboBox.TabIndex = 19;
            ATRTPComboBox.UseSelectable = true;

            if (ATRTP)
                ATRTPComboBox.SelectedIndex = 1;
            else
                ATRTPComboBox.SelectedIndex = 0;
            // 
            // ATRSLComboBox
            // 
            ATRSLComboBox.FormattingEnabled = true;
            ATRSLComboBox.ItemHeight = 24;
            ATRSLComboBox.Items.AddRange(new object[] {
            "No",
            "Yes"});
            ATRSLComboBox.Location = new Point(181, 137);
            ATRSLComboBox.Name = "ATRSLComboBox";
            ATRSLComboBox.Size = new Size(121, 30);
            ATRSLComboBox.TabIndex = 20;
            ATRSLComboBox.UseSelectable = true;

            if (ATRSL)
                ATRSLComboBox.SelectedIndex = 1;
            else
                ATRSLComboBox.SelectedIndex = 0;

            // 
            // ATRMultiplierLabel
            // 
            ATRMultiplierLabel.AutoSize = true;
            ATRMultiplierLabel.Location = new Point(19, 237);
            ATRMultiplierLabel.Name = "ATRMultiplierLabel";
            ATRMultiplierLabel.Size = new Size(104, 20);
            ATRMultiplierLabel.TabIndex = 21;
            ATRMultiplierLabel.Text = "ATR Multiplier : ";
            ATRMultiplierLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(271, 493);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 23);
            SaveButton.TabIndex = 24;
            SaveButton.Text = "Save";
            SaveButton.UseSelectable = true;
            SaveButton.Click += new EventHandler(SaveOptions);

            // 
            // MoveUpButton
            // 
            MoveUpButton.Location = new Point(307, 73);
            MoveUpButton.Name = "MoveUpButton";
            MoveUpButton.Size = new Size(96, 23);
            MoveUpButton.TabIndex = 25;
            MoveUpButton.Text = "Move Up";
            MoveUpButton.UseSelectable = true;
            MoveUpButton.Click += new EventHandler(MoveUpSymbols);

            // 
            // ATRPeriodsLabel
            // 
            ATRPeriodsLabel.AutoSize = true;
            ATRPeriodsLabel.Location = new Point(20, 192);
            ATRPeriodsLabel.Name = "ATRPeriodsLabel";
            ATRPeriodsLabel.Size = new Size(92, 20);
            ATRPeriodsLabel.TabIndex = 26;
            ATRPeriodsLabel.Text = "ATR Periods : ";
            ATRPeriodsLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // OptionsProgressBar
            // 
            OptionsProgressBar.Location = new Point(23, 493);
            OptionsProgressBar.Name = "OptionsProgressBar";
            OptionsProgressBar.Size = new Size(186, 23);
            OptionsProgressBar.TabIndex = 29;
            OptionsProgressBar.Visible = false;

            // 
            // OnCandleCloseLabel
            // 
            OnCandleCloseLabel.AutoSize = true;
            OnCandleCloseLabel.Location = new Point(21, 33);
            OnCandleCloseLabel.Name = "OnCandleCloseLabel";
            OnCandleCloseLabel.Size = new Size(116, 20);
            OnCandleCloseLabel.TabIndex = 30;
            OnCandleCloseLabel.Text = "On Candle Close:";
            OnCandleCloseLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // OnCandleCloseComboBox
            // 
            OnCandleCloseComboBox.FormattingEnabled = true;
            OnCandleCloseComboBox.ItemHeight = 24;
            OnCandleCloseComboBox.Items.AddRange(new object[] {
            "No",
            "Yes"});
            OnCandleCloseComboBox.Location = new Point(285, 23);
            OnCandleCloseComboBox.Name = "OnCandleCloseComboBox";
            OnCandleCloseComboBox.Size = new Size(121, 30);
            OnCandleCloseComboBox.TabIndex = 31;
            OnCandleCloseComboBox.UseSelectable = true;

            if (EntryOnCandleClose)
                OnCandleCloseComboBox.SelectedIndex = 1;
            else
                OnCandleCloseComboBox.SelectedIndex = 0;

            // 
            // OneOpenPositionSymbolComboBox
            // 
            OneOpenPositionSymbolComboBox.FormattingEnabled = true;
            OneOpenPositionSymbolComboBox.ItemHeight = 24;
            OneOpenPositionSymbolComboBox.Items.AddRange(new object[] {
            "No",
            "Yes"});
            OneOpenPositionSymbolComboBox.Location = new Point(285, 74);
            OneOpenPositionSymbolComboBox.Name = "OneOpenPositionSymbolComboBox";
            OneOpenPositionSymbolComboBox.Size = new Size(121, 30);
            OneOpenPositionSymbolComboBox.TabIndex = 34;
            OneOpenPositionSymbolComboBox.UseSelectable = true;

            if (OneOpenPositionSymbol)
                OneOpenPositionSymbolComboBox.SelectedIndex = 1;
            else
                OneOpenPositionSymbolComboBox.SelectedIndex = 0;

            // 
            // OneOpenPositionSymbolLabel
            // 
            OneOpenPositionSymbolLabel.AutoSize = true;
            OneOpenPositionSymbolLabel.Location = new Point(21, 84);
            OneOpenPositionSymbolLabel.Name = "OneOpenPositionSymbolLabel";
            OneOpenPositionSymbolLabel.Size = new Size(235, 20);
            OneOpenPositionSymbolLabel.TabIndex = 33;
            OneOpenPositionSymbolLabel.Text = "Only One Open Position Per Symbol:";
            OneOpenPositionSymbolLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // OptionsTabControl
            //
            OptionsTabControl.Controls.Add(SymbolsTab);
            OptionsTabControl.Controls.Add(SLTPTab);
            OptionsTabControl.Controls.Add(EntryTab);
            OptionsTabControl.Location = new Point(23, 63);
            OptionsTabControl.Name = "OptionsTabControl";
            OptionsTabControl.SelectedIndex = 0;
            OptionsTabControl.Size = new Size(570, 401);
            OptionsTabControl.TabIndex = 35;
            OptionsTabControl.UseSelectable = true;
            // 
            // SymbolsTab
            // 
            SymbolsTab.Controls.Add(SymbolsGrid);
            SymbolsTab.Controls.Add(DeleteSymbolsButton);
            SymbolsTab.Controls.Add(AddSymbolTextBox);
            SymbolsTab.Controls.Add(AddButton);
            SymbolsTab.Controls.Add(MoveUpButton);
            SymbolsTab.Controls.Add(MoveDownButton);
            SymbolsTab.HorizontalScrollbarBarColor = true;
            SymbolsTab.HorizontalScrollbarHighlightOnWheel = false;
            SymbolsTab.HorizontalScrollbarSize = 10;
            SymbolsTab.Location = new Point(4, 38);
            SymbolsTab.Name = "SymbolsTab";
            SymbolsTab.Size = new Size(562, 359);
            SymbolsTab.TabIndex = 0;
            SymbolsTab.Text = "Symbols";
            SymbolsTab.VerticalScrollbarBarColor = true;
            SymbolsTab.VerticalScrollbarHighlightOnWheel = false;
            SymbolsTab.VerticalScrollbarSize = 10;
            // 
            // SLTPTab
            // 
            SLTPTab.Controls.Add(ATRMultiplierSLNumber);
            SLTPTab.Controls.Add(ATRMultiplierTPNumber);
            SLTPTab.Controls.Add(PipsTPNumber);
            SLTPTab.Controls.Add(ATRPeriodsTPNumber);
            SLTPTab.Controls.Add(ATRPeriodsSLNumber);
            SLTPTab.Controls.Add(PipsSLNumber);
            SLTPTab.Controls.Add(SLLabel);
            SLTPTab.Controls.Add(TPLabel);
            SLTPTab.Controls.Add(PipsAmountLabel);
            SLTPTab.Controls.Add(FixedPipsBasedLabel);
            SLTPTab.Controls.Add(FixedPipsTPComboBox);
            SLTPTab.Controls.Add(ATRPeriodsLabel);
            SLTPTab.Controls.Add(FixedPipsSLComboBox);
            SLTPTab.Controls.Add(ATRBasedLabel);
            SLTPTab.Controls.Add(ATRTPComboBox);
            SLTPTab.Controls.Add(ATRSLComboBox);
            SLTPTab.Controls.Add(ATRMultiplierLabel);
            SLTPTab.HorizontalScrollbarBarColor = true;
            SLTPTab.HorizontalScrollbarHighlightOnWheel = false;
            SLTPTab.HorizontalScrollbarSize = 10;
            SLTPTab.Location = new Point(4, 38);
            SLTPTab.Name = "SLTPTab";
            SLTPTab.Size = new Size(562, 359);
            SLTPTab.TabIndex = 1;
            SLTPTab.Text = "SL And TP";
            SLTPTab.VerticalScrollbarBarColor = true;
            SLTPTab.VerticalScrollbarHighlightOnWheel = false;
            SLTPTab.VerticalScrollbarSize = 10;
            // 
            // EntryTab
            // 
            EntryTab.Controls.Add(OneOpenPositionCurrencyComboBox);
            EntryTab.Controls.Add(OneOpenPositionCurrencyLabel);
            EntryTab.Controls.Add(OneOpenPositionSymbolComboBox);
            EntryTab.Controls.Add(OneOpenPositionSymbolLabel);
            EntryTab.Controls.Add(OnCandleCloseComboBox);
            EntryTab.Controls.Add(OnCandleCloseLabel);
            EntryTab.HorizontalScrollbarBarColor = true;
            EntryTab.HorizontalScrollbarHighlightOnWheel = false;
            EntryTab.HorizontalScrollbarSize = 10;
            EntryTab.Location = new Point(4, 38);
            EntryTab.Name = "EntryTab";
            EntryTab.Size = new Size(562, 359);
            EntryTab.TabIndex = 2;
            EntryTab.Text = "Entry";
            EntryTab.VerticalScrollbarBarColor = true;
            EntryTab.VerticalScrollbarHighlightOnWheel = false;
            EntryTab.VerticalScrollbarSize = 10;
            // 
            // NumberColumn
            // 
            NumberColumn.HeaderText = "Number";
            NumberColumn.Name = "NumberColumn";
            NumberColumn.ReadOnly = true;
            NumberColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // SymbolColumn
            // 
            SymbolColumn.HeaderText = "Symbol";
            SymbolColumn.Name = "SymbolColumn";
            SymbolColumn.ReadOnly = true;
            SymbolColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // PipsSLNumber
            // 
            PipsSLNumber.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            PipsSLNumber.DecimalPlaces = 1;
            PipsSLNumber.Location = new Point(181, 100);
            PipsSLNumber.Name = "PipsSLNumber";
            PipsSLNumber.Size = new Size(121, 22);
            PipsSLNumber.TabIndex = 27;
            PipsSLNumber.ThousandsSeparator = true;
            PipsSLNumber.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // ATRPeriodsSLNumber
            // 
            ATRPeriodsSLNumber.Location = new Point(181, 190);
            ATRPeriodsSLNumber.Name = "ATRPeriodsSLNumber";
            ATRPeriodsSLNumber.Size = new Size(121, 22);
            ATRPeriodsSLNumber.TabIndex = 28;
            ATRPeriodsSLNumber.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // ATRPeriodsTPNumber
            // 
            ATRPeriodsTPNumber.Location = new Point(358, 190);
            ATRPeriodsTPNumber.Name = "ATRPeriodsTPNumber";
            ATRPeriodsTPNumber.Size = new Size(121, 22);
            ATRPeriodsTPNumber.TabIndex = 29;
            ATRPeriodsTPNumber.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // PipsTPNumber
            // 
            PipsTPNumber.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            PipsTPNumber.DecimalPlaces = 1;
            PipsTPNumber.Location = new Point(358, 100);
            PipsTPNumber.Name = "PipsTPNumber";
            PipsTPNumber.Size = new Size(121, 22);
            PipsTPNumber.TabIndex = 30;
            PipsTPNumber.ThousandsSeparator = true;
            PipsTPNumber.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // ATRMultiplierTPNumber
            // 
            ATRMultiplierTPNumber.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            ATRMultiplierTPNumber.DecimalPlaces = 1;
            ATRMultiplierTPNumber.Location = new Point(358, 235);
            ATRMultiplierTPNumber.Name = "ATRMultiplierTPNumber";
            ATRMultiplierTPNumber.Size = new Size(121, 22);
            ATRMultiplierTPNumber.TabIndex = 31;
            ATRMultiplierTPNumber.ThousandsSeparator = true;
            ATRMultiplierTPNumber.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // ATRMultiplierSLNumber
            // 
            ATRMultiplierSLNumber.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            ATRMultiplierSLNumber.DecimalPlaces = 1;
            ATRMultiplierSLNumber.Location = new Point(181, 235);
            ATRMultiplierSLNumber.Name = "ATRMultiplierSLNumber";
            ATRMultiplierSLNumber.Size = new Size(121, 22);
            ATRMultiplierSLNumber.TabIndex = 32;
            ATRMultiplierSLNumber.ThousandsSeparator = true;
            ATRMultiplierSLNumber.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // OneOpenPositionCurrencyComboBox
            // 
            OneOpenPositionCurrencyComboBox.FormattingEnabled = true;
            OneOpenPositionCurrencyComboBox.ItemHeight = 24;
            OneOpenPositionCurrencyComboBox.Items.AddRange(new object[] {
            "No",
            "Yes"});
            OneOpenPositionCurrencyComboBox.Location = new Point(285, 125);
            OneOpenPositionCurrencyComboBox.Name = "OneOpenPositionCurrencyComboBox";
            OneOpenPositionCurrencyComboBox.Size = new Size(121, 30);
            OneOpenPositionCurrencyComboBox.TabIndex = 36;
            OneOpenPositionCurrencyComboBox.UseSelectable = true;

            if (OneOpenPositionCurrency)
                OneOpenPositionCurrencyComboBox.SelectedIndex = 1;
            else
                OneOpenPositionCurrencyComboBox.SelectedIndex = 0;

            // 
            // OneOpenPositionCurrencyLabel
            // 
            OneOpenPositionCurrencyLabel.AutoSize = true;
            OneOpenPositionCurrencyLabel.Location = new Point(21, 135);
            OneOpenPositionCurrencyLabel.Name = "OneOpenPositionCurrencyLabel";
            OneOpenPositionCurrencyLabel.Size = new Size(247, 20);
            OneOpenPositionCurrencyLabel.TabIndex = 35;
            OneOpenPositionCurrencyLabel.Text = "Only One Open Position Per Currency:";
            OneOpenPositionCurrencyLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // OptionsForm
            // 
            OptionsForm.AutoScaleDimensions = new SizeF(8F, 16F);
            OptionsForm.AutoScaleMode = AutoScaleMode.Font;
            OptionsForm.ClientSize = new Size(616, 539);
            OptionsForm.Controls.Add(OptionsTabControl);
            OptionsForm.Controls.Add(OptionsProgressBar);
            OptionsForm.Controls.Add(SaveButton);
            OptionsForm.MaximizeBox = false;
            OptionsForm.Name = "OptionsForm";
            OptionsForm.Resizable = false;
            OptionsForm.Text = "Options";
            OptionsForm.ShowIcon = false;
            OptionsForm.Theme = MetroFramework.MetroThemeStyle.Dark;
            OptionsForm.Style = MetroFramework.MetroColorStyle.Blue;
            OptionsForm.Load += new EventHandler(OptionsFormLoad);
            OptionsForm.Shown += new EventHandler(OptionsFormShown);
            OptionsForm.FormClosed += new FormClosedEventHandler(OptionsFormClosed);
            ((System.ComponentModel.ISupportInitialize)(SymbolsGrid)).EndInit();
            OptionsTabControl.ResumeLayout(false);
            SymbolsTab.ResumeLayout(false);
            SLTPTab.ResumeLayout(false);
            SLTPTab.PerformLayout();
            EntryTab.ResumeLayout(false);
            EntryTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(PipsSLNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(ATRPeriodsSLNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(ATRPeriodsTPNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(PipsTPNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(ATRMultiplierTPNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(ATRMultiplierSLNumber)).EndInit();
            OptionsForm.ResumeLayout(false);




            SymbolsGrid.Rows.Clear();
            int symNum = 0;
            foreach (string sym in SymbolsList)
            {
                symNum += 1;
                SymbolsGrid.Rows.Add(symNum, sym);
            }

            SymbolsGrid.PerformLayout();

            Application.Run(OptionsForm);
        }

        // Events

        private void PositionsOnClosed(PositionClosedEventArgs args)
        {
            var position = args.Position;
            if (position.Label == label && (OneOpenPositionCurrency || OneOpenPositionSymbol))
            {
                bool sameSymbolPosition = false;

                bool sameFirstCurrencyPosition = false;
                bool sameSecondCurrencyPosition = false;

                string positionFirstCurrency = position.SymbolCode.Substring(0, 3);
                string positionSecondCurrency = position.SymbolCode.Substring(3, 3);



                foreach (Position pos in Positions)
                {
                    if (pos.Label == label)
                    {
                        if (pos.SymbolCode == position.SymbolCode)
                            sameSymbolPosition = true;

                        string posFirstCurrency = pos.SymbolCode.Substring(0, 3);
                        string posSecondCurrency = pos.SymbolCode.Substring(3, 3);

                        if (posFirstCurrency == positionFirstCurrency || posSecondCurrency == positionFirstCurrency)
                            sameFirstCurrencyPosition = true;
                        if (posFirstCurrency == positionSecondCurrency || posSecondCurrency == positionSecondCurrency)
                            sameSecondCurrencyPosition = true;
                    }
                }


                bool enableButtonsSymbolBased = false;
                if (OneOpenPositionSymbol)
                {
                    if (OpenPositionsSymbols.Contains(position.SymbolCode) && !sameSymbolPosition)
                    {
                        OpenPositionsSymbols.Remove(position.SymbolCode);

                        if (SymbolsComboBox.Text == position.SymbolCode)
                        {
                            enableButtonsSymbolBased = true;
                        }
                    }
                }
                else
                    enableButtonsSymbolBased = true;



                bool enableButtonsCurrencyBased = false;
                if (OneOpenPositionCurrency)
                {
                    if (!sameFirstCurrencyPosition && OpenPositionsCurrencies.Contains(positionFirstCurrency))
                        OpenPositionsCurrencies.Remove(positionFirstCurrency);
                    if (!sameSecondCurrencyPosition && OpenPositionsCurrencies.Contains(positionSecondCurrency))
                        OpenPositionsCurrencies.Remove(positionSecondCurrency);

                    if (!IsCurrenciesHaveOpenPosition(SymbolsComboBox.Text))
                        enableButtonsCurrencyBased = true;
                }
                else
                    enableButtonsCurrencyBased = true;


                if (enableButtonsSymbolBased && enableButtonsCurrencyBased)
                {
                    SellButton.Enabled = true;
                    BuyButton.Enabled = true;
                }

            }
        }


        private void SymbolsComboBoxSelectionChanged(object sender, EventArgs e)
        {
            if (OneOpenPositionSymbol)
            {
                if (OpenPositionsSymbols.Contains(SymbolsComboBox.Text))
                {
                    SellButton.Enabled = false;
                    BuyButton.Enabled = false;
                }
                else
                {
                    SellButton.Enabled = true;
                    BuyButton.Enabled = true;
                }
            }

            if (OneOpenPositionCurrency)
            {
                if (IsCurrenciesHaveOpenPosition(SymbolsComboBox.Text))
                {
                    SellButton.Enabled = false;
                    BuyButton.Enabled = false;
                }
                else
                {
                    SellButton.Enabled = true;
                    BuyButton.Enabled = true;
                }
            }
        }

        private void MainFormShown(object sender, EventArgs e)
        {
            MainForm.TopMost = true;
        }

        private void MainFormClosed(object sender, FormClosedEventArgs e)
        {
            Stop();
        }

        private void OptionsFormLoad(object sender, EventArgs e)
        {
            SellButton.Enabled = true;
            BuyButton.Enabled = true;
            MainForm.Enabled = false;
        }

        private void OptionsFormShown(object sender, EventArgs e)
        {
            MainForm.TopMost = false;
            OptionsForm.TopMost = true;
        }

        private void OptionsFormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.Enabled = true;
            MainForm.TopMost = true;

            if (OneOpenPositionSymbol && OpenPositionsSymbols.Contains(SymbolsComboBox.Text))
            {
                SellButton.Enabled = false;
                BuyButton.Enabled = false;
            }

            if (OneOpenPositionCurrency && IsCurrenciesHaveOpenPosition(SymbolsComboBox.Text))
            {
                SellButton.Enabled = false;
                BuyButton.Enabled = false;
            }
        }

        private void SaveOptions(object sender, EventArgs e)
        {
            if (FixedPipsSLComboBox.SelectedIndex == 1)
                FixedSL = true;
            else
                FixedSL = false;

            if (FixedPipsTPComboBox.SelectedIndex == 1)
                FixedTP = true;
            else
                FixedTP = false;

            if (ATRSLComboBox.SelectedIndex == 1)
                ATRSL = true;
            else
                ATRSL = false;

            if (ATRTPComboBox.SelectedIndex == 1)
                ATRTP = true;
            else
                ATRTP = false;

            if (OnCandleCloseComboBox.SelectedIndex == 1)
                EntryOnCandleClose = true;
            else
                EntryOnCandleClose = false;

            if (OneOpenPositionSymbolComboBox.SelectedIndex == 1)
                OneOpenPositionSymbol = true;
            else
                OneOpenPositionSymbol = false;

            if (OneOpenPositionCurrencyComboBox.SelectedIndex == 1)
                OneOpenPositionCurrency = true;
            else
                OneOpenPositionCurrency = false;

            PipsSL = decimal.ToDouble(PipsSLNumber.Value);
            PipsTP = decimal.ToDouble(PipsTPNumber.Value);
            ATRPeriodsSL = decimal.ToInt32(ATRPeriodsSLNumber.Value);
            ATRPeriodsTP = decimal.ToInt32(ATRPeriodsTPNumber.Value);
            ATRMultiplierSL = decimal.ToDouble(ATRMultiplierSLNumber.Value);
            ATRMultiplierTP = decimal.ToDouble(ATRMultiplierTPNumber.Value);




            SymbolsList.Clear();
            foreach (DataGridViewRow row in SymbolsGrid.Rows)
            {
                string sym = row.Cells[1].Value.ToString();
                SymbolsList.Add(sym);
            }

            FillSymbolsComboBox();

            if (ATRSL || ATRTP)
            {
                Task getSeries = Task.Factory.StartNew(() =>
                {
                    OptionsForm.Enabled = false;

                    RefreshData();
                    OptionsProgressBar.Visible = true;
                    OptionsProgressBar.Maximum = SymbolsList.Count;
                    OptionsProgressBar.Value = 0;

                    foreach (string sym in SymbolsList)
                    {
                        MarketSeries symSeries = MarketData.GetSeries(sym, MarketSeries.TimeFrame);
                        OptionsProgressBar.Value += 1;
                    }

                    OptionsProgressBar.Visible = false;
                    OptionsForm.Enabled = true;
                    OptionsForm.Close();
                });
            }
            else
                OptionsForm.Close();

        }


        private void ExecuteBuyOrder(object sender, EventArgs e)
        {
            double? sl = GetSL();
            if (!sl.HasValue)
            {
                Task showOptionsForm = Task.Factory.StartNew(() => { OptionsFormInitializer(); });
                return;
            }

            double slippagePips = decimal.ToDouble(SlippageNumber.Value);
            long volume = PositionVolume(sl.Value);
            Symbol sym = GetSymbol();


            if (OnCandleClose(TradeType.Buy))
            {
                SellButton.Enabled = false;
                BuyButton.Enabled = false;

                ExecuteMarketOrder(TradeType.Buy, sym, volume, label, sl, GetTP(), slippagePips);

                if (!OneOpenPositionSymbol)
                {
                    SellButton.Enabled = true;
                    BuyButton.Enabled = true;
                }

                if (!OpenPositionsSymbols.Contains(sym.Code))
                    OpenPositionsSymbols.Add(sym.Code);

                AddOpenPositionCurrencies(sym.Code);
            }
        }

        private void ExecuteSellOrder(object sender, EventArgs e)
        {
            double? sl = GetSL();
            if (!sl.HasValue)
            {
                Task showOptionsForm = Task.Factory.StartNew(() => { OptionsFormInitializer(); });
                return;
            }

            double slippagePips = decimal.ToDouble(SlippageNumber.Value);
            long volume = PositionVolume(sl.Value);
            Symbol sym = GetSymbol();




            if (OnCandleClose(TradeType.Sell))
            {
                SellButton.Enabled = false;
                BuyButton.Enabled = false;

                ExecuteMarketOrder(TradeType.Sell, sym, volume, label, sl, GetTP(), slippagePips);

                if (!OneOpenPositionSymbol)
                {
                    SellButton.Enabled = true;
                    BuyButton.Enabled = true;
                }

                if (!OpenPositionsSymbols.Contains(sym.Code))
                    OpenPositionsSymbols.Add(sym.Code);

                AddOpenPositionCurrencies(sym.Code);
            }
        }

        private void ShowOptionsForm(object sender, EventArgs e)
        {
            Task showOptionsForm = Task.Factory.StartNew(() => { OptionsFormInitializer(); });
        }


        private void AddSymbol(object sender, EventArgs e)
        {
            if (AddSymbolTextBox.Text.Length == 6 && AddSymbolTextBox.Text == AddSymbolTextBox.Text.ToUpper() && !SymbolGridContains(AddSymbolTextBox.Text))
            {
                SymbolsGrid.Rows.Add(SymbolsGrid.Rows.Count + 1, AddSymbolTextBox.Text);
                SymbolsGrid.ClearSelection();
                SymbolsGrid.Rows[SymbolsGrid.Rows.Count - 1].Selected = true;
                SymbolsGrid.FirstDisplayedScrollingRowIndex = SymbolsGrid.Rows.Count - 1;
                SymbolsGrid.PerformLayout();
            }
            else if (AddSymbolTextBox.Text != AddSymbolTextBox.Text.ToUpper())
                Print("Please enter symbol code only in upper cases");
            else if (SymbolGridContains(AddSymbolTextBox.Text))
                Print("The entered symbol code already exists.");
            else if (AddSymbolTextBox.Text.Length >= 6)
                Print("The entered symbol code is not in correct format.");
        }

        private void DeleteSymbols(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in SymbolsGrid.SelectedRows)
            {
                SymbolsGrid.Rows.Remove(row);
            }

            Dictionary<int, string> tempGridData = new Dictionary<int, string>();

            foreach (DataGridViewRow row in SymbolsGrid.Rows)
            {
                int num = int.Parse(row.Cells[0].Value.ToString());
                string sym = row.Cells[1].Value.ToString();
                tempGridData.Add(num, sym);
            }

            SymbolsGrid.Rows.Clear();
            int rowNum = 1;
            foreach (KeyValuePair<int, string> item in tempGridData)
            {
                SymbolsGrid.Rows.Add(rowNum, item.Value);
                rowNum += 1;
            }

            SymbolsGrid.PerformLayout();

        }

        private void MoveUpSymbols(object sender, EventArgs e)
        {
            if (SymbolsGrid.SelectedRows.Count == 1)
            {
                try
                {
                    DataGridViewRow selectedRow = SymbolsGrid.SelectedRows[0];
                    DataGridViewRow previousRow = SymbolsGrid.Rows[selectedRow.Index - 1];
                    string previousRowSymbol = previousRow.Cells[1].Value.ToString();
                    previousRow.Cells[1].Value = selectedRow.Cells[1].Value;
                    selectedRow.Cells[1].Value = previousRowSymbol;

                    selectedRow.Selected = false;
                    previousRow.Selected = true;

                    SymbolsGrid.FirstDisplayedScrollingRowIndex = previousRow.Index;
                    SymbolsGrid.PerformLayout();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Print("Symbol was out of range.");
                }
            }
            else if (SymbolsGrid.SelectedRows.Count >= 1)
                Print("Please select just one row.");
        }

        private void MoveDownSymbols(object sender, EventArgs e)
        {
            if (SymbolsGrid.SelectedRows.Count == 1)
            {
                try
                {
                    DataGridViewRow selectedRow = SymbolsGrid.SelectedRows[0];
                    DataGridViewRow nextRow = SymbolsGrid.Rows[selectedRow.Index + 1];
                    string nextRowRowSymbol = nextRow.Cells[1].Value.ToString();
                    nextRow.Cells[1].Value = selectedRow.Cells[1].Value;
                    selectedRow.Cells[1].Value = nextRowRowSymbol;

                    selectedRow.Selected = false;
                    nextRow.Selected = true;

                    SymbolsGrid.FirstDisplayedScrollingRowIndex = nextRow.Index;
                    SymbolsGrid.PerformLayout();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Print("Symbol was out of range.");
                }
            }
            else if (SymbolsGrid.SelectedRows.Count >= 1)
                Print("Please select just one row.");
        }

        // Extra Methods
        private double? GetSL()
        {
            if (FixedSL)
                return PipsSL;
            else if (ATRSL)
                return ATRValue("SL");
            return null;
        }

        private double? GetTP()
        {
            if (FixedTP)
                return PipsTP;
            else if (ATRTP)
                return ATRValue("TP");
            return null;
        }

        private Symbol GetSymbol()
        {
            string symCode = SymbolsList[SymbolsComboBox.SelectedIndex];
            Symbol sym = MarketData.GetSymbol(symCode);
            return sym;
        }

        private double ATRValue(string type)
        {
            int periods;
            double multiplier;
            if (type == "SL")
            {
                periods = ATRPeriodsSL;
                multiplier = ATRMultiplierSL;
            }
            else
            {
                periods = ATRPeriodsTP;
                multiplier = ATRMultiplierTP;
            }

            MarketSeries symSeries = MarketData.GetSeries(GetSymbol().Code, MarketSeries.TimeFrame);
            AverageTrueRange atr = Indicators.AverageTrueRange(symSeries, periods, MovingAverageType.Exponential);

            double atrPips = Math.Round((atr.Result.LastValue * Math.Pow(10, GetSymbol().Digits - 1)) * multiplier, 1);
            return atrPips;
        }

        private long PositionVolume(double stopLossInPips)
        {
            double riskPercent = decimal.ToDouble(PercentNumber.Value);

            double costPerPip = (double)((int)(GetSymbol().PipValue * 10000000)) / 100;
            double positionSizeForRisk = Math.Round((Account.Balance * riskPercent / 100) / (stopLossInPips * costPerPip), 2);

            if (positionSizeForRisk < 0.01)
                positionSizeForRisk = 0.01;
            return Symbol.QuantityToVolume(positionSizeForRisk);

        }

        private void FillSymbolsComboBox()
        {
            if (SymbolsComboBox.Items.Count > 0)
                SymbolsComboBox.Items.Clear();

            foreach (string sym in SymbolsList)
            {
                SymbolsComboBox.Items.Add(sym);
            }

            if (SymbolsList.Contains(Symbol.Code))
                SymbolsComboBox.SelectedItem = Symbol.Code;
            else
                SymbolsComboBox.SelectedIndex = 0;
        }

        private bool SymbolGridContains(string symbolCode)
        {
            foreach (DataGridViewRow row in SymbolsGrid.Rows)
            {
                string rowSymbol = row.Cells[1].Value.ToString();
                if (rowSymbol == symbolCode)
                    return true;
            }

            return false;
        }


        private bool OnCandleClose(TradeType tradeType)
        {
            if (EntryOnCandleClose)
            {
                Symbol sym = GetSymbol();
                MarketSeries symSeries = MarketData.GetSeries(sym.Code, MarketSeries.TimeFrame);

                if (tradeType == TradeType.Buy)
                {
                    if (sym.Bid <= symSeries.Close.Last(1) || sym.Ask <= symSeries.Close.Last(1))
                        return true;
                    else
                        return false;
                }
                else
                {
                    if (sym.Bid >= symSeries.Close.Last(1) || sym.Ask >= symSeries.Close.Last(1))
                        return true;
                    else
                        return false;
                }
            }
            else
                return true;
        }

        private void AddOpenPositionCurrencies(string symbolCode)
        {
            string firstCurrency = symbolCode.Substring(0, 3);
            string secondCurrency = symbolCode.Substring(3, 3);
            if (!OpenPositionsCurrencies.Contains(firstCurrency))
                OpenPositionsCurrencies.Add(firstCurrency);
            if (!OpenPositionsCurrencies.Contains(secondCurrency))
                OpenPositionsCurrencies.Add(secondCurrency);
        }

        private bool IsCurrenciesHaveOpenPosition(string symbolCode)
        {
            string firstCurrency = symbolCode.Substring(0, 3);
            string secondCurrency = symbolCode.Substring(3, 3);

            if (OpenPositionsCurrencies.Contains(firstCurrency))
                return true;

            if (OpenPositionsCurrencies.Contains(secondCurrency))
                return true;

            return false;
        }
    }
}
