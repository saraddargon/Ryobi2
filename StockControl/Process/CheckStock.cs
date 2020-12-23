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
    public partial class CheckStock : Telerik.WinControls.UI.RadRibbonForm
    {
        public CheckStock()
        {
            this.Name = "CheckStock";
            //  MessageBox.Show(this.Name);
            InitializeComponent();
            if (!dbClss.PermissionScreen(this.Name))
            {
                MessageBox.Show("Access denied", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            CallLang();

        }
        private void CallLang()
        {
            if (dbClss.Language.Equals("ENG"))
            {
                this.Text = "Check Stock";
                radLabelElement1.Text = "Check Stock";
                btnRefresh.Text = "Refresh";
                btnFilter1.Text = "Filter";
                btnUnfilter1.Text = "Unfilter";
                btnSave.Text = "New +";
                radButtonElement3.Text = "Close Doc.";
                btnPrint.Text = "Print";
                radButtonElement2.Text = "Open Doc.";
                btnExport.Text = "Export";
                radButtonElement1.Text = "Delete";
                radButton1.Text = "Search..";

                radLabel1.Text = "Check No.:";
                radLabel2.Text = "Status";

                dgvData.Columns[0].HeaderText = "No";
                dgvData.Columns[1].HeaderText = "Status";
                dgvData.Columns[2].HeaderText = "Check No.";
                dgvData.Columns[3].HeaderText = "CreateBy";
                dgvData.Columns[4].HeaderText = "CreateDate";



            }
        }

    Telerik.WinControls.UI.RadTextBox SHNo_tt = new Telerik.WinControls.UI.RadTextBox();
        Telerik.WinControls.UI.RadTextBox CodeNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
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
  
        private void Unit_Load(object sender, EventArgs e)
        {

            dgvData.ReadOnly = true;
            dgvData.AutoGenerateColumns = false;
            DataLoad();

         
        }
        private void DataLoad()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                dgvData.DataSource = null;
                dgvData.DataSource = db.spx_017_ListCheckStock(dbClss.DeptSC, txtSHNo.Text, cboStatus.Text).ToList();
                int Countss = 0;
                foreach(GridViewRowInfo rd in dgvData.Rows)
                {
                    Countss += 1;
                    rd.Cells["No"].Value = Countss;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("ต้องการสร้างใบเช็คสินค้า(ทูล)?", "สร้างเอกสารใหม่?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    progressBar1.Visible = true;
                    progressBar1.Minimum = 1;
                    progressBar1.Maximum = 0;

                    string CheckNo = dbClss.GetNo(33, 2);
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        var ii = db.tb_Items.Where(i => i.Status == "Active" && i.Dept==dbClss.DeptSC).ToList();
                        if (ii.Count > 0)
                        {
                            int countA = 0;

                            progressBar1.Maximum = ii.Count + 1;
                            tb_CheckStock cN = new tb_CheckStock();
                            cN.CheckNo = CheckNo;
                            cN.CheckDate = DateTime.Now;
                            cN.CheckBy = dbClss.UserID;
                            cN.Dept = dbClss.DeptSC;
                            cN.Status = "Process";
                            db.tb_CheckStocks.InsertOnSubmit(cN);
                            db.SubmitChanges();

                            foreach (var rd in ii)
                            {
                                countA += 1;

                                //Store//
                                tb_CheckStockList ch = new tb_CheckStockList();
                                ch.Dept = dbClss.DeptSC;
                                ch.DeptCode = rd.DeptCode;
                                ch.CodeNo = rd.CodeNo;
                                ch.Calbit = false;
                                ch.AccountCode = "";
                                ch.Amount = 0;
                                ch.CreateBy = dbClss.UserID;
                                ch.CreateDate = DateTime.Now;
                                ch.CheckNo = CheckNo;
                                ch.InputBy = "";
                                ch.InputDate = null;
                                ch.InputQty = null;
                                ch.ItemDescription = rd.ItemDescription;
                                ch.ItemNo = rd.ItemNo;
                                ch.LotNo = "";
                                ch.PCSUnit = rd.PCSUnit;
                                ch.Unit = rd.UnitShip;
                                ch.Status = "Waiting";
                                ch.StockType = "Invoice";
                                ch.StandardCost = 0;
                                ch.SerialNo = "";
                                ch.Seq = countA;
                                ch.Remark = rd.Remark;
                                ch.Reason = "";
                                ch.Qty = rd.StockInv;
                                ch.ShelfNo = rd.ShelfNo;
                                ch.GroupType = rd.GroupCode ;
                                ch.InputFlag = false;
                                ch.AdjustFlag = false;
                                ch.Adjust = null;
                                ch.Diff = null;

                                db.tb_CheckStockLists.InsertOnSubmit(ch);
                                db.SubmitChanges();




                                progressBar1.Value = countA;
                                progressBar1.PerformStep();
                            }
                        }
                    }
                    MessageBox.Show("Create Completed.");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            progressBar1.Visible = false;
            DataLoad();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        int row = 0;
        private void dgvData_CellClick(object sender, GridViewCellEventArgs e)
        {
            row = e.RowIndex;
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            if(row>=0)
            {
                if(MessageBox.Show("ต้องการลบรายการ [ "+ dgvData.Rows[row].Cells["CheckNo"].Value.ToString()+" ] ?","ลบรายการ",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    try
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            tb_CheckStock ck = db.tb_CheckStocks.Where(c => c.Status.Equals("Process") && c.CheckNo == dgvData.Rows[row].Cells["CheckNo"].Value.ToString()).FirstOrDefault();
                            if (ck != null)
                            {
                                db.spx_018_DeleteCheckStock(dgvData.Rows[row].Cells["CheckNo"].Value.ToString());

                                MessageBox.Show("Delete Completed.");
                                DataLoad();
                            }
                            else
                            {
                                MessageBox.Show("ไม่สามารถลบรายการได้ ?");
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (row >= 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {

                        Report.Reportx1.Value = new string[2];
                        Report.Reportx1.Value[0] = dgvData.Rows[row].Cells["CheckNo"].Value.ToString();
                        // Report.Reportx1.Value[1] = PRNo2;
                        Report.Reportx1.WReport = "ReportCheckStock";
                        Report.Reportx1 op = new Report.Reportx1("ReportCheckStockList.rpt");
                        op.Show();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            try
            {
                if (row >= 0)
                {
                    CheckStockList cl = new CheckStockList(dgvData.Rows[row].Cells["CheckNo"].Value.ToString());
                    cl.Show();
                    //this.Cursor = Cursors.WaitCursor;
                    //using (DataClasses1DataContext db = new DataClasses1DataContext())
                    //{

                    //    Report.Reportx1.Value = new string[2];
                    //    Report.Reportx1.Value[0] = dgvData.Rows[row].Cells["CheckNo"].Value.ToString();
                    //    // Report.Reportx1.Value[1] = PRNo2;
                    //    Report.Reportx1.WReport = "ReportCheckStock";
                    //    Report.Reportx1 op = new Report.Reportx1("ReportCheckStockList.rpt");
                    //    op.Show();
                    //}
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            //his.Cursor = Cursors.Default;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            dbClss.ExportGridXlSX(dgvData);
        }

        private void btnFilter1_Click(object sender, EventArgs e)
        {
            dgvData.EnableFiltering = true;
        }

        private void btnUnfilter1_Click(object sender, EventArgs e)
        {
            dgvData.EnableFiltering = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void radButtonElement3_Click(object sender, EventArgs e)
        {
            if (row >= 0)
            {
                if (MessageBox.Show("ต้องการปิดเอกสาร [ " + dgvData.Rows[row].Cells["CheckNo"].Value.ToString() + " ] ?", "ปิดรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        tb_CheckStock ck = db.tb_CheckStocks.Where(c => c.CheckNo == dgvData.Rows[row].Cells["CheckNo"].Value.ToString()
                        && !c.Status.Equals("Completed")
                        ).FirstOrDefault();
                        if(ck!=null)
                        {
                            ck.Status = "Completed";
                            db.SubmitChanges();
                            DataLoad();
                        }
                    }
                }
            }
        }
    }
}
