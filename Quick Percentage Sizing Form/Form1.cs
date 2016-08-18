using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace Quick_Percentage_Sizing_Form
{
    public partial class MainForm : MetroForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
        }
    }
}
