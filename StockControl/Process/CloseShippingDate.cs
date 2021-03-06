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
    public partial class CloseShippingDate : Telerik.WinControls.UI.RadRibbonForm
    {
        public CloseShippingDate()
        {
            this.Name = "CloseShippingDate";
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
                this.Text = "Close Shipping";
                radLabelElement1.Text = "Status: Close List";
                btnRefresh.Text = "Refresh";
                btnNew.Text = "New +";
                btnSave.Text = "Save Data";
                btnView.Text = "Display List";
                btnEdit.Text = "Edit Data";
                btnDelete.Text = "Delete";
                btnExport.Text = "Export";
                btnImport.Text = "Import";
                btnFilter1.Text = "Filter";
                btnUnfilter1.Text = "Unfilter";


                // radButtonElement1.Text = "Contact";

                //radGridView1.Columns[0].HeaderText = "Default";
                //radGridView1.Columns[1].HeaderText = "Contact";
                //radGridView1.Columns[2].HeaderText = "Phone";
                //radGridView1.Columns[3].HeaderText = "Fax";
                //radGridView1.Columns[4].HeaderText = "Email";
                //radGridView1.Columns[5].HeaderText = "VendorNo.";




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
            //dt.Columns.Add(new DataColumn("UnitCode", typeof(string)));
            //dt.Columns.Add(new DataColumn("UnitDetail", typeof(string)));
            //dt.Columns.Add(new DataColumn("UnitActive", typeof(bool)));
        }
        private void Unit_Load(object sender, EventArgs e)
        {
            //RMenu3.Click += RMenu3_Click;
            //RMenu4.Click += RMenu4_Click;
            //RMenu5.Click += RMenu5_Click;
            //RMenu6.Click += RMenu6_Click;
            radGridView1.ReadOnly = true;
            radGridView1.AutoGenerateColumns = false;
          //  GETDTRow();
           
            
            DataLoad();
        }

        private void RMenu6_Click(object sender, EventArgs e)
        {
           
           // DeleteUnit();
            DataLoad();
        }

        private void RMenu5_Click(object sender, EventArgs e)
        {
            EditClick();
        }

        private void RMenu4_Click(object sender, EventArgs e)
        {
            ViewClick();
        }

        private void RMenu3_Click(object sender, EventArgs e)
        {
            NewClick();

        }

        private void DataLoad()
        {
            //dt.Rows.Clear();
            int ck = 0;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                //dt = ClassLib.Classlib.LINQToDataTable(db.tb_Units.ToList());
                radGridView1.DataSource = db.tb_CloseDates.OrderByDescending(o => o.CloseDate).ToList();
                foreach(var x in radGridView1.Rows)
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
                    int id = Convert.ToInt32(radGridView1.Rows[row].Cells["id"].Value);
                    
                    radGridView1.EndEdit();
                    if (MessageBox.Show("ต้องการลบรายการ ( "+ Convert.ToDateTime(radGridView1.Rows[row].Cells["CloseDate"].Value).ToString("dd/MMM/yyyy")+" ) หรือไม่ ?", "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            tb_CloseDate cl = db.tb_CloseDates.Where(c => c.id == id).FirstOrDefault();
                            if (cl != null)
                            {
                                db.tb_CloseDates.DeleteOnSubmit(cl);
                                db.SubmitChanges();
                                C += 1;
                            }
                        }
                    }
                }
            }

            catch (Exception ex) { MessageBox.Show(ex.Message);
                dbClss.AddError("Delete Shipping Close Date", ex.Message, this.Name);
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
            DataLoad();
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
            DataLoad();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            // NewClick();
            CloseDateAdd ca = new CloseDateAdd();
            ca.ShowDialog();
            DataLoad();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ViewClick();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            EditClick();
        }
        private void Saveclick()
        {
            if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AddUnit();
                DataLoad();
            }
        }
        private void DeleteClick()
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
          //  Saveclick();
        }


        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                radGridView1.Rows[e.RowIndex].Cells["dgvC"].Value = "T";
                string check1 = Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["UnitCode"].Value);
                string TM= Convert.ToString(radGridView1.Rows[e.RowIndex].Cells["dgvCodeTemp"].Value);
                if (!check1.Trim().Equals("") && TM.Equals(""))
                {
                    
                    if (!CheckDuplicate(check1.Trim()))
                    {
                        MessageBox.Show("ข้อมูล รหัสหน่วย ซ้ำ");
                        radGridView1.Rows[e.RowIndex].Cells["UnitCode"].Value = "";
                        radGridView1.Rows[e.RowIndex].Cells["UnitCode"].IsSelected = true;

                    }
                }
        

            }
            catch(Exception ex) { }
        }

        private void Unit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {


        }

        private void radGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            if (e.KeyData == (Keys.Control | Keys.S))
            {
                if (MessageBox.Show("ต้องการบันทึก ?", "บันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AddUnit();
                    DataLoad();
                }
            }
            else if (e.KeyData == (Keys.Control | Keys.N))
            {
                if (MessageBox.Show("ต้องการสร้างใหม่ ?", "สร้างใหม่", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    NewClick();
                }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
                DeleteUnit();
                DataLoad();
            
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

                    DataLoad();
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
