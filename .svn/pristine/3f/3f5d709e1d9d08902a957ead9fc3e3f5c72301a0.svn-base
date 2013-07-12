using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HIS.common;

namespace HIS
{
    public partial class TreatmentInfo : Form
    {
        private int pid = 0;
        public TreatmentInfo(int patientID)
        {
            pid = patientID;
            InitializeComponent();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            BadReactionDetail openDialog = new BadReactionDetail(null);
            //Butcher_EnterregisterDetail openDialog = new Butcher_EnterregisterDetail(0, 0, "");
            openDialog.ShowDialog();
            if (openDialog.DialogResult == DialogResult.OK)
            {
                LoadBadActionList();
            }
        }

        private void LoadBadActionList()
        {
            Maticsoft.BLL.BadReaction bll = new Maticsoft.BLL.BadReaction();
            DataSet ds = bll.GetList("PID=" + pid.ToString() + " AND peroid=" + TreatPeroid.InTreatment);
            this.gridInfo.DataSource = ds.Tables[0];
            this.gridInfo.DataBind();
        }
    }
}
