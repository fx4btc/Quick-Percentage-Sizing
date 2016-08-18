namespace Quick_Percentage_Sizing_Form
{
    partial class MainForm
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
            this.SymbolsComboBox = new MetroFramework.Controls.MetroComboBox();
            this.SellButton = new MetroFramework.Controls.MetroButton();
            this.BuyButton = new MetroFramework.Controls.MetroButton();
            this.PercentNumber = new System.Windows.Forms.NumericUpDown();
            this.PercentLabel = new MetroFramework.Controls.MetroLabel();
            this.OptionsButton = new MetroFramework.Controls.MetroButton();
            this.RiskLabel = new MetroFramework.Controls.MetroLabel();
            this.SlippageLabel = new MetroFramework.Controls.MetroLabel();
            this.SlippageNumber = new System.Windows.Forms.NumericUpDown();
            this.SlippagePipsLabel = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.PercentNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SlippageNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // SymbolsComboBox
            // 
            this.SymbolsComboBox.FormattingEnabled = true;
            this.SymbolsComboBox.ItemHeight = 24;
            this.SymbolsComboBox.Location = new System.Drawing.Point(23, 18);
            this.SymbolsComboBox.Name = "SymbolsComboBox";
            this.SymbolsComboBox.Size = new System.Drawing.Size(139, 30);
            this.SymbolsComboBox.TabIndex = 0;
            this.SymbolsComboBox.UseSelectable = true;
            // 
            // SellButton
            // 
            this.SellButton.Location = new System.Drawing.Point(94, 136);
            this.SellButton.Name = "SellButton";
            this.SellButton.Size = new System.Drawing.Size(50, 23);
            this.SellButton.TabIndex = 1;
            this.SellButton.Text = "Sell";
            this.SellButton.UseSelectable = true;
            // 
            // BuyButton
            // 
            this.BuyButton.Location = new System.Drawing.Point(23, 136);
            this.BuyButton.Name = "BuyButton";
            this.BuyButton.Size = new System.Drawing.Size(50, 23);
            this.BuyButton.TabIndex = 2;
            this.BuyButton.Text = "Buy";
            this.BuyButton.UseSelectable = true;
            // 
            // PercentNumber
            // 
            this.PercentNumber.DecimalPlaces = 1;
            this.PercentNumber.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.PercentNumber.Location = new System.Drawing.Point(112, 61);
            this.PercentNumber.Name = "PercentNumber";
            this.PercentNumber.Size = new System.Drawing.Size(50, 22);
            this.PercentNumber.TabIndex = 4;
            this.PercentNumber.ThousandsSeparator = true;
            this.PercentNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // PercentLabel
            // 
            this.PercentLabel.AutoSize = true;
            this.PercentLabel.BackColor = System.Drawing.Color.Lime;
            this.PercentLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.PercentLabel.ForeColor = System.Drawing.Color.Lime;
            this.PercentLabel.Location = new System.Drawing.Point(181, 60);
            this.PercentLabel.Name = "PercentLabel";
            this.PercentLabel.Size = new System.Drawing.Size(22, 20);
            this.PercentLabel.TabIndex = 5;
            this.PercentLabel.Text = "%";
            this.PercentLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.PercentLabel.UseCustomForeColor = true;
            // 
            // OptionsButton
            // 
            this.OptionsButton.Location = new System.Drawing.Point(170, 136);
            this.OptionsButton.Name = "OptionsButton";
            this.OptionsButton.Size = new System.Drawing.Size(76, 23);
            this.OptionsButton.TabIndex = 6;
            this.OptionsButton.Text = "Options";
            this.OptionsButton.UseSelectable = true;
            this.OptionsButton.Click += new System.EventHandler(this.metroButton3_Click);
            // 
            // RiskLabel
            // 
            this.RiskLabel.AutoSize = true;
            this.RiskLabel.Location = new System.Drawing.Point(23, 63);
            this.RiskLabel.Name = "RiskLabel";
            this.RiskLabel.Size = new System.Drawing.Size(36, 20);
            this.RiskLabel.TabIndex = 7;
            this.RiskLabel.Text = "Risk:";
            this.RiskLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // SlippageLabel
            // 
            this.SlippageLabel.AutoSize = true;
            this.SlippageLabel.Location = new System.Drawing.Point(23, 91);
            this.SlippageLabel.Name = "SlippageLabel";
            this.SlippageLabel.Size = new System.Drawing.Size(64, 20);
            this.SlippageLabel.TabIndex = 9;
            this.SlippageLabel.Text = "Slippage:";
            this.SlippageLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // SlippageNumber
            // 
            this.SlippageNumber.DecimalPlaces = 1;
            this.SlippageNumber.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.SlippageNumber.Location = new System.Drawing.Point(112, 93);
            this.SlippageNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SlippageNumber.Name = "SlippageNumber";
            this.SlippageNumber.Size = new System.Drawing.Size(50, 22);
            this.SlippageNumber.TabIndex = 8;
            this.SlippageNumber.ThousandsSeparator = true;
            this.SlippageNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SlippagePipsLabel
            // 
            this.SlippagePipsLabel.AutoSize = true;
            this.SlippagePipsLabel.BackColor = System.Drawing.Color.Lime;
            this.SlippagePipsLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.SlippagePipsLabel.ForeColor = System.Drawing.Color.Lime;
            this.SlippagePipsLabel.Location = new System.Drawing.Point(181, 93);
            this.SlippagePipsLabel.Name = "SlippagePipsLabel";
            this.SlippagePipsLabel.Size = new System.Drawing.Size(38, 20);
            this.SlippagePipsLabel.TabIndex = 10;
            this.SlippagePipsLabel.Text = "Pips";
            this.SlippagePipsLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.SlippagePipsLabel.UseCustomForeColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 177);
            this.Controls.Add(this.SlippagePipsLabel);
            this.Controls.Add(this.SlippageLabel);
            this.Controls.Add(this.SlippageNumber);
            this.Controls.Add(this.RiskLabel);
            this.Controls.Add(this.OptionsButton);
            this.Controls.Add(this.PercentLabel);
            this.Controls.Add(this.PercentNumber);
            this.Controls.Add(this.BuyButton);
            this.Controls.Add(this.SellButton);
            this.Controls.Add(this.SymbolsComboBox);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Resizable = false;
            this.ShowIcon = false;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PercentNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SlippageNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroComboBox SymbolsComboBox;
        private MetroFramework.Controls.MetroButton SellButton;
        private MetroFramework.Controls.MetroButton BuyButton;
        private System.Windows.Forms.NumericUpDown PercentNumber;
        private MetroFramework.Controls.MetroLabel PercentLabel;
        private MetroFramework.Controls.MetroButton OptionsButton;
        private MetroFramework.Controls.MetroLabel RiskLabel;
        private MetroFramework.Controls.MetroLabel SlippageLabel;
        private System.Windows.Forms.NumericUpDown SlippageNumber;
        private MetroFramework.Controls.MetroLabel SlippagePipsLabel;
    }
}

