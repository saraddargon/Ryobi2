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
    public partial class CheckStockList : Telerik.WinControls.UI.RadRibbonForm
    {
        public CheckStockList()
        {
            this.Name = "CheckStockList";
            //  MessageBox.Show(this.Name);
            InitializeComponent();
            //if (!dbClss.PermissionScreen(this.Name))
            //{
            //    MessageBox.Show("Access denied", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.Close();
            //}
            CallLang();
        }
        private void CallLang()
        {
            if (dbClss.Language.Equals("ENG"))
            {
                this.Text = "Check Stock List";
                radLabelElement1.Text = "Status: Check Stock List";
                btnRefresh.Text = "Refresh";
                btnPrint.Text = "Print";   
                       
                btnExport.Text = "Export";
            
                btnFilter1.Text = "Filter";
                btnUnfilter1.Text = "Unfilter";
                radButton1.Text = "Search..";
                radLabel1.Text = "Check No.:";
                radLabel2.Text = "Status:";


                // radButtonElement1.Text = "Contact";

                //radGridView1.Columns[0].HeaderText = "Default";
                //radGridView1.Columns[1].HeaderText = "Contact";
                //radGridView1.Columns[2].HeaderText = "Phone";
                //radGridView1.Columns[3].HeaderText = "Fax";
                //radGridView1.Columns[4].HeaderText = "Email";
                //radGridView1.Columns[5].HeaderText = "VendorNo.";




            }
        }
        public CheckStockList(string CheckNo)
        {
            InitializeComponent();
            txtSHNo.Text = CheckNo;
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

           // dgvData.ReadOnly = true;
            dgvData.AutoGenerateColumns = false;
            DataLoad();

         
        }
        string StatusCH = "";
        private void DataLoad()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    tb_CheckStock ck = db.tb_CheckStocks.Where(c => c.CheckNo == txtSHNo.Text
                        && c.Status.Equals("Completed")
                        ).FirstOrDefault();
                    if (ck != null)
                    {
                        txtSHNo.Enabled = false;
                        dgvData.ReadOnly = true;
                        radButtonElement1.Enabled = false;
                        btnSave.Enabled = false;
                    }
                    else
                    {
                        txtSHNo.Enabled = false;
                        dgvData.ReadOnly = false;
                        radButtonElement1.Enabled = true;
                        btnSave.Enabled = true;
                    }

                    dgvData.DataSource = null;
                    dgvData.DataSource = db.spx19_ListCheckStock(txtSHNo.Text, cboStatus.Text).ToList();
                    int Countss = 0;
                    foreach (GridViewRowInfo rd in dgvData.Rows)
                    {
                        Countss += 1;
                        rd.Cells["No"].Value = Countss;
                    }
                }
            }
            catch { }
            this.Cursor = Cursors.Default;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("ต้องการเปรียบเทียบข้อมูล (ทูล)?", "Compare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    progressBar1.Visible = true;
                    progressBar1.Minimum = 1;
                    progressBar1.Maximum = dgvData.RowCount + 1;
                    int CountA = 0;
                    int idxx = 0;
                  //  string CheckNo = dbClss.GetNo(33, 2);
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                       foreach(GridViewRowInfo rd in dgvData.Rows)
                        {
                            idxx = 0;
                            int.TryParse(Convert.ToString(rd.Cells["id"].Value), out idxx);
                            if(idxx>0)
                            {
                                CountA += 1;
                                progressBar1.Value = CountA;
                                progressBar1.PerformStep();
                                db.spx21_CompareStock(idxx);
                            }
                        }
                        
                    }
                    MessageBox.Show("Compare Completed.");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
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
                        Report.Reportx1.Value[0] = txtSHNo.Text;
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
        int idx = 0;
        private void dgvData_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            try
            {

                if (e.ColumnIndex == dgvData.Columns["InputQty"].Index)
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        decimal qtya = 0;
                        int.TryParse(dgvData.Rows[e.RowIndex].Cells["id"].Value.ToString(), out idx);
                        if (idx > 0)
                        {
                            decimal.TryParse(dgvData.Rows[e.RowIndex].Cells["InputQty"].Value.ToString(), out qtya);
                            db.spx20_UpdateCheckStock(idx,dbClss.UserID,qtya,"",0);
                            dgvData.Rows[e.RowIndex].Cells["InputFlag"].Value = true;
                        }

                        idx = 0;
                    }

                }
                else if (e.ColumnIndex == dgvData.Columns["Reason"].Index)
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        int.TryParse(dgvData.Rows[e.RowIndex].Cells["id"].Value.ToString(), out idx);
                        if (idx > 0)
                        {
                            db.spx20_UpdateCheckStock(idx, dbClss.UserID, 0, dgvData.Rows[e.RowIndex].Cells["Reason"].Value.ToString(), 2);
                        }

                        idx = 0;
                    }
                }
                else if (e.ColumnIndex == dgvData.Columns["AdjustFlag"].Index)
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        int.TryParse(dgvData.Rows[e.RowIndex].Cells["id"].Value.ToString(), out idx);
                        if (idx > 0)
                        {
                           // MessageBox.Show("" + dgvData.Rows[e.RowIndex].Cells["AdjustFlag"].Value.ToString());
                            db.spx20_UpdateCheckStock(idx, dbClss.UserID, 0, dgvData.Rows[e.RowIndex].Cells["AdjustFlag"].Value.ToString().ToUpper(), 3);
                        }

                        idx = 0;
                    }
                }
                //else if (e.ColumnIndex == dgvData.Columns["Adjust"].Index)
                //{
                //    using (DataClasses1DataContext db = new DataClasses1DataContext())
                //    {
                //        int.TryParse(dgvData.Rows[e.RowIndex].Cells["id"].Value.ToString(), out idx);
                //        if (idx > 0)
                //        {
                //            db.spx20_UpdateCheckStock(idx, dbClss.UserID, 0, dgvData.Rows[e.RowIndex].Cells["AdjustFlag"].Value.ToString(), 3);
                //        }

                //        idx = 0;
                //    }
                //}
            }
            catch { }
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
            dgvData.EnableFiltering = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void radButtonElement1_Click_1(object sender, EventArgs e)
        {
            if(!StatusCH.Equals("Completed"))
            {
                AdjustStock ad = new AdjustStock(txtSHNo.Text);
                ad.Show();
            }
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            try
            {
                if (row >= 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {

                        Report.Reportx1.Value = new string[2];
                        Report.Reportx1.Value[0] = txtSHNo.Text;
                        // Report.Reportx1.Value[1] = PRNo2;
                        Report.Reportx1.WReport = "ReportCheckStock";
                        Report.Reportx1 op = new Report.Reportx1("ReportCheckStockListAdjust.rpt");
                        op.Show();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }
    }
}
