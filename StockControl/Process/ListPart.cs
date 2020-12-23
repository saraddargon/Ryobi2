using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using Telerik.WinControls.UI;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
namespace StockControl
{
    public partial class ListPart : Telerik.WinControls.UI.RadRibbonForm
    {
        public ListPart(string CodeNox)
        {
            this.Name = "ListPart";
            //  MessageBox.Show(this.Name);
            InitializeComponent();
            if (!dbClss.PermissionScreen(this.Name))
            {
                MessageBox.Show("Access denied", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            
            CodeNo = CodeNox;
            CallLang();
            //this.Text = "ประวัติ "+ Screen;
        }
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public ListPart(Telerik.WinControls.UI.RadTextBox  CodeNox)
        {
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
            CallLang();
        }
        public ListPart()
        {
            this.Name = "ListPart";

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

        private void CallLang()
        {
            if (dbClss.Language.Equals("ENG"))
            {
                //gridViewTextBoxColumn1.HeaderText = "ลำดับ";
                //gridViewCheckBoxColumn1.HeaderText = "เลือก";
                this.Text = "Tools List";
                dbClss.ChangeTextEng(radButtonElement1, 2, this.Name, "New Item");
                dbClss.ChangeTextEng(btnExport, 2, this.Name, "Export");
                dbClss.ChangeTextEng(btnRefresh, 2, this.Name, "Refresh");
                dbClss.ChangeTextEng(btnFilter1, 2, this.Name, "Filter");
                dbClss.ChangeTextEng(btnUnfilter1, 2, this.Name, "UnFilter");
                btnSearch.Text = "Search..";
                radLabel37.Text = "Code No.:";
                radLabel2.Text = "Description.:";
                radLabel1.Text = "Tool Name:";
                radLabel3.Text = "Vendor :";
                radLabelElement1.Text = "Item List All";

                radGridView1.Columns[0].HeaderText = "No.";                
                radGridView1.Columns[1].HeaderText = "Select";
                radGridView1.Columns[2].HeaderText = "Dept.";
                radGridView1.Columns[3].HeaderText = "Dept. Code";
                radGridView1.Columns[4].HeaderText = "CodeNo.";
                radGridView1.Columns[5].HeaderText = "Tool Name";
                radGridView1.Columns[6].HeaderText = "Description.";
                radGridView1.Columns[7].HeaderText = "Stock(Remain)";

                radGridView1.Columns[8].HeaderText = "Order Qty";
                radGridView1.Columns[9].HeaderText = "Size";
                radGridView1.Columns[10].HeaderText = "Shelf";
                radGridView1.Columns[11].HeaderText = "Stop Order";
                radGridView1.Columns[12].HeaderText = "Group Type";
                radGridView1.Columns[13].HeaderText = "Type";
                radGridView1.Columns[14].HeaderText = "Cost";
                radGridView1.Columns[15].HeaderText = "Unit(pur)";
                radGridView1.Columns[16].HeaderText = "Pcs/Unit";
                radGridView1.Columns[17].HeaderText = "Unit(Ship)";

                radGridView1.Columns[18].HeaderText = "VendorNo";
                radGridView1.Columns[19].HeaderText = "VendorName";
                radGridView1.Columns[20].HeaderText = "Maker";
                radGridView1.Columns[21].HeaderText = "Leadtime";
                radGridView1.Columns[22].HeaderText = "Package Std.";
                radGridView1.Columns[23].HeaderText = "Max Stock";
                radGridView1.Columns[24].HeaderText = "Min Stock";
                radGridView1.Columns[25].HeaderText = "Tool Life.";

                radGridView1.Columns[26].HeaderText = "Avg Std.";
                radGridView1.Columns[27].HeaderText = "Status";
                radGridView1.Columns[28].HeaderText = "Remark";
               




            }
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

                    //var g = (from ix in db.tb_Items select ix).Where(a => a.CodeNo.Contains(txtCodeNo.Text)
                    //    && a.ItemNo.Contains(txtPartName.Text)
                    //    && a.ItemDescription.Contains(txtDescription.Text)
                    //    && a.VendorItemName.Contains(txtVendorName.Text))
                    //    .ToList();

                    if (ckList1000.Checked)
                    {
                        var g = (from ix in db.sp_014_Select_PartList2(txtCodeNo.Text, txtPartName.Text, txtDescription.Text, "", txtVendorName.Text, "", dbClss.DeptSC, cboFilter.Text) select ix).ToList();
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
                    else
                    {
                        var g = (from ix in db.sp_014_Select_PartList(txtCodeNo.Text, txtPartName.Text, txtDescription.Text, "", txtVendorName.Text, "", dbClss.DeptSC, cboFilter.Text) select ix).ToList();
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
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;


            //    radGridView1.DataSource = dt;
        }
        private bool CheckDuplicate(string code)
        {
            bool ck = false;

            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                int i = (from ix in db.tb_Units where ix.UnitCode == code select ix).Count();
                if (i > 0)
                    ck = false;
                else
                    ck = true;
            }
            return ck;
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
            CreatePart sc = new CreatePart();
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
                    CodeNo_tt.Text = Convert.ToString(e.Row.Cells["CodeNo"].Value);
                    this.Close();
                }
                else
                {
                    CreatePart sc = new CreatePart(Convert.ToString(e.Row.Cells["CodeNo"].Value));
                    this.Cursor = Cursors.Default;
                    sc.Show();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        DataTable dt_ShelfTag = new DataTable();
        DataTable dt_Kanban = new DataTable();

        private void Set_dt_Print()
        {
            dt_ShelfTag.Columns.Add(new DataColumn("CodeNo", typeof(string)));
            dt_ShelfTag.Columns.Add(new DataColumn("PartDescription", typeof(string)));
            dt_ShelfTag.Columns.Add(new DataColumn("ShelfNo", typeof(string)));


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
        private void Print_Shelftag_datatable()
        {
            try
            {
                dt_ShelfTag.Rows.Clear();

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var g = (from ix in db.tb_Items select ix).Where(a => a.CodeNo == txtCodeNo.Text).ToList();
                    if (g.Count() > 0)
                    {
                        foreach (var gg in g)
                        {
                            dt_ShelfTag.Rows.Add(gg.CodeNo, gg.ItemDescription, gg.ShelfNo);
                        }
                        //DataTable DT =  StockControl.dbClss.LINQToDataTable(g);
                        //Reportx1 po = new Reportx1("Report_PurchaseRequest_Content1.rpt", DT, "FromDT");
                        //po.Show();

                        Report.Reportx1 op = new Report.Reportx1("002_BoxShelf_Part.rpt", dt_ShelfTag, "FromDL");
                        op.Show();
                    }
                    else
                        MessageBox.Show("not found.");
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btn_PrintShelfTag_Click(object sender, EventArgs e)
        {
            try
            {

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //delete ทิ้งก่อน
                    var deleteItem = (from ii in db.TempPrintShelfs where ii.UserName == dbClss.UserID select ii);
                    foreach (var d in deleteItem)
                    {
                        db.TempPrintShelfs.DeleteOnSubmit(d);
                        db.SubmitChanges();
                    }

                    int c = 0;
                    string CodeNo = "";
                    radGridView1.EndEdit();
                    //insert
                    foreach (var Rowinfo in radGridView1.Rows)
                    {
                        if (StockControl.dbClss.TBo(Rowinfo.Cells["S"].Value).Equals(true))
                        {
                            CodeNo = StockControl.dbClss.TSt(Rowinfo.Cells["CodeNo"].Value);
                            var g = (from ix in db.tb_Items select ix).Where(a => a.CodeNo == CodeNo).ToList();
                            if (g.Count() > 0)
                            {
                                
                                c += 1;
                                TempPrintShelf ps = new TempPrintShelf();
                                ps.UserName = dbClss.UserID;
                                ps.CodeNo = g.FirstOrDefault().CodeNo;
                                ps.PartDescription = g.FirstOrDefault().ItemDescription;
                                ps.PartNo = g.FirstOrDefault().ItemNo;
                                ps.ShelfNo = g.FirstOrDefault().ShelfNo;
                                ps.Max = Convert.ToDecimal(g.FirstOrDefault().MaximumStock);
                                ps.Min = Convert.ToDecimal(g.FirstOrDefault().MinimumStock);
                                ps.OrderPoint = Convert.ToDecimal(g.FirstOrDefault().ReOrderPoint);
                                db.TempPrintShelfs.InsertOnSubmit(ps);
                                db.SubmitChanges();
                            }
                        }

                    }
                    if (c > 0)
                    {
                        Report.Reportx1.Value = new string[2];
                        Report.Reportx1.Value[0] = dbClss.UserID;
                        Report.Reportx1.WReport = "002_BoxShelf_Part";
                        Report.Reportx1 op = new Report.Reportx1("002_BoxShelf_Part.rpt");
                        op.Show();
                    }
                    else
                        MessageBox.Show("not found.");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btn_Print_Barcode_Click(object sender, EventArgs e)
        {
            try
            {
                dt_Kanban.Rows.Clear();
                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {

                    db.spx_012_DeleteBarcode();
                   
                        // Step 1 delete UserName
                        var deleteItem = (from ii in db.TempPrintKanbans where ii.UserName == dbClss.UserID select ii);
                        foreach (var d in deleteItem)
                        {
                            db.TempPrintKanbans.DeleteOnSubmit(d);
                            db.SubmitChanges();
                        }

                        // Step 2 Insert to Table

                        int c = 0;
                        string CodeNo = "";
                        radGridView1.EndEdit();
                        //insert
                        foreach (var Rowinfo in radGridView1.Rows)
                        {
                            if (StockControl.dbClss.TBo(Rowinfo.Cells["S"].Value).Equals(true))
                            {
                                CodeNo = StockControl.dbClss.TSt(Rowinfo.Cells["CodeNo"].Value);
                                var g = (from ix in db.tb_Items select ix).Where(a => a.CodeNo == CodeNo).ToList();
                                if (g.Count() > 0)
                                {
                                    c += 1;
                                    TempPrintKanban tm = new TempPrintKanban();
                                    tm.UserName = dbClss.UserID;
                                    tm.CodeNo = g.FirstOrDefault().CodeNo;
                                    tm.PartDescription = g.FirstOrDefault().ItemDescription;
                                    tm.PartNo = g.FirstOrDefault().ItemNo;
                                    tm.VendorName = g.FirstOrDefault().VendorItemName;
                                    tm.ShelfNo = g.FirstOrDefault().ShelfNo;
                                    tm.GroupType = g.FirstOrDefault().GroupCode;
                                    tm.Max = Convert.ToDecimal(g.FirstOrDefault().MaximumStock);
                                    tm.Min = Convert.ToDecimal(g.FirstOrDefault().MinimumStock);
                                    tm.ReOrderPoint = Convert.ToDecimal(g.FirstOrDefault().ReOrderPoint);
                                    tm.ToolLife = Convert.ToDecimal(g.FirstOrDefault().Toollife);
                                    byte[] barcode = StockControl.dbClss.SaveQRCode2D(g.FirstOrDefault().CodeNo);
                                    tm.BarCode = barcode;
                                    db.TempPrintKanbans.InsertOnSubmit(tm);
                                    db.SubmitChanges();
                                    this.Cursor = Cursors.Default;
                                  
                                }
                            }
                        }
                        if (c > 0)
                        {
                            Report.Reportx1.Value = new string[2];
                            Report.Reportx1.Value[0] = dbClss.UserID;
                            Report.Reportx1.WReport = "001_Kanban_Part";
                            Report.Reportx1 op = new Report.Reportx1("001_Kanban_Part.rpt");
                            op.Show();
                        }
                        else
                            MessageBox.Show("not found.");
                   
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
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

        int rows1 = 0;
        private void btnReportStockCard_Click(object sender, EventArgs e)
        {
            if(rows1 >= 0)
            {
                string CodeNos = radGridView1.Rows[rows1].Cells["CodeNo"].Value.ToString();
                if(!CodeNos.Equals(""))
                {
                    PrintStockCard1 ps = new PrintStockCard1(CodeNos);
                    ps.Show();
                }
            }
        }

        private void radGridView1_CellClick_1(object sender, GridViewCellEventArgs e)
        {
            rows1 = e.RowIndex;
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            if (rows1 >= 0)
            {
                string CodeNos = radGridView1.Rows[rows1].Cells["CodeNo"].Value.ToString();
                if (!CodeNos.Equals(""))
                {
                    StockItemCost cs = new StockItemCost(CodeNos);
                    cs.ShowDialog();
                }
            }
        }

        private void radButtonElement3_Click(object sender, EventArgs e)
        {
            StockItemCostVendor sv = new StockItemCostVendor("");
            sv.ShowDialog();
        }

        private void radButtonElement4_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("คุณต้องการอัพเดต Shelf No.","อัพเดต",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                try
                {
                    openFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx";
                    openFileDialog1.FilterIndex = 2;
                    openFileDialog1.RestoreDirectory = true;
                    openFileDialog1.FileName = "";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {

                        // txtPath.Text = openFileDialog1.FileName;
                        UploadExcel(openFileDialog1.FileName);
                    }
                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void UploadExcel(string filePaht)
        {
            try
            {
                progressBar1.Visible = true;
                string Shelf = "";
                string CodeNo = "";
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook excelBook = excelApp.Workbooks.Open(
                  filePaht, 0, true, 5,
                  "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false,
                  0, true);
                Excel.Sheets sheets = excelBook.Worksheets;
                Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);
                int countP = 2000;
                int EndofTAG = 0;
                int Rowx = 0;
                int rows = 0;
                int countRow = 0;
                

                progressBar1.Minimum = 0;
                progressBar1.Maximum = 2000;
                progressBar1.Step = 1;
                

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    for (int ixi = 0; ixi < countP; ixi++)
                    {
                        rows += 1;
                        Rowx += 1;
                        System.Array myvalues;
                        Excel.Range range = worksheet.get_Range("A" + Rowx.ToString(), "B" + Rowx.ToString());
                        myvalues = (System.Array)range.Cells.Value;
                        string[] strArray = ConvertToStringArray(myvalues);

                        if (!Convert.ToString(strArray[0]).Equals("") &&
                            !Convert.ToString(strArray[1]).Equals("")
                            )
                        {
                            //Update Shelf//
                            tb_Item item = db.tb_Items.Where(i => i.CodeNo.Equals(Convert.ToString(strArray[0]))).FirstOrDefault();
                            if(item!=null)
                            {
                                progressBar1.Value = rows;
                                progressBar1.PerformStep();
                                item.ShelfNo = Convert.ToString(strArray[1]);
                                db.SubmitChanges();

                            }
                        }
                    }
                    excelBook.Close(0);
                    excelApp.Quit();

                    releaseObject(worksheet);
                    releaseObject(excelBook);
                    releaseObject(excelApp);
                    Marshal.FinalReleaseComObject(worksheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelBook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                    GC.GetTotalMemory(false);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    GC.GetTotalMemory(true);
                    /////////////////////////////////////

                    MessageBox.Show("Import Completed.\n row=" + rows);

                }

            }
            catch { }
            progressBar1.Visible = false;
        }
        private string[] ConvertToStringArray(System.Array values)
        {

            // create a new string array
            string[] theArray = new string[values.Length];

            // loop through the 2-D System.Array and populate the 1-D String Array
            for (int i = 1; i <= values.Length; i++)
            {
                if (values.GetValue(1, i) == null)
                    theArray[i - 1] = "";
                else
                    theArray[i - 1] = (string)values.GetValue(1, i).ToString();
            }

            return theArray;
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                // MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
