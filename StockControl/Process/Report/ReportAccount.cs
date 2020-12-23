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

namespace StockControl
{
    public partial class ReportAccount : Telerik.WinControls.UI.RadRibbonForm
    {
        public ReportAccount()
        {
            InitializeComponent();
            CallLang();
        }
        private void CallLang()
        {
            if(dbClss.Language.Equals("ENG"))
            {
                this.Text = "Report For Account";
                radLabelElement1.Text = "Report For Account";

                btnCal.Text = "Calculate";
                btnView.Text = "Export Report";
                btnRefresh.Text = "Refresh";
                radButtonElement1.Text = "Shipping List";
                radButtonElement2.Text = "Receipt List";
                btnFind.Text = "Search..";
                radLabel3.Text = "To";
                btnFilter1.Text = "Filter";
                btnUnfilter1.Text = "Unfilter";
                radButtonElement10.Text = "Filter";
                radButtonElement11.Text = "Unfilter";
            }
        }
        //private int RowView = 50;
        //private int ColView = 10;
        DataTable dt = new DataTable();
        private void radMenuItem2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            HistoryView hw = new HistoryView(this.Name);
            this.Cursor = Cursors.Default;
            hw.ShowDialog();
        }

        private void radRibbonBar1_Click(object sender, EventArgs e)
        {

        }
        private void GETDTRow()
        {

            //dt.Columns.Add(new DataColumn("CodeNo", typeof(string)));
            //dt.Columns.Add(new DataColumn("ItemDescription", typeof(string)));
            //dt.Columns.Add(new DataColumn("Order", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("BackOrder", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("StockQty", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("UnitBuy", typeof(string)));
            //dt.Columns.Add(new DataColumn("PCSUnit", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("LeadTime", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("MaxStock", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("MinStock", typeof(decimal)));
            //dt.Columns.Add(new DataColumn("VendorNo", typeof(string)));
            //dt.Columns.Add(new DataColumn("VendorName", typeof(string)));



        }
        private void Group_Gridview()
        {
            try
            {
                ColumnGroupsViewDefinition view = new ColumnGroupsViewDefinition();
                view.ColumnGroups.Add(new GridViewColumnGroup("Item Information"));
                view.ColumnGroups.Add(new GridViewColumnGroup("Purchase Information"));
                view.ColumnGroups.Add(new GridViewColumnGroup("Current Inventory"));
                view.ColumnGroups.Add(new GridViewColumnGroup("On Order"));
                view.ColumnGroups.Add(new GridViewColumnGroup("Recived"));
                view.ColumnGroups.Add(new GridViewColumnGroup("Backorder"));
                view.ColumnGroups.Add(new GridViewColumnGroup("Supplied"));
                view.ColumnGroups.Add(new GridViewColumnGroup(""));

                view.ColumnGroups[0].Rows.Add(new GridViewColumnGroupRow());
                /*
                view.ColumnGroups[0].Rows[0].Columns.Add(this.dgvData.Columns["No"]);
                view.ColumnGroups[0].Rows[0].Columns.Add(this.dgvData.Columns["InventoryID"]);
                view.ColumnGroups[0].Rows[0].Columns.Add(this.dgvData.Columns["Name"]);
                view.ColumnGroups[0].Rows[0].Columns.Add(this.dgvData.Columns["Description"]);
                view.ColumnGroups[0].Rows[0].Columns.Add(this.dgvData.Columns["Area"]);
                view.ColumnGroups[0].Rows[0].Columns.Add(this.dgvData.Columns["ShelfBin"]);

                view.ColumnGroups[1].Rows.Add(new GridViewColumnGroupRow());
                view.ColumnGroups[1].Rows[0].Columns.Add(this.dgvData.Columns["Maker"]);
                view.ColumnGroups[1].Rows[0].Columns.Add(this.dgvData.Columns["Supplier"]);
                view.ColumnGroups[1].Rows[0].Columns.Add(this.dgvData.Columns["Price"]);
                view.ColumnGroups[1].Rows[0].Columns.Add(this.dgvData.Columns["Leadtime"]);
                view.ColumnGroups[1].Rows[0].Columns.Add(this.dgvData.Columns["MinStock"]);
                view.ColumnGroups[1].Rows[0].Columns.Add(this.dgvData.Columns["Plan"]);

                view.ColumnGroups[2].Rows.Add(new GridViewColumnGroupRow());
                view.ColumnGroups[2].Rows[0].Columns.Add(this.dgvData.Columns["CurrentInventory_Qty"]);
                view.ColumnGroups[2].Rows[0].Columns.Add(this.dgvData.Columns["CurrentInventory_Velue"]);

                view.ColumnGroups[3].Rows.Add(new GridViewColumnGroupRow());
                view.ColumnGroups[3].Rows[0].Columns.Add(this.dgvData.Columns["OnOrder_Qty"]);
                view.ColumnGroups[3].Rows[0].Columns.Add(this.dgvData.Columns["OnOrder_Value"]);

                view.ColumnGroups[4].Rows.Add(new GridViewColumnGroupRow());
                view.ColumnGroups[4].Rows[0].Columns.Add(this.dgvData.Columns["Receive_Qty"]);
                view.ColumnGroups[4].Rows[0].Columns.Add(this.dgvData.Columns["Receive_Value"]);

                view.ColumnGroups[5].Rows.Add(new GridViewColumnGroupRow());
                view.ColumnGroups[5].Rows[0].Columns.Add(this.dgvData.Columns["BackOrder_Qty"]);
                view.ColumnGroups[5].Rows[0].Columns.Add(this.dgvData.Columns["BackOrder_Value"]);

                view.ColumnGroups[6].Rows.Add(new GridViewColumnGroupRow());
                view.ColumnGroups[6].Rows[0].Columns.Add(this.dgvData.Columns["Supplied_Qty"]);
                view.ColumnGroups[6].Rows[0].Columns.Add(this.dgvData.Columns["Supplied_Value"]);

                view.ColumnGroups[7].Rows.Add(new GridViewColumnGroupRow());
                view.ColumnGroups[7].Rows[0].Columns.Add(this.dgvData.Columns["Balance_Value"]);
                view.ColumnGroups[7].Rows[0].Columns.Add(this.dgvData.Columns["GrandTotal_Value"]);
                view.ColumnGroups[7].Rows[0].Columns.Add(this.dgvData.Columns["Remark"]);
                */
                

                dgvData.ViewDefinition = view;
            }catch(Exception ex) { MessageBox.Show(ex.Message); }

        }
        int crow = 99;
        private void Unit_Load(object sender, EventArgs e)
        {

            DateTime firstOfNextMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1);
            
            DateTime lastOfThisMonth = firstOfNextMonth.AddDays(-1);
            //firstOfNextMonth = Convert.ToDateTime(DateTime.Today.ToString("yyyy-mm-01"));
            string aa = DateTime.Today.ToString("yyyy-MM-01");
            dtDate1.Value = Convert.ToDateTime(aa);
            dtDate2.Value = lastOfThisMonth;
            // GETDTRow();
            //DefaultItem();

            //Group_Gridview();
            GridViewSummaryItem summaryItemShipName1 = new GridViewSummaryItem("BL_Amount", "{0:N2}", GridAggregateFunction.Sum);
            GridViewSummaryItem summaryItemShipName2 = new GridViewSummaryItem("BL_Qty", "{0:N2}", GridAggregateFunction.Sum);
            GridViewSummaryItem summaryItemShipName3 = new GridViewSummaryItem("BL_Qty2", "{0:N2}", GridAggregateFunction.Sum);
            GridViewSummaryItem summaryItemShipName4 = new GridViewSummaryItem("B_Amount", "{0:N2}", GridAggregateFunction.Sum);
            GridViewSummaryItem summaryItemShipName5 = new GridViewSummaryItem("R_Amount", "{0:N2}", GridAggregateFunction.Sum);
            GridViewSummaryItem summaryItemShipName6 = new GridViewSummaryItem("S_Amount", "{0:N2}", GridAggregateFunction.Sum);

            GridViewSummaryItem summaryItemShipName7 = new GridViewSummaryItem("B_Qty", "{0:N2}", GridAggregateFunction.Sum);
            GridViewSummaryItem summaryItemShipName8 = new GridViewSummaryItem("B_Qty2", "{0:N2}", GridAggregateFunction.Sum);
            GridViewSummaryItem summaryItemShipName9 = new GridViewSummaryItem("S_Qty", "{0:N2}", GridAggregateFunction.Sum);
            GridViewSummaryItem summaryItemShipName10 = new GridViewSummaryItem("S_Qty2", "{0:N2}", GridAggregateFunction.Sum);
            GridViewSummaryItem summaryItemShipName11 = new GridViewSummaryItem("R_Qty", "{0:N2}", GridAggregateFunction.Sum);
            GridViewSummaryItem summaryItemShipName12 = new GridViewSummaryItem("R_Qty2", "{0:N2}", GridAggregateFunction.Sum);

            //GridViewSummaryItem summaryItemFreight = new GridViewSummaryItem("RM", "Remain = {0:N2}", GridAggregateFunction.Sum);
            GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem(
                new GridViewSummaryItem[] { summaryItemShipName1,summaryItemShipName2, summaryItemShipName3, summaryItemShipName4, summaryItemShipName5, summaryItemShipName6,
                summaryItemShipName7,summaryItemShipName8, summaryItemShipName9, summaryItemShipName10, summaryItemShipName11, summaryItemShipName12
                });

            this.dgvData.SummaryRowsTop.Add(summaryRowItem);

            crow = 0;
        }

        private void DefaultItem()
        {
            
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var gt = (from ix in db.tb_GroupTypes where ix.GroupActive == true select ix).ToList();
                //GridViewComboBoxColumn comboBoxColumn = this.radGridView1.Columns["GroupCode"] as GridViewComboBoxColumn;
                 cboGroupType.DisplayMember = "GroupCode";
                 cboGroupType.ValueMember = "GroupCode";
                 cboGroupType.DataSource = gt;
                cboGroupType.SelectedIndex = -1;
                }
        }
        private void DataLoad()
        {
            //dt.Rows.Clear();
            
            try
            {

                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //dt = ClassLib.Classlib.LINQToDataTable(db.tb_Units.ToList());
                    try
                    {
                        // int year1 = 2017;

                        //var gd = (from ix in db.tb_ForcastCalculates
                        //          where ix.MMM == dbClss.getMonth(cboMonth.Text) && ix.YYYY == year1
                        //          select new { ix.YYYY, ix.MMM, Month = dbClss.getMonthRevest(ix.MMM)
                        //          , ix.CodeNo
                        //          , ItemDescription =db.tb_Items.Where(s => s.CodeNo == ix.CodeNo).Select(o => o.ItemDescription).FirstOrDefault()
                        //          ,ix.ForeCastQty,ix.Toolife_spc,ix.SumQty,ix.ExtendQty,ix.UsePerDay,ix.LeadTime,ix.KeepStock,ix.AddErrQty,ix.OrderQty}).ToList();
                        var gd = (from a in db.tb_Items

                                  select new {
                                      CodeNo = a.CodeNo,
                                      ItemDescription = a.ItemDescription,
                                      Order = 10,
                                      StockQty = 0,
                                      BackOrder = 0,
                                      UnitBuy = "PCS",
                                      PCSUnit = 1,
                                      LeadTime = a.Leadtime,
                                      MaxStock = a.MaximumStock,
                                      MinStock = a.MinimumStock,
                                      VendorNo = "V0001",
                                      VendorName = "HHL Interade Co.,LTD.",
                                      CreateDate = DateTime.Now,
                                      CreateBy = "Administrator",
                                      Status = "รับเข้าแล้ว",
                                      ItemName = a.ItemNo,
                                      Delivery = DateTime.Now,
                                      PRNo="PR201705-0001",
                                      ReceiveNo="RC201705-001",
                                      Cost=1000
                                   
                                  }).ToList();
                        //radGridView1.DataSource = gd;

                        //int rowcount = 0;
                        //foreach (var x in radGridView1.Rows)
                        //{
                        //    rowcount += 1;
                        //    x.Cells["dgvNo"].Value = rowcount;
                        //    x.Cells["dgvCodeTemp"].Value = x.Cells["CodeNo"].Value.ToString();
                        //    x.Cells["dgvCodeTemp2"].Value = x.Cells["VendorNo"].Value.ToString();
                        //    //x.Cells["dgvCodeTemp3"].Value = x.Cells["MMM"].Value.ToString();
                        //    //  MessageBox.Show("ss");
                        //    // x.Cells["ModelName"].ReadOnly = true;
                        //    //x.Cells["YYYY"].ReadOnly = true;
                        //    //x.Cells["MMM"].ReadOnly = true;
                        //}
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }

                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;


            //    radGridView1.DataSource = dt;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("ต้องการออกรายงาน หรือไม่ ?","ออกรายงาน",MessageBoxButtons.OKCancel,MessageBoxIcon.Question)==DialogResult.OK)
            {
                saveFileDialog1.Filter = "Excel|*.xls";
                saveFileDialog1.Title = "Save an Excel File";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    dbClss.ExportGridXlSX2(dgvData, saveFileDialog1.FileName);
                    //if (GetData2())
                       MessageBox.Show("Export Report Completed.");

                }

            }
        }
        private bool GetData(string FileName)
        {
            bool ck = false;
            this.Cursor = Cursors.WaitCursor ;
            try
            {

                //System.IO.File.Copy(Report.CRRReport.dbPartReport + "Account_Sheet.xls", FileName, true);
                ////System.Diagnostics.Process.Start();
                //dbClss.AddHistory(this.Name, "ออกรายงาน", "เลือกออกรายงาน "+dtDate1.Value.ToString("dd/MMM/yyyy")+"-"+dtDate2.Value.ToString("dd/MMM/yyyy"), "");
                //ck = true;

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //    string date1 = "";
                    //    string date2 = "";
                    //    date1 = dtDate1.Value.ToString("yyyyMMdd");
                    //    date2 = dtDate2.Value.ToString("yyyyMMdd");
                    //    radGridView1.AutoGenerateColumns = true;
                    //    radGridView1.DataSource = db.sp_E008_ReportAccount(date1, date2, cboGroupType.Text);
                    //}
                    //dbClss.ExportGridXlSX2(radGridView1, FileName);

                    var g = (from ix in db.sp_SS_Account_SelectItem() select ix).ToList().ToList();
                    if (g.Count > 0)
                    {
                        progressBar1.Visible = true;
                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = g.Count;
                        int value = 0;
                        foreach (var r in g)
                        {
                            value += 1;
                            progressBar1.Value = value;
                            progressBar1.PerformStep();
                            db.sp_SS_Account_Insert(r.CodeNo);
                        }
                    }
                }
                System.Diagnostics.Process.Start(@"Report\FM-EN-23 Rev. 01Tool Inventory Control .xlsx");
                dbClss.AddHistory(this.Name, "ออกรายงาน", "เลือกออกรายงาน Report Account "+dtDate1.Value.ToString("dd/MMM/yyyy"), "");
                ck = true;


            }
            catch { ck = false; }
            progressBar1.Visible = false;
            this.Cursor = Cursors.Default;
            return ck;
        }
        private bool GetData2()
        {
            bool ck = false;
            string FileName = "FM-EN-23 Rev. 01Tool Inventory Control 2.xlsx";
            object missing = System.Reflection.Missing.Value;
            this.Cursor = Cursors.WaitCursor;
            try
            {

                try
                {
                    File.Delete(Path.GetTempPath() + FileName);
                }
                catch { }
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"Report\FM-EN-23 Rev. 01Tool Inventory Control 2.xlsx", Path.GetTempPath() + FileName, true);

                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook excelBook = excelApp.Workbooks.Open(Path.GetTempPath() + FileName);
                Excel._Worksheet excelWorksheet = (Excel._Worksheet)excelBook.Worksheets.get_Item(1);

                
                excelWorksheet.Cells[4, 2] = Convert.ToDateTime(dtDate1.Value).ToString("dd/MMM/yyyy") + Convert.ToDateTime(dtDate2.Value).ToString("dd/MMM/yyyy");
                //excelWorksheet.Cells[4, 13] = Convert.ToDecimal(txtinventoryTotal.Text);
                //excelWorksheet.Cells[4, 15] = Convert.ToDecimal(txtOnOrderTotal.Text);
                //excelWorksheet.Cells[4, 17] = Convert.ToDecimal(txtReceivedTotal.Text);
                //excelWorksheet.Cells[4, 19] = Convert.ToDecimal(txtbackorderTotal.Text);
                //excelWorksheet.Cells[4, 21] = Convert.ToDecimal(txtsuppliedTotal.Text);
                //excelWorksheet.Cells[4, 23] = Convert.ToDecimal(txtBalanceTotal.Text);

                decimal inventoryTotal = 0;
                decimal OnOrderTotal = 0;
                decimal ReceivedTotal = 0;
                decimal backorderTotal = 0;
                decimal suppliedTotal = 0;
                decimal BalanceTotal = 0;

                progressBar1.Visible = true;
                progressBar1.Maximum = 2;//37
                progressBar1.Minimum = 1;
                int i = 1;
                int Rowcc = 8;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var g = (from ix in db.sp_013_Select_ReportAccount(dtDate1.Value, dtDate2.Value) select ix).ToList().ToList();
                    if (g.Count > 0)
                    {
                        progressBar1.Maximum = g.Count;
                        foreach (var Row in g)
                        {
                            progressBar1.Value = i;
                            progressBar1.PerformStep();

                            excelWorksheet.Cells[Rowcc, 1] = Convert.ToString(Row.InventoryID);
                            excelWorksheet.Cells[Rowcc, 2] = Convert.ToString(Row.Name);
                            excelWorksheet.Cells[Rowcc, 3] = Convert.ToString(Row.Description);
                            excelWorksheet.Cells[Rowcc, 4] = Convert.ToString(Row.Area);
                            excelWorksheet.Cells[Rowcc, 5] = Convert.ToString(Row.ShelfBin);
                            excelWorksheet.Cells[Rowcc, 6] = Convert.ToString(Row.Maker);
                            excelWorksheet.Cells[Rowcc, 7] = Convert.ToString(Row.Supplier);
                            excelWorksheet.Cells[Rowcc, 8] = Convert.ToDecimal(Row.Price);
                            excelWorksheet.Cells[Rowcc, 9] = Convert.ToInt16(Row.Leadtime);
                            excelWorksheet.Cells[Rowcc, 10] = Convert.ToDecimal(Row.MinStock);
                            excelWorksheet.Cells[Rowcc, 11] = Convert.ToDecimal(Row.Plan);
                            excelWorksheet.Cells[Rowcc, 12] = Convert.ToDecimal(Row.CurrentInventory_Qty);
                            excelWorksheet.Cells[Rowcc, 13] = Convert.ToDecimal(Row.CurrentInventory_Velue);
                            excelWorksheet.Cells[Rowcc, 14] = Convert.ToDecimal(Row.OnOrder_Qty);
                            excelWorksheet.Cells[Rowcc, 15] = Convert.ToDecimal(Row.OnOrder_Value);
                            excelWorksheet.Cells[Rowcc, 16] = Convert.ToDecimal(Row.Receive_Qty);
                            excelWorksheet.Cells[Rowcc, 17] = Convert.ToDecimal(Row.Receive_Value);
                            excelWorksheet.Cells[Rowcc, 18] = Convert.ToDecimal(Row.BackOrder_Qty);
                            excelWorksheet.Cells[Rowcc, 19] = Convert.ToDecimal(Row.BackOrder_Value);
                            excelWorksheet.Cells[Rowcc, 20] = Convert.ToDecimal(Row.Supplied_Qty);
                            excelWorksheet.Cells[Rowcc, 21] = Convert.ToDecimal(Row.Supplied_Value);
                            excelWorksheet.Cells[Rowcc, 22] = Convert.ToDecimal(Row.Balance_Value);
                            excelWorksheet.Cells[Rowcc, 23] = Convert.ToDecimal(Row.GrandTotal_Value);
                            excelWorksheet.Cells[Rowcc, 24] = Convert.ToString(Row.Remark);

                            inventoryTotal = inventoryTotal + Convert.ToDecimal(Row.CurrentInventory_Velue);
                            OnOrderTotal = inventoryTotal + Convert.ToDecimal(Row.OnOrder_Value);
                            ReceivedTotal = inventoryTotal + Convert.ToDecimal(Row.Receive_Value);
                            backorderTotal = inventoryTotal + Convert.ToDecimal(Row.BackOrder_Value);
                            suppliedTotal = inventoryTotal + Convert.ToDecimal(Row.Supplied_Value);
                            BalanceTotal = inventoryTotal + Convert.ToDecimal(Row.Balance_Value);

                            i++;
                            Rowcc++;
                        }
                    }
                }
                progressBar1.PerformStep();
                progressBar1.Visible = false;

                excelWorksheet.Cells[4, 13] = Convert.ToDecimal(txtinventoryTotal.Text);
                excelWorksheet.Cells[4, 15] = Convert.ToDecimal(txtOnOrderTotal.Text);
                excelWorksheet.Cells[4, 17] = Convert.ToDecimal(txtReceivedTotal.Text);
                excelWorksheet.Cells[4, 19] = Convert.ToDecimal(txtbackorderTotal.Text);
                excelWorksheet.Cells[4, 21] = Convert.ToDecimal(txtsuppliedTotal.Text);
                excelWorksheet.Cells[4, 23] = Convert.ToDecimal(txtBalanceTotal.Text);

                releaseObject(excelWorksheet);
                releaseObject(excelBook);
                releaseObject(excelApp);
                System.Diagnostics.Process.Start(Path.GetTempPath() + FileName);

                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);

                //System.Diagnostics.Process.Start(@"Report\FM-EN-23 Rev. 01Tool Inventory Control 2.xlsx");

                dbClss.AddHistory(this.Name, "ออกรายงาน", "เลือกออกรายงาน Report Account " + dtDate1.Value.ToString("dd/MMM/yyyy"), "");
                ck = true;


            }
            catch { ck = false; }
            progressBar1.Visible = false;
            this.Cursor = Cursors.Default;
            return ck;
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
        private void btnFind_Click(object sender, EventArgs e)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                int value = 0;
                var g = db.spx_014_StockCard2_Select(dbClss.DeptSC, txtTool.Text, txtGroup.Text).ToList();
                dgvData.DataSource = g;
                if (dgvData.Rows.Count > 0)
                {
                    value = 0;
                    foreach (GridViewRowInfo rd in dgvData.Rows)
                    {
                        value += 1;
                        rd.Cells["No"].Value = value;
                    }
                }
            }
        }
        private void CalculateFF()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    progressBar1.Visible = true;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = 1;
                    dgvData.Rows.Clear();
                    dgvData.DataSource = null;
                    int value = 0;
                    db.spx_013_StockCard2_Delete();

                    if (chkAllDept.Checked) // All Dept
                    {
                        var gt = db.tb_Items.Where(i => i.Status == "Active").ToList();
                        progressBar1.Maximum = gt.Count + 1;
                        foreach (var rd in gt)
                        {
                            value += 1;
                            progressBar1.Value = value;
                            db.spx_013_StockCard2(dtDate1.Value, dtDate2.Value, rd.CodeNo);
                            db.spx_013_StockCard2_23(dtDate1.Value, dtDate2.Value, rd.CodeNo);
                            db.spx_013_StockCard2_24(dtDate1.Value, dtDate2.Value, rd.CodeNo);
                            db.spx_013_StockCard2_22(dtDate1.Value, dtDate2.Value, rd.CodeNo);
                            progressBar1.PerformStep();
                        }

                        var g = db.spx_014_StockCard2_Select("", txtTool.Text, txtGroup.Text).ToList();
                        dgvData.DataSource = g;
                        if (dgvData.Rows.Count > 0)
                        {
                            value = 0;
                            foreach (GridViewRowInfo rd in dgvData.Rows)
                            {
                                value += 1;
                                rd.Cells["No"].Value = value;
                            }
                        }
                    }
                    else //For Dept Only
                    {

                        var gt = db.tb_Items.Where(i => i.Dept == dbClss.DeptSC && i.Status == "Active").ToList();
                        progressBar1.Maximum = gt.Count + 1;
                        foreach (var rd in gt)
                        {
                            value += 1;
                            progressBar1.Value = value;
                            db.spx_013_StockCard2(dtDate1.Value, dtDate2.Value, rd.CodeNo);
                            db.spx_013_StockCard2_23(dtDate1.Value, dtDate2.Value, rd.CodeNo);
                            db.spx_013_StockCard2_24(dtDate1.Value, dtDate2.Value, rd.CodeNo);
                            db.spx_013_StockCard2_22(dtDate1.Value, dtDate2.Value, rd.CodeNo);
                            progressBar1.PerformStep();
                        }

                        var g = db.spx_014_StockCard2_Select(dbClss.DeptSC, txtTool.Text, txtGroup.Text).ToList();
                        dgvData.DataSource = g;
                        if (dgvData.Rows.Count > 0)
                        {
                            value = 0;
                            foreach (GridViewRowInfo rd in dgvData.Rows)
                            {
                                value += 1;
                                rd.Cells["No"].Value = value;
                            }
                        }
                    }



                    progressBar1.Visible = false;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); dbClss.AddError("ReportAccount", ex.Message, this.Name); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void btnCal_Click(object sender, EventArgs e)
        {           
            CalculateFF();
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการออกรายงาน Shipment หรือไม่ ?", "ออกรายงาน", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                saveFileDialog1.Filter = "Excel|*.xls";
                saveFileDialog1.Title = "Save an Excel File";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    if (ExportshippingGroup(saveFileDialog1.FileName))
                        MessageBox.Show("Export Report Completed.");

                }

            }
        }
        private bool ExportshippingGroup(string FileName)
        {
            bool ck = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {

                //System.IO.File.Copy(Report.CRRReport.dbPartReport + "Account_Sheet.xls", FileName, true);
                //System.Diagnostics.Process.Start();
                radGridView1.DataSource = null;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    string date1 = "";
                    string date2 = "";
                    date1 = dtDate1.Value.ToString("yyyyMMdd");
                    date2 = dtDate2.Value.ToString("yyyyMMdd");

                    radGridView1.AutoGenerateColumns = true;
                    radGridView1.DataSource = db.spx_015_SelectShipping(dbClss.DeptSC, dtDate1.Value, dtDate2.Value);
                }
                dbClss.ExportGridXlSX2(radGridView1, FileName);
               // dbClss.AddHistory(this.Name, "ออกรายงาน", "เลือกออกรายงาน ", "ShippingGroup");
                ck = true;

            }
            catch { ck = false; }
            this.Cursor = Cursors.Default;
            return ck;
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการออกรายงาน Receive หรือไม่ ?", "ออกรายงาน", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                saveFileDialog1.Filter = "Excel|*.xls";
                saveFileDialog1.Title = "Save an Excel File";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    if (ExportshippingGroup2(saveFileDialog1.FileName))
                        MessageBox.Show("Export Report Completed.");

                }

            }
        }
        private bool ExportshippingGroup2(string FileName)
        {
            bool ck = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {

                //System.IO.File.Copy(Report.CRRReport.dbPartReport + "Account_Sheet.xls", FileName, true);
                //System.Diagnostics.Process.Start();
                radGridView1.DataSource = null;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    string date1 = "";
                    string date2 = "";
                    date1 = dtDate1.Value.ToString("yyyyMMdd");
                    date2 = dtDate2.Value.ToString("yyyyMMdd");

                    radGridView1.AutoGenerateColumns = true;
                    radGridView1.DataSource = db.spx_015_SelectReceive(dbClss.DeptSC, dtDate1.Value, dtDate2.Value);
                }
                dbClss.ExportGridXlSX2(radGridView1, FileName);
                // dbClss.AddHistory(this.Name, "ออกรายงาน", "เลือกออกรายงาน ", "ShippingGroup");
                ck = true;

            }
            catch { ck = false; }
            this.Cursor = Cursors.Default;
            return ck;
        }

        private void radButtonElement3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการออกรายงาน Group (Shipping) หรือไม่ ?", "ออกรายงาน", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                saveFileDialog1.Filter = "Excel|*.xls";
                saveFileDialog1.Title = "Save an Excel File";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    if (ExportshippingGroup3(saveFileDialog1.FileName))
                        MessageBox.Show("Export Report Completed.");

                }

            }
        }
        private bool ExportshippingGroup3(string FileName)
        {
            bool ck = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {

                //System.IO.File.Copy(Report.CRRReport.dbPartReport + "Account_Sheet.xls", FileName, true);
                //System.Diagnostics.Process.Start();
                radGridView1.DataSource = null;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    string date1 = "";
                    string date2 = "";
                    date1 = dtDate1.Value.ToString("yyyyMMdd");
                    date2 = dtDate2.Value.ToString("yyyyMMdd");

                    radGridView1.AutoGenerateColumns = true;
                    radGridView1.DataSource = db.sp_E003_ReportShipping2(date1, date2, "",dbClss.DeptSC);
                }
                dbClss.ExportGridXlSX2(radGridView1, FileName);
                // dbClss.AddHistory(this.Name, "ออกรายงาน", "เลือกออกรายงาน ", "ShippingGroup");
                ck = true;

            }
            catch { ck = false; }
            this.Cursor = Cursors.Default;
            return ck;
        }

        private void radButtonElement4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการออกรายงาน Group (LineName) หรือไม่ ?", "ออกรายงาน", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                saveFileDialog1.Filter = "Excel|*.xls";
                saveFileDialog1.Title = "Save an Excel File";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    if (ExportshippingGroup4(saveFileDialog1.FileName))
                        MessageBox.Show("Export Report Completed.");

                }

            }
        }
        private bool ExportshippingGroup4(string FileName)
        {
            bool ck = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {

                //System.IO.File.Copy(Report.CRRReport.dbPartReport + "Account_Sheet.xls", FileName, true);
                //System.Diagnostics.Process.Start();
                radGridView1.DataSource = null;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    string date1 = "";
                    string date2 = "";
                    date1 = dtDate1.Value.ToString("yyyyMMdd");
                    date2 = dtDate2.Value.ToString("yyyyMMdd");

                    radGridView1.AutoGenerateColumns = true;
                    radGridView1.DataSource = db.sp_E003_ReportShipping2_2(date1, date2, "", dbClss.DeptSC);
                }
                dbClss.ExportGridXlSX2(radGridView1, FileName);
                // dbClss.AddHistory(this.Name, "ออกรายงาน", "เลือกออกรายงาน ", "ShippingGroup");
                ck = true;

            }
            catch { ck = false; }
            this.Cursor = Cursors.Default;
            return ck;
        }

        private void radButtonElement5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการออกรายงาน Group (Machine) หรือไม่ ?", "ออกรายงาน", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                saveFileDialog1.Filter = "Excel|*.xls";
                saveFileDialog1.Title = "Save an Excel File";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    if (ExportshippingGroup5(saveFileDialog1.FileName))
                        MessageBox.Show("Export Report Completed.");

                }

            }
        }

        private void radButtonElement6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการออกรายงาน Group (MOLD) หรือไม่ ?", "ออกรายงาน", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                saveFileDialog1.Filter = "Excel|*.xls";
                saveFileDialog1.Title = "Save an Excel File";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    if (ExportshippingGroup6(saveFileDialog1.FileName))
                        MessageBox.Show("Export Report Completed.");

                }

            }
        }
        private bool ExportshippingGroup5(string FileName)
        {
            bool ck = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {

                //System.IO.File.Copy(Report.CRRReport.dbPartReport + "Account_Sheet.xls", FileName, true);
                //System.Diagnostics.Process.Start();
                radGridView1.DataSource = null;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    string date1 = "";
                    string date2 = "";
                    date1 = dtDate1.Value.ToString("yyyyMMdd");
                    date2 = dtDate2.Value.ToString("yyyyMMdd");

                    radGridView1.AutoGenerateColumns = true;
                    radGridView1.DataSource = db.sp_E003_ReportShipping2_3(date1, date2, "", dbClss.DeptSC);
                }
                dbClss.ExportGridXlSX2(radGridView1, FileName);
                // dbClss.AddHistory(this.Name, "ออกรายงาน", "เลือกออกรายงาน ", "ShippingGroup");
                ck = true;

            }
            catch { ck = false; }
            this.Cursor = Cursors.Default;
            return ck;
        }
        private bool ExportshippingGroup6(string FileName)
        {
            bool ck = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {

                //System.IO.File.Copy(Report.CRRReport.dbPartReport + "Account_Sheet.xls", FileName, true);
                //System.Diagnostics.Process.Start();
                radGridView1.DataSource = null;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    string date1 = "";
                    string date2 = "";
                    date1 = dtDate1.Value.ToString("yyyyMMdd");
                    date2 = dtDate2.Value.ToString("yyyyMMdd");

                    radGridView1.AutoGenerateColumns = true;
                    radGridView1.DataSource = db.sp_E003_ReportShipping2_4(date1, date2, "", dbClss.DeptSC);
                }
                dbClss.ExportGridXlSX2(radGridView1, FileName);
                // dbClss.AddHistory(this.Name, "ออกรายงาน", "เลือกออกรายงาน ", "ShippingGroup");
                ck = true;

            }
            catch { ck = false; }
            this.Cursor = Cursors.Default;
            return ck;
        }

        private void radButtonElement7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการออกรายงาน Summary by Item (Cost) หรือไม่ ?", "ออกรายงาน", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                saveFileDialog1.Filter = "Excel|*.xls";
                saveFileDialog1.Title = "Save an Excel File";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    if (ExportshippingGroup7(saveFileDialog1.FileName))
                        MessageBox.Show("Export Report Completed.");

                }

            }
        }
        private bool ExportshippingGroup7(string FileName)
        {
            bool ck = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {

                //System.IO.File.Copy(Report.CRRReport.dbPartReport + "Account_Sheet.xls", FileName, true);
                //System.Diagnostics.Process.Start();
                radGridView1.DataSource = null;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    string date1 = "";
                    string date2 = "";
                    date1 = dtDate1.Value.ToString("yyyyMMdd");
                    date2 = dtDate2.Value.ToString("yyyyMMdd");
                    db.spx24_ListReportForSummaryByCost_New(dbClss.DeptSC, dtDate1.Value, dtDate2.Value);
                    radGridView1.AutoGenerateColumns = true;
                    radGridView1.DataSource = db.spx24_ListReportForSummaryByCost(dbClss.DeptSC).ToList();
                }
                dbClss.ExportGridXlSX2(radGridView1, FileName);
                // dbClss.AddHistory(this.Name, "ออกรายงาน", "เลือกออกรายงาน ", "ShippingGroup");
                ck = true;

            }
            catch { ck = false; }
            this.Cursor = Cursors.Default;
            return ck;
        }

        private void radButtonElement8_Click(object sender, EventArgs e)
        {
            string myTempFile = Path.Combine(Path.GetTempPath(), "Export_Account01.xlsx");
            string SourceFile= Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Report\Template1.xlsx");
            try
            {
                if(File.Exists(myTempFile))
                {
                    File.Delete(myTempFile);
                }
                //System.Diagnostics.Process.Start(SourceFile);
                 File.Copy(SourceFile, myTempFile, true);
                System.Diagnostics.Process.Start(myTempFile);
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void radButtonElement9_Click(object sender, EventArgs e)
        {
            string myTempFile = Path.Combine(Path.GetTempPath(), "Export_Account02.xlsx");
            string SourceFile = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Report\Template2.xlsx");
            try
            {
                if (File.Exists(myTempFile))
                {
                    File.Delete(myTempFile);
                   
                    
                }
                File.Copy(SourceFile, myTempFile, true);
                System.Diagnostics.Process.Start(myTempFile);
            }
            catch { }
        }

        private void radButtonElement10_Click(object sender, EventArgs e)
        {

            dgvData.EnableFiltering = true;
        }

        private void radButtonElement11_Click(object sender, EventArgs e)
        {
            dgvData.EnableFiltering = false;
        }

        private void radButtonElement12_Click(object sender, EventArgs e)
        {
            try
            {                

                if (MessageBox.Show("ต้องการออกรายงาน Summary by Group Type หรือไม่ ?", "ออกรายงาน", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    saveFileDialog1.Filter = "Excel|*.xls";
                    saveFileDialog1.Title = "Save an Excel File";
                    saveFileDialog1.ShowDialog();
                    if (saveFileDialog1.FileName != "")
                    {
                        if (ExportGrouptype(saveFileDialog1.FileName))
                            MessageBox.Show("Export Report Completed.");

                    }

                }
            }
            catch { }
        }
        private bool ExportGrouptype(string FileName)
        {
            bool ck = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {

                //System.IO.File.Copy(Report.CRRReport.dbPartReport + "Account_Sheet.xls", FileName, true);
                //System.Diagnostics.Process.Start();
                radGridView1.DataSource = null;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //string date1 = "";
                    //string date2 = "";
                    //date1 = dtDate1.Value.ToString("yyyyMMdd");
                    //date2 = dtDate2.Value.ToString("yyyyMMdd");

                    radGridView1.AutoGenerateColumns = true;
                    radGridView1.DataSource = db.sp_R009_ReportGrouptype().ToList();
                }
                dbClss.ExportGridXlSX2(radGridView1, FileName);
                // dbClss.AddHistory(this.Name, "ออกรายงาน", "เลือกออกรายงาน ", "ShippingGroup");
                ck = true;

            }
            catch { ck = false; }
            this.Cursor = Cursors.Default;
            return ck;
        }

        private void radButtonElement14_Click(object sender, EventArgs e)
        {
            string myTempFile = Path.Combine(Path.GetTempPath(), "Export_Group(1).xlsx");
            string SourceFile = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Report\GroupType.xlsx");
            try
            {
                if (File.Exists(myTempFile))
                {
                    File.Delete(myTempFile);
                }
                //System.Diagnostics.Process.Start(SourceFile);
                File.Copy(SourceFile, myTempFile, true);
                System.Diagnostics.Process.Start(myTempFile);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void radButtonElement13_Click(object sender, EventArgs e)
        {
            try
            {


                if (MessageBox.Show("ต้องการออกรายงาน Summary by Group Type(2) หรือไม่ ? \n "+dtDate1.Value.ToString("dd-MMM-yyyy") +" - "+dtDate2.Value.ToString("dd-MMM-yyyy"), "ออกรายงาน", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    saveFileDialog1.Filter = "Excel|*.xls";
                    saveFileDialog1.Title = "Save an Excel File";
                    saveFileDialog1.ShowDialog();
                    if (saveFileDialog1.FileName != "")
                    {
                        if (ExportGrouptype2(saveFileDialog1.FileName))
                            MessageBox.Show("Export Report Completed.");

                    }

                }
            }
            catch { }
        }
        private bool ExportGrouptype2(string FileName)
        {
            bool ck = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {

                //System.IO.File.Copy(Report.CRRReport.dbPartReport + "Account_Sheet.xls", FileName, true);
                //System.Diagnostics.Process.Start();
                radGridView1.DataSource = null;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //string date1 = "";
                    //string date2 = "";
                    //date1 = dtDate1.Value.ToString("yyyyMMdd");
                    //date2 = dtDate2.Value.ToString("yyyyMMdd");

                    radGridView1.AutoGenerateColumns = true;
                    radGridView1.DataSource = db.sp_R010_ReportGrouptype2(dtDate1.Value,dtDate2.Value).ToList();
                }
                dbClss.ExportGridXlSX2(radGridView1, FileName);
                // dbClss.AddHistory(this.Name, "ออกรายงาน", "เลือกออกรายงาน ", "ShippingGroup");
                ck = true;

            }
            catch { ck = false; }
            this.Cursor = Cursors.Default;
            return ck;
        }

        private void radButtonElement15_Click(object sender, EventArgs e)
        {
            string myTempFile = Path.Combine(Path.GetTempPath(), "Export_Group(2).xlsx");
            string SourceFile = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Report\GroupType2.xlsx");
            try
            {
                if (File.Exists(myTempFile))
                {
                    File.Delete(myTempFile);
                }
                //System.Diagnostics.Process.Start(SourceFile);
                File.Copy(SourceFile, myTempFile, true);
                System.Diagnostics.Process.Start(myTempFile);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void radButtonElement16_Click(object sender, EventArgs e)
        {
            try
            {
                Report.Reportx1.Value = new string[2];
                Report.Reportx1.Value[0] = dtDate1.Value.ToString();
                Report.Reportx1.Value[1] = dtDate2.Value.ToString();
                Report.Reportx1.WReport = "Report_CostCenter";
                Report.Reportx1 op = new Report.Reportx1("Report_CostCenter.rpt");
                op.Show();
            }
            catch { }
        }

        private void radButtonElement17_Click(object sender, EventArgs e)
        {
            try
            {
                Report.Reportx1.Value = new string[2];
                Report.Reportx1.Value[0] = dtDate1.Value.ToString();
                Report.Reportx1.Value[1] = dtDate2.Value.ToString();
                Report.Reportx1.WReport = "Report_CostCenter";
                Report.Reportx1 op = new Report.Reportx1("Report_CostCenter2.rpt");
                op.Show();
            }
            catch { }
        }
    }
}
