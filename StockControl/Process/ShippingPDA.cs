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
    public partial class ShippingPDA : Telerik.WinControls.UI.RadRibbonForm
    {
        public ShippingPDA()
        {
            this.Name = "StockItemCost";
            
            InitializeComponent();
          
            
        }
        public ShippingPDA(string CodeNox)
        {
            this.Name = "StockItemCost";
            CodeNo = CodeNox;
            InitializeComponent();


        }
        private string CodeNo = "";

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
            dt.Columns.Add(new DataColumn("UnitCode", typeof(string)));
            dt.Columns.Add(new DataColumn("UnitDetail", typeof(string)));
            dt.Columns.Add(new DataColumn("UnitActive", typeof(bool)));
        }
        private void Unit_Load(object sender, EventArgs e)
        {
            //RMenu3.Click += RMenu3_Click;
            //RMenu4.Click += RMenu4_Click;
            //RMenu5.Click += RMenu5_Click;
            //RMenu6.Click += RMenu6_Click;
            radGridView1.ReadOnly = true;
            radGridView1.AutoGenerateColumns = false;
            
            //GridViewSummaryItem summaryItemShipName = new GridViewSummaryItem("Amount", "{0:N2}", GridAggregateFunction.Sum);
            //GridViewSummaryItem summaryItemFreight = new GridViewSummaryItem("RM", "Remain = {0:N2}", GridAggregateFunction.Sum);
            //GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem(
            //    new GridViewSummaryItem[] { summaryItemShipName, summaryItemFreight });
            //this.radGridView1.SummaryRowsTop.Add(summaryRowItem);

            DataLoad(true);

        }

        private void RMenu6_Click(object sender, EventArgs e)
        {
           
           // DeleteUnit();
           // DataLoad(true);
        }

        private void RMenu5_Click(object sender, EventArgs e)
        {
           // EditClick();
        }

        private void RMenu4_Click(object sender, EventArgs e)
        {
          //  ViewClick();
        }

        private void RMenu3_Click(object sender, EventArgs e)
        {
          //  NewClick();

        }

        private void DataLoad(bool Load)
        {
            //dt.Rows.Clear();
            int ck = 0;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                string Status = "";
                if (Load)
                    Status = "Waiting";
                else
                    Status = "";
                //dt = ClassLib.Classlib.LINQToDataTable(db.tb_Units.ToList());
                radGridView1.DataSource = db.spx22_ListPDAList(dbClss.DeptSC, Status).ToList();
                foreach (var x in radGridView1.Rows)
                {
                    ck += 1;
                    x.Cells["No"].Value = ck;                                      
                    
                }
               
            }


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

        private bool AddUnit()
        {
            
            bool ck = false;
            int C = 0;
            try
            {


                radGridView1.EndEdit();
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    foreach (var g in radGridView1.Rows)
                    {
                        if (!Convert.ToString(g.Cells["UnitCode"].Value).Equals(""))
                        {
                            if (Convert.ToString(g.Cells["dgvC"].Value).Equals("T"))
                            {
                               
                                if (Convert.ToString(g.Cells["dgvCodeTemp"].Value).Equals(""))
                                {
                                   // MessageBox.Show("11");
                                    
                                    tb_Unit u = new tb_Unit();
                                    u.UnitCode = Convert.ToString(g.Cells["UnitCode"].Value);
                                    u.UnitActive = Convert.ToBoolean(g.Cells["UnitActive"].Value);
                                    u.UnitDetail= Convert.ToString(g.Cells["UnitDetail"].Value);
                                    db.tb_Units.InsertOnSubmit(u);
                                    db.SubmitChanges();
                                    C += 1;
                                    dbClss.AddHistory(this.Name, "เพิ่ม", "Insert Unit Code [" + u.UnitCode+"]","");
                                }
                                else
                                {
                                   
                                    var unit1 = (from ix in db.tb_Units
                                                 where ix.UnitCode == Convert.ToString(g.Cells["dgvCodeTemp"].Value)
                                                 select ix).First();
                                       unit1.UnitDetail = Convert.ToString(g.Cells["UnitDetail"].Value);
                                       unit1.UnitActive = Convert.ToBoolean(g.Cells["UnitActive"].Value);
                                    
                                    C += 1;

                                    db.SubmitChanges();
                                    dbClss.AddHistory(this.Name, "แก้ไข", "Update Unit Code [" + unit1.UnitCode+"]","");

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message);
                dbClss.AddError("AddUnit", ex.Message, this.Name);
            }

            if (C > 0)
                MessageBox.Show("บันทึกสำเร็จ!");

            return ck;
        }
        private bool DeleteUnit()
        {
            bool ck = false;
         
            int C = 0;
            try
            {
                
                if (row >= 0)
                {
                    string CodeDelete = Convert.ToString(radGridView1.Rows[row].Cells["UnitCode"].Value);
                    string CodeTemp = Convert.ToString(radGridView1.Rows[row].Cells["dgvCodeTemp"].Value);
                    radGridView1.EndEdit();
                    if (MessageBox.Show("ต้องการลบรายการ ( "+ CodeDelete+" ) หรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {

                            if (!CodeDelete.Equals(""))
                            {
                                if (!CodeTemp.Equals(""))
                                {

                                    var unit1 = (from ix in db.tb_Units
                                                 where ix.UnitCode == CodeDelete
                                                 select ix).ToList();
                                    foreach (var d in unit1)
                                    {
                                        db.tb_Units.DeleteOnSubmit(d);
                                        dbClss.AddHistory(this.Name, "ลบ Unit", "Delete Unit Code ["+d.UnitCode+"]","");
                                    }
                                    C += 1;



                                    db.SubmitChanges();
                                }
                            }

                        }
                    }
                }
            }

            catch (Exception ex) { MessageBox.Show(ex.Message);
                dbClss.AddError("DeleteUnit", ex.Message, this.Name);
            }

            if (C > 0)
            {
                    row = row - 1;
                    MessageBox.Show("ลบรายการ สำเร็จ!");
            }
              

           

            return ck;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataLoad(false);
        }
        private void NewClick()
        {
            radGridView1.ReadOnly = false;
            radGridView1.AllowAddNewRow = false;
            btnEdit.Enabled = false;
            btnView.Enabled = true;
            radGridView1.Rows.AddNew();
        }
        private void EditClick()
        {
            radGridView1.ReadOnly = false;
            btnEdit.Enabled = false;
            btnView.Enabled = true;
            radGridView1.AllowAddNewRow = false;
        }
        private void ViewClick()
        {
            radGridView1.ReadOnly = true;
            btnView.Enabled = false;
            btnEdit.Enabled = true;
            radGridView1.AllowAddNewRow = false;
            DataLoad(true);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataLoad(true);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DataLoad(false);

        }
        private void Saveclick()
        {
            
        }
        private void DeleteClick()
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


        }

        private void radGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("ต้องการลบรายการ หรือไม่ ?","ลบรายการ",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        int id = 0;
                        int.TryParse(radGridView1.Rows[row].Cells["id"].Value.ToString(), out id);
                        tb_ShippingPDA pd = db.tb_ShippingPDAs.Where(s => s.id == id && s.Status=="Waiting").FirstOrDefault();
                        if(pd!=null)
                        {
                           // db.tb_ShippingPDAs.DeleteOnSubmit(pd);
                            pd.Status = "Cancel";
                            db.SubmitChanges();
                            MessageBox.Show("Delete Completed.");
                            DataLoad(true);
                        }
                    }

                }

            }
            catch { }             
            
        }

        int row = -1;
        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            row = e.RowIndex;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //dbClss.ExportGridCSV(radGridView1);
           dbClss.ExportGridXlSX(radGridView1);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Spread Sheet files (*.csv)|*.csv|All files (*.csv)|*.csv";
            if (op.ShowDialog() == DialogResult.OK)
            {


                using (TextFieldParser parser = new TextFieldParser(op.FileName))
                {
                    dt.Rows.Clear();
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    int a = 0;
                    int c = 0;
                    while (!parser.EndOfData)
                    {
                        //Processing row
                        a += 1;
                        DataRow rd = dt.NewRow();
                        // MessageBox.Show(a.ToString());
                        string[] fields = parser.ReadFields();
                        c = 0;
                        foreach (string field in fields)
                        {
                            c += 1;
                            //TODO: Process field
                            //MessageBox.Show(field);
                            if (a>1)
                            {
                                if(c==1)
                                    rd["UnitCode"] = Convert.ToString(field);
                                else if(c==2)
                                    rd["UnitDetail"] = Convert.ToString(field);
                                else if(c==3)
                                    rd["UnitActive"] = Convert.ToBoolean(field);

                            }
                            else
                            {
                                if (c == 1)
                                    rd["UnitCode"] = "";
                                else if (c == 2)
                                    rd["UnitDetail"] = "";
                                else if (c == 3)
                                    rd["UnitActive"] = false;




                            }

                            //
                            //rd[""] = "";
                            //rd[""]
                        }
                        dt.Rows.Add(rd);

                    }
                }
                if(dt.Rows.Count>0)
                {
                    dbClss.AddHistory(this.Name, "Import", "Import file CSV in to System", "");
                    ImportData();
                    MessageBox.Show("Import Completed.");

                   
                }
               
            }
        }

        private void ImportData()
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                   
                    foreach (DataRow rd in dt.Rows)
                    {
                        if (!rd["UnitCode"].ToString().Equals(""))
                        {

                            var x = (from ix in db.tb_Units where ix.UnitCode.ToLower().Trim() == rd["UnitCode"].ToString().ToLower().Trim() select ix).FirstOrDefault();

                            if(x==null)
                            {
                                tb_Unit ts = new tb_Unit();
                                ts.UnitCode = Convert.ToString(rd["UnitCode"].ToString());
                                ts.UnitDetail = Convert.ToString(rd["UnitDetail"].ToString());
                                ts.UnitActive = Convert.ToBoolean(rd["UnitActive"].ToString());
                                db.tb_Units.InsertOnSubmit(ts);
                                db.SubmitChanges();
                            }
                            else
                            {
                                x.UnitDetail = Convert.ToString(rd["UnitDetail"].ToString());
                                x.UnitActive = Convert.ToBoolean(rd["UnitActive"].ToString());
                                db.SubmitChanges();

                            }

                       
                        }
                    }
                   
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message);
                dbClss.AddError("InportData", ex.Message, this.Name);
            }
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
    }
}
