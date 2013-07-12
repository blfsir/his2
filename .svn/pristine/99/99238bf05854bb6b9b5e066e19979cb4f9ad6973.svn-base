using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using HIS.common;
using System.Windows.Forms;
using Maticsoft.Model;
using Infragistics.Win.UltraWinGrid;

namespace HIS
{
    public partial class AfterTreatTrace : Form
    {
        private int pid = 0; //患者ID
        private int period = 0; //治疗后期间
        private string periodName = "";
        private string patientName = "";
        public AfterTreatTrace(int patientID, int treatPeriod)
        {
            InitializeComponent();
            pid = patientID;
            period = treatPeriod;
            Maticsoft.BLL.Patient patientBLL = new Maticsoft.BLL.Patient();
            Patient p =patientBLL.GetModel(pid);
            if (p != null)
            {
                patientName = p.Name;
            }
            switch (period) {
                case TreatPeroid.AfterTreatment.OneWeek:
                    periodName = "治疗后第1周";
                    break;
                case TreatPeroid.AfterTreatment.OneMonth:
                    periodName = "治疗后1月";
                    break;
                case TreatPeroid.AfterTreatment.ThreeMonth:
                    periodName = "治疗后3月";
                    break;
                case TreatPeroid.AfterTreatment.SixMonth:
                    periodName = "治疗后6月";
                    break;
                case TreatPeroid.AfterTreatment.OneYear:
                    periodName = "治疗后1年";
                    break;
                case TreatPeroid.AfterTreatment.TwoYear :
                    periodName = "治疗后2年";
                    break;
                case TreatPeroid.AfterTreatment.ThreeYear:
                    periodName = "治疗后3年 ";
                    break;
                case TreatPeroid.AfterTreatment.FourYear:
                    periodName = "治疗后4年";
                    break;
                case TreatPeroid.AfterTreatment.FiveYear:
                    periodName = "治疗后5年";
                    break;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("填写");
            sb.Append(periodName);
            sb.Append("随访数据 - ");
            sb.Append("[患者:");
            sb.Append(patientName);
            sb.Append("]");
            this.Text = sb.ToString();
            BindWeekInfo();
            FreezeForm.ChangeControlEditable(this, false);
             
        }
        //internal static void ChangeControlEditable(Control f, bool isEditable)
        //{
        //    foreach (Control c in f.Controls)
        //    {


        //        // MessageBox.Show(c.GetType().ToString());
        //        if (c.HasChildren)
        //        {
        //            ChangeControlEditable(c, isEditable);
        //        }
        //        else if (c is TextBox)
        //        {
        //            TextBox lll = (TextBox)c;
        //            if (lll.Name != "txtLungPicFile")
        //            {
        //                lll.ReadOnly = !isEditable;
        //            }

        //        }
        //        else if (c is DateTimePicker)
        //        {
        //            DateTimePicker dtp = (DateTimePicker)c;
        //            dtp.Enabled = isEditable;
        //        }
        //        else if (c is Button)
        //        {
        //            Button b = (Button)c;
        //            if (b.Text != "编辑" && b.Text != "查看")
        //            {
        //                b.Enabled = isEditable;
        //            }
        //        }
                
        //    }
        //}
        private void BindWeekInfo()
        {
            this.BindBloodGasInfo_w();
            this.BindLungInfo_w();
            this.BindDicomInfo_w();
            this.BindSportLifeInfo_w();
            this.bindBadAction_w();
        }
        private void BindSportLifeInfo_w()
        {
            Maticsoft.BLL.SportLife bll = new Maticsoft.BLL.SportLife();

            List<SportLife> lungList = bll.GetModelList("PID=" + pid.ToString() + " AND Period=" + period.ToString());
            if (lungList != null && lungList.Count > 0)
            {
                SportLife sl = lungList[0];
                //lblSportLifeID.Tag = sl.ID;
                txtSixTest_w.Text = sl.SixMinuteTest.ToString();
                dtpSixTest_w.Value = sl.SixMinuteTestDate.Value;
                txtGeorgeTest_w.Text = sl.StGeorgeTest.ToString();
                dtpGeorgeTest_w.Value = sl.StGeorgeTestDate.Value;
                //txtGeorgeFile_w.Text = sl.StGeorgeTestFile;
                this.uploaddicomGeorge_w.SetFile(sl.StGeorgeTestFile);
                txtCatTest_w.Text = sl.CATTest.ToString();
                dtpCatTest_w.Value = sl.CATTestDate.Value;
                txtMmrcTest_w.Text = sl.MMRCTest.ToString();
                dtpMmrc_w.Value = sl.MMRCTestDate.Value;


            }

        }
        private void BindLungInfo_w()
        {
            Maticsoft.BLL.Lung bll = new Maticsoft.BLL.Lung();

            List<Lung> lungList = bll.GetModelList("PID=" + pid.ToString() + " AND Period=" + period.ToString());
            if (lungList != null && lungList.Count > 0)
            {
                Lung lung = lungList[0];
                txtFev1_w.Text = lung.fev1;
                txtFev1pre_w.Text = lung.fev1pre;
                txtFvc_w.Text = lung.fvc;
                txtFvcpre_w.Text = lung.fvcpre;
                txtFev1fvc_w.Text = lung.fev1fvc;
                txtTlc_w.Text = lung.tlc;
                txtTlvpre_w.Text = lung.tlvpre;
                txtRV_w.Text = lung.rv;
                txtRvpre_w.Text = lung.rvpre;
                txtRvtlc_w.Text = lung.rvtlc;
                dtpLung_w.Value = lung.CheckTime.Value;

                this.uploaddicomLung_w.SetFile(lung.File);
            }
        }
        private void BindDicomInfo_w()
        {
            Maticsoft.BLL.dicom dicomBLL = new Maticsoft.BLL.dicom();

            List<dicom> lungList = dicomBLL.GetModelList("PID=" + pid.ToString() + " AND Period=" + period.ToString());
            if (lungList != null && lungList.Count > 0)
            {
                dicom dicom = lungList[0];

                //txtHeter.Text = dicom.Heterogeneity;
                //txtLobeSplit.Text = dicom.LobeSplit;
                txtLobeVolumn_w.Text = dicom.TreatLobeVolumn;
                txtLobeVolumn2_w.Text = dicom.UnTreatLobeVolumn;
                dtpDicom_w.Value = dicom.CheckDate.Value;
                //txtDicomFile_w.Text = dicom.File;
                this.uploaddicom_w.SetFile(dicom.File);

            }
        }
        private void bindBadAction_w()
        {
            Maticsoft.BLL.BadReaction bll = new Maticsoft.BLL.BadReaction();
            DataSet ds = bll.GetList("PID=" + pid.ToString() + " AND Peroid=" + period.ToString());
            this.ugBadAction_w.DataSource = ds;
            this.ugBadAction_w.DataBind();

        }
        private void BindBloodGasInfo_w()
        {
            Maticsoft.BLL.BloodGas bll = new Maticsoft.BLL.BloodGas();

            List<BloodGas> bloodList = bll.GetModelList("PID=" + pid.ToString() + " AND Period=" + period.ToString());
            if (bloodList != null && bloodList.Count > 0)
            {
                BloodGas bg = bloodList[0];
                //this.lblBloodID.Tag = bg.ID;
                this.txtPH_w.Text = bg.pH;
                this.txtPaO2_w.Text = bg.PaO2;
                this.txtSaO2_w.Text = bg.SaO2;
                this.txtPcO2_w.Text = bg.PaCO2;
                this.txtRealHCO3_w.Text = bg.RealHCO3;
                this.txtStandHCO3_w.Text = bg.StandHCO3;
                this.txtAB_w.Text = bg.AB;
                this.txtBE_w.Text = bg.BE;
                this.txtAG_w.Text = bg.AG;
                this.dtpBloodGas_w.Value = bg.CheckDate.Value;
            }
        }


        private void button10_Click(object sender, EventArgs e)
        {
            bool result = false;

            BloodGas bg = new BloodGas();
            bg.PID = pid;
            bg.Period = period;
            bg.pH = txtPH_w.Text;
            bg.PaO2 = txtPaO2_w.Text;
            bg.SaO2 = txtSaO2_w.Text;
            bg.PaCO2 = txtPcO2_w.Text;
            bg.RealHCO3 = txtRealHCO3_w.Text;
            bg.StandHCO3 = txtStandHCO3_w.Text;
            bg.AB = txtAB_w.Text;
            bg.BE = txtBE_w.Text;
            bg.AG = txtAG_w.Text;
            bg.CheckDate = dtpBloodGas_w.Value;
            Maticsoft.BLL.BloodGas bgBll = new Maticsoft.BLL.BloodGas();
            result = bgBll.SaveOrUpdate(bg);

            Lung lung = new Lung();
            lung.PID = pid;
            lung.Period = period;
            lung.fev1 = txtFev1_w.Text;
            lung.fev1pre = txtFev1pre_w.Text;
            lung.fvc = txtFvc_w.Text;
            lung.fvcpre = txtFvcpre_w.Text;
            lung.fev1fvc = txtFev1fvc_w.Text;
            lung.tlc = txtTlc_w.Text;
            lung.tlvpre = txtTlvpre_w.Text;
            lung.rv = txtRV_w.Text;
            lung.rvpre = txtRvpre_w.Text;
            lung.rvtlc = txtRvtlc_w.Text;
            lung.CheckTime = dtpLung_w.Value;
            lung.File = this.uploaddicomLung_w.File;
            Maticsoft.BLL.Lung lungBLL = new Maticsoft.BLL.Lung();
            result = lungBLL.SaveOrUpdate(lung);

            dicom di = new dicom();
            di.PID = pid;
            di.Period = period;
            di.TreatLobeVolumn = txtLobeVolumn_w.Text;
            di.UnTreatLobeVolumn = txtLobeVolumn2_w.Text;

            di.File = this.uploaddicom_w.File;
            di.CheckDate = dtpDicom_w.Value;
            di.Heterogeneity = "";
            di.LobeSplit = "";

            Maticsoft.BLL.dicom diBLL = new Maticsoft.BLL.dicom();
            result = diBLL.SaveOrUpdate(di);

            SportLife sl = new SportLife();
            sl.PID = pid;
            sl.Period = period;
            int sixTest = 0;
            int.TryParse(txtSixTest_w.Text, out sixTest);

            sl.SixMinuteTest = sixTest;
            sl.SixMinuteTestDate = dtpSixTest_w.Value;
            sixTest = 0;
            int.TryParse(txtGeorgeTest_w.Text, out sixTest);

            sl.StGeorgeTest = sixTest;// int.Parse(txtGeorgeTest_w.Text);
            sl.StGeorgeTestDate = dtpGeorgeTest_w.Value;
            sl.StGeorgeTestFile = this.uploaddicomGeorge_w.File;
            sixTest = 0;
            int.TryParse(txtCatTest_w.Text, out sixTest);

            sl.CATTest = sixTest;// int.Parse(txtCatTest_w.Text);
            sl.CATTestDate = dtpCatTest_w.Value;
            sixTest = 0;
            int.TryParse(txtMmrcTest_w.Text, out sixTest);


            sl.MMRCTest = sixTest;// int.Parse(this.txtMmrc_w.Text);
            sl.MMRCTestDate = dtpMmrc_w.Value;
            Maticsoft.BLL.SportLife slBLL = new Maticsoft.BLL.SportLife();
            result = slBLL.SaveOrUpdate(sl);

            List<BadReaction> treatHistories = new List<BadReaction>();
            foreach (UltraGridRow r in this.ugBadAction_w.Rows)
            {
                BadReaction th = new BadReaction();
                th.PID = pid;
                if (r.Cells["ID"].Text != "")
                {
                    th.ID = int.Parse(r.Cells["ID"].Text);
                }
                th.Peroid = period;
                th.ReactionName = r.Cells["ReactionName"].Value.ToString();
                th.OccurDate = DateTime.Parse(r.Cells["OccurDate"].Value.ToString());
                th.Severity = r.Cells["Severity"].Value.ToString();
                th.TreatMethod = r.Cells["TreatMethod"].Value.ToString();
                th.TreatResult = r.Cells["TreatResult"].Value.ToString();
                treatHistories.Add(th);
            }
            Maticsoft.BLL.BadReaction brBLL = new Maticsoft.BLL.BadReaction();
            foreach (BadReaction th in treatHistories)
            {
                if (th.ID != 0)
                {
                    result = brBLL.Update(th);
                }
                else
                {
                    result = brBLL.Add(th);
                }

            }
            if (result)
            {
                MessageBox.Show(this, periodName +"数据保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FreezeForm.ChangeControlEditable(this, false);
            }
            else
            {
                MessageBox.Show(this, periodName+"数据保存失败,请重试!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FreezeForm.ChangeControlEditable(this, true);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            BadReactionDetail openDialog = new BadReactionDetail(null);

            openDialog.ShowDialog();
            if (openDialog.DialogResult == DialogResult.OK)
            {
                if (openDialog.returnValue != null)
                {
                    BadReaction bd = openDialog.returnValue;

                    ugBadAction_w.DisplayLayout.Rows.Band.AddNew();
                    ugBadAction_w.Rows[ugBadAction_w.Rows.Count - 1].Cells["PID"].Value = this.pid;


                    ugBadAction_w.Rows[ugBadAction_w.Rows.Count - 1].Cells["Peroid"].Value = bd.Peroid;
                    ugBadAction_w.Rows[ugBadAction_w.Rows.Count - 1].Cells["ReactionName"].Value = bd.ReactionName;
                    ugBadAction_w.Rows[ugBadAction_w.Rows.Count - 1].Cells["OccurDate"].Value = bd.OccurDate.Value.ToString("yyyy-MM-dd");
                    ugBadAction_w.Rows[ugBadAction_w.Rows.Count - 1].Cells["Severity"].Value = bd.Severity;
                    ugBadAction_w.Rows[ugBadAction_w.Rows.Count - 1].Cells["TreatMethod"].Value = bd.TreatMethod;
                    ugBadAction_w.Rows[ugBadAction_w.Rows.Count - 1].Cells["TreatResult"].Value = bd.TreatResult;
                }
            }
        }

        private void ugBadAction_w_ClickCellButton(object sender, CellEventArgs e)
        {
            if (e.Cell.Text == "删除")
            {
                DialogResult dr = MessageBox.Show(this, "确定要删除此行?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                {
                    if (ugBadAction_w.ActiveRow.Cells["ID"].Text != "")
                    {
                        Maticsoft.BLL.BadReaction bll = new Maticsoft.BLL.BadReaction();
                        bll.Delete(int.Parse(ugBadAction_w.ActiveRow.Cells["ID"].Text));
                    }
                    ugBadAction_w.ActiveRow.Delete(false);//删除所选行
                }
            }
            if (e.Cell.Text == "编辑")
            {
                BadReaction bd = new BadReaction();
                // bd.ID = int.Parse(ugBadReaction.ActiveRow.Cells["ID"].Text);
                bd.ReactionName = ugBadAction_w.ActiveRow.Cells["ReactionName"].Text;
                bd.OccurDate = DateTime.Parse(ugBadAction_w.ActiveRow.Cells["OccurDate"].Text);
                bd.Severity = ugBadAction_w.ActiveRow.Cells["Severity"].Text;
                bd.TreatMethod = ugBadAction_w.ActiveRow.Cells["TreatMethod"].Text;
                bd.TreatResult = ugBadAction_w.ActiveRow.Cells["TreatResult"].Text;

                BadReactionDetail openDialog = new BadReactionDetail(bd);
                //Butcher_EnterregisterDetail openDialog = new Butcher_EnterregisterDetail(0, 0, "");
                openDialog.ShowDialog();
                if (openDialog.DialogResult == DialogResult.OK)
                {
                    if (openDialog.returnValue != null)
                    {
                        BadReaction returnAction = openDialog.returnValue;

                        ugBadAction_w.ActiveRow.Cells["ReactionName"].Value = returnAction.ReactionName;
                        ugBadAction_w.ActiveRow.Cells["OccurDate"].Value = returnAction.OccurDate.Value.ToString("yyyy-MM-dd");
                        ugBadAction_w.ActiveRow.Cells["Severity"].Value = returnAction.Severity;
                        ugBadAction_w.ActiveRow.Cells["TreatMethod"].Value = returnAction.TreatMethod;
                        ugBadAction_w.ActiveRow.Cells["TreatResult"].Value = returnAction.TreatResult;

                    }
                }
            }
        }
    }
}
