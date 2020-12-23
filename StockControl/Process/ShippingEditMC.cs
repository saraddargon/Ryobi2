using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
namespace StockControl
{
    public partial class ShippingEditMC : Telerik.WinControls.UI.RadRibbonForm
    {
    
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public ShippingEditMC(Telerik.WinControls.UI.RadTextBox  CodeNox)
        {
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
        }
        public ShippingEditMC(int idx)
        {
            InitializeComponent();
            id = idx;
           
        }
        public ShippingEditMC()
        {
            InitializeComponent();
        }
        string CodeNo = "";
        string PR1 = "";
        string PR2 = "";
        string Type = "";
        int id = 0;
        //private int RowView = 50;
        //private int ColView = 10;
        //DataTable dt = new DataTable();
        private void radMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void radRibbonBar1_Click(object sender, EventArgs e)
        {

        }
        private void GETDTRow()
        {
            //dt.Columns.Add(new DataColumn("UnitCode", typeof(string)));
            //dt.Columns.Add(new DataColumn("UnitDetail", typeof(string)));
            //dt.Columns.Add(new DataColumn("UnitActive", typeof(bool)));
        }
        private void Unit_Load(object sender, EventArgs e)
        {
            var today = DateTime.Now;
            var month = new DateTime(today.Year, today.Month, 1);
            var first = month;

            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                tb_Shipping sp = db.tb_Shippings.Where(s => s.id == id).FirstOrDefault();
                if(sp!=null)
                {
                    txtLIne.Text = sp.LineName;
                    txtMC.Text = sp.MachineName;
                    txtMold.Text = sp.MOLD;
                }
            }
           
        }

        private void DataLoad()
        {
        
        }
     

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
           
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
         
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {

        }

        private void Unit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // MessageBox.Show(e.KeyCode.ToString());
        }

        private void radGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            DataLoad();

        }

        int row = -1;
        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            row = e.RowIndex;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ////  dbClss.ExportGridCSV(radGridView1);
            //dbClss.ExportGridXlSX(radGridView1);

            try
            {
                if (MessageBox.Show("ต้องการอัพเดต?", "อัพเดต", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        tb_Shipping dd = db.tb_Shippings.Where(s => s.id == id).FirstOrDefault();
                        if(dd!=null)
                        {
                            dd.MOLD = txtMold.Text.Trim();
                            dd.LineName = txtLIne.Text.Trim();
                            dd.MachineName = txtMC.Text.Trim();
                            db.SubmitChanges();
                            MessageBox.Show("อัพเดต เรียบร้อย!");
                        }
                    }
                }
            }
            catch { }
        }



        private void btnFilter1_Click(object sender, EventArgs e)
        {
            //radGridView1.EnableFiltering = true;
        }

        private void btnUnfilter1_Click(object sender, EventArgs e)
        {
            //radGridView1.EnableFiltering = false;
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
          //  DataLoad();
        }

        private void radGridView1_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            //try
            //{
            //    if (screen.Equals(1))
            //    {
            //        CodeNo_tt.Text = Convert.ToString(e.Row.Cells["TempNo"].Value);
            //        this.Close();
            //    }
            //    else
            //    {
            //        CreatePR a = new CreatePR(Convert.ToString(e.Row.Cells["TempNo"].Value));
            //        a.ShowDialog();
            //        this.Close();
            //    }
               
            //}
            //catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        
        DataTable dt_Kanban = new DataTable();

        private void Set_dt_Print()
        {
          

        }
       
        private void btn_Print_Barcode_Click(object sender, EventArgs e)
        {
           
        }

        private void btn_PrintPR_Click(object sender, EventArgs e)
        {
           
        }
    }
}
