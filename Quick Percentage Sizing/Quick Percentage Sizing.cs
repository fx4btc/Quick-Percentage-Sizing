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
        private MetroForm MainForm;
        private MetroForm OptionsForm;
        private MetroButton BuyButton;
        private MetroButton SellButton;
        private MetroButton OptionsButton;
        private MetroButton AddButton;
        private MetroButton MoveUpButton;
        private MetroButton MoveDownButton;
        private MetroButton DeleteSymbolsButton;
        private MetroButton SaveButton;
        private MetroLabel PercentLabel;
        private MetroLabel SymbolsLabel;
        private MetroLabel SLAndTPLabel;
        private MetroLabel SLLabel;
        private MetroLabel TPLabel;
        private MetroLabel FixedPipsBasedLabel;
        private MetroLabel PipsAmountLabel;
        private MetroLabel ATRBasedLabel;
        private MetroLabel ATRMultiplierLabel;
        private MetroLabel ATRPeriodsLabel;
        private NumericUpDown PercentNumber;
        private MetroComboBox SymbolsComboBox;
        private MetroComboBox FixedPipsSLComboBox;
        private MetroComboBox FixedPipsTPComboBox;
        private MetroComboBox ATRSLComboBox;
        private MetroComboBox ATRTPComboBox;
        private MetroComboBox OnCandleCloseComboBox;
        private MetroTextBox AddSymbolTextBox;
        private MetroTextBox PipsSLTextBox;
        private MetroTextBox PipsTPTextBox;
        private MetroTextBox ATRMultiplierSLTextBox;
        private MetroTextBox ATRMultiplierTPTextBox;
        private MetroTextBox ATRPeriodsSLTextBox;
        private MetroTextBox ATRPeriodsTPTextBox;
        private MetroGrid SymbolsGrid;
        private MetroProgressBar OptionsProgressBar;
        private MetroLabel RiskLabel;
        private MetroLabel SlippageLabel;
        private NumericUpDown SlippageNumber;
        private MetroLabel SlippagePipsLabel;
        private MetroLabel OnCandleCloseLabel;
        private MetroLabel EntryParametersLabel;
        private MetroLabel OneOpenPositionLabel;
        private MetroComboBox OneOpenPositionComboBox;




        // Collections
        List<string> SymbolsList = new List<string>();
        List<string> OpenPositionsSymbols = new List<string>();

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
        bool OneOpenPosition = false;


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
            // Form
            OptionsForm = new MetroForm();
            OptionsForm.Name = "OptionsForm";
            OptionsForm.Text = "Options";
            OptionsForm.Size = new Size(603, 739);
            OptionsForm.StartPosition = FormStartPosition.CenterScreen;
            OptionsForm.Resizable = false;
            OptionsForm.Theme = MetroFramework.MetroThemeStyle.Dark;
            OptionsForm.Style = MetroFramework.MetroColorStyle.Blue;
            OptionsForm.MaximizeBox = false;
            OptionsForm.ShowIcon = false;
            OptionsForm.Load += new EventHandler(OptionsFormLoad);
            OptionsForm.Shown += new EventHandler(OptionsFormShown);
            OptionsForm.FormClosed += new FormClosedEventHandler(OptionsFormClosed);

            // Labels
            SymbolsLabel = new MetroLabel();
            SymbolsLabel.Name = "SymbolsLabel";
            SymbolsLabel.Text = "Symbols";
            SymbolsLabel.Size = new Size(68, 20);
            SymbolsLabel.Location = new Point(23, 73);
            SymbolsLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            SymbolsLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;

            SLAndTPLabel = new MetroLabel();
            SLAndTPLabel.Name = "SLAndTPLabel";
            SLAndTPLabel.Text = "Stop Loss And Take Profit";
            SLAndTPLabel.Size = new Size(189, 20);
            SLAndTPLabel.Location = new Point(20, 290);
            SLAndTPLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            SLAndTPLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;

            SLLabel = new MetroLabel();
            SLLabel.Name = "SLLabel";
            SLLabel.Text = "Stop Loss";
            SLLabel.Size = new Size(76, 20);
            SLLabel.Location = new Point(228, 309);
            SLLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            SLLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;

            TPLabel = new MetroLabel();
            TPLabel.Name = "TPLabel";
            TPLabel.Text = "Take Profit";
            TPLabel.Size = new Size(85, 20);
            TPLabel.Location = new Point(399, 309);
            TPLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            TPLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;

            FixedPipsBasedLabel = new MetroLabel();
            FixedPipsBasedLabel.Name = "FixedPipsBasedLabel";
            FixedPipsBasedLabel.Text = "Fixed Pips Based?";
            FixedPipsBasedLabel.Size = new Size(117, 20);
            FixedPipsBasedLabel.Location = new Point(42, 343);
            FixedPipsBasedLabel.Theme = MetroFramework.MetroThemeStyle.Dark;

            PipsAmountLabel = new MetroLabel();
            PipsAmountLabel.Name = "PipsAmountLabel";
            PipsAmountLabel.Text = "Pips Amount";
            PipsAmountLabel.Size = new Size(98, 20);
            PipsAmountLabel.Location = new Point(43, 389);
            PipsAmountLabel.Theme = MetroFramework.MetroThemeStyle.Dark;

            ATRBasedLabel = new MetroLabel();
            ATRBasedLabel.Name = "ATRBasedLabel";
            ATRBasedLabel.Text = "ATR Based?";
            ATRBasedLabel.Size = new Size(79, 20);
            ATRBasedLabel.Location = new Point(42, 434);
            ATRBasedLabel.Theme = MetroFramework.MetroThemeStyle.Dark;

            ATRMultiplierLabel = new MetroLabel();
            ATRMultiplierLabel.Name = "ATRMultiplierLabel";
            ATRMultiplierLabel.Text = "ATR Multiplier : ";
            ATRMultiplierLabel.Size = new Size(104, 20);
            ATRMultiplierLabel.Location = new Point(42, 524);
            ATRMultiplierLabel.Theme = MetroFramework.MetroThemeStyle.Dark;

            ATRPeriodsLabel = new MetroLabel();
            ATRPeriodsLabel.Name = "ATRPeriodsLabel";
            ATRPeriodsLabel.Text = "ATR Periods : ";
            ATRPeriodsLabel.Size = new Size(92, 20);
            ATRPeriodsLabel.Location = new Point(43, 479);
            ATRPeriodsLabel.Theme = MetroFramework.MetroThemeStyle.Dark;

            OnCandleCloseLabel = new MetroLabel();
            OnCandleCloseLabel.Name = "OnCandleCloseLabel";
            OnCandleCloseLabel.Text = "On Candle Close:";
            OnCandleCloseLabel.Size = new Size(116, 20);
            OnCandleCloseLabel.Location = new Point(42, 605);
            OnCandleCloseLabel.Theme = MetroFramework.MetroThemeStyle.Dark;

            EntryParametersLabel = new MetroLabel();
            EntryParametersLabel.Name = "EntryParametersLabel";
            EntryParametersLabel.Text = "Entry Parameters";
            EntryParametersLabel.Size = new Size(131, 20);
            EntryParametersLabel.Location = new Point(19, 568);
            EntryParametersLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            EntryParametersLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;

            OneOpenPositionLabel = new MetroLabel();
            OneOpenPositionLabel.Name = "OneOpenPositionLabel";
            OneOpenPositionLabel.Text = "One Open Position Only:";
            OneOpenPositionLabel.Size = new Size(162, 20);
            OneOpenPositionLabel.Location = new Point(42, 656);
            OneOpenPositionLabel.Theme = MetroFramework.MetroThemeStyle.Dark;

            // Buttons
            AddButton = new MetroButton();
            AddButton.Name = "AddButton";
            AddButton.Text = "Add";
            AddButton.Size = new Size(75, 23);
            AddButton.Location = new Point(505, 106);
            AddButton.TextAlign = ContentAlignment.MiddleCenter;
            AddButton.FontWeight = MetroFramework.MetroButtonWeight.Bold;
            AddButton.Click += new EventHandler(AddSymbol);

            MoveUpButton = new MetroButton();
            MoveUpButton.Name = "MoveUpButton";
            MoveUpButton.Text = "Move Up";
            MoveUpButton.Size = new Size(95, 23);
            MoveUpButton.Location = new Point(330, 147);
            MoveUpButton.TextAlign = ContentAlignment.MiddleCenter;
            MoveUpButton.FontWeight = MetroFramework.MetroButtonWeight.Bold;
            MoveUpButton.Click += new EventHandler(MoveUpSymbols);

            MoveDownButton = new MetroButton();
            MoveDownButton.Name = "MoveDownButton";
            MoveDownButton.Text = "Move Down";
            MoveDownButton.Size = new Size(95, 23);
            MoveDownButton.Location = new Point(431, 147);
            MoveDownButton.TextAlign = ContentAlignment.MiddleCenter;
            MoveDownButton.FontWeight = MetroFramework.MetroButtonWeight.Bold;
            MoveDownButton.Click += new EventHandler(MoveDownSymbols);

            DeleteSymbolsButton = new MetroButton();
            DeleteSymbolsButton.Name = "DeleteSymbolsButton";
            DeleteSymbolsButton.Text = "Delete Selected Symbols";
            DeleteSymbolsButton.Size = new Size(195, 23);
            DeleteSymbolsButton.Location = new Point(330, 230);
            DeleteSymbolsButton.TextAlign = ContentAlignment.MiddleCenter;
            DeleteSymbolsButton.FontWeight = MetroFramework.MetroButtonWeight.Bold;
            DeleteSymbolsButton.Click += new EventHandler(DeleteSymbols);

            SaveButton = new MetroButton();
            SaveButton.Name = "SaveButton";
            SaveButton.Text = "Save";
            SaveButton.Size = new Size(75, 23);
            SaveButton.Location = new Point(275, 697);
            SaveButton.TextAlign = ContentAlignment.MiddleCenter;
            SaveButton.FontWeight = MetroFramework.MetroButtonWeight.Bold;
            SaveButton.Click += new EventHandler(SaveOptions);

            // Text Boxes
            AddSymbolTextBox = new MetroTextBox();
            AddSymbolTextBox.Name = "AddSymbolTextBox";
            AddSymbolTextBox.Text = "Enter Symbol Code";
            AddSymbolTextBox.Size = new Size(154, 23);
            AddSymbolTextBox.Location = new Point(330, 106);

            PipsSLTextBox = new MetroTextBox();
            PipsSLTextBox.Name = "PipsSLTextBox";
            PipsSLTextBox.Size = new Size(121, 30);
            PipsSLTextBox.Location = new Point(204, 389);
            if (PipsSL != 0)
                PipsSLTextBox.Text = PipsSL.ToString();
            else
                PipsSLTextBox.Text = "5";

            PipsTPTextBox = new MetroTextBox();
            PipsTPTextBox.Name = "PipsTPTextBox";
            PipsTPTextBox.Text = "10";
            PipsTPTextBox.Size = new Size(121, 30);
            PipsTPTextBox.Location = new Point(381, 389);
            if (PipsTP != 0)
                PipsTPTextBox.Text = PipsTP.ToString();
            else
                PipsTPTextBox.Text = "10";

            ATRMultiplierSLTextBox = new MetroTextBox();
            ATRMultiplierSLTextBox.Name = "ATRMultiplierSLTextBox";
            ATRMultiplierSLTextBox.Text = "2";
            ATRMultiplierSLTextBox.Size = new Size(121, 30);
            ATRMultiplierSLTextBox.Location = new Point(204, 524);
            if (ATRMultiplierSL != 0)
                ATRMultiplierSLTextBox.Text = ATRMultiplierSL.ToString();
            else
                ATRMultiplierSLTextBox.Text = "2";

            ATRMultiplierTPTextBox = new MetroTextBox();
            ATRMultiplierTPTextBox.Name = "ATRMultiplierTPTextBox";
            ATRMultiplierTPTextBox.Text = "2";
            ATRMultiplierTPTextBox.Size = new Size(121, 30);
            ATRMultiplierTPTextBox.Location = new Point(381, 524);
            if (ATRMultiplierTP != 0)
                ATRMultiplierTPTextBox.Text = ATRMultiplierTP.ToString();
            else
                ATRMultiplierTPTextBox.Text = "2";

            ATRPeriodsSLTextBox = new MetroTextBox();
            ATRPeriodsSLTextBox.Name = "ATRPeriodsSLTextBox";
            ATRPeriodsSLTextBox.Text = "14";
            ATRPeriodsSLTextBox.Size = new Size(121, 30);
            ATRPeriodsSLTextBox.Location = new Point(204, 479);
            if (ATRPeriodsSL != 0)
                ATRPeriodsSLTextBox.Text = ATRPeriodsSL.ToString();
            else
                ATRPeriodsSLTextBox.Text = "14";

            ATRPeriodsTPTextBox = new MetroTextBox();
            ATRPeriodsTPTextBox.Name = "ATRPeriodsTPTextBox";
            ATRPeriodsTPTextBox.Text = "14";
            ATRPeriodsTPTextBox.Size = new Size(121, 30);
            ATRPeriodsTPTextBox.Location = new Point(381, 479);
            if (ATRPeriodsTP != 0)
                ATRPeriodsTPTextBox.Text = ATRPeriodsTP.ToString();
            else
                ATRPeriodsTPTextBox.Text = "14";

            // ComboBoxes
            FixedPipsSLComboBox = new MetroComboBox();
            FixedPipsSLComboBox.Name = "FixedPipsSLComboBox";
            FixedPipsSLComboBox.Size = new Size(121, 30);
            FixedPipsSLComboBox.Location = new Point(204, 343);
            FixedPipsSLComboBox.Items.Add("No");
            FixedPipsSLComboBox.Items.Add("Yes");
            if (FixedSL)
                FixedPipsSLComboBox.SelectedIndex = 1;
            else
                FixedPipsSLComboBox.SelectedIndex = 0;

            FixedPipsTPComboBox = new MetroComboBox();
            FixedPipsTPComboBox.Name = "FixedPipsTPComboBox";
            FixedPipsTPComboBox.Size = new Size(121, 30);
            FixedPipsTPComboBox.Location = new Point(381, 343);
            FixedPipsTPComboBox.Items.Add("No");
            FixedPipsTPComboBox.Items.Add("Yes");
            if (FixedTP)
                FixedPipsTPComboBox.SelectedIndex = 1;
            else
                FixedPipsTPComboBox.SelectedIndex = 0;

            ATRSLComboBox = new MetroComboBox();
            ATRSLComboBox.Name = "ATRSLComboBox";
            ATRSLComboBox.Size = new Size(121, 30);
            ATRSLComboBox.Location = new Point(204, 434);
            ATRSLComboBox.Items.Add("No");
            ATRSLComboBox.Items.Add("Yes");
            if (ATRSL)
                ATRSLComboBox.SelectedIndex = 1;
            else
                ATRSLComboBox.SelectedIndex = 0;

            ATRTPComboBox = new MetroComboBox();
            ATRTPComboBox.Name = "ATRTPComboBox";
            ATRTPComboBox.Size = new Size(121, 30);
            ATRTPComboBox.Location = new Point(381, 434);
            ATRTPComboBox.Items.Add("No");
            ATRTPComboBox.Items.Add("Yes");
            if (ATRTP)
                ATRTPComboBox.SelectedIndex = 1;
            else
                ATRTPComboBox.SelectedIndex = 0;

            OnCandleCloseComboBox = new MetroComboBox();
            OnCandleCloseComboBox.Name = "OnCandleCloseComboBox";
            OnCandleCloseComboBox.Size = new Size(121, 30);
            OnCandleCloseComboBox.Location = new Point(218, 595);
            OnCandleCloseComboBox.Items.Add("No");
            OnCandleCloseComboBox.Items.Add("Yes");
            if (EntryOnCandleClose)
                OnCandleCloseComboBox.SelectedIndex = 1;
            else
                OnCandleCloseComboBox.SelectedIndex = 0;


            OneOpenPositionComboBox = new MetroComboBox();
            OneOpenPositionComboBox.Name = "OneOpenPositionComboBox";
            OneOpenPositionComboBox.Size = new Size(121, 30);
            OneOpenPositionComboBox.Location = new Point(218, 646);
            OneOpenPositionComboBox.Items.Add("No");
            OneOpenPositionComboBox.Items.Add("Yes");
            if (OneOpenPosition)
                OneOpenPositionComboBox.SelectedIndex = 1;
            else
                OneOpenPositionComboBox.SelectedIndex = 0;

            // Grid
            SymbolsGrid = new MetroGrid();
            SymbolsGrid.Name = "SymbolsGrid";
            SymbolsGrid.Size = new Size(280, 177);
            SymbolsGrid.Location = new Point(23, 106);
            SymbolsGrid.AllowUserToAddRows = false;
            SymbolsGrid.AllowUserToDeleteRows = false;
            SymbolsGrid.AllowUserToOrderColumns = false;
            SymbolsGrid.AllowUserToResizeColumns = false;
            SymbolsGrid.AllowUserToResizeRows = false;
            SymbolsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            SymbolsGrid.ReadOnly = true;

            DataGridViewTextBoxColumn numberColumn = new DataGridViewTextBoxColumn();
            numberColumn.Name = "numberColumn";
            numberColumn.HeaderText = "Number";
            numberColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            SymbolsGrid.Columns.Add(numberColumn);

            DataGridViewTextBoxColumn symbolColumn = new DataGridViewTextBoxColumn();
            symbolColumn.Name = "symbolsColumn";
            symbolColumn.HeaderText = "Symbol";
            symbolColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            SymbolsGrid.Columns.Add(symbolColumn);


            SymbolsGrid.Rows.Clear();
            int symNum = 0;
            foreach (string sym in SymbolsList)
            {
                symNum += 1;
                SymbolsGrid.Rows.Add(symNum, sym);
            }

            SymbolsGrid.PerformLayout();

            // Progress Bar
            OptionsProgressBar = new MetroProgressBar();
            OptionsProgressBar.Name = "OptionsProgressBar";
            OptionsProgressBar.Size = new Size(186, 23);
            OptionsProgressBar.Location = new Point(19, 697);
            OptionsProgressBar.Visible = false;

            // Adding Controls
            OptionsForm.Controls.AddRange(new Control[]
            {
                SymbolsGrid,
                SymbolsLabel,
                SLAndTPLabel,
                SLLabel,
                TPLabel,
                FixedPipsBasedLabel,
                PipsAmountLabel,
                ATRBasedLabel,
                ATRMultiplierLabel,
                ATRPeriodsLabel,
                AddButton,
                MoveUpButton,
                MoveDownButton,
                DeleteSymbolsButton,
                SaveButton,
                AddSymbolTextBox,
                PipsSLTextBox,
                PipsTPTextBox,
                ATRMultiplierSLTextBox,
                ATRMultiplierTPTextBox,
                ATRPeriodsSLTextBox,
                ATRPeriodsTPTextBox,
                FixedPipsSLComboBox,
                FixedPipsTPComboBox,
                ATRSLComboBox,
                ATRTPComboBox,
                EntryParametersLabel,
                OnCandleCloseLabel,
                OnCandleCloseComboBox,
                OneOpenPositionLabel,
                OneOpenPositionComboBox,
                OptionsProgressBar
            });

            Application.Run(OptionsForm);
        }

        // Events

        private void PositionsOnClosed(PositionClosedEventArgs args)
        {
            var position = args.Position;
            if (position.Label == label)
            {
                if (OpenPositionsSymbols.Contains(position.SymbolCode))
                    OpenPositionsSymbols.Remove(position.SymbolCode);

                if (SymbolsComboBox.Text == position.SymbolCode)
                {
                    SellButton.Enabled = true;
                    BuyButton.Enabled = true;
                }
            }
        }


        private void SymbolsComboBoxSelectionChanged(object sender, EventArgs e)
        {
            if (OneOpenPosition)
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

            if (OpenPositionsSymbols.Contains(SymbolsComboBox.Text))
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

            if (OneOpenPositionComboBox.SelectedIndex == 1)
                OneOpenPosition = true;
            else
                OneOpenPosition = false;

            double.TryParse(PipsSLTextBox.Text.ToString(), out PipsSL);
            double.TryParse(PipsTPTextBox.Text.ToString(), out PipsTP);
            double.TryParse(ATRMultiplierSLTextBox.Text.ToString(), out ATRMultiplierSL);
            double.TryParse(ATRMultiplierTPTextBox.Text.ToString(), out ATRMultiplierTP);
            int.TryParse(ATRPeriodsSLTextBox.Text.ToString(), out ATRPeriodsSL);
            int.TryParse(ATRPeriodsTPTextBox.Text.ToString(), out ATRPeriodsTP);


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



            if (OnCandleClose(TradeType.Buy))
            {
                SellButton.Enabled = false;
                BuyButton.Enabled = false;
                
                ExecuteMarketOrder(TradeType.Buy, GetSymbol(), volume, label, sl, GetTP(), slippagePips);

                if (!OneOpenPosition)
                {
                    SellButton.Enabled = true;
                    BuyButton.Enabled = true;
                }
                else if (!OpenPositionsSymbols.Contains(GetSymbol().Code))
                    OpenPositionsSymbols.Add(GetSymbol().Code);
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



            if (OnCandleClose(TradeType.Sell))
            {
                SellButton.Enabled = false;
                BuyButton.Enabled = false;

                ExecuteMarketOrder(TradeType.Sell, GetSymbol(), volume, label, sl, GetTP(), slippagePips);

                if (!OneOpenPosition)
                {
                    SellButton.Enabled = true;
                    BuyButton.Enabled = true;
                }
                else if (!OpenPositionsSymbols.Contains(GetSymbol().Code))
                    OpenPositionsSymbols.Add(GetSymbol().Code);
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


    }
}
