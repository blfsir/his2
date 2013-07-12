using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Maticsoft.Model;
using HIS.common;

namespace HIS
{
    public partial class BadReactionDetail : Form
    {
        private OperateType OperatType = OperateType.VIew;//操作类型 0新增 1 编辑 2 查看 
        private string ActionID = "";//编号
        private int PID = 0;//患者编号
        private int CurrentPeriod = TreatPeroid.InTreatment; //
        public BadReaction returnValue =null;
        private BadReaction action = null;
        public BadReactionDetail(BadReaction badAction)
        {
            InitializeComponent();
            action = badAction;
            BindActionDetail();
            
        }

        private void BindActionDetail()
        {
            //DataTable dt = new DataTable();
            //Maticsoft.BLL.BadReaction bll = new Maticsoft.BLL.BadReaction();

            //BadReaction action = bll.GetModel(int.Parse(ActionID));

            if (action != null)
            {
                this.txtReactionName.Text = action.ReactionName;
                dtpOccurDate.Value = action.OccurDate.Value;
                this.txtSeverity.Text = action.Severity;
                txtTreatMethod.Text = action.TreatMethod;
                txtTreatResult.Text = action.TreatResult;
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            bool result = false;
            Maticsoft.BLL.BadReaction bll = new Maticsoft.BLL.BadReaction();

            BadReaction action = new BadReaction();

            action.Peroid = CurrentPeriod;
            action.PID = PID;
            action.ReactionName = txtReactionName.Text;
            action.Severity = txtSeverity.Text;
            action.TreatMethod = txtTreatMethod.Text;
            action.TreatResult = txtTreatResult.Text;
            action.OccurDate = dtpOccurDate.Value;
            //if (OperatType == OperateType.ADD)//编辑，查看
            //{
            //    result=bll.Add(action);
            //}
            //else
            //{
            //    action.ID = int.Parse(ActionID);
            //    result = bll.Update(action);
            //}
            //if (result) 
            //{
               // MessageBox.Show(this, "操作成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                this.DialogResult = DialogResult.OK;
                this.returnValue = action;
                this.Close();
             
            //}
        }
    }
}
