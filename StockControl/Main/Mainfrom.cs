using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using ClassLib;
using System.Security.Permissions;
using System.Threading;
using System.Linq;

namespace StockControl
{
    public partial class Mainfrom : Telerik.WinControls.UI.RadForm
    {
        public Mainfrom()
        {
            InitializeComponent();
            lblUser.Text= ClassLib.Classlib.User;
            lblDomain.Text = Classlib.DomainUser;
            lblresolution.Text = Classlib.ScreenWidth.ToString("#,###") + " x " + Classlib.ScreenHight.ToString("#,###");
            linkLabel1.Text = "        Menu ["+dbClss.UserID+"] Dept. "+dbClss.DeptSC;
            this.Text = dbClss.versioin;
           // radLabel1.Text = dbClss.versioin;
        }
   

        string SqlGetName= "";
        display formshow;
        Home1600x900 homeshow;
        private void Mainfrom_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch { }
        }

        private void radMenuItem5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Mainfrom_Load(object sender, EventArgs e)
        {
            TreeManu.ExpandAll();
            //SqlGetName = "PartSetting";
            SqlGetName = "home";
            txtposition.Text = "x0:y0";
            CallDisplayHome();
            lblUser.Text = dbClss.UserID;
            picAlert.Visible = false;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                tb_Control cn = db.tb_Controls.Where(c => c.DocumentName == "SH" && c.Format.Equals(dbClss.DeptSC)).FirstOrDefault();
                if (cn != null)
                {
                    SHnoPK = cn.ControlNo;
                }
            }
            CallPic();
            timer1.Enabled = true;
            timer1.Start();
        }
        private void CallDisplayHome()
        {
            if (CountDisplay == 0 && !SqlGetName.Equals(""))
            {
                homeshow = new Home1600x900(ref txtposition);
                ShowTreeForm(homeshow);
                GC.Collect();
                GC.WaitForFullGCComplete();
                CountDisplay = 1;
            }

        }
        private void TreeManu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                CountDisplay = 1;
                TreeManu.SelectedNode.Expand();
                SqlGetName = TreeManu.SelectedNode.Name.ToString();
                formshow = new display(ref SqlGetName);
                //formshow.lblModule.Text = TreeManu.SelectedNode.Text.ToString();
                //formshow.lblDatabase.Text = ConnectDB.Db.ToUpper();
                //formshow.lblServer.Text = ConnectDB.Server.ToUpper();
                //formshow.lblVersion.Text = "1.0";
                //formshow.lblUser.Text = ConnectDB.UserName.ToUpper();
                ShowTreeForm(formshow);

                GC.Collect();
                GC.WaitForFullGCComplete();
            }
            catch { }
        }
        public void ShowTreeForm(Form Show1)
        {
            Show1.TopLevel = false;
            Show1.Dock = DockStyle.Fill;
            Show1.WindowState = FormWindowState.Maximized;
            Show1.FormBorderStyle = FormBorderStyle.None;
            Show1.ShowInTaskbar = false;
            // set panal1 show
            
            this.panel3.Controls.Clear();
            this.panel3.Controls.Add(Show1);
            Show1.Show();

        }

        private void radMenuItem15_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Unit unit = new Unit();
                this.Cursor = Cursors.Default;
                unit.ShowDialog();
                GC.Collect();
                GC.WaitForPendingFinalizers();

                ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                ClassLib.Memory.Heap();
            }
            catch { }
        }

        private void radMenuItem17_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Types tb = new Types();
                this.Cursor = Cursors.Default;
                tb.ShowDialog();
                GC.Collect();
                GC.WaitForPendingFinalizers();

                ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                ClassLib.Memory.Heap();
            }
            catch { }
        }

        private void Mainfrom_MaximumSizeChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("xx");
            //formshow = new display(ref SqlGetName);
            //ShowTreeForm(formshow);
            //GC.Collect();
            //GC.WaitForFullGCComplete();
        }

        private void Mainfrom_ResizeEnd(object sender, EventArgs e)
        {
            //MessageBox.Show("resize");
        }

        private void Mainfrom_MinimumSizeChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("2xx");
        }

        int CountDisplay = 0;
        private void CallDisplay()
        {
            if (CountDisplay == 0 && SqlGetName.Equals("home"))
            {
                CallDisplayHome();
                return;
            }
            else if(CountDisplay==0 && !SqlGetName.Equals(""))
            {
                formshow = new display(ref SqlGetName);
                ShowTreeForm(formshow);
                GC.Collect();
                GC.WaitForFullGCComplete();
                CountDisplay = 1;
            }

        }
        private void Mainfrom_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //MessageBox.Show("Minimize");
               
            }
            else if(WindowState==FormWindowState.Normal)
            {
                // MessageBox.Show("restore down");
                CountDisplay = 0;
                CallDisplay();
            }
            else if (WindowState == FormWindowState.Maximized)
            {
                CountDisplay = 0;
                CallDisplay();
                // MessageBox.Show("Maximize");
            }
        }

        private void radMenuItem4_Click(object sender, EventArgs e)
        {
            CountDisplay = 0;
            SqlGetName = "home";
            CallDisplayHome();
        }

        private void radMenuItem16_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                GroupType gy = new GroupType();
                this.Cursor = Cursors.Default;
                gy.ShowDialog();
                GC.Collect();
                GC.WaitForPendingFinalizers();

                ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                ClassLib.Memory.Heap();
            }
            catch { }
        }

        private void radMenuItem22_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            HistoryScreen gy = new HistoryScreen("");
            this.Cursor = Cursors.Default;
            gy.ShowDialog();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            ClassLib.Memory.Heap();
        }

        private void radMenuItem21_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            PathConfig pf = new PathConfig();
            this.Cursor = Cursors.Default;
            pf.ShowDialog();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            ClassLib.Memory.Heap();
        }

        private void radMenuItem8_Click(object sender, EventArgs e)
        {
           
            if (MessageBox.Show("ต้องการที่จะ Run Job Query หรือไม่ ?", "Run Job", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    db.sp_RunJOB();
                    db.sp_SelectItemUpdate();
                }
                    MessageBox.Show("Script Run StoreProcedure Agent Completed.");
            }
        }

        private void radMenuItem7_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("ต้องการที่จะ Backup ฐานข้อมูล","Backup",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                try
                {

                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        db.sp_BackupDatabase();
                    }
                    MessageBox.Show("Backup Completed.");
                }
                catch (Exception ex) { MessageBox.Show("ไม่สามารถ Backup ได้โปรดเช็คสถานที่เก็บไฟล์!"); }
            }
        }

        private void radMenuItem19_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการที่จะ Update หรือไม่ ?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("AutoUpdate.exe");
                Application.Exit();
            }
        }

        private void radMenuItem3_Click(object sender, EventArgs e)
        {
           
          

            this.Cursor = Cursors.WaitCursor;
            ServerConfig sc = new ServerConfig();
            this.Cursor = Cursors.Default;
            sc.ShowDialog();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            ClassLib.Memory.Heap();
        }

        private void radMenuItem12_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            About sc = new About();
            this.Cursor = Cursors.Default;
            sc.ShowDialog();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            ClassLib.Memory.Heap();
        }

        private void radMenuItem11_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reset Layout Completed.");
        }

        private void radMenuItem18_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Vendor sc = new Vendor();
                this.Cursor = Cursors.Default;
                sc.ShowDialog();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                ClassLib.Memory.Heap();
            }
            catch { }


            
        }

        private void radMenuItem20_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            WorkDays sc = new WorkDays();
            this.Cursor = Cursors.Default;
            sc.ShowDialog();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            ClassLib.Memory.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            ClassLib.Memory.Heap();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void linkLabel1_MouseClick(object sender, MouseEventArgs e)
        {
            //  MessageBox.Show("aa");
            CountDisplay = 0;
            SqlGetName.Equals("home");
            CallDisplayHome();
        }

        private void radMenuItem10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Product is activated.");
        }

        private void radMenuItem9_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"Report\ManualHHL.pdf");
            }
            catch { }
        }

        private void radMenuItem23_Click(object sender, EventArgs e)
        {
            //Add User//
            try
            {
                UserList ul = new UserList();
                ul.ShowDialog();
            }
            catch { }
        }

        private void radMenuItem24_Click(object sender, EventArgs e)
        {
            try
            {
                DepartmentList dl = new DepartmentList();
                dl.ShowDialog();
            }
            catch { }
        }

        private void radMenuItem25_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeDept cd = new ChangeDept(linkLabel1);
                cd.ShowDialog();
            }
            catch { }
        }

        private void radMenuItem26_Click(object sender, EventArgs e)
        {
            try
            {
                MachineType mc = new MachineType();
                mc.ShowDialog();
            }
            catch { }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            WaitingPR wp = new WaitingPR();
            wp.ShowDialog();
        }
        private void CalFN()
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    //  db.Sp_B004_CALLALL();

                    var getpr = db.spx_002_getPRListWaiting("", dbClss.DeptSC).ToList();

                    //tb_WorkProcess wp = db.tb_WorkProcesses.Where(w => w.Status == "OverDue").FirstOrDefault();
                    if (getpr.Count>0)
                    {
                        // picAlert
                        picAlert.Invoke((MethodInvoker)(() => picAlert.Visible = true));
                    }
                    else
                    {
                        picAlert.Invoke((MethodInvoker)(() => picAlert.Visible = false));
                        lblTimetick.Invoke((MethodInvoker)(() => lblTimetick.Text = ""));
                    }

                    
                }
               // CallShip();
            }
            catch { }
        }
        int SHnoPK = 0;
        private void CallShip()
        {
            //return;
            try
            {
                int ERR = 0;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var listShip = db.spx23_ListShippingCodeItem(dbClss.DeptSC, "").ToList();
                    if(listShip.Count>0)
                    {
                        string SHNo = "";

                        foreach (var rd in listShip)
                        {
                            if (StockControl.dbClss.TDe(rd.Qty) > 0)
                            {
                                SHNo = StockControl.dbClss.GetNo(SHnoPK, 2);
                                //Create Header//
                                ERR = AddShipHeader(rd.CreateBy, SHNo);
                                if (ERR == 1)
                                {
                                    //Create Detail
                                    tb_Shipping u = new tb_Shipping();
                                    tb_Item tm = db.tb_Items.Where(i => i.CodeNo == rd.CodeNo).FirstOrDefault();
                                    if (tm != null)
                                    {
                                        u.DeptCode = tm.DeptCode;
                                        u.AccountCode = "";

                                        u.ShippingNo = SHNo;
                                        u.CodeNo = rd.CodeNo;
                                        u.ItemNo = tm.ItemNo;
                                        u.ItemDescription = tm.ItemDescription;
                                        u.QTY = StockControl.dbClss.TDe(rd.Qty);
                                        u.PCSUnit = 1;
                                        u.UnitShip = tm.UnitShip;
                                        u.Remark = "By Auto Ship";
                                        u.LotNo = "";
                                        u.SerialNo = "";// StockControl.dbClss.TSt(g.Cells["SerialNo"].Value);
                                        u.MachineName = rd.Machine;
                                        u.LineName = rd.LineName;
                                        u.MOLD = rd.Mold;
                                        u.Calbit = false;
                                        u.ClearFlag = false;
                                        u.ClearDate = DateTime.Now;
                                        u.Seq = 1;
                                        u.Status = "Completed";
                                        u.ShipType = "SH";
                                        u.UnitCost = tm.StandardCost;
                                        u.Amount = u.QTY * u.UnitCost;
                                        u.Dept = dbClss.DeptSC; 
                                        db.tb_Shippings.InsertOnSubmit(u);
                                        db.SubmitChanges();

                                        tb_ShippingPDA pd = db.tb_ShippingPDAs.Where(p => p.id ==rd.id).FirstOrDefault();
                                        if (pd != null)
                                        {
                                            pd.Status = "Completed";
                                            db.SubmitChanges();
                                        }

                                        //Insert Stock
                                        var g = (from ix in db.tb_Shippings
                                                 where ix.ShippingNo.Trim() == SHNo && ix.Status != "Cancel"

                                                 select ix).ToList();
                                        if (g.Count > 0)
                                        {
                                            //insert Stock

                                            foreach (var vv in g)
                                            {                                               
                                                db.spx_010_CustStock(DateTime.Now, "Shipping", 1, SHNo, "", StockControl.dbClss.TDe(rd.Qty), dbClss.UserID, DateTime.Now, rd.CodeNo
                                         , 3, vv.id, dbClss.DeptSC, rd.DeptCode, 1, "", "");
                                            }
                                        }

                                        //update Stock เข้า item
                                        db.sp_010_Update_StockItem(Convert.ToString(rd.CodeNo), "");
                                    }
                                }
                            }

                        }
                    }
                }

            }
            catch { }
        }

        private int AddShipHeader(string UserShip,string SHNo)
        {
            int ERR = 0;
            try
            {
                
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    byte[] barcode = null;
                    DateTime? UpdateDate = null;

                    DateTime? RequireDate = DateTime.Now;                   

                    tb_ShippingH gg = new tb_ShippingH();
                    gg.ShippingNo = SHNo;
                    gg.ShipDate = RequireDate;
                    gg.UpdateBy = null;
                    gg.UpdateDate = UpdateDate;
                    gg.CreateBy = ClassLib.Classlib.User;
                    gg.CreateDate = DateTime.Now;
                    gg.ShipName = UserShip;
                    gg.Remark = "By AutoShip";
                    gg.Dept = dbClss.DeptSC;

                    // gg.BarCode = barcode;
                    gg.Status = "Completed";
                    db.tb_ShippingHs.InsertOnSubmit(gg);
                    db.SubmitChanges();
                    ERR = 1;
                }
            }
            catch { ERR = 0; }
            return ERR;
        }

        private void CallPic()
        {
            try
            {
                lblTimetick.Text = "Run:" + DateTime.Now.ToString("HH:mm:ss");
                Thread t = new Thread(new ThreadStart(CalFN));
                t.Start();



                //label5.Invoke((MethodInvoker)(() => label5.Text = "Requested" + repeats + "Times"));
            }
            catch { }
        }

        private void radMenuItem27_Click(object sender, EventArgs e)
        {
            try
            {
                CloseShippingDate cl = new CloseShippingDate();
                cl.ShowDialog();
            }
            catch { }
        }
    }
}
