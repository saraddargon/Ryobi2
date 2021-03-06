﻿using System;
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
    public partial class CreatePR_List : Telerik.WinControls.UI.RadRibbonForm
    {
        public CreatePR_List(string CodeNox)
        {
            this.Name = "CreatePR_List";
            //  MessageBox.Show(this.Name);
            InitializeComponent();
            if (!dbClss.PermissionScreen(this.Name))
            {
                MessageBox.Show("Access denied", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
           // InitializeComponent();
            CodeNo = CodeNox;
            //this.Text = "ประวัติ "+ Screen;
            CallLang();
        }
        private void CallLang()
        {
            if (dbClss.Language.Equals("ENG"))
            {
                radButtonElement1.Text = "New P/R";

                btn_PrintPR.Text = "Print P/R";
                btnExport.Text = "Export";
                btnRefresh.Text = "Refresh";
                btnFilter1.Text = "Filter";
                btnUnfilter1.Text = "UnFilter";
                this.Text = "List P/R";
                radLabelElement1.Text = "List P/R";
                btnSearch.Text = "Search..";
                radLabel37.Text = "Vendor No. :";
                radLabel2.Text = "P/R No. :";
                radLabel5.Text = "Select";
                radLabel1.Text = "Desc.:";
                radLabel3.Text = "Code No.:";
                radLabel4.Text = "To";



                radGridView1.Columns[0].HeaderText = "No.";
                radGridView1.Columns[1].HeaderText = "Status";
                radGridView1.Columns[2].HeaderText = "P/R No.";
                radGridView1.Columns[3].HeaderText = "Vendor Name";
                radGridView1.Columns[4].HeaderText = "Code No.";
                radGridView1.Columns[5].HeaderText = "Tool Name";
                radGridView1.Columns[6].HeaderText = "Description";
                radGridView1.Columns[7].HeaderText = "Quantity";
                radGridView1.Columns[8].HeaderText = "Cost";
                radGridView1.Columns[9].HeaderText = "Amount";
                radGridView1.Columns[10].HeaderText = "Pcs/Unit";
                radGridView1.Columns[11].HeaderText = "Unit";

                radGridView1.Columns[12].HeaderText = "Remain Receipt";
                radGridView1.Columns[13].HeaderText = "Dept. Code";

                radGridView1.Columns[14].HeaderText = "CreateBy";
                radGridView1.Columns[15].HeaderText = "CreateDate";
                radGridView1.Columns[16].HeaderText = "Ref.No";

                this.Text = "List P/R";
                radLabelElement1.Text = "List P/R";



            }
        }
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public CreatePR_List(Telerik.WinControls.UI.RadTextBox  CodeNox)
        {
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
            CallLang();
        }
        public CreatePR_List()
        {
            this.Name = "CreatePR_List";
            //  MessageBox.Show(this.Name);
            InitializeComponent();
            if (!dbClss.PermissionScreen(this.Name))
            {
                MessageBox.Show("Access denied", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            // InitializeComponent();
            CallLang();
        }

        string CodeNo = "";
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
            var last = month.AddMonths(1).AddDays(-1);

            dtDateFrom.Value = first;
            dtDateTo.Value = last;
            Set_dt_Print();
            //radGridView1.ReadOnly = true;
            radGridView1.AutoGenerateColumns = false;
            DataLoad();
        }

        private void DataLoad()
        {
            //dt.Rows.Clear();
            try
            {
                radGridView1.DataSource = null;
                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //dt = ClassLib.Classlib.LINQToDataTable(db.tb_Units.ToList());
                    //radGridView1.DataSource = db.tb_Histories.Where(s => s.ScreenName == ScreenSearch).OrderBy(o => o.CreateDate).ToList();
                    int c = 0;
                  
                    DateTime inclusiveStart = dtDateFrom.Value.Date;
                    // Include the *whole* of the day indicated by searchEndDate
                    DateTime exclusiveEnd = dtDateTo.Value.Date.AddDays(1);

                    //var g = (from ix in db.tb_PurchaseRequests select ix)
                    //    .Where(a => a.VendorNo.Contains(txtVendorNo.Text)
                    //    && (a.Status != "Cancel")
                    //    && a.TEMPNo.Contains(txtTempNo.Text)
                    //    && a.PRNo.Contains(txtPRNo.Text)
                    //    && a.Dept == dbClss.DeptSC
                    //    && a.VendorName.Contains(txtVendorName.Text)
                    //    && (a.CreateDate >= inclusiveStart
                    //            && a.CreateDate < exclusiveEnd)
                    //     )
                    //    .ToList();
                    var g = db.spx_004_SelectListPR(dbClss.DeptSC, chkDate.Checked, dtDateFrom.Value, dtDateTo.Value, txtPRNo.Text, txtTempNo.Text, txtVendorNo.Text, txtVendorName.Text).ToList();
                    if (g.Count > 0)
                    {

                        radGridView1.DataSource = g;
                        foreach (var x in radGridView1.Rows)
                        {
                            c += 1;
                            x.Cells["No"].Value = c;
                        }
                    }
                    
                       
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;


            //    radGridView1.DataSource = dt;
        }
        private int compart_date(DateTime date1, DateTime date2)
        {
            int result = 0;
            //DateTime date1 = new DateTime(2009, 8, 1, 0, 0, 0);
            //DateTime date2 = new DateTime(2009, 8, 1, 12, 0, 0);
            result = DateTime.Compare(date1, date2);

            

            return result;
        }
       

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            radGridView1.ReadOnly = false;
            radGridView1.AllowAddNewRow = false;
            radGridView1.Rows.AddNew();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            radGridView1.ReadOnly = true;

            radGridView1.AllowAddNewRow = false;
            DataLoad();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            radGridView1.ReadOnly = false;

            radGridView1.AllowAddNewRow = false;
            //DataLoad();
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
            //  dbClss.ExportGridCSV(radGridView1);
            dbClss.ExportGridXlSX(radGridView1);
        }



        private void btnFilter1_Click(object sender, EventArgs e)
        {
            radGridView1.EnableFiltering = true;
        }

        private void btnUnfilter1_Click(object sender, EventArgs e)
        {
            radGridView1.EnableFiltering = false;
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            CreatePR sc = new CreatePR();
            this.Cursor = Cursors.Default;
            sc.ShowDialog();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            ClassLib.Memory.Heap();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void radGridView1_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                if (screen.Equals(1))
                {
                    CodeNo_tt.Text = Convert.ToString(e.Row.Cells["TempNo"].Value);
                    this.Close();
                }
                else
                {
                    CreatePR a = new CreatePR(Convert.ToString(e.Row.Cells["TempNo"].Value));
                    a.ShowDialog();
                    //this.Close();
                }
               
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        
        DataTable dt_Kanban = new DataTable();

        private void Set_dt_Print()
        {
          
            dt_Kanban.Columns.Add(new DataColumn("CodeNo", typeof(string)));
            dt_Kanban.Columns.Add(new DataColumn("PartNo", typeof(string)));
            dt_Kanban.Columns.Add(new DataColumn("PartDescription", typeof(string)));
            dt_Kanban.Columns.Add(new DataColumn("ShelfNo", typeof(string)));
            dt_Kanban.Columns.Add(new DataColumn("LeadTime", typeof(decimal)));
            dt_Kanban.Columns.Add(new DataColumn("VendorName", typeof(string)));
            dt_Kanban.Columns.Add(new DataColumn("GroupType", typeof(string)));
            dt_Kanban.Columns.Add(new DataColumn("ToolLife", typeof(decimal)));
            dt_Kanban.Columns.Add(new DataColumn("Max", typeof(decimal)));
            dt_Kanban.Columns.Add(new DataColumn("Min", typeof(decimal)));
            dt_Kanban.Columns.Add(new DataColumn("ReOrderPoint", typeof(decimal)));
            dt_Kanban.Columns.Add(new DataColumn("BarCode", typeof(Image)));

        }
       
        private void btn_Print_Barcode_Click(object sender, EventArgs e)
        {
            try
            {
                dt_Kanban.Rows.Clear();

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var g = (from ix in db.tb_Items select ix).Where(a => a.CodeNo == txtVendorNo.Text).ToList();
                    if (g.Count() > 0)
                    {
                        foreach (var gg in g)
                        {
                            dt_Kanban.Rows.Add(gg.CodeNo, gg.ItemNo, gg.ItemDescription, gg.ShelfNo, gg.Leadtime, gg.VendorItemName, gg.GroupCode, gg.Toollife, gg.MaximumStock, gg.MinimumStock, gg.ReOrderPoint, gg.BarCode);
                        }
                        //DataTable DT =  StockControl.dbClss.LINQToDataTable(g);
                        //Reportx1 po = new Reportx1("Report_PurchaseRequest_Content1.rpt", DT, "FromDT");
                        //po.Show();

                        Report.Reportx1 op = new Report.Reportx1("001_Kanban_Part.rpt", dt_Kanban, "FromDL");
                        op.Show();
                    }
                    else
                        MessageBox.Show("not found.");
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btn_PrintPR_Click(object sender, EventArgs e)
        {
            try
            {
                
                //dt_ShelfTag.Rows.Clear();
                string PRNo = "";
                if(radGridView1.Rows.Count > 0)
                    PRNo = StockControl.dbClss.TSt(radGridView1.CurrentRow.Cells["PRNo"].Value);

                PrintPR a = new PrintPR(PRNo, PRNo,"PR");
                a.ShowDialog();

                //using (DataClasses1DataContext db = new DataClasses1DataContext())
                //{
                //    var g = (from ix in db.sp_R002_ReportPR(PRNo, DateTime.Now) select ix).ToList();
                //    if (g.Count() > 0)
                //    {

                //        Report.Reportx1.Value = new string[2];
                //        Report.Reportx1.Value[0] = PRNo;
                //        Report.Reportx1.WReport = "ReportPR";
                //        Report.Reportx1 op = new Report.Reportx1("ReportPR.rpt");
                //        op.Show();

                //    }
                //    else
                //        MessageBox.Show("not found.");
                //}

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            try
            {
                string PRNo = "";
                if (radGridView1.Rows.Count > 0)
                    PRNo = StockControl.dbClss.TSt(radGridView1.CurrentRow.Cells["PRNo"].Value);

                PrintPR a = new PrintPR(PRNo, PRNo, "PR");
                a.ShowDialog();
            }
            catch { }
        }

        private void frezzRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (radGridView1.Rows.Count > 0)
                {

                    int Row = 0;
                    Row = radGridView1.CurrentRow.Index;
                    dbClss.Set_Freeze_Row(radGridView1, Row);

                    //foreach (var rd in radGridView1.Rows)
                    //{
                    //    if (rd.Index <= Row)
                    //    {
                    //        radGridView1.Rows[rd.Index].PinPosition = PinnedRowPosition.Top;
                    //    }
                    //    else
                    //        break;
                    //}
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void frezzColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (radGridView1.Columns.Count > 0)
                {
                    int Col = 0;
                    Col = radGridView1.CurrentColumn.Index;
                    dbClss.Set_Freeze_Column(radGridView1, Col);

                    //foreach (var rd in radGridView1.Columns)
                    //{
                    //    if (rd.Index <= Col)
                    //    {
                    //        radGridView1.Columns[rd.Index].PinPosition = PinnedColumnPosition.Left;
                    //    }
                    //    else
                    //        break;
                    //}
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void unFrezzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                dbClss.Set_Freeze_UnColumn(radGridView1);
                dbClss.Set_Freeze_UnRows(radGridView1);
                //foreach (var rd in radGridView1.Rows)
                //{
                //    radGridView1.Rows[rd.Index].IsPinned = false;
                //}
                //foreach (var rd in radGridView1.Columns)
                //{
                //    radGridView1.Columns[rd.Index].IsPinned = false;                   
                //}

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
