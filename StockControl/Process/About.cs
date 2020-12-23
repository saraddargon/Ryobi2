using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace StockControl
{
    public partial class About : Telerik.WinControls.UI.RadForm
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            if(dbClss.Language.Equals("ENG"))
            {
                label1.Text = "Info Software System";
                label2.Text = "Software Name:";
                label3.Text = "Version :";
                label4.Text = "Company";
                label5.Text = "Develop by";
            }
        }
    }
}
