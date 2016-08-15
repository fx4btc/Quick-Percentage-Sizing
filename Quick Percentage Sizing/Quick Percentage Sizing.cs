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
        [Parameter("Default Symbols", DefaultValue = "EURUSD GBPUSD EURJPY USDJPY AUDUSD USDCHF GBPJPY USDCAD EURGBP EURCHF AUDJPY NZDUSD CHFJPY EURAUD CADJPY GBPAUD EURCAD AUDCAD GBPCAD AUDNZD NZDJPY AUDCHF GBPNZD EURNZD CADCHF NZDCAD NZDCHF GBPCHF XAUUSD XAGUSD")]
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
        private NumericUpDown PercentNumber;
        private MetroComboBox SymbolsComboBox;
        private MetroComboBox FixedPipsSLComboBox;
        private MetroComboBox FixedPipsTPComboBox;
        private MetroComboBox ATRSLComboBox;
        private MetroComboBox ATRTPComboBox;
        private MetroTextBox AddSymbolTextBox;
        private MetroTextBox PipsSLTextBox;
        private MetroTextBox PipsTPTextBox;
        private MetroTextBox ATRMultiplierSLTextBox;
        private MetroTextBox ATRMultiplierTPTextBox;
        private MetroGrid SymbolsGrid;





        // Collections
        List<string> SymbolsList = new List<string>();


        // Bot start method
        protected override void OnStart()
        {
            foreach (string sym in defaultSymbols.Split(' '))
            {
                SymbolsList.Add(sym);
            }


            Task showMainForm = Task.Factory.StartNew(() => { MainFormInitializer(); });

        }


        private void MainFormInitializer()
        {
            // Form
            MainForm = new MetroForm();
            MainForm.Name = "MainForm";
            MainForm.Text = "";
            MainForm.Size = new Size(248, 130);
            MainForm.StartPosition = FormStartPosition.CenterScreen;
            MainForm.Resizable = false;
            MainForm.Theme = MetroFramework.MetroThemeStyle.Dark;
            MainForm.Style = MetroFramework.MetroColorStyle.Blue;
            MainForm.MaximizeBox = false;
            MainForm.ShowIcon = false;
            MainForm.Load += new EventHandler(MainFormLoad);
            MainForm.Shown += new EventHandler(MainFormShown);
            MainForm.FormClosed += new FormClosedEventHandler(MainFormClosed);

            // ComboBox
            SymbolsComboBox = new MetroComboBox();
            SymbolsComboBox.Name = "SymbolsComboBox";
            SymbolsComboBox.Size = new Size(121, 30);
            SymbolsComboBox.Location = new Point(23, 17);

            foreach (string sym in SymbolsList)
            {
                SymbolsComboBox.Items.Add(sym);
            }

            if (SymbolsList.Contains(Symbol.Code))
                SymbolsComboBox.SelectedItem = Symbol.Code;

            // Buttons
            BuyButton = new MetroButton();
            BuyButton.Name = "BuyButton";
            BuyButton.Text = "Buy";
            BuyButton.Size = new Size(50, 23);
            BuyButton.Location = new Point(23, 101);
            BuyButton.TextAlign = ContentAlignment.MiddleCenter;
            BuyButton.FontWeight = MetroFramework.MetroButtonWeight.Bold;
            BuyButton.Click += new EventHandler(ExecuteBuyOrder);

            SellButton = new MetroButton();
            SellButton.Name = "SellButton";
            SellButton.Text = "Sell";
            SellButton.Size = new Size(50, 23);
            SellButton.Location = new Point(94, 101);
            SellButton.TextAlign = ContentAlignment.MiddleCenter;
            SellButton.FontWeight = MetroFramework.MetroButtonWeight.Bold;
            SellButton.Click += new EventHandler(ExecuteSellOrder);


            OptionsButton = new MetroButton();
            OptionsButton.Name = "OptionsButton";
            OptionsButton.Text = "Options";
            OptionsButton.Size = new Size(76, 23);
            OptionsButton.Location = new Point(158, 63);
            OptionsButton.TextAlign = ContentAlignment.MiddleCenter;
            OptionsButton.FontWeight = MetroFramework.MetroButtonWeight.Bold;
            OptionsButton.Click += new EventHandler(ShowOptionsForm);


            // Numeric Up Down
            PercentNumber = new NumericUpDown();
            PercentNumber.Name = "PercentNumber";
            PercentNumber.DecimalPlaces = 1;
            PercentNumber.Value = 1;
            PercentNumber.Size = new Size(64, 22);
            PercentNumber.Location = new Point(39, 61);
            PercentNumber.Increment = 0.1m;


            // Label
            PercentLabel = new MetroLabel();
            PercentLabel.Name = "PercentLabel";
            PercentLabel.Text = "%";
            PercentLabel.Size = new Size(21, 20);
            PercentLabel.Location = new Point(109, 63);
            PercentLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            PercentLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;

            // Adding Controls
            MainForm.Controls.AddRange(new Control[] 
            {
                SymbolsComboBox,
                PercentNumber,
                PercentLabel,
                BuyButton,
                SellButton,
                OptionsButton
            });

            Application.Run(MainForm);
        }

        private void OptionsFormInitializer()
        {
            Print("OptionsFormInitializer");

            // Form
            OptionsForm = new MetroForm();
            OptionsForm.Name = "OptionsForm";
            OptionsForm.Text = "Options";
            OptionsForm.Size = new Size(670, 660);
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
            SLAndTPLabel.Location = new Point(20, 314);
            SLAndTPLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            SLAndTPLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;

            SLLabel = new MetroLabel();
            SLLabel.Name = "SLLabel";
            SLLabel.Text = "Stop Loss";
            SLLabel.Size = new Size(76, 20);
            SLLabel.Location = new Point(268, 347);
            SLLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            SLLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;

            TPLabel = new MetroLabel();
            TPLabel.Name = "TPLabel";
            TPLabel.Text = "Take Profit";
            TPLabel.Size = new Size(85, 20);
            TPLabel.Location = new Point(440, 347);
            TPLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            TPLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;

            FixedPipsBasedLabel = new MetroLabel();
            FixedPipsBasedLabel.Name = "FixedPipsBasedLabel";
            FixedPipsBasedLabel.Text = "Fixed Pips Based?";
            FixedPipsBasedLabel.Size = new Size(117, 20);
            FixedPipsBasedLabel.Location = new Point(92, 396);
            FixedPipsBasedLabel.Theme = MetroFramework.MetroThemeStyle.Dark;

            PipsAmountLabel = new MetroLabel();
            PipsAmountLabel.Name = "PipsAmountLabel";
            PipsAmountLabel.Text = "Pips Amount";
            PipsAmountLabel.Size = new Size(98, 20);
            PipsAmountLabel.Location = new Point(92, 447);
            PipsAmountLabel.Theme = MetroFramework.MetroThemeStyle.Dark;

            ATRBasedLabel = new MetroLabel();
            ATRBasedLabel.Name = "ATRBasedLabel";
            ATRBasedLabel.Text = "ATR Based?";
            ATRBasedLabel.Size = new Size(79, 20);
            ATRBasedLabel.Location = new Point(92, 502);
            ATRBasedLabel.Theme = MetroFramework.MetroThemeStyle.Dark;

            ATRMultiplierLabel = new MetroLabel();
            ATRMultiplierLabel.Name = "ATRMultiplierLabel";
            ATRMultiplierLabel.Text = "ATR Multiplier : ";
            ATRMultiplierLabel.Size = new Size(104, 20);
            ATRMultiplierLabel.Location = new Point(92, 556);
            ATRMultiplierLabel.Theme = MetroFramework.MetroThemeStyle.Dark;


            // Buttons
            AddButton = new MetroButton();
            AddButton.Name = "AddButton";
            AddButton.Text = "Add";
            AddButton.Size = new Size(75, 23);
            AddButton.Location = new Point(572, 106);
            AddButton.TextAlign = ContentAlignment.MiddleCenter;
            AddButton.FontWeight = MetroFramework.MetroButtonWeight.Bold;

            MoveUpButton = new MetroButton();
            MoveUpButton.Name = "MoveUpButton";
            MoveUpButton.Text = "Move Up";
            MoveUpButton.Size = new Size(95, 23);
            MoveUpButton.Location = new Point(397, 147);
            MoveUpButton.TextAlign = ContentAlignment.MiddleCenter;
            MoveUpButton.FontWeight = MetroFramework.MetroButtonWeight.Bold;

            MoveDownButton = new MetroButton();
            MoveDownButton.Name = "MoveDownButton";
            MoveDownButton.Text = "Move Down";
            MoveDownButton.Size = new Size(95, 23);
            MoveDownButton.Location = new Point(498, 147);
            MoveDownButton.TextAlign = ContentAlignment.MiddleCenter;
            MoveDownButton.FontWeight = MetroFramework.MetroButtonWeight.Bold;

            DeleteSymbolsButton = new MetroButton();
            DeleteSymbolsButton.Name = "DeleteSymbolsButton";
            DeleteSymbolsButton.Text = "Delete Selected Symbols";
            DeleteSymbolsButton.Size = new Size(195, 23);
            DeleteSymbolsButton.Location = new Point(398, 260);
            DeleteSymbolsButton.TextAlign = ContentAlignment.MiddleCenter;
            DeleteSymbolsButton.FontWeight = MetroFramework.MetroButtonWeight.Bold;

            SaveButton = new MetroButton();
            SaveButton.Name = "SaveButton";
            SaveButton.Text = "Save";
            SaveButton.Size = new Size(75, 23);
            SaveButton.Location = new Point(299, 618);
            SaveButton.TextAlign = ContentAlignment.MiddleCenter;
            SaveButton.FontWeight = MetroFramework.MetroButtonWeight.Bold;

            // Text Boxes
            AddSymbolTextBox = new MetroTextBox();
            AddSymbolTextBox.Name = "AddSymbolTextBox";
            AddSymbolTextBox.Text = "Enter Symbol Code";
            AddSymbolTextBox.Size = new Size(154, 23);
            AddSymbolTextBox.Location = new Point(397, 106);

            PipsSLTextBox = new MetroTextBox();
            PipsSLTextBox.Name = "PipsSLTextBox";
            PipsSLTextBox.Text = "5";
            PipsSLTextBox.Size = new Size(121, 30);
            PipsSLTextBox.Location = new Point(253, 437);

            PipsTPTextBox = new MetroTextBox();
            PipsTPTextBox.Name = "PipsTPTextBox";
            PipsTPTextBox.Text = "10";
            PipsTPTextBox.Size = new Size(121, 30);
            PipsTPTextBox.Location = new Point(430, 437);

            ATRMultiplierSLTextBox = new MetroTextBox();
            ATRMultiplierSLTextBox.Name = "ATRMultiplierSLTextBox";
            ATRMultiplierSLTextBox.Text = "2";
            ATRMultiplierSLTextBox.Size = new Size(121, 30);
            ATRMultiplierSLTextBox.Location = new Point(253, 546);

            ATRMultiplierTPTextBox = new MetroTextBox();
            ATRMultiplierTPTextBox.Name = "ATRMultiplierTPTextBox";
            ATRMultiplierTPTextBox.Text = "2";
            ATRMultiplierTPTextBox.Size = new Size(121, 30);
            ATRMultiplierTPTextBox.Location = new Point(430, 546);

            // ComboBoxes
            FixedPipsSLComboBox = new MetroComboBox();
            FixedPipsSLComboBox.Name = "FixedPipsSLComboBox";
            FixedPipsSLComboBox.Size = new Size(121, 30);
            FixedPipsSLComboBox.Location = new Point(253, 386);
            FixedPipsSLComboBox.Items.Add("No");
            FixedPipsSLComboBox.Items.Add("Yes");
            FixedPipsSLComboBox.SelectedIndex = 0;

            FixedPipsTPComboBox = new MetroComboBox();
            FixedPipsTPComboBox.Name = "FixedPipsTPComboBox";
            FixedPipsTPComboBox.Size = new Size(121, 30);
            FixedPipsTPComboBox.Location = new Point(430, 386);
            FixedPipsTPComboBox.Items.Add("No");
            FixedPipsTPComboBox.Items.Add("Yes");
            FixedPipsTPComboBox.SelectedIndex = 0;

            ATRSLComboBox = new MetroComboBox();
            ATRSLComboBox.Name = "ATRSLComboBox";
            ATRSLComboBox.Size = new Size(121, 30);
            ATRSLComboBox.Location = new Point(253, 492);
            ATRSLComboBox.Items.Add("No");
            ATRSLComboBox.Items.Add("Yes");
            ATRSLComboBox.SelectedIndex = 0;

            ATRTPComboBox = new MetroComboBox();
            ATRTPComboBox.Name = "ATRTPComboBox";
            ATRTPComboBox.Size = new Size(121, 30);
            ATRTPComboBox.Location = new Point(430, 492);
            ATRTPComboBox.Items.Add("No");
            ATRTPComboBox.Items.Add("Yes");
            ATRTPComboBox.SelectedIndex = 0;

            // Grid
            SymbolsGrid = new MetroGrid();
            SymbolsGrid.Name = "SymbolsGrid";
            SymbolsGrid.Size = new Size(300, 177);
            SymbolsGrid.Location = new Point(23, 106);
            SymbolsGrid.AllowUserToAddRows = false;
            SymbolsGrid.AllowUserToDeleteRows = false;
            SymbolsGrid.AllowUserToOrderColumns = false;
            SymbolsGrid.AllowUserToResizeColumns = false;
            SymbolsGrid.AllowUserToResizeRows = false;
            SymbolsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            DataGridViewTextBoxColumn numberColumn = new DataGridViewTextBoxColumn();
            numberColumn.Name = "numberColumn";
            numberColumn.HeaderText = "Number";
            SymbolsGrid.Columns.Add(numberColumn);

            DataGridViewTextBoxColumn symbolColumn = new DataGridViewTextBoxColumn();
            symbolColumn.Name = "symbolsColumn";
            symbolColumn.HeaderText = "Symbol";
            SymbolsGrid.Columns.Add(symbolColumn);

            Print("Adding Symbols to Grid");

            SymbolsGrid.Rows.Clear();
            int symNum = 0;
            foreach (string sym in SymbolsList)
            {
                Print("Foreach Start");
                symNum += 1;
                Print("Adding Row");
                SymbolsGrid.Rows.Add(symNum, sym);
                Print("Foreach End");
            }

            Print("PerformLayout");
            SymbolsGrid.PerformLayout();


            // Adding Controls
            Print("Adding Controls");

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
                FixedPipsSLComboBox,
                FixedPipsTPComboBox,
                ATRSLComboBox,
                ATRTPComboBox
            });

            Print("Going to show OptionsForm");

            Application.Run(OptionsForm);


        }

        // Events
        private void MainFormLoad(object sender, EventArgs e)
        {
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
            MainForm.Enabled = false;
        }

        private void OptionsFormShown(object sender, EventArgs e)
        {
            OptionsForm.TopMost = true;
        }

        private void OptionsFormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.Enabled = true;
        }



        private void ExecuteBuyOrder(object sender, EventArgs e)
        {

        }

        private void ExecuteSellOrder(object sender, EventArgs e)
        {

        }

        private void ShowOptionsForm(object sender, EventArgs e)
        {
            Task showOptionsForm = Task.Factory.StartNew(() => { OptionsFormInitializer(); });
        }
    }
}
