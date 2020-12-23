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

namespace StockControl
{
    public partial class ListPartForShip : Telerik.WinControls.UI.RadRibbonForm
    {
        public ListPartForShip(RadGridView CodeNox)
        {
            this.Name = "ListPart";
            //  MessageBox.Show(this.Name);
            InitializeComponent();
            //if (!dbClss.PermissionScreen(this.Name))
            //{
            //    MessageBox.Show("Access denied", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.Close();
            //}

            dgvData = CodeNox;
            //this.Text = "ประวัติ "+ Screen;
            CallLang();
        }
        private void CallLang()
        {
            if (dbClss.Language.Equals("ENG"))
            {
                this.Text = "Select List";
                radLabelElement1.Text = "Status: Select List";
                btnRefresh.Text = "Refresh";              
                btnExport.Text = "Export";
                radButtonElement1.Text = "Select";
             
                btnFilter1.Text = "Filter";
                btnUnfilter1.Text = "Unfilter";
                btnSearch.Text = "Search..";
                radLabel37.Text = "CodeNo.:";
                radLabel2.Text = "Description:";
                radLabel1.Text = "ToolName:";
                radLabel3.Text = "Vendor Name:";

                // radButtonElement1.Text = "Contact";

                radGridView1.Columns[0].HeaderText = "No";
                radGridView1.Columns[1].HeaderText = "Select";
                radGridView1.Columns[2].HeaderText = "Dept.";
                radGridView1.Columns[3].HeaderText = "Dept. Code";
                radGridView1.Columns[4].HeaderText = "CodeNo";
                radGridView1.Columns[5].HeaderText = "ToolName";

                radGridView1.Columns[6].HeaderText = "Description";
                radGridView1.Columns[7].HeaderText = "Remain Q'ty";
                radGridView1.Columns[8].HeaderText = "Shelf";
                radGridView1.Columns[9].HeaderText = "Group Type";
                radGridView1.Columns[10].HeaderText = "Type";
                radGridView1.Columns[11].HeaderText = "Cost";
                radGridView1.Columns[12].HeaderText = "Unit";
                radGridView1.Columns[13].HeaderText = "Pcs/Unit";
                radGridView1.Columns[14].HeaderText = "Unit(Ship)";
                radGridView1.Columns[15].HeaderText = "VendorNo";
                radGridView1.Columns[16].HeaderText = "VendorName";
                radGridView1.Columns[17].HeaderText = "Maker";
                radGridView1.Columns[18].HeaderText = "LeadTime";
                radGridView1.Columns[19].HeaderText = "Minimum";
                radGridView1.Columns[20].HeaderText = "Maximum";
                radGridView1.Columns[21].HeaderText = "ToolLife";
                radGridView1.Columns[22].HeaderText = "Status";
                radGridView1.Columns[23].HeaderText = "Remark";




            }
        }
        Telerik.WinControls.UI.RadGridView dgvData = new RadGridView();
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public ListPartForShip(Telerik.WinControls.UI.RadTextBox  CodeNox)
        {
            InitializeComponent();
            CodeNo_tt = CodeNox;
            screen = 1;
        }
        public ListPartForShip()
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
                        var g = (from ix in db.sp_014_Select_PartList2(txtCodeNo.Text, txtPartName.Text, txtDescription.Text, "", txtVendorName.Text, "", dbClss.DeptSC,"") select ix).ToList();
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
                        var g = (from ix in db.sp_014_Select_PartList(txtCodeNo.Text, txtPartName.Text, txtDescription.Text, "", txtVendorName.Text, "", dbClss.DeptSC,"") select ix).ToList();
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
          //  this.Cursor = Cursors.WaitCursor;
          //  //CreatePart sc = new CreatePart();
           // this.Cursor = Cursors.Default;

            try
            {
                int dgvNo = 0;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    radGridView1.EndEdit();
                    foreach (GridViewRowInfo rd in radGridView1.Rows)
                    {

                        if (Convert.ToBoolean(rd.Cells["S"].Value))
                        {
                            var r = (from i in db.tb_Items
                                         //join s in db.tb_Stocks on i.CodeNo equals s.RefNo

                                     where i.Status == "Active" //&& d.verticalID == VerticalID
                                        && i.CodeNo == rd.Cells["CodeNo"].Value.ToString()
                                        && i.Dept == dbClss.DeptSC
                                     //&& h.VendorNo.Contains(VendorNo_ss)
                                     select new
                                     {
                                         CodeNo = i.CodeNo,
                                         ItemNo = i.ItemNo,
                                         ItemDescription = i.ItemDescription,
                                         RemainQty = (Convert.ToDecimal(db.Cal_QTY(i.CodeNo, "", 0))),
                                         UnitShip = i.UnitShip,
                                         PCSUnit = 1,// i.PCSUnit,
                                         StandardCodt = i.StandardCost / i.PCSUnit,// Convert.ToDecimal(db.Cal_CostAVG(i.CodeNo)),  //i.StandardCost,//Convert.ToDecimal(dbClss.Get_Stock(i.CodeNo, "", "", "Avg")),//i.StandardCost
                                         Amount = 0,
                                         QTY = 0,
                                         LotNo = "",
                                         SerialNo = "",
                                         MachineName = "",
                                         LineName = "",
                                         Remark = "",
                                         id = 0,
                                         ShelfNo=i.ShelfNo,
                                         ToolLife=i.Toollife,
                                         RemarkTool=i.Remark

                                     }
                            ).ToList();



                            if (r.Count > 0)
                            {
                                dgvNo = dgvData.Rows.Count() + 1;

                                foreach (var vv in r)
                                {
                                    //dgvData.Rows.Add(dgvNo.ToString(), vv.CodeNo, vv.ItemNo, vv.ItemDescription
                                    //            , vv.RemainQty, vv.QTY, vv.UnitShip, vv.ShelfNo, vv.PCSUnit, vv.StandardCodt, vv.Amount,
                                    //            vv.ToolLife,vv.LotNo, vv.SerialNo, vv.MachineName, vv.LineName, vv.Remark, vv.id, 0,"",vv.RemarkTool
                                    //            );
                                    dgvData.Rows.Add(dgvNo.ToString(), vv.CodeNo, vv.ItemNo, vv.ItemDescription
                                           , vv.RemainQty, vv.QTY, vv.UnitShip, vv.ShelfNo, vv.ToolLife, vv.LotNo, vv.MachineName, vv.LineName, vv.SerialNo, vv.Remark, vv.RemarkTool, "", vv.PCSUnit, vv.id, 0, 0, 0
                                           );
                                }

                            }
                        }
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            //sc.ShowDialog();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            ClassLib.Memory.Heap();
            this.Close();

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
    }
}
