using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HIS.common;
using System.Threading;

namespace HIS
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            ShowPops();
        }

        

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            bool ismdiChildrenForm = true;//是否是MDI子窗体,默认为true;
            //已打开窗口不打开.
            if (ShowForm(e.Tool.Key) == false)
            {
                return;
            }
            //要显示的窗体
            Form mdiChildrenForm = null;
            switch (e.Tool.Key.Trim())
            {
                 //用户信息
                 

                //退出
                case "Mm_Exit":
                    Application.Exit();
                    break;
                 
                case "Mm_Patient":
                    mdiChildrenForm = new PatientList();
                    break;
                case "Mm_PatientManage":
                    mdiChildrenForm = new PatientList();
                    break;
                case "M_Trace":
                    mdiChildrenForm = new TraceList();
                    break;
                case "M_ETC":
                    mdiChildrenForm = new DataETC(); 
                    break;    
                #region 基础设置
                //用户管理
                case "Mm_UserManager":
                    //mdiChildrenForm = new Sys.UsersManagerMain();
                    break;
                //用户组管理
                case "Mm_UserGroupManager":
                    //mdiChildrenForm = new Sys.UsersGroupMain();
                    break;
                #endregion

                #region 帮助
                 case "Mm_VersionInfo":
                    ismdiChildrenForm = false;
                    mdiChildrenForm = new About();
                    break;

                #endregion
            }
            if (mdiChildrenForm != null)
            {
                mdiChildrenForm.Tag = e.Tool.Key;
                if (ismdiChildrenForm == true)
                {
                    mdiChildrenForm.MdiParent = this;
                    mdiChildrenForm.Location = new Point(0, 0);
                    mdiChildrenForm.Show();
                }
                else
                {
                    mdiChildrenForm.ShowDialog();
                }
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (MessageBox.Show("确定退出吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                //this.notifyIcon1.Dispose();
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }


        /// <summary>
        /// 打开窗体的不打开,标签号移到打开窗口位置.
        /// </summary>
        /// <param name="formName"></param>
        /// <returns></returns>
        public bool ShowForm(string formkey)
        {
            try
            {
                foreach (Form f in this.MdiChildren)
                {
                    if (f.Tag.Equals(formkey))
                    {
                        f.Activate();
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ShowPops()
        {
            Maticsoft.BLL.Patient bll = new Maticsoft.BLL.Patient();
            DataSet ds = bll.GetTracedPatient();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                { 
                    ShowPatientPop(dr["ID"].ToString(), dr["Name"].ToString(), dr["Period"].ToString(), dr["PeriodName"].ToString());
                    
                }
            }
        }

        private void ShowPatientPop(string pid, string name, string period, string periodName)
        {
            PopupNotifier pop = new PopupNotifier(this.components); 
            pop.TitleText = name;
            pop.ContentText = periodName;
            pop.ShowCloseButton = true;// chkClose.Checked;
            pop.ShowOptionsButton = true;// chkMenu.Checked;
            pop.ShowGrip = true;// chkGrip.Checked;
            pop.Delay = 3000;
            pop.OptionsMenu = this.contextMenuStrip1;
            pop.AnimationInterval = 300;// int.Parse(txtInterval.Text);
            pop.AnimationDuration = 1000;// int.Parse(txtAnimationDuration.Text);
            pop.TitlePadding = new Padding(0);
            pop.ContentPadding = new Padding(0);
            pop.ImagePadding = new Padding(0);
            pop.Scroll = true;// chkScroll.Checked;
            pop.Image = Properties.Resources.Grip;
            // Create the thread object, passing in the Alpha.Beta method
            // via a ThreadStart delegate. This does not start the thread.
           
      

            pop.Popup();
        }


        //private void button1_Click(object sender, EventArgs e)
        //{
        //    popupNotifier1.TitleText = "txtTitle.Text";
        //    popupNotifier1.ContentText = "txtText.Text";
        //    popupNotifier1.ShowCloseButton = true;// chkClose.Checked;
        //    popupNotifier1.ShowOptionsButton = true;// chkMenu.Checked;
        //    popupNotifier1.ShowGrip = true;// chkGrip.Checked;
        //    popupNotifier1.Delay = 3000;
        //    popupNotifier1.OptionsMenu = this.contextMenuStrip1;
        //    popupNotifier1.AnimationInterval = 300;// int.Parse(txtInterval.Text);
        //    popupNotifier1.AnimationDuration = 1000;// int.Parse(txtAnimationDuration.Text);
        //    popupNotifier1.TitlePadding =  new Padding(0);
        //    popupNotifier1.ContentPadding =  new Padding(0);
        //    popupNotifier1.ImagePadding =  new Padding(0);
        //    popupNotifier1.Scroll = true;// chkScroll.Checked;
             

        //    //if (chkIcon.Checked)
        //    //{
        //    //    popupNotifier1.Image = Properties.Resources._157_GetPermission_48x48_72;
        //    //}
        //    //else
        //    //{
        //    popupNotifier1.Image = Properties.Resources.Grip;
        //    //}

        //    popupNotifier1.Popup();
        //}

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Icon Notify Double Clicked"); 
        }

    }
}
