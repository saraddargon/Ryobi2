using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.Linq;
namespace StockControl
{
    public partial class Login : Telerik.WinControls.UI.RadForm
    {
        public Login()
        {
            InitializeComponent();
            radLabel3.Text = dbClss.versioin;
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!cboDept.Text.Equals(""))
                {
                    string VersionMatch = "";
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        tb_User um = db.tb_Users.Where(m => m.UserID == txtUser.Text && m.Password == txtPassword.Text && m.Active == true).FirstOrDefault();
                        if (um != null)
                        {
                            tb_UserDept uc = db.tb_UserDepts.Where(ux => ux.UserID.ToLower() == txtUser.Text.ToLower() && ux.DeptCode.ToLower()==cboDept.Text.ToLower()).FirstOrDefault();
                            if (uc != null || txtUser.Text.ToLower().Equals("admin"))
                            {

                                dbClss.UserID = txtUser.Text;
                                dbClss.DeptSC = cboDept.Text;
                                this.Hide();
                                tb_UserMachine mc = db.tb_UserMachines.Where(m => m.MachineName == Environment.MachineName).FirstOrDefault();
                                if (mc != null)
                                {
                                    //mc.UserID = txtUser.Text;
                                    //mc.DeptCode = cboDept.Text;
                                    db.tb_UserMachines.DeleteOnSubmit(mc);
                                    db.SubmitChanges();
                                }
                                tb_company02 cb = db.tb_company02s.FirstOrDefault();
                                if(cb!=null)
                                {
                                    //RYOBI
                                    //OGUSU
                                    dbClss.Company = cb.Company.ToString();
                                    if(cb.Versions.Equals(dbClss.VersionCheck))
                                    {
                                        VersionMatch = "OK";
                                    }else
                                    {
                                        VersionMatch = "Version Not Match!";
                                    }
                                   
                                }
                                if (VersionMatch.Equals("OK"))
                                {
                                    dbClss.Language = radDropDownList1.Text;
                                    tb_UserMachine usm = new tb_UserMachine();
                                    usm.UserID = txtUser.Text;
                                    usm.MachineName = Environment.MachineName;
                                    usm.DeptCode = cboDept.Text;
                                    usm.Lang = radDropDownList1.Text;
                                    db.tb_UserMachines.InsertOnSubmit(usm);
                                    db.SubmitChanges();


                                    Mainfrom mf = new Mainfrom();
                                    mf.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show(VersionMatch);
                                }
                                this.Close();
                            }else
                            {
                                MessageBox.Show("ไม่มีสิทธ์เข้าใช้งานแผนกนี้!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }


                        }
                        else
                        {
                            MessageBox.Show("ไม่พบข้อมูลผู้ใช้งาน !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else { MessageBox.Show("กรุณาเลือกแผนก!"); }


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        string DeptCode = "";
        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
               
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    tb_UserMachine um = db.tb_UserMachines.Where(m => m.MachineName == Environment.MachineName).FirstOrDefault();
                    if (um != null)
                    {
                        txtUser.Text = um.UserID.ToString();
                        DeptCode = um.DeptCode.ToString();
                        radDropDownList1.Text = um.Lang.ToString();
                    }

                }
                LoadDefault();
                if(!DeptCode.Equals(""))
                {
                    cboDept.Text = DeptCode;
                }
                if(CCount==1)
                {
                    cboDept.SelectedIndex = 0;
                }
                


            }
            catch { }
        }
        int CCount = 0;
        private void LoadDefault()
        {
            try
            {
                CCount = 0;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var dept = db.tb_UserDepts.Where(u => u.UserID == txtUser.Text).ToList();
                    if (dept.Count > 0)
                    {
                        cboDept.Items.Clear();
                        //cboDept.Items.Add("");
                        foreach (var rd in dept)
                        {
                            CCount += 1;
                            cboDept.Items.Add(rd.DeptCode.ToString());
                        }
                    }
                    else
                    {
                        var dept2 = db.spx_001_GroupDept().ToList();
                        if (dept2.Count > 0)
                        {
                            cboDept.Items.Clear();
                            //cboDept.Items.Add("");
                            foreach (var rd in dept2)
                            {
                                CCount += 1;
                                cboDept.Items.Add(rd.DeptAccount.ToString());
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                radButton1_Click(sender, e);
            }
        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                LoadDefault();
            }
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการที่จะ Update หรือไม่ ?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("AutoUpdate.exe");
                Application.Exit();
            }
        }
    }
}
