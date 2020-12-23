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
    public partial class PrintStockCard1 : Telerik.WinControls.UI.RadRibbonForm
    {
    
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public PrintStockCard1(Telerik.WinControls.UI.RadTextBox  CodeNox)
        {
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
            CallLang();
        }
        public PrintStockCard1(string CodeNox)
        {
            InitializeComponent();
            CodeNo = CodeNox;
            txtCodeNo.Text = CodeNox;
            screen = 1;
            CallLang();
        }
        public PrintStockCard1()
        {
            InitializeComponent();
            CallLang();
        }
        private void CallLang()
        {
            if (dbClss.Language.Equals("ENG"))
            {
                radLabel1.Text = "Code No.";
                radLabel2.Text = "Start Date";
                btnExport.Text = "Export";
                btn_PrintPR.Text = "Preview Doc.";
                this.Text = "Report";
            }
        }

        string CodeNo = "";
        string PR1 = "";
        string PR2 = "";
        string Type = "";
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
            dtDate.Value = first;
            //if(CodeNo.Equals(""))
            // {
            //     txtCodeNo.Text = CodeNo;
            // }
        }

        private void DataLoad()
        {
            //dt.Rows.Clear();
            try
            {
               
                this.Cursor = Cursors.WaitCursor;
                //using (DataClasses1DataContext db = new DataClasses1DataContext())
                //{
                   
                    
                       
                //}
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;


            //    radGridView1.DataSource = dt;
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
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    db.spx_011_StockCard(dtDate.Value, txtCodeNo.Text);

                    Report.Reportx1.Value = new string[2];
                    Report.Reportx1.Value[0] =txtCodeNo.Text;
                   // Report.Reportx1.Value[1] = PRNo2;
                    Report.Reportx1.WReport = "ReportStockCard";
                    Report.Reportx1 op = new Report.Reportx1("rp_StockCard1.rpt");
                    op.Show();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }
    }
}
