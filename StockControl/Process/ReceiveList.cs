﻿using System;
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
    public partial class ReceiveList : Telerik.WinControls.UI.RadRibbonForm
    {
        public ReceiveList()
        {
            this.Name = "ReceiveList";
            //  MessageBox.Show(this.Name);
            InitializeComponent();
            if (!dbClss.PermissionScreen(this.Name))
            {
                MessageBox.Show("Access denied", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               this.Close();
            }
            CallLang();


        }
        Telerik.WinControls.UI.RadTextBox RCNo_tt = new Telerik.WinControls.UI.RadTextBox();
        Telerik.WinControls.UI.RadTextBox PRNo_tt = new Telerik.WinControls.UI.RadTextBox();
        int screen = 0;
        public ReceiveList(Telerik.WinControls.UI.RadTextBox RCNoxxx
                    , Telerik.WinControls.UI.RadTextBox PRNoxxx
                )
        {
            InitializeComponent();
            RCNo_tt = RCNoxxx;
            PRNo_tt = PRNoxxx;
            screen = 1;
            CallLang();
        }

        private void CallLang()
        {
            if(dbClss.Language.Equals("ENG"))
            {
                btnSave.Text = "Open Doc.";
                btnRefresh.Text = "Refresh";
                btnFilter1.Text = "Filter";
                btnUnfilter1.Text = "UnFilter";
                btnExport.Text = "Export";
                btnPrint.Text = "Print";

                radLabel7.Text = "CodeNo:";
                radLabel8.Text = "Description:";
                radLabel2.Text = "Status:";
                radLabel9.Text = "Vendor :";
                radLabel4.Text = "To";
                radLabel3.Text = "Select";
                radLabel5.Text = "Case Status ALL need Select Date";
                radButton1.Text = "Search..";

                radLabelElement1.Text = "Receive List";
                this.Text = "Receive List";


                dgvData.Columns[0].HeaderText = "No";
                dgvData.Columns[1].HeaderText = "Status";
                dgvData.Columns[2].HeaderText = "Inv No.";
                dgvData.Columns[3].HeaderText = "Inv Date";
                dgvData.Columns[4].HeaderText = "PR No.";
                dgvData.Columns[5].HeaderText = "Dept. Code";
                dgvData.Columns[6].HeaderText = "Code No.";
                dgvData.Columns[7].HeaderText = "Tool Name";
                dgvData.Columns[8].HeaderText = "Description";
                dgvData.Columns[9].HeaderText = "Recipt Q'ty";
                dgvData.Columns[10].HeaderText = "Cost";
                dgvData.Columns[11].HeaderText = "Amount";
                dgvData.Columns[12].HeaderText = "Unit";
                dgvData.Columns[13].HeaderText = "Pcs/Unit";
                dgvData.Columns[14].HeaderText = "VendorNo";
                dgvData.Columns[15].HeaderText = "VendorName";
                dgvData.Columns[16].HeaderText = "LotNo";
                dgvData.Columns[17].HeaderText = "Machine";
                dgvData.Columns[18].HeaderText = "Line No.";
                dgvData.Columns[19].HeaderText = "Createby";
                dgvData.Columns[20].HeaderText = "CreateDate";
                dgvData.Columns[21].HeaderText = "AccountCode";
                dgvData.Columns[22].HeaderText = "Receipt No.";
            


            }
        }

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
            var today = DateTime.Now;
            var month = new DateTime(today.Year, today.Month, 1);
            var first = month;
            var last = month.AddMonths(1).AddDays(-1);

            dtDate1.Value = first;
            dtDate2.Value = last;
           // cboStatus.Text = "ทั้งหมด";
            dgvData.AutoGenerateColumns = false;
            GETDTRow();
            DefaultItem();
            //dgvData.ReadOnly = false;
            DataLoad();
            //txtVendorNo.Text = "";
            
        }
        private void DefaultItem()
        {
            return;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                cboVendorName.AutoCompleteMode = AutoCompleteMode.Append;
                cboVendorName.DisplayMember = "VendorName";
                cboVendorName.ValueMember = "VendorNo";
                cboVendorName.DataSource =(from ix in db.tb_Vendors.Where(s => s.Active == true) select new { ix.VendorNo,ix.VendorName}).ToList();
                cboVendorName.SelectedIndex = -1;
                cboVendorName.SelectedValue = "";
                try
                {

               

                    //GridViewMultiComboBoxColumn col = (GridViewMultiComboBoxColumn)radGridView1.Columns["CodeNo"];
                    //col.DataSource = (from ix in db.tb_Items.Where(s => s.Status.Equals("Active")) select new { ix.CodeNo, ix.ItemDescription }).ToList();
                    //col.DisplayMember = "CodeNo";
                    //col.ValueMember = "CodeNo";

                    //col.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
                    //col.FilteringMode = GridViewFilteringMode.DisplayMember;

                    // col.AutoSizeMode = BestFitColumnMode.DisplayedDataCells;
                }
                catch { }

                //col.TextAlignment = ContentAlignment.MiddleCenter;
                //col.Name = "CodeNo";
                //this.radGridView1.Columns.Add(col);

                //this.radGridView1.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

                //this.radGridView1.CellEditorInitialized += radGridView1_CellEditorInitialized;
            }
        }
        private void Load_WaitingReceive()  //รอรับเข้า (รอ Receive)
        {
            return;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                string VendorNo_ss = "";
                if (!cboVendorName.Text.Equals(""))
                    VendorNo_ss = txtVendorNo.Text;

                int dgvNo = 0;
                bool S = false; 
                //string RCNo = "";
                //string PRNo = "";
                //string CodeNo = "";
                //string ItemName = "";
                //string ItemNo = "";
                //string ItemDescription = "";
                //DateTime? DeliveryDate = null;
                //decimal QTY = 0;
                //decimal BackOrder = 0;
                //decimal RemainQty = 0;
                //string Unit = "";
                //decimal PCSUnit = 0;
                //decimal Leadtime = 0;
                //decimal MaxStock = 0;
                //decimal MinStock = 0;
                //string VendorNo = "";
                //string VendorName = "";
                //DateTime? CreateDate = null;
                //string CreateBy = "";
                //string Status = "รอรับเข้า";


                //var g = (from ix in db.tb_PurchaseRequests select ix).Where(a => a.VendorNo.Contains(VendorNo_ss)
                //    //&& a.Status != "Cancel"
                //    && a.Status == "Waiting"
                //    )
                //    .ToList();
                //if (g.Count() > 0)
                //{

                    var r = (from h in db.tb_PurchaseRequests
                             join d in db.tb_PurchaseRequestLines on h.PRNo equals d.PRNo
                             join i in db.tb_Items on d.CodeNo equals i.CodeNo

                             where //h.Status == "Waiting" //&& d.verticalID == VerticalID
                                Convert.ToDecimal(d.OrderQty ) == Convert.ToDecimal(d.RemainQty)
                                && h.VendorNo.Contains(VendorNo_ss)
                                && d.SS == 1
                             select new
                             {
                                 CodeNo = d.CodeNo,
                                 S = false,
                                 ItemNo = d.ItemName,
                                 ItemDescription = d.ItemDesc,
                                 RCNo = "",
                                 PRNo = d.PRNo,
                                 DeliveryDate = d.DeliveryDate,
                                 QTY = d.OrderQty,
                                 BackOrder = d.RemainQty,
                                 RemainQty = d.RemainQty,
                                 Unit = d.UnitCode,
                                 PCSUnit = d.PCSUnit,
                                 MaxStock = i.MaximumStock,
                                 MinStock = i.MinimumStock,
                                 VendorNo = h.VendorNo,
                                 VendorName = h.VendorName,
                                 CreateBy = h.CreateBy,
                                 CreateDate = h.CreateDate,
                                 Status = "รอรับเข้า"
                             }
               ).ToList();
                    if (r.Count > 0)
                    {
                        dgvNo = dgvData.Rows.Count() + 1;

                        foreach (var vv in r)
                        {
                            dgvData.Rows.Add(dgvNo.ToString(), S, vv.RCNo, vv.PRNo, vv.CodeNo, vv.ItemNo, vv.ItemDescription
                                        , vv.DeliveryDate, vv.QTY, vv.BackOrder, vv.RemainQty, vv.Unit, vv.PCSUnit, vv.MaxStock,
                                        vv.MinStock, vv.VendorNo, vv.VendorName, vv.CreateBy, vv.CreateDate, vv.Status
                                        );
                        }

                    }
                    //var gg = (from ix in db.tb_PurchaseRequestLines select ix)
                    //    .Where(a => a.SS.Equals(true) && (a.PRNo==(StockControl.dbClss.TSt(g.FirstOrDefault().PRNo)))
                    //   && a.OrderQty == a.RemainQty
                    //   && a.OrderQty >0
                    //).ToList();
                    //if (gg.Count() > 0)
                    //{
                    //    foreach (var vv in gg)
                    //    {
                    //        if (!StockControl.dbClss.TSt(vv.DeliveryDate).Equals(""))
                    //            DeliveryDate = Convert.ToDateTime(vv.DeliveryDate);

                    //        decimal.TryParse(StockControl.dbClss.TSt(vv.OrderQty), out QTY);
                    //        decimal.TryParse(StockControl.dbClss.TSt(vv.RemainQty), out BackOrder);
                    //        decimal.TryParse(StockControl.dbClss.TSt(vv.RemainQty), out RemainQty);

                    //        dgvNo = dgvData.Rows.Count() + 1;
                    //        dgvData.Rows.Add(dgvNo.ToString(), S, RCNo,vv.PRNo,vv.CodeNo,vv.ItemName,vv.ItemDesc
                    //            , DeliveryDate, QTY, BackOrder, RemainQty);
                    //    }
                    //}
                //}
            }
        }
        private void Load_PratitalReceive() //รับเข้าบางส่วน
        {
            return;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                string VendorNo_ss = "";
                if (!cboVendorName.Text.Equals(""))
                    VendorNo_ss = txtVendorNo.Text;

                int dgvNo = 0;
                bool S = false;
                string RCNo = "";
                //string PRNo = "";
                //string CodeNo = "";
                //string ItemName = "";
                //string ItemNo = "";
                //string ItemDescription = "";
                //DateTime? DeliveryDate = null;
                //decimal QTY = 0;
                //decimal BackOrder = 0;
                //decimal RemainQty = 0;
                //string Unit = "";
                //decimal PCSUnit = 0;
                //decimal Leadtime = 0;
                //decimal MaxStock = 0;
                //decimal MinStock = 0;
                //string VendorNo = "";
                //string VendorName = "";
                //DateTime? CreateDate = null;
                //string CreateBy = "";
                //string Status = "รอรับเข้า";
                DateTime inclusiveStart = dtDate1.Value.Date;
                // Include the *whole* of the day indicated by searchEndDate
                DateTime exclusiveEnd = dtDate2.Value.Date.AddDays(1);

                
                var r = (from d in db.tb_Receives
                         join c in db.tb_ReceiveHs on d.RCNo equals c.RCNo
                         join p in db.tb_PurchaseRequestLines on d.PRID equals p.id
                         join i in db.tb_Items on d.CodeNo equals i.CodeNo

                         where d.Status == "Partial" && c.VendorNo.Contains(VendorNo_ss)
                             && p.SS == 1
                             && (c.RCDate >= inclusiveStart
                                        && c.RCDate < exclusiveEnd)

                         select new
                         {
                             CodeNo = d.CodeNo,
                             S = false,
                             ItemNo = d.ItemNo,
                             ItemDescription = d.ItemDescription,
                             RCNo = d.RCNo,
                             PRNo = d.PRNo,
                             DeliveryDate = p.DeliveryDate,
                             QTY = d.QTY,
                             BackOrder = d.RemainQty,
                             RemainQty = d.RemainQty,
                             Unit = d.Unit,
                             PCSUnit = d.PCSUnit,
                             MaxStock = i.MaximumStock
                             ,MinStock = i.MinimumStock
                            , VendorNo = c.VendorNo
                            ,VendorName = c.VendorName
                            ,CreateBy = d.CreateBy
                            ,CreateDate = d.RCDate
                            ,Status = "รับเข้าบางส่วน"//d.Status
                            ,InvNo = c.InvoiceNo
                            ,SerialNo =  d.SerialNo
                            ,LotNo = d.LotNo
                            ,ShelfNo = d.ShelfNo
                         }
                ).ToList();
                //dgvData.DataSource = StockControl.dbClss.LINQToDataTable(r);
                if(r.Count > 0)
                {
                    dgvNo = dgvData.Rows.Count() + 1;

                    foreach (var vv in r)
                    {
                        dgvData.Rows.Add(dgvNo.ToString(), S, vv.RCNo, vv.PRNo, vv.InvNo ,vv.CodeNo, vv.ItemNo, vv.ItemDescription
                                    , vv.DeliveryDate, vv.QTY, vv.BackOrder, vv.RemainQty,vv.Unit,vv.PCSUnit,vv.MaxStock,
                                    vv.MinStock,vv.VendorNo,vv.VendorName,vv.LotNo,vv.SerialNo,vv.ShelfNo,vv.CreateBy,vv.CreateDate,vv.Status
                                    );
                    }

                }

                //int rowcount = 0;
                //foreach (var x in dgvData.Rows)
                //{
                //    rowcount += 1;
                //    x.Cells["dgvNo"].Value = rowcount;
                //}


            }
        }
        private void Load_CompletedReceive()//รับเข้าแล้ว
        {
            return;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                string VendorNo_ss = "";
                if (!cboVendorName.Text.Equals(""))
                    VendorNo_ss = txtVendorNo.Text;

                int dgvNo = 0;
                bool S = false;
                string RCNo = "";
                //string PRNo = "";
                //string CodeNo = "";
                //string ItemName = "";
                //string ItemNo = "";
                //string ItemDescription = "";
                //DateTime? DeliveryDate = null;
                //decimal QTY = 0;
                //decimal BackOrder = 0;
                //decimal RemainQty = 0;
                //string Unit = "";
                //decimal PCSUnit = 0;
                //decimal Leadtime = 0;
                //decimal MaxStock = 0;
                //decimal MinStock = 0;
                //string VendorNo = "";
                //string VendorName = "";
                //DateTime? CreateDate = null;
                //string CreateBy = "";
                //string Status = "รับเข้าแล้ว";
                DateTime inclusiveStart = dtDate1.Value.Date;
                // Include the *whole* of the day indicated by searchEndDate
                DateTime exclusiveEnd = dtDate2.Value.Date.AddDays(1);

                var r = (from d in db.tb_Receives
                         join c in db.tb_ReceiveHs on d.RCNo equals c.RCNo
                         join p in db.tb_PurchaseRequestLines on d.PRID equals p.id
                         join i in db.tb_Items on d.CodeNo equals i.CodeNo

                         where d.Status == "Completed" && c.VendorNo.Contains(VendorNo_ss)
                              && p.SS == 1
                               && (c.RCDate >= inclusiveStart
                                        && c.RCDate < exclusiveEnd)

                         select new
                         {
                             CodeNo = d.CodeNo,
                             S = false,
                             ItemNo = d.ItemNo,
                             ItemDescription = d.ItemDescription,
                             RCNo = d.RCNo,
                             PRNo = d.PRNo,
                             DeliveryDate = p.DeliveryDate,
                             QTY = d.QTY,
                             BackOrder = d.RemainQty,
                             RemainQty = d.RemainQty,
                             Unit = d.Unit,
                             PCSUnit = d.PCSUnit,
                             MaxStock = i.MaximumStock
                             ,
                             MinStock = i.MinimumStock
                            ,
                             VendorNo = c.VendorNo
                            ,
                             VendorName = c.VendorName
                            ,
                             CreateBy = d.CreateBy
                            ,
                             CreateDate = d.RCDate
                            ,
                             Status = "รับเข้าแล้ว"//d.Status
                             ,
                             InvNo = c.InvoiceNo
                              ,
                             SerialNo = d.SerialNo
                            ,
                             LotNo = d.LotNo
                            ,
                             ShelfNo = d.ShelfNo
                         }
                ).ToList();
                //dgvData.DataSource = StockControl.dbClss.LINQToDataTable(r);
                if (r.Count > 0)
                {
                    dgvNo = dgvData.Rows.Count() + 1;

                    foreach (var vv in r)
                    {
                        dgvData.Rows.Add(dgvNo.ToString(), S, vv.RCNo, vv.PRNo,vv.InvNo ,vv.CodeNo, vv.ItemNo, vv.ItemDescription
                                    , vv.DeliveryDate, vv.QTY, vv.BackOrder, vv.RemainQty, vv.Unit, vv.PCSUnit, vv.MaxStock,
                                    vv.MinStock, vv.VendorNo, vv.VendorName, vv.LotNo, vv.SerialNo, vv.ShelfNo,vv.CreateBy, vv.CreateDate, vv.Status
                                    );
                    }

                }

                //int rowcount = 0;
                //foreach (var x in dgvData.Rows)
                //{
                //    rowcount += 1;
                //    x.Cells["dgvNo"].Value = rowcount;
                //}

            }
        }
        private void DataLoad()
        {
            //dt.Rows.Clear();
            
            try
            {

                this.Cursor = Cursors.WaitCursor;
                dgvData.Rows.Clear();
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    
                    try
                    {
                        //if (cboStatus.Text.Equals("รอรับเข้า"))
                        //    Load_WaitingReceive();
                        //if (cboStatus.Text.Equals("รับเข้าบางส่วน"))
                        //    Load_PratitalReceive();
                        //else if (cboStatus.Text.Equals("รับเข้าแล้ว"))
                        //    Load_CompletedReceive();
                        //else
                        //{
                        //    //Load_WaitingReceive();
                        //    Load_PratitalReceive();
                        //    Load_CompletedReceive();
                        //}
                        dgvData.DataSource = null;
                        var gList = db.spx_006_selectReceive(dbClss.DeptSC.ToUpper(), chkDate.Checked, dtDate1.Value, dtDate2.Value
                            , txtCodeNo.Text, txtItemDescription.Text, txtInvoice.Text, txtVendor.Text, cboStatus.Text).ToList();
                        dgvData.DataSource = gList;


                        int rowcount = 0;
                        foreach (var x in dgvData.Rows)
                        {
                            rowcount += 1;
                            x.Cells["dgvNo"].Value = rowcount;
                            
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }

                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;


            //    radGridView1.DataSource = dt;
        }
        //private bool CheckDuplicate(string code, string Code2)
        //{
        //    bool ck = false;

        //    using (DataClasses1DataContext db = new DataClasses1DataContext())
        //    {
        //        int i = (from ix in db.tb_Models
        //                 where ix.ModelName == code

        //                 select ix).Count();
        //        if (i > 0)
        //            ck = false;
        //        else
        //            ck = true;
        //    }

        //    return ck;
        //}

        
       
        private void btnCancel_Click(object sender, EventArgs e)
        {
            
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            return;
            dgvData.ReadOnly = false;
            dgvData.AllowAddNewRow = false;
            dgvData.Rows.AddNew();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            dgvData.ReadOnly = false;
           // btnEdit.Enabled = false;
            btnPrint.Enabled = true;
            dgvData.AllowAddNewRow = false;
            //DataLoad();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (screen.Equals(1))
                {
                    if (!Convert.ToString(dgvData.CurrentRow.Cells["RCNo"].Value).Equals(""))
                    {
                        RCNo_tt.Text = Convert.ToString(dgvData.CurrentRow.Cells["RCNo"].Value);
                        this.Close();
                    }
                    else
                    {
                        RCNo_tt.Text = Convert.ToString(dgvData.CurrentRow.Cells["RCNo"].Value);
                        PRNo_tt.Text = Convert.ToString(dgvData.CurrentRow.Cells["PRNo"].Value);
                        this.Close();
                    }
                }
                else
                {
                    Receive a = new Receive(Convert.ToString(dgvData.CurrentRow.Cells["RCNo"].Value),
                        Convert.ToString(dgvData.CurrentRow.Cells["PRNo"].Value));
                    a.ShowDialog();
                    //this.Close();
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                

            }
            catch (Exception ex) { }
        }

        private void Unit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // MessageBox.Show(e.KeyCode.ToString());
        }


       

        int row = -1;
        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            row = e.RowIndex;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //dbClss.ExportGridCSV(radGridView1);
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

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void radGridView1_Click(object sender, EventArgs e)
        {

        }

        private void chkActive_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {

        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void radGridView1_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            //if (e.CellElement.ColumnInfo.Name == "ModelName")
            //{
            //    if (e.CellElement.RowInfo.Cells["ModelName"].Value != null)
            //    {
            //        if (!e.CellElement.RowInfo.Cells["ModelName"].Value.Equals(""))
            //        {
            //            e.CellElement.DrawFill = true;
            //            // e.CellElement.ForeColor = Color.Blue;
            //            e.CellElement.NumberOfColors = 1;
            //            e.CellElement.BackColor = Color.WhiteSmoke;
            //        }

            //    }
            //}
        }

        private void txtModelName_TextChanged(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void radPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboVendorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cboVendorName.Text.Equals(""))
                txtVendorNo.Text = cboVendorName.SelectedValue.ToString();
            else
                txtVendorNo.Text = "";
        }

        private void MasterTemplate_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (screen.Equals(1))
            {
                if (!Convert.ToString(e.Row.Cells["RCNo"].Value).Equals(""))
                {
                    RCNo_tt.Text = Convert.ToString(e.Row.Cells["RCNo"].Value);
                    this.Close();
                }
                else
                {
                    RCNo_tt.Text = Convert.ToString(e.Row.Cells["RCNo"].Value);
                    PRNo_tt.Text = Convert.ToString(e.Row.Cells["PRNo"].Value);
                    this.Close();
                }
            }
            else
            {
                Receive a = new Receive(Convert.ToString(e.Row.Cells["RCNo"].Value),
                    Convert.ToString(e.Row.Cells["PRNo"].Value));
                a.ShowDialog();
               // this.Close();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvData.Rows.Count <= 0)
                    return;

                //dt_ShelfTag.Rows.Clear();
                string RCNo = "";
                RCNo = StockControl.dbClss.TSt(dgvData.CurrentRow.Cells["RCNo"].Value);
                PrintPR a = new PrintPR(RCNo, RCNo, "Receive");
                a.ShowDialog();

                //using (DataClasses1DataContext db = new DataClasses1DataContext())
                //{
                //    var g = (from ix in db.sp_R003_ReportReceive(RCNo, DateTime.Now) select ix).ToList();
                //    if (g.Count() > 0)
                //    {

                //        Report.Reportx1.Value = new string[2];
                //        Report.Reportx1.Value[0] = RCNo;
                //        Report.Reportx1.WReport = "ReportReceive";
                //        Report.Reportx1 op = new Report.Reportx1("ReportReceive.rpt");
                //        op.Show();

                //    }
                //    else
                //        MessageBox.Show("not found.");
                //}

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void frezzRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvData.Rows.Count > 0)
                {

                    int Row = 0;
                    Row = dgvData.CurrentRow.Index;
                    dbClss.Set_Freeze_Row(dgvData, Row);

                    
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void frezzColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvData.Columns.Count > 0)
                {

                    int Col = 0;
                    Col = dgvData.CurrentColumn.Index;
                    dbClss.Set_Freeze_Column(dgvData, Col);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void unFrezzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                dbClss.Set_Freeze_UnColumn(dgvData);
                dbClss.Set_Freeze_UnRows(dgvData);
               

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DataLoad();
        }
    }
}
