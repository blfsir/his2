﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HIS.common;
using Maticsoft.Model;
using Infragistics.Win.UltraWinGrid;
using System.Diagnostics;
using Infragistics.Win;
using System.IO;


namespace HIS
{
    public partial class DetailInfo : Form
    {
        private GeneralInfo generalInfo = new GeneralInfo(); //一般情况

        private OperateType OperatType = OperateType.VIew;//操作类型 0新增 1 编辑 2 查看
        private int pid = 0; //患者ID

        public DetailInfo(OperateType operatType, int patientID)
        {
            InitializeComponent();
            //加载UltarGrid样式
            //this.gridInfo.DisplayLayout.LoadFromXml(System.IO.Path.GetFullPath(Application.StartupPath + @"\UltragridStyle.xml"));

            //加载UltarGrid样式
            //  this.ugBadReaction.DisplayLayout.LoadFromXml(System.IO.Path.GetFullPath(Application.StartupPath + @"\UltragridStyle.xml"));

            OperatType = operatType;//操作类型 0新增 1 编辑 2 查看
            pid = patientID;

            BindingComboBox();//绑定所有的下拉框
            BindAllInfos();//绑定每页的各项信息

            //查看状态下禁止页面编辑
            if (OperatType == OperateType.VIew)
            {
                //DisableEdit(true);
                FreezeForm.ChangeControlEditable(this, false);
            }

        }

        private void DisableEdit(bool isDesalbeEdit)
        {
            DisableGeneralInfoEdit(isDesalbeEdit);
            this.DisableCOPDEdit(isDesalbeEdit);
            DiseableBeforeBloodPageEdit(isDesalbeEdit);
        }

        private void DisableGeneralInfoEdit(bool isDesalbeEdit)
        {
            if (isDesalbeEdit)
            {
                this.gridInfo.DisplayLayout.Bands[0].Columns["DELETE"].Hidden = true;
                this.button3.Enabled = false;
                btnAdd.Visible = false;
                this.txtHeight.ReadOnly = true;
                this.txtWaist.ReadOnly = true;
                this.txtWeight.ReadOnly = true;
                this.txtSmokeYear.ReadOnly = true;
                this.txtSmokePerDay.ReadOnly = true;
                this.ckbIsQuitSmoke.Enabled = false;

            }
            else
            {
                this.gridInfo.DisplayLayout.Bands[0].Columns["DELETE"].Hidden = false;
                this.button3.Enabled = true;
                this.btnAdd.Enabled = true;
                btnAdd.Visible = true;
                this.txtHeight.ReadOnly = false;
                this.txtWaist.ReadOnly = false;
                this.txtWeight.ReadOnly = false;
                this.txtSmokeYear.ReadOnly = false;
                this.txtSmokePerDay.ReadOnly = false;
                this.ckbIsQuitSmoke.Enabled = true;

            }
        }


        /// <summary>
        /// 绑定每页的各项信息
        /// </summary>
        private void BindAllInfos()
        {

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy";
            dateTimePicker1.ShowUpDown = true;

            dtpBirthday.Format = DateTimePickerFormat.Custom;
            dtpBirthday.CustomFormat = "yyyy-MM-dd";
            dtpBirthday.ShowUpDown = true;



            //绑定人口学数据            
            BindDemographidInfo();
            //绑定一般情况
            BindGeneralInfo();
            ////绑定COPD数据
            BindCOPDInfo();

            ////绑定动脉血气数据
            BindBloodGasInfo();

            ////绑定肺功能数据
            BindLungInfo();

           this.BindChartisInfo();
           this.BindDicomInfo();
            this.BindSportLifeInfo();
        }

        private void BindBloodGasInfo()
        {
            Maticsoft.BLL.BloodGas bll = new Maticsoft.BLL.BloodGas();

            List<BloodGas> bloodList = bll.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.BeforTreatment.ToString());
            if (bloodList != null && bloodList.Count > 0)
            {
                BloodGas bg = bloodList[0];
                this.lblBloodID.Tag = bg.ID;
                this.txtPH.Text = bg.pH;
                this.txtPaO2.Text = bg.PaO2;
                this.txtSaO2.Text = bg.SaO2;
                this.txtPcO2.Text = bg.PaCO2;
                this.txtRealHCO3.Text = bg.RealHCO3;
                this.txtStandHCO3.Text = bg.StandHCO3;
                this.txtAB.Text = bg.AB;
                this.txtBE.Text = bg.BE;
                this.txtAG.Text = bg.AG;
                this.dtpBloodGas.Value = bg.CheckDate.Value;
            }
        }
        private void BindBloodGasInfo_w()
        {
            Maticsoft.BLL.BloodGas bll = new Maticsoft.BLL.BloodGas();

            List<BloodGas> bloodList = bll.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.AfterTreatment.OneWeek.ToString());
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


        private void BindCOPDInfo()
        {
            Maticsoft.BLL.COPDMedication bll = new Maticsoft.BLL.COPDMedication();
            List<COPDMedication> copdList = bll.GetModelList("PID=" + pid.ToString());
            foreach (COPDMedication copd in copdList)
            {
                #region Oral
                if (copd.COPDTypeName.Equals(COPD.Oral.茶碱.ToString()))
                {
                    ckbKouFu1.Tag = copd.ID;
                    ckbKouFu1.Checked = true;
                    txtKouFu1.Text = copd.DrugName;
                    txtKouFuJi1.Text = copd.Dose;
                    txtKouFuFa1.Text = copd.Usage;
                }
                if (copd.COPDTypeName.Equals(COPD.Oral.白三烯受体拮抗剂.ToString()))
                {
                    ckbKouFu2.Tag = copd.ID;
                    ckbKouFu2.Checked = true;
                    txtKouFu2.Text = copd.DrugName;
                    txtKouFuJi2.Text = copd.Dose;
                    txtKouFuFa2.Text = copd.Usage;
                }
                if (copd.COPDTypeName.Equals(COPD.Oral.选择性磷酸二酯酶4抑制剂.ToString()))
                {
                    ckbKouFu3.Tag = copd.ID;
                    ckbKouFu3.Checked = true;
                    txtKouFu3.Text = copd.DrugName;
                    txtKouFuJi3.Text = copd.Dose;
                    txtKouFuFa3.Text = copd.Usage;
                }
                if (copd.COPDTypeName.Equals(COPD.Oral.激素.ToString()))
                {
                    ckbKouFu4.Tag = copd.ID;
                    ckbKouFu4.Checked = true;
                    txtKouFu4.Text = copd.DrugName;
                    txtKouFuJi4.Text = copd.Dose;
                    txtKouFuFa4.Text = copd.Usage;
                }
                if (copd.COPDTypeName.Equals(COPD.Oral.化痰药.ToString()))
                {
                    ckbKouFu5.Tag = copd.ID;
                    ckbKouFu5.Checked = true;
                    txtKouFu5.Text = copd.DrugName;
                    txtKouFuJi5.Text = copd.Dose;
                    txtKouFuFa5.Text = copd.Usage;
                }
                if (copd.COPDTypeName.Equals(COPD.Oral.镇咳药.ToString()))
                {
                    ckbKouFu6.Tag = copd.ID;
                    ckbKouFu6.Checked = true;
                    txtKouFu6.Text = copd.DrugName;
                    txtKouFuJi6.Text = copd.Dose;
                    txtKouFuFa6.Text = copd.Usage;
                }
                if (copd.COPDTypeName.Equals(COPD.Oral.其它.ToString()))
                {
                    ckbKouFu7.Tag = copd.ID;
                    ckbKouFu7.Checked = true;
                    txtKouFu7.Text = copd.DrugName;
                    txtKouFuJi7.Text = copd.Dose;
                    txtKouFuFa7.Text = copd.Usage;
                }

                #endregion

                #region Suck
                if (copd.COPDTypeName.Equals(COPD.Suck.短效β受体激动剂.ToString()))
                {
                    ckbXiRu1.Tag = copd.ID;
                    ckbXiRu1.Checked = true;
                    txtXiRu1.Text = copd.DrugName;
                    txtXiRuJi1.Text = copd.Dose;
                    txtXiRuFa1.Text = copd.Usage;
                }
                if (copd.COPDTypeName.Equals(COPD.Suck.长效β受体激动剂.ToString()))
                {
                    ckbXiRu2.Tag = copd.ID;
                    ckbXiRu2.Checked = true;
                    txtXiRu2.Text = copd.DrugName;
                    txtXiRuJi2.Text = copd.Dose;
                    txtXiRuFa2.Text = copd.Usage;
                }
                if (copd.COPDTypeName.Equals(COPD.Suck.吸入激素.ToString()))
                {
                    ckbXiRu3.Tag = copd.ID;
                    ckbXiRu3.Checked = true;
                    txtXiRu3.Text = copd.DrugName;
                    txtXiRuJi3.Text = copd.Dose;
                    txtXiRuFa3.Text = copd.Usage;
                }
                if (copd.COPDTypeName.Equals(COPD.Suck.长效β受体激动剂或激素.ToString()))
                {
                    ckbXiRu4.Tag = copd.ID;
                    ckbXiRu4.Checked = true;
                    txtXiRu4.Text = copd.DrugName;
                    txtXiRuJi4.Text = copd.Dose;
                    txtXiRuFa4.Text = copd.Usage;
                }
                if (copd.COPDTypeName.Equals(COPD.Suck.长效抗胆碱能药物.ToString()))
                {
                    ckbXiRu5.Tag = copd.ID;
                    ckbXiRu5.Checked = true;
                    txtXiRu5.Text = copd.DrugName;
                    txtXiRuJi5.Text = copd.Dose;
                    txtXiRuFa5.Text = copd.Usage;
                }
                if (copd.COPDTypeName.Equals(COPD.Suck.其它.ToString()))
                {
                    ckbXiRu6.Tag = copd.ID;
                    ckbXiRu6.Checked = true;
                    txtXiRu6.Text = copd.DrugName;
                    txtXiRuJi6.Text = copd.Dose;
                    txtXiRuFa6.Text = copd.Usage;
                }
                #endregion
            }

        }

        private void BindDemographidInfo()
        {
            Maticsoft.BLL.Demographic bll = new Maticsoft.BLL.Demographic();
            Demographic d = bll.GetByPID(this.pid);
            if (d.Gender == "男")
            {
                this.rbnMale.Checked = true;
                this.rbnFemale.Checked = false;
            }
            else
            {
                this.rbnFemale.Checked = true;
                this.rbnMale.Checked = false;
            }
            this.txtEmail.Text = d.Email;
            this.txtAddress.Text = d.Address;
            this.txtPhone2.Text = d.Phone2;
            this.txtPhone1.Text = d.Phone1;
            this.txtAge.Text = d.Age.ToString();
            if (d.HomeTown != null)
            {
                this.cmbFrom.SelectedValue = d.HomeTown;
            }
            if (d.Race != null)
            {
                this.cmbRace.SelectedValue = d.Race;
            }
            if (d.BirthDay.HasValue)
            {
                this.dtpBirthday.Value = d.BirthDay.Value;
            }
        }

        private void BindGeneralInfo()
        {
            Maticsoft.BLL.GeneralInfo bll = new Maticsoft.BLL.GeneralInfo();
            GeneralInfo gi = bll.GetByPID(pid);
            this.txtWeight.Text = gi.Weight.ToString();
            this.txtHeight.Text = gi.Height.ToString();
            this.txtWaist.Text = gi.Waist.ToString();
            this.txtBMI.Text = gi.BMI.ToString();
            if (gi.SmokeYear.HasValue)
            {
                txtSmokeYear.Value = gi.SmokeYear.Value;
            }
            if (gi.SmokePerDay.HasValue)
            {
                txtSmokePerDay.Value = gi.SmokePerDay.Value;
            }
            if (gi.SmokeIndex.HasValue)
            {
                txtSmokeIndex.Text = gi.SmokeIndex.Value.ToString();
            }
            this.ckbIsQuitSmoke.Checked = gi.QuitSmoke;
            if (gi.QuitSmoke)
            {
                this.dateTimePicker1.Value = new DateTime(int.Parse(gi.QuitYear), 1, 1);
            }

            Maticsoft.BLL.TreatHistory treatHistoryBLL = new Maticsoft.BLL.TreatHistory();
            DataSet ds = treatHistoryBLL.GetList("PID=" + pid.ToString());
            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                this.dsTreatHistory.Rows.Add(new object[]{
                  
                     dr["ID"] ,
                     dr["PID"],
                     dr["GID"],
                     dr["Disease"],
                     dr["IllYear"],
                     dr["TreatInfo"],
                     dr["TreatResult"],               
                     dr["MedicateHistory"]
                   
                    });
            }
            //            foreach (TreatHistory th in treatHistories)
            //            {
            //                this.treatHistoryDataSource.Rows.Add(new object[]{
            //                    th.IllYear,th.TreatInfo,th.TreatResult,th.MedicateHistory,th.Disease,th.PID,th.ID
            ////                       illyear
            ////treatinfo
            ////treatresult
            ////medicatehistory
            ////disease
            ////pid
            ////id

            //                    });
            //            }
        }

        /// <summary>
        /// 绑定所有的下拉框
        /// </summary>
        private void BindingComboBox()
        {
            //Todo
            //民族 
            Constants constants = new Constants();
            this.cmbRace.DataSource = constants.races();
            cmbRace.DisplayMember = "Name";
            cmbRace.ValueMember = "Name";

            this.cmbFrom.DataSource = constants.regions();
            cmbFrom.DisplayMember = "Name";
            cmbFrom.ValueMember = "Name";
            cmbTreatResult.DataSource = constants.TreatResults();
            cmbTreatResult.DisplayMember = "Name";
            cmbTreatResult.ValueMember = "Name";


        }

        private void ultraTabbedMdiManager1_InitializeTab(object sender, Infragistics.Win.UltraWinTabbedMdi.MdiTabEventArgs e)
        {

        }

        private void ultraLabel27_Click(object sender, EventArgs e)
        {

        }

        private void ultraOptionSet1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtBillCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMeatNO_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

            TreatmentInfo ti = new TreatmentInfo(pid);
            ti.ShowDialog();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

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

      

        //保存人口学数据
        private void button2_Click(object sender, EventArgs e)
        {
            Demographic d = new Demographic();
            Maticsoft.BLL.Demographic bll = new Maticsoft.BLL.Demographic();

            d.PID = this.pid;
            if (this.rbnFemale.Checked)
            {
                d.Gender = "女";
            }
            if (this.rbnMale.Checked)
            {
                d.Gender = "男";
            }
            d.BirthDay = this.dtpBirthday.Value;
            d.Age = DateTime.Now.Year - d.BirthDay.Value.Year;
            d.Race = cmbRace.SelectedValue.ToString();
            d.HomeTown = cmbFrom.SelectedValue.ToString();
            d.Phone1 = this.txtPhone1.Text;
            d.Phone2 = this.txtPhone2.Text;
            d.Address = this.txtAddress.Text;
            d.Email = this.txtEmail.Text;
            bool i = bll.SaveOrUpdate(d);//.Add(d);
            if (i)
            {
                MessageBox.Show(this, "人口学数据保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show(this, "人口学数据保存失败,请重试!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //this.Close();
            }
            FreezeForm.ChangeControlEditable(this.tabPage1, false);
        }

        private void dtpBirthday_ValueChanged(object sender, EventArgs e)
        {
            this.txtAge.Text = (DateTime.Now.Year - this.dtpBirthday.Value.Year).ToString();
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            //bmi=体重/身高的平方
            ComputerBMI();
        }

        private void ComputerBMI()
        {
            double h = 0;
            double w = 0;
            bool resultHeight = double.TryParse(this.txtHeight.Text, out h);
            bool resultWeight = double.TryParse(this.txtWeight.Text, out w);
            if (resultHeight && resultWeight)
            {
                h *= 0.01;
                if (h > 0)
                {
                    double bmi = w / (h * h);
                    this.txtBMI.Text = bmi.ToString("f1");
                }

            }
        }

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            ComputerBMI();
        }

        /// <summary>
        /// 数据验证
        /// </summary>
        private bool ValidateGeneralInfoData()
        {

            return true;
        }

        //保存一般情况数据
        private void button3_Click(object sender, EventArgs e)
        {
            //验证数据是否正确
            if (!ValidateGeneralInfoData())
            {
                return;
            }
            if (SaveGeneralInfo())
            {
                MessageBox.Show(this, "一般情况数据保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //this.DisableGeneralInfoEdit(true);
                FreezeForm.ChangeControlEditable(this.tabPage2, false);
            }
            else
            {
                MessageBox.Show(this, "一般情况数据保存失败,请重试!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //this.Close();
            }
            ////generalInfo
            //generalInfo.Height = decimal.Parse(this.txtHeight.Text);
            //generalInfo.Weight = decimal.Parse(this.txtWeight.Text);
            //generalInfo.Waist = decimal.Parse(this.txtWeight.Text);
            //generalInfo.BMI = decimal.Parse(this.txtBMI.Text);

            //generalInfo.SmokeYear =  this.txtSmokeYear.Value ;
            //generalInfo.SmokePerDay =  this.txtSmokePerDay.Value;
            //generalInfo.SmokeIndex = decimal.Parse(this.txtSmokeIndex.Text);
            //if (this.ckbIsQuitSmoke.Checked)
            //{
            //    generalInfo.QuitSmoke = true;
            //    generalInfo.QuitYear = dateTimePicker1.Value.ToString("yyyy");
            //}
            //else
            //{
            //    generalInfo.QuitSmoke = true;
            //}

            //Maticsoft.BLL.GeneralInfo gi = new Maticsoft.BLL.GeneralInfo();

            //bool i = gi.Add(generalInfo);
            //if (i)
            //{
            //    MessageBox.Show(this, "保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //}
        }

        private bool SaveGeneralInfo()
        {
            bool result = true;
            //generalInfo
            generalInfo.Height = decimal.Parse(this.txtHeight.Text);
            generalInfo.Weight = decimal.Parse(this.txtWeight.Text);
            generalInfo.Waist = decimal.Parse(this.txtWeight.Text);
            generalInfo.BMI = decimal.Parse(this.txtBMI.Text);
            generalInfo.PID = pid;

            generalInfo.SmokeYear = this.txtSmokeYear.Value;
            generalInfo.SmokePerDay = this.txtSmokePerDay.Value;
            generalInfo.SmokeIndex = decimal.Parse(this.txtSmokeIndex.Text);
            if (this.ckbIsQuitSmoke.Checked)
            {
                generalInfo.QuitSmoke = true;
                generalInfo.QuitYear = dateTimePicker1.Value.ToString("yyyy");
            }
            else
            {
                generalInfo.QuitSmoke = false;
                generalInfo.QuitYear = DateTime.MinValue.ToString("yyyy");
            }

            Maticsoft.BLL.GeneralInfo gi = new Maticsoft.BLL.GeneralInfo();

            result = gi.SaveOrUpdate(generalInfo);

            //TreatHistory info
            Maticsoft.BLL.TreatHistory bll = new Maticsoft.BLL.TreatHistory();
            List<TreatHistory> oldList = bll.GetModelList("PID=" + pid.ToString());
            foreach (TreatHistory th in oldList)
            {
                bll.Delete(th.ID);
            }
            List<TreatHistory> treatHistories = new List<TreatHistory>();
            foreach (UltraGridRow r in gridInfo.Rows)
            {
                TreatHistory th = new TreatHistory();
                th.PID = pid;
                th.Disease = r.Cells["Disease"].Value.ToString();
                th.IllYear = r.Cells["IllYear"].Value.ToString();
                th.TreatInfo = r.Cells["TreatInfo"].Value.ToString();
                th.TreatResult = r.Cells["TreatResult"].Value.ToString();
                th.MedicateHistory = r.Cells["MedicateHistory"].Value.ToString();
                treatHistories.Add(th);
            }
            foreach (TreatHistory th in treatHistories)
            {
                result = bll.Add(th);
            }
            return result;
        }

        private void txtSmokeYear_TabIndexChanged(object sender, EventArgs e)
        {
            ComputerSmokeIndex();
        }

        private void ComputerSmokeIndex()
        {
            decimal year = this.txtSmokeYear.Value;
            decimal smokePerDay = this.txtSmokePerDay.Value;
            decimal smokeIndex = year * 365 * smokePerDay;
            this.txtSmokeIndex.Text = smokeIndex.ToString("f1");
        }

        private void txtSmokePerDay_ValueChanged(object sender, EventArgs e)
        {
            ComputerSmokeIndex();
        }

        private void txtSmokePerDay_TabIndexChanged(object sender, EventArgs e)
        {
            ComputerSmokeIndex();
        }

        private void txtSmokeYear_ValueChanged(object sender, EventArgs e)
        {
            ComputerSmokeIndex();
        }

        private void txtSmokePerDay_ValueChanged_1(object sender, EventArgs e)
        {
            ComputerSmokeIndex();
        }

        private void ckbIsQuitSmoke_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbIsQuitSmoke.Checked)
            {
                this.dateTimePicker1.Enabled = true;
            }
            else
            {
                this.dateTimePicker1.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtDisease.Text == "")
            {
                MessageBox.Show(this, "请输入疾病诊断信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                this.txtDisease.Focus();
                return;
            }
            if (this.txtTreatInfo.Text == "")
            {
                MessageBox.Show(this, "请输入[治疗情况]信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                this.txtTreatInfo.Focus();
                return;
            }
            //if (txtICCardNo.Text == "")
            //{
            //    MessageBox.Show(this, "未找到买家信息，请读卡或选择", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    return;
            //}
            AddNewRow();
            this.txtDisease.Text = "";
            this.txtTreatInfo.Text = "";
        }
        /// <summary>
        /// 在GRID中新增一行交易详情
        /// </summary>
        private void AddNewRow()
        {

            gridInfo.DisplayLayout.Rows.Band.AddNew();
            gridInfo.Rows[gridInfo.Rows.Count - 1].Cells["PID"].Value = this.pid;


            gridInfo.Rows[gridInfo.Rows.Count - 1].Cells["Disease"].Value = this.txtDisease.Text;
            gridInfo.Rows[gridInfo.Rows.Count - 1].Cells["IllYear"].Value = this.dtpIllYear.Value.ToString("yyyy");
            gridInfo.Rows[gridInfo.Rows.Count - 1].Cells["TreatInfo"].Value = this.txtTreatInfo.Text;
            gridInfo.Rows[gridInfo.Rows.Count - 1].Cells["TreatResult"].Value = this.cmbTreatResult.SelectedValue;
            gridInfo.Rows[gridInfo.Rows.Count - 1].Cells["MedicateHistory"].Value = txtMidecateHistory.Text;

        }

        private void gridInfo_ClickCellButton(object sender, CellEventArgs e)
        {
            DialogResult dr = MessageBox.Show(this, "确定要删除此行?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.Yes)
            {
                gridInfo.ActiveRow.Delete(false);//删除所选行
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //this.DisableGeneralInfoEdit(false);
            FreezeForm.ChangeControlEditable(this.tabPage2, true);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        //COPD保存
        private void button5_Click(object sender, EventArgs e)
        {
            List<COPDMedication> copdList = new List<COPDMedication>();
            List<int> idList = new List<int>();

            #region Oral
            if (ckbKouFu1.Checked) //茶碱 checked
            {
                COPDMedication koufu = new COPDMedication();
                //int id = 0;
                if (ckbKouFu1.Tag != null)
                {
                    koufu.ID = int.Parse(ckbKouFu1.Tag.ToString());
                }

                koufu.PID = pid;
                koufu.COPDTypeID = (int)COPD.Oral.茶碱;
                koufu.COPDTypeName = COPD.Oral.茶碱.ToString();
                koufu.DrugName = txtKouFu1.Text;
                koufu.Dose = txtKouFuJi1.Text;
                koufu.Usage = txtKouFuFa1.Text;
                copdList.Add(koufu);
            }
            else
            {
                if (ckbKouFu1.Tag != null)
                {
                    int id = int.Parse(ckbKouFu1.Tag.ToString());
                    idList.Add(id);
                }
            }
            if (ckbKouFu2.Checked)
            {
                COPDMedication koufu = new COPDMedication();
                //int id = 0;
                if (ckbKouFu2.Tag != null)
                {
                    koufu.ID = int.Parse(ckbKouFu2.Tag.ToString());
                }

                koufu.PID = pid;
                koufu.COPDTypeID = (int)COPD.Oral.白三烯受体拮抗剂;
                koufu.COPDTypeName = COPD.Oral.白三烯受体拮抗剂.ToString();
                koufu.DrugName = txtKouFu2.Text;
                koufu.Dose = txtKouFuJi2.Text;
                koufu.Usage = txtKouFuFa2.Text;
                copdList.Add(koufu);
            }
            else
            {
                if (ckbKouFu2.Tag != null)
                {
                    int id = int.Parse(ckbKouFu2.Tag.ToString());
                    idList.Add(id);
                }
            }

            if (ckbKouFu3.Checked) //茶碱 checked
            {
                COPDMedication koufu = new COPDMedication();
                //int id = 0;
                if (ckbKouFu3.Tag != null)
                {
                    koufu.ID = int.Parse(ckbKouFu3.Tag.ToString());
                }

                koufu.PID = pid;
                koufu.COPDTypeID = (int)COPD.Oral.选择性磷酸二酯酶4抑制剂;
                koufu.COPDTypeName = COPD.Oral.选择性磷酸二酯酶4抑制剂.ToString();
                koufu.DrugName = txtKouFu3.Text;
                koufu.Dose = txtKouFuJi3.Text;
                koufu.Usage = txtKouFuFa3.Text;
                copdList.Add(koufu);
            }
            if (ckbKouFu4.Checked) //茶碱 checked
            {
                COPDMedication koufu = new COPDMedication();
                //int id = 0;
                if (ckbKouFu4.Tag != null)
                {
                    koufu.ID = int.Parse(ckbKouFu4.Tag.ToString());
                }

                koufu.PID = pid;
                koufu.COPDTypeID = (int)COPD.Oral.激素;
                koufu.COPDTypeName = COPD.Oral.激素.ToString();
                koufu.DrugName = txtKouFu4.Text;
                koufu.Dose = txtKouFuJi4.Text;
                koufu.Usage = txtKouFuFa4.Text;
                copdList.Add(koufu);
            }
            if (ckbKouFu5.Checked) //茶碱 checked
            {
                COPDMedication koufu = new COPDMedication();
                //int id = 0;
                if (ckbKouFu5.Tag != null)
                {
                    koufu.ID = int.Parse(ckbKouFu5.Tag.ToString());
                }

                koufu.PID = pid;
                koufu.COPDTypeID = (int)COPD.Oral.化痰药;
                koufu.COPDTypeName = COPD.Oral.化痰药.ToString();
                koufu.DrugName = txtKouFu5.Text;
                koufu.Dose = txtKouFuJi5.Text;
                koufu.Usage = txtKouFuFa5.Text;
                copdList.Add(koufu);
            }
            if (ckbKouFu6.Checked) //茶碱 checked
            {
                COPDMedication koufu = new COPDMedication();
                //int id = 0;
                if (ckbKouFu6.Tag != null)
                {
                    koufu.ID = int.Parse(ckbKouFu6.Tag.ToString());
                }

                koufu.PID = pid;
                koufu.COPDTypeID = (int)COPD.Oral.镇咳药;
                koufu.COPDTypeName = COPD.Oral.镇咳药.ToString();
                koufu.DrugName = txtKouFu6.Text;
                koufu.Dose = txtKouFuJi6.Text;
                koufu.Usage = txtKouFuFa6.Text;
                copdList.Add(koufu);
            }
            if (ckbKouFu7.Checked) //茶碱 checked
            {
                COPDMedication koufu = new COPDMedication();
                //int id = 0;
                if (ckbKouFu7.Tag != null)
                {
                    koufu.ID = int.Parse(ckbKouFu7.Tag.ToString());
                }

                koufu.PID = pid;
                koufu.COPDTypeID = (int)COPD.Oral.其它;
                koufu.COPDTypeName = COPD.Oral.其它.ToString();
                koufu.DrugName = txtKouFu7.Text;
                koufu.Dose = txtKouFuJi7.Text;
                koufu.Usage = txtKouFuFa7.Text;
                copdList.Add(koufu);
            }
            #endregion
            #region Suck
            if (ckbXiRu1.Checked)
            {
                COPDMedication XiRu = new COPDMedication();
                //int id = 0;
                if (ckbXiRu1.Tag != null)
                {
                    XiRu.ID = int.Parse(ckbXiRu1.Tag.ToString());
                }

                XiRu.PID = pid;
                XiRu.COPDTypeID = (int)COPD.Suck.短效β受体激动剂;
                XiRu.COPDTypeName = COPD.Suck.短效β受体激动剂.ToString();
                XiRu.DrugName = txtXiRu1.Text;
                XiRu.Dose = txtXiRuJi1.Text;
                XiRu.Usage = txtXiRuFa1.Text;
                copdList.Add(XiRu);
            }
            if (ckbXiRu2.Checked)
            {
                COPDMedication XiRu = new COPDMedication();
                //int id = 0;
                if (ckbXiRu2.Tag != null)
                {
                    XiRu.ID = int.Parse(ckbXiRu2.Tag.ToString());
                }

                XiRu.PID = pid;
                XiRu.COPDTypeID = (int)COPD.Suck.长效β受体激动剂;
                XiRu.COPDTypeName = COPD.Suck.长效β受体激动剂.ToString();
                XiRu.DrugName = txtXiRu2.Text;
                XiRu.Dose = txtXiRuJi2.Text;
                XiRu.Usage = txtXiRuFa2.Text;
                copdList.Add(XiRu);
            }
            if (ckbXiRu3.Checked)
            {
                COPDMedication XiRu = new COPDMedication();
                //int id = 0;
                if (ckbXiRu3.Tag != null)
                {
                    XiRu.ID = int.Parse(ckbXiRu3.Tag.ToString());
                }

                XiRu.PID = pid;
                XiRu.COPDTypeID = (int)COPD.Suck.吸入激素;
                XiRu.COPDTypeName = COPD.Suck.吸入激素.ToString();
                XiRu.DrugName = txtXiRu3.Text;
                XiRu.Dose = txtXiRuJi3.Text;
                XiRu.Usage = txtXiRuFa3.Text;
                copdList.Add(XiRu);
            }
            if (ckbXiRu4.Checked)
            {
                COPDMedication XiRu = new COPDMedication();
                //int id = 0;
                if (ckbXiRu4.Tag != null)
                {
                    XiRu.ID = int.Parse(ckbXiRu4.Tag.ToString());
                }

                XiRu.PID = pid;
                XiRu.COPDTypeID = (int)COPD.Suck.长效β受体激动剂或激素;
                XiRu.COPDTypeName = COPD.Suck.长效β受体激动剂或激素.ToString();
                XiRu.DrugName = txtXiRu4.Text;
                XiRu.Dose = txtXiRuJi4.Text;
                XiRu.Usage = txtXiRuFa4.Text;
                copdList.Add(XiRu);
            }
            if (ckbXiRu5.Checked)
            {
                COPDMedication XiRu = new COPDMedication();
                //int id = 0;
                if (ckbXiRu5.Tag != null)
                {
                    XiRu.ID = int.Parse(ckbXiRu5.Tag.ToString());
                }

                XiRu.PID = pid;
                XiRu.COPDTypeID = (int)COPD.Suck.长效抗胆碱能药物;
                XiRu.COPDTypeName = COPD.Suck.长效抗胆碱能药物.ToString();
                XiRu.DrugName = txtXiRu5.Text;
                XiRu.Dose = txtXiRuJi5.Text;
                XiRu.Usage = txtXiRuFa5.Text;
                copdList.Add(XiRu);
            }
            if (ckbXiRu6.Checked) //茶碱 checked
            {
                COPDMedication XiRu = new COPDMedication();
                //int id = 0;
                if (ckbXiRu6.Tag != null)
                {
                    XiRu.ID = int.Parse(ckbXiRu6.Tag.ToString());
                }

                XiRu.PID = pid;
                XiRu.COPDTypeID = (int)COPD.Suck.其它;
                XiRu.COPDTypeName = COPD.Suck.其它.ToString();
                XiRu.DrugName = txtXiRu6.Text;
                XiRu.Dose = txtXiRuJi6.Text;
                XiRu.Usage = txtXiRuFa6.Text;
                copdList.Add(XiRu);
            }
            #endregion
            foreach (COPDMedication copd in copdList)
            {
                Maticsoft.BLL.COPDMedication bll = new Maticsoft.BLL.COPDMedication();
                if (copd.ID != null && copd.ID > 0)
                {
                    bll.Update(copd);
                }
                else
                {
                    bll.Add(copd);
                }

            }
            foreach (int id in idList)
            {
                Maticsoft.BLL.COPDMedication bll = new Maticsoft.BLL.COPDMedication();
                bll.Delete(id);

            }
            //this.BindCOPDInfo();
            //DisableCOPDEdit(true);
            FreezeForm.ChangeControlEditable(this.tpCOPD, false);
        }

        private void DisableCOPDEdit(bool isDisabled)
        {

            this.button5.Enabled = !isDisabled;
            this.ckbKouFu1.Enabled = !isDisabled;
            this.txtKouFu1.Enabled = !isDisabled;
            this.txtKouFuJi1.Enabled = !isDisabled;
            this.txtKouFuFa1.Enabled = !isDisabled;

            this.ckbXiRu1.Enabled = !isDisabled;
            this.txtXiRu1.Enabled = !isDisabled;
            this.txtXiRuJi1.Enabled = !isDisabled;
            this.txtXiRuFa1.Enabled = !isDisabled;

            this.ckbKouFu2.Enabled = !isDisabled;
            this.ckbKouFu3.Enabled = !isDisabled;
            this.ckbKouFu4.Enabled = !isDisabled;
            this.ckbKouFu5.Enabled = !isDisabled;
            this.ckbKouFu6.Enabled = !isDisabled;
            this.ckbKouFu7.Enabled = !isDisabled;

            this.txtKouFu2.Enabled = !isDisabled;
            this.txtKouFu3.Enabled = !isDisabled;
            this.txtKouFu4.Enabled = !isDisabled;
            this.txtKouFu5.Enabled = !isDisabled;
            this.txtKouFu6.Enabled = !isDisabled;
            this.txtKouFu7.Enabled = !isDisabled;

            this.txtKouFuJi2.Enabled = !isDisabled;
            this.txtKouFuJi3.Enabled = !isDisabled;
            this.txtKouFuJi4.Enabled = !isDisabled;
            this.txtKouFuJi5.Enabled = !isDisabled;
            this.txtKouFuJi6.Enabled = !isDisabled;
            this.txtKouFuJi7.Enabled = !isDisabled;

            this.txtKouFuFa2.Enabled = !isDisabled;
            this.txtKouFuFa3.Enabled = !isDisabled;
            this.txtKouFuFa4.Enabled = !isDisabled;
            this.txtKouFuFa5.Enabled = !isDisabled;
            this.txtKouFuFa6.Enabled = !isDisabled;
            this.txtKouFuFa7.Enabled = !isDisabled;
            this.ckbXiRu2.Enabled = !isDisabled;
            this.ckbXiRu3.Enabled = !isDisabled;
            this.ckbXiRu4.Enabled = !isDisabled;
            this.ckbXiRu5.Enabled = !isDisabled;
            this.ckbXiRu6.Enabled = !isDisabled;


            this.txtXiRu2.Enabled = !isDisabled;
            this.txtXiRu3.Enabled = !isDisabled;
            this.txtXiRu4.Enabled = !isDisabled;
            this.txtXiRu5.Enabled = !isDisabled;
            this.txtXiRu6.Enabled = !isDisabled;


            this.txtXiRuJi2.Enabled = !isDisabled;
            this.txtXiRuJi3.Enabled = !isDisabled;
            this.txtXiRuJi4.Enabled = !isDisabled;
            this.txtXiRuJi5.Enabled = !isDisabled;
            this.txtXiRuJi6.Enabled = !isDisabled;


            this.txtXiRuFa2.Enabled = !isDisabled;
            this.txtXiRuFa3.Enabled = !isDisabled;
            this.txtXiRuFa4.Enabled = !isDisabled;
            this.txtXiRuFa5.Enabled = !isDisabled;
            this.txtXiRuFa6.Enabled = !isDisabled;
        }

        //茶碱 checked
        private void ckbKouFu1_CheckedChanged(object sender, EventArgs e)
        {
            this.txtKouFu1.Enabled = ckbKouFu1.Checked;
            this.txtKouFuJi1.Enabled = ckbKouFu1.Checked;
            this.txtKouFuFa1.Enabled = ckbKouFu1.Checked;

            this.txtKouFu1.Text = "";
            this.txtKouFuJi1.Text = "";
            this.txtKouFuFa1.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //DisableCOPDEdit(false);
            FreezeForm.ChangeControlEditable(this.tpCOPD, true);
        }

        #region 治疗前基线数据-保存

        //保存动脉数据 治疗前基线数据
        private void button7_Click(object sender, EventArgs e)
        {
            BloodGas bg = new BloodGas();
            Maticsoft.BLL.BloodGas bll = new Maticsoft.BLL.BloodGas();
            bg.PID = pid;
            bg.Period = TreatPeroid.BeforTreatment;
            bg.pH = txtPH.Text;
            bg.PaO2 = txtPaO2.Text;
            bg.SaO2 = txtSaO2.Text;
            bg.PaCO2 = txtPcO2.Text;
            bg.RealHCO3 = txtRealHCO3.Text;
            bg.StandHCO3 = txtStandHCO3.Text;
            bg.AB = txtAB.Text;
            bg.BE = txtBE.Text;
            bg.AG = txtAG.Text;
            bg.CheckDate = dtpBloodGas.Value;
            if (lblBloodID.Tag != null)
            {
                bg.ID = int.Parse(lblBloodID.Tag.ToString());
                bll.Update(bg);
            }
            else
            {
                bll.Add(bg);
            }

            //this.BindBloodGasInfo();
            //DiseableBeforeBloodPageEdit(true);
            FreezeForm.ChangeControlEditable(this.tpBloodGas, false);

        }

        //保存肺功能
        private void button25_Click(object sender, EventArgs e)
        {
            Maticsoft.BLL.Lung bll = new Maticsoft.BLL.Lung();

            Lung lung = new Lung();
            lung.PID = pid;
            lung.Period = TreatPeroid.BeforTreatment;
            lung.fev1 = txtFev1.Text;
            lung.fev1pre = txtFev1pre.Text;
            lung.fvc = txtFvc.Text;
            lung.fvcpre = txtFvcpre.Text;
            lung.fev1fvc = txtFev1fvc.Text;
            lung.tlc = txtTlc.Text;
            lung.tlvpre = txtTlvpre.Text;
            lung.rv = txtRV.Text;
            lung.rvpre = txtRvpre.Text;
            lung.rvtlc = txtRvtlc.Text;
            lung.CheckTime = dtpLung.Value;
            lung.File = uploaddicom1.File; // txtLungPicFile.Text;
             
            if (this.lblLungID.Tag != null)
            {
                lung.ID = int.Parse(lblLungID.Tag.ToString());
                bll.Update(lung);
            }
            else
            {
                bll.Add(lung);
            }

            //BindLungInfo();
            //DiseableBeforeLungPageEdit(true);
            FreezeForm.ChangeControlEditable(this.tpLung, false);


        }

        private void DiseableBeforeLungPageEdit(bool isDiseableEdit)
        {
            txtFev1.ReadOnly = isDiseableEdit;
            txtFev1pre.ReadOnly = isDiseableEdit;
            txtFvc.ReadOnly = isDiseableEdit;
            txtFvcpre.ReadOnly = isDiseableEdit;
            txtFev1fvc.ReadOnly = isDiseableEdit;
            txtTlc.ReadOnly = isDiseableEdit;
            txtTlvpre.ReadOnly = isDiseableEdit;
            txtRV.ReadOnly = isDiseableEdit;
            txtRvpre.ReadOnly = isDiseableEdit;
            txtRvtlc.ReadOnly = isDiseableEdit;
            dtpLung.Enabled = !isDiseableEdit;

        }

        private void BindLungInfo()
        {
            Maticsoft.BLL.Lung bll = new Maticsoft.BLL.Lung();

            List<Lung> lungList = bll.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.BeforTreatment.ToString());
            if (lungList != null && lungList.Count > 0)
            {
                Lung lung = lungList[0];
                txtFev1.Text = lung.fev1;
                txtFev1pre.Text = lung.fev1pre;
                txtFvc.Text = lung.fvc;
                txtFvcpre.Text = lung.fvcpre;
                txtFev1fvc.Text = lung.fev1fvc;
                txtTlc.Text = lung.tlc;
                txtTlvpre.Text = lung.tlvpre;
                txtRV.Text = lung.rv;
                txtRvpre.Text = lung.rvpre;
                txtRvtlc.Text = lung.rvtlc;
                dtpLung.Value = lung.CheckTime.Value;
                lblLungID.Tag = lung.ID; 
                this.uploaddicom1.SetFile(lung.File); 
            }
        }
        private void BindLungInfo_w()
        {
            Maticsoft.BLL.Lung bll = new Maticsoft.BLL.Lung();

            List<Lung> lungList = bll.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.AfterTreatment.OneWeek.ToString());
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
        #endregion
        private void DiseableBeforeBloodPageEdit(bool isDiseableEdit)
        {
            txtPcO2.ReadOnly = isDiseableEdit;
            txtPH.ReadOnly = isDiseableEdit;
            txtPaO2.ReadOnly = isDiseableEdit;
            txtSaO2.ReadOnly = isDiseableEdit;
            txtPaO2.ReadOnly = isDiseableEdit;
            txtRealHCO3.ReadOnly = isDiseableEdit;
            txtStandHCO3.ReadOnly = isDiseableEdit;
            txtAB.ReadOnly = isDiseableEdit;
            txtBE.ReadOnly = isDiseableEdit;
            txtAG.ReadOnly = isDiseableEdit;
            dtpBloodGas.Enabled = !isDiseableEdit;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //DiseableBeforeBloodPageEdit(false);
            FreezeForm.ChangeControlEditable(this.tpBloodGas, true);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            //DiseableBeforeLungPageEdit(false);
            FreezeForm.ChangeControlEditable(this.tpLung, true);
        }

        private void btnDicomSave_Click(object sender, EventArgs e)
        {
            Maticsoft.BLL.dicom dicomBLL = new Maticsoft.BLL.dicom();
            Maticsoft.BLL.Chartis chartisBLL = new Maticsoft.BLL.Chartis();
            dicom d = new dicom();
            d.PID = pid;
            d.Period = TreatPeroid.BeforTreatment;
            d.Heterogeneity = txtHeter.Text;
            d.LobeSplit = txtLobeSplit.Text;
            d.TreatLobeVolumn = txtLobeVolumn.Text;
            d.UnTreatLobeVolumn = txtLobeVolumn2.Text;
            d.CheckDate = dtpDicom.Value;
            //this.uploaddicom2.SetPid(pid.ToString()); 
            d.File = this.uploaddicom2.File; 

            if (lblDicomID.Tag != null)
            {
                d.ID = int.Parse(lblDicomID.Tag.ToString());
                dicomBLL.Update(d);
            }
            else
            {
                dicomBLL.Add(d);
            }

            Chartis chartis = new Chartis();
            chartis.PID = pid;
            chartis.CheckDate = dtpChartis.Value;
            chartis.IsAerate = false;
            if (rbYes.Checked)
            {
                chartis.IsAerate = true;
            }
            //this.uploaddicom3.SetPid(pid.ToString());
            chartis.File = this.uploaddicom3.File; 
            if (lblChartisID.Tag != null)
            {
                chartis.ID = int.Parse(lblChartisID.Tag.ToString());
                chartisBLL.Update(chartis);
            }
            else
            {
                chartisBLL.Add(chartis);
            }
            //BindDicomInfo();
            //BindChartisInfo();
            //DisableDicomeEdit(true);
            //DisableChartisEdit(true);
            FreezeForm.ChangeControlEditable(this.tpDicom, false);
        }

        private void BindDicomInfo()
        {
            Maticsoft.BLL.dicom dicomBLL = new Maticsoft.BLL.dicom();

            List<dicom> lungList = dicomBLL.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.BeforTreatment.ToString());
            if (lungList != null && lungList.Count > 0)
            {
                dicom dicom = lungList[0];
                lblDicomID.Tag = dicom.ID;
                txtHeter.Text = dicom.Heterogeneity;
                txtLobeSplit.Text = dicom.LobeSplit;
                txtLobeVolumn.Text = dicom.TreatLobeVolumn;
                txtLobeVolumn2.Text = dicom.UnTreatLobeVolumn;
                dtpDicom.Value = dicom.CheckDate.Value;


            }
        }
        private void BindDicomInfo_w()
        {
            Maticsoft.BLL.dicom dicomBLL = new Maticsoft.BLL.dicom();

            List<dicom> lungList = dicomBLL.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.AfterTreatment.OneWeek.ToString());
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
        private void BindChartisInfo()
        {
            Maticsoft.BLL.Chartis bll = new Maticsoft.BLL.Chartis();

            List<Chartis> lungList = bll.GetModelList("PID=" + pid.ToString());
            if (lungList != null && lungList.Count > 0)
            {
                Chartis chartis = lungList[0];
                lblChartisID.Tag = chartis.ID;
                dtpChartis.Value = chartis.CheckDate.Value;
                if (chartis.IsAerate)
                {
                    rbYes.Checked = true;
                }
                else
                {
                    rbNo.Checked = true;
                }
            }
        }

        private void DisableDicomeEdit(bool isDisableEdit)
        {

            txtHeter.ReadOnly = isDisableEdit;
            txtLobeSplit.ReadOnly = isDisableEdit;
            txtLobeVolumn.ReadOnly = isDisableEdit;
            txtLobeVolumn2.ReadOnly = isDisableEdit;
            dtpDicom.Enabled = !isDisableEdit;

        }

        private void DisableChartisEdit(bool isDisableEdit)
        {
            dtpChartis.Enabled = !isDisableEdit;
            rbYes.Enabled = !isDisableEdit;
            rbNo.Enabled = !isDisableEdit;
            //TODO chartis file upload function
        }

        private void btnDicomEdit_Click(object sender, EventArgs e)
        {
            FreezeForm.ChangeControlEditable(this.tpDicom, true);
        }

        //保存运动生活测试
        private void button29_Click(object sender, EventArgs e)
        {
            Maticsoft.BLL.SportLife bll = new Maticsoft.BLL.SportLife();
            SportLife sl = new SportLife();
            sl.PID = pid;
            sl.Period = TreatPeroid.BeforTreatment;
            int sixTest = 0;
            int.TryParse(txtSixTest.Text, out sixTest);
            sl.SixMinuteTest = sixTest;
            sl.SixMinuteTestDate = dtpSixTest.Value;
            sixTest = 0;
            int.TryParse(txtGeorgeTest.Text, out sixTest);
            sl.StGeorgeTest = sixTest;
            sl.StGeorgeTestDate = dtpGeorgeTest.Value;
            sl.StGeorgeTestFile = this.uploaddicom4.File;
            sixTest = 0;
            int.TryParse(txtCatTest.Text, out sixTest);
            sl.CATTest = sixTest;
            sl.CATTestDate = dtpCatTest.Value;
            sixTest = 0;
            int.TryParse(txtMmrcTest.Text, out sixTest);
            sl.MMRCTest = sixTest;
            sl.MMRCTestDate = dtpMmrc.Value;
            if (lblSportLifeID.Tag != null)
            {
                sl.ID = int.Parse(lblSportLifeID.Tag.ToString());
                bll.Update(sl);
            }
            else
            {
                bll.Add(sl);
            }
            //BindSportLifeInfo();
            //DisableSportLifeEdit(true);
            FreezeForm.ChangeControlEditable(tpSportLife, false);
        }

        private void BindSportLifeInfo()
        {
            Maticsoft.BLL.SportLife bll = new Maticsoft.BLL.SportLife();

            List<SportLife> lungList = bll.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.BeforTreatment.ToString());
            if (lungList != null && lungList.Count > 0)
            {
                SportLife sl = lungList[0];
                lblSportLifeID.Tag = sl.ID;
                txtSixTest.Text = sl.SixMinuteTest.ToString();
                dtpSixTest.Value = sl.SixMinuteTestDate.Value;
                txtGeorgeTest.Text = sl.StGeorgeTest.ToString();
                dtpGeorgeTest.Value = sl.StGeorgeTestDate.Value;
                //txtGeorgeFile.Text = sl.StGeorgeTestFile;
                this.uploaddicom4.SetFile(sl.StGeorgeTestFile);
                txtCatTest.Text = sl.CATTest.ToString();
                dtpCatTest.Value = sl.CATTestDate.Value;
                txtMmrcTest.Text = sl.MMRCTest.ToString();
                dtpMmrc.Value = sl.MMRCTestDate.Value;


            }

        }
        private void BindSportLifeInfo_w()
        {
            Maticsoft.BLL.SportLife bll = new Maticsoft.BLL.SportLife();

            List<SportLife> lungList = bll.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.AfterTreatment.OneWeek.ToString());
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

        private void DisableSportLifeEdit(bool isFreezeEdit)
        {
            this.txtSixTest.ReadOnly = isFreezeEdit;
            this.dtpSixTest.Enabled = !isFreezeEdit;
            txtGeorgeTest.ReadOnly = isFreezeEdit;
            dtpGeorgeTest.Enabled = !isFreezeEdit;
            txtCatTest.ReadOnly = isFreezeEdit;
            dtpCatTest.Enabled = !isFreezeEdit;
            txtMmrcTest.ReadOnly = isFreezeEdit;
            dtpMmrc.Enabled = !isFreezeEdit;
        }

        //enable sport life page edit
        private void button30_Click(object sender, EventArgs e)
        {
            //DisableSportLifeEdit(false);
            FreezeForm.ChangeControlEditable(tpSportLife, true);
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            BadReactionDetail openDialog = new BadReactionDetail(null);
            //Butcher_EnterregisterDetail openDialog = new Butcher_EnterregisterDetail(0, 0, "");
            openDialog.ShowDialog();
            if (openDialog.DialogResult == DialogResult.OK)
            {
                if (openDialog.returnValue != null)
                {
                    BadReaction bd = openDialog.returnValue;
                    ugBadReaction.DisplayLayout.Rows.Band.AddNew();
                    ugBadReaction.Rows[ugBadReaction.Rows.Count - 1].Cells["PID"].Value = this.pid;


                    ugBadReaction.Rows[ugBadReaction.Rows.Count - 1].Cells["Peroid"].Value = bd.Peroid;
                    ugBadReaction.Rows[ugBadReaction.Rows.Count - 1].Cells["ReactionName"].Value = bd.ReactionName;
                    ugBadReaction.Rows[ugBadReaction.Rows.Count - 1].Cells["OccurDate"].Value = bd.OccurDate.Value.ToString("yyyy-MM-dd");
                    ugBadReaction.Rows[ugBadReaction.Rows.Count - 1].Cells["Severity"].Value = bd.Severity;
                    ugBadReaction.Rows[ugBadReaction.Rows.Count - 1].Cells["TreatMethod"].Value = bd.TreatMethod;
                    ugBadReaction.Rows[ugBadReaction.Rows.Count - 1].Cells["TreatResult"].Value = bd.TreatResult;
                }
                //else
                //{
                //    LoadBadActionList();
                //}

            }
        }

        private void LoadBadActionList()
        {
            Maticsoft.BLL.BadReaction bll = new Maticsoft.BLL.BadReaction();
            DataSet ds = bll.GetList("PID=" + pid.ToString() + " AND peroid=" + TreatPeroid.InTreatment);
            this.ugBadReaction.DataSource = ds.Tables[0];
            this.ugBadReaction.DataBind();
        }

        private void ugBadReaction_ClickCellButton(object sender, CellEventArgs e)
        {
            //Debug.WriteLine("Button in " + e.Cell.Text  + " cell was clicked.");
            if (e.Cell.Text == "删除")
            {
                DialogResult dr = MessageBox.Show(this, "确定要删除此行?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                {
                    // MessageBox.Show(this, ugBadReaction.ActiveRow.Cells["ID"].Text);
                    if (ugBadReaction.ActiveRow.Cells["ID"].Text != "")
                    {
                        Maticsoft.BLL.BadReaction bll = new Maticsoft.BLL.BadReaction();
                        bll.Delete(int.Parse(ugBadReaction.ActiveRow.Cells["ID"].Text));
                    }
                    ugBadReaction.ActiveRow.Delete(false);//删除所选行
                }
            }
            if (e.Cell.Text == "编辑")
            {
                BadReaction bd = new BadReaction();
                // bd.ID = int.Parse(ugBadReaction.ActiveRow.Cells["ID"].Text);
                bd.ReactionName = ugBadReaction.ActiveRow.Cells["ReactionName"].Text;
                bd.OccurDate = DateTime.Parse(ugBadReaction.ActiveRow.Cells["OccurDate"].Text);
                bd.Severity = ugBadReaction.ActiveRow.Cells["Severity"].Text;
                bd.TreatMethod = ugBadReaction.ActiveRow.Cells["TreatMethod"].Text;
                bd.TreatResult = ugBadReaction.ActiveRow.Cells["TreatResult"].Text;

                BadReactionDetail openDialog = new BadReactionDetail(bd);
                //Butcher_EnterregisterDetail openDialog = new Butcher_EnterregisterDetail(0, 0, "");
                openDialog.ShowDialog();
                if (openDialog.DialogResult == DialogResult.OK)
                {
                    if (openDialog.returnValue != null)
                    {
                        BadReaction returnAction = openDialog.returnValue;

                        ugBadReaction.ActiveRow.Cells["ReactionName"].Value = returnAction.ReactionName;
                        ugBadReaction.ActiveRow.Cells["OccurDate"].Value = returnAction.OccurDate.Value.ToString("yyyy-MM-dd");
                        ugBadReaction.ActiveRow.Cells["Severity"].Value = returnAction.Severity;
                        ugBadReaction.ActiveRow.Cells["TreatMethod"].Value = returnAction.TreatMethod;
                        ugBadReaction.ActiveRow.Cells["TreatResult"].Value = returnAction.TreatResult;

                    }
                    //else
                    //{
                    //    LoadBadActionList();
                    //}

                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //验证数据是否正确
            if (!ValidateTreatInfoData())
            {
                return;
            }
            if (SaveTreatInfo())
            {
                MessageBox.Show(this, "治疗情况保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //this.DisableTreatInfoEdit(true);
                FreezeForm.ChangeControlEditable(this.tabPage4, false);
            }
            else
            {
                MessageBox.Show(this, "治疗情况保存失败,请重试!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            }



        }

        private void DisableTreatInfoEdit(bool isDisableEdit)
        {
            this.dtpTreatDate.Enabled = !isDisableEdit;
            this.txtTreatLobe.ReadOnly = isDisableEdit;
            this.txtValveCount.ReadOnly = isDisableEdit;
            txtValvePosition.ReadOnly = isDisableEdit;
            dtpSergeryDate.Enabled = !isDisableEdit;
            //ugBadReaction.Enabled = !isDisableEdit;
            if (isDisableEdit)
            {
                this.ugBadReaction.DisplayLayout.Bands[0].Columns["DELETE"].CellActivation = Activation.Disabled;
                this.ugBadReaction.DisplayLayout.Bands[0].Columns["EDIT"].CellActivation = Activation.Disabled;
            }
            else
            {
                this.ugBadReaction.DisplayLayout.Bands[0].Columns["DELETE"].CellActivation = Activation.NoEdit;
                this.ugBadReaction.DisplayLayout.Bands[0].Columns["EDIT"].CellActivation = Activation.NoEdit;
            }

            this.button9.Enabled = !isDisableEdit;
        }

        private bool ValidateTreatInfoData()
        {
            return true;
        }

        private bool SaveTreatInfo()
        {
            bool result = false;
            Maticsoft.BLL.TreatInfo treatInfoBll = new Maticsoft.BLL.TreatInfo();
            TreatInfo tr = new TreatInfo();
            tr.PID = pid;

            tr.TreatDate = this.dtpTreatDate.Value;
            tr.TreatLobe = txtTreatLobe.Text;
            tr.ValveCount = int.Parse(txtValveCount.Text);
            tr.ValvePosition = txtValvePosition.Text;
            tr.SurgeryDate = dtpSergeryDate.Value;
            result = treatInfoBll.SaveOrUpdate(tr);

            List<BadReaction> treatHistories = new List<BadReaction>();
            foreach (UltraGridRow r in this.ugBadReaction.Rows)
            {
                BadReaction th = new BadReaction();
                th.PID = pid;
                if (r.Cells["ID"].Text != "")
                {
                    th.ID = int.Parse(r.Cells["ID"].Text);
                }
                th.Peroid = TreatPeroid.InTreatment;
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

            return result;

        }

        private void tc_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == this.tabPage4)
            {
                BindTreatInfo();
            }
            if (e.TabPage == this.tpWeek)
            {
                BindWeekInfo();
                //DisableWeekPage(false);
                ChangeControlEditable(this.tpWeek, false);
            }
            if (e.TabPage == this.tpMonth1)
            {
                BindMonth1Info();
                ChangeControlEditable(this.tpMonth1, false);
            }
            if (e.TabPage == this.tpMonth3)
            {
                BindMonth3Info();
                ChangeControlEditable(this.tpMonth3, false);
            }
            if (e.TabPage == this.tpMonth6)
            {
                BindMonth6Info();
                ChangeControlEditable(this.tpMonth6, false);
            }
            if (e.TabPage == this.tpYear1)
            {
                BindYear1Info();
                ChangeControlEditable(this.tpYear1, false);
            }

            if (e.TabPage == this.tpAfterManyYears)
            {
                BindAfterManyYearsInfo();
               // ChangeControlEditable(this.tpYear1, false);
            }
        }

        private void BindAfterManyYearsInfo()
        {
            DataTable dt = new DataTable();
            

            DataColumn dc = new DataColumn("Period",typeof(int));
            DataColumn dc2 = new DataColumn("PeriodName",typeof(string));
            dt.Columns.Add(dc);
            dt.Columns.Add(dc2);
            DataRow row = dt.NewRow();
            row["Period"] = TreatPeroid.AfterTreatment.TwoYear;
            row["PeriodName"] ="治疗后2年";
            dt.Rows.Add(row);
            row = dt.NewRow();
            row["Period"] = TreatPeroid.AfterTreatment.ThreeYear;
            row["PeriodName"] ="治疗后3年";
            dt.Rows.Add(row);
            row = dt.NewRow();
            row["Period"] = TreatPeroid.AfterTreatment.FourYear;
            row["PeriodName"] ="治疗后4年";
            dt.Rows.Add(row);
            row = dt.NewRow();
            row["Period"] = TreatPeroid.AfterTreatment.FiveYear;
            row["PeriodName"] = "治疗后5年";
            dt.Rows.Add(row);
            this.ugAfterManyYears.DataSource = dt;
            this.ugAfterManyYears.DataBind();
            //  public const int TwoYear=6;
            //public const int ThreeYear=7;
            //public const int FourYear =8;
            //public const int FiveYear = 9;
        }

        private void BindMonth3Info()
        {
            this.BindBloodGasInfo_m3();
            this.BindLungInfo_m3();
            this.BindDicomInfo_m3();
            this.BindSportLifeInfo_m3();
            this.bindBadAction_m3();
        }

       

        private void BindBloodGasInfo_m3()
        {
            Maticsoft.BLL.BloodGas bll = new Maticsoft.BLL.BloodGas();

            List<BloodGas> bloodList = bll.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.AfterTreatment.ThreeMonth.ToString());
            if (bloodList != null && bloodList.Count > 0)
            {
                BloodGas bg = bloodList[0];
                //this.lblBloodID.Tag = bg.ID;
                this.txtPH_m3.Text = bg.pH;
                this.txtPaO2_m3.Text = bg.PaO2;
                this.txtSaO2_m3.Text = bg.SaO2;
                this.txtPcO2_m3.Text = bg.PaCO2;
                this.txtRealHCO3_m3.Text = bg.RealHCO3;
                this.txtStandHCO3_m3.Text = bg.StandHCO3;
                this.txtAB_m3.Text = bg.AB;
                this.txtBE_m3.Text = bg.BE;
                this.txtAG_m3.Text = bg.AG;
                this.dtpBloodGas_m3.Value = bg.CheckDate.Value;
            }
        }

        private void BindMonth6Info()
        {
            this.BindBloodGasInfo_m6();
            this.BindLungInfo_m6();
            this.BindDicomInfo_m6();
            this.BindSportLifeInfo_m6();
            this.bindBadAction_m6();
        }

        private void BindBloodGasInfo_m6()
        {
            Maticsoft.BLL.BloodGas bll = new Maticsoft.BLL.BloodGas();

            List<BloodGas> bloodList = bll.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.AfterTreatment.SixMonth.ToString());
            if (bloodList != null && bloodList.Count > 0)
            {
                BloodGas bg = bloodList[0];
                //this.lblBloodID.Tag = bg.ID;
                this.txtPH_m6.Text = bg.pH;
                this.txtPao2_m6.Text = bg.PaO2;
                this.txtSaO2_m6.Text = bg.SaO2;
                this.txtPaco2_m6.Text = bg.PaCO2;
                this.txtRealHCO3_m6.Text = bg.RealHCO3;
                this.txtStandHCO3_m6.Text = bg.StandHCO3;
                this.txtAB_m6.Text = bg.AB;
                this.txtBE_m6.Text = bg.BE;
                this.txtAG_m6.Text = bg.AG;
                this.dtpBloodGas_m6.Value = bg.CheckDate.Value;
            }
        }

        private void BindYear1Info()
        {
            this.BindBloodGasInfo_y1();
            this.BindLungInfo_y1();
            this.BindDicomInfo_y1();
            this.BindSportLifeInfo_y1();
            this.bindBadAction_y1();
        }

        private void BindBloodGasInfo_y1()
        {
            Maticsoft.BLL.BloodGas bll = new Maticsoft.BLL.BloodGas();

            List<BloodGas> bloodList = bll.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.AfterTreatment.OneYear.ToString());
                
            if (bloodList != null && bloodList.Count > 0)
            {
                BloodGas bg = bloodList[0];
                //this.lblBloodID.Tag = bg.ID;
                this.txtPH_y1.Text = bg.pH;
                this.txtPao2_y1.Text = bg.PaO2;
                this.txtSaO2_y1.Text = bg.SaO2;
                this.txtPaco2_y1.Text = bg.PaCO2;
                this.txtRealHCO3_y1.Text = bg.RealHCO3;
                this.txtStandHCO3_y1.Text = bg.StandHCO3;
                this.txtAB_y1.Text = bg.AB;
                this.txtBE_y1.Text = bg.BE;
                this.txtAG_y1.Text = bg.AG;
                this.dtpBloodGas_y1.Value = bg.CheckDate.Value;
            }
        }

        private void BindMonth1Info()
        {
            this.BindBloodGasInfo_m1();
            this.BindLungInfo_m1();
            this.BindDicomInfo_m1();
            this.BindSportLifeInfo_m1();
            this.bindBadAction_m1();
        }

        private void bindBadAction_m1()
        {
            Maticsoft.BLL.BadReaction bll = new Maticsoft.BLL.BadReaction();
            DataSet ds = bll.GetList("PID=" + pid.ToString() + " AND Peroid=" + TreatPeroid.AfterTreatment.OneMonth.ToString());
            this.ugBadAction_m1.DataSource = ds;
            this.ugBadAction_m1.DataBind();
        }
        private void bindBadAction_m3()
        {
            Maticsoft.BLL.BadReaction bll = new Maticsoft.BLL.BadReaction();
            DataSet ds = bll.GetList("PID=" + pid.ToString() + " AND Peroid=" + TreatPeroid.AfterTreatment.ThreeMonth.ToString());
            this.ugBadAction_m3.DataSource = ds;
            this.ugBadAction_m3.DataBind();
        }
        private void bindBadAction_m6()
        {
            Maticsoft.BLL.BadReaction bll = new Maticsoft.BLL.BadReaction();
            DataSet ds = bll.GetList("PID=" + pid.ToString() + " AND Peroid=" + TreatPeroid.AfterTreatment.SixMonth.ToString());
            this.ugBadAction_m6.DataSource = ds;
            this.ugBadAction_m6.DataBind();
        }
        private void bindBadAction_y1()
        {
            Maticsoft.BLL.BadReaction bll = new Maticsoft.BLL.BadReaction();
            DataSet ds = bll.GetList("PID=" + pid.ToString() + " AND Peroid=" + TreatPeroid.AfterTreatment.OneYear.ToString());
            this.ugBadAction_y1.DataSource = ds;
            this.ugBadAction_y1.DataBind();
        }

        private void BindSportLifeInfo_m1()
        {
            Maticsoft.BLL.SportLife bll = new Maticsoft.BLL.SportLife();

            List<SportLife> lungList = bll.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.AfterTreatment.OneMonth.ToString());
            if (lungList != null && lungList.Count > 0)
            {
                SportLife sl = lungList[0];
                //lblSportLifeID.Tag = sl.ID;
                txtSixTest_m1.Text = sl.SixMinuteTest.ToString();
                dtpSixTest_m1.Value = sl.SixMinuteTestDate.Value;
                txtGeorgeTest_m1.Text = sl.StGeorgeTest.ToString();
                dtpGeorgeTest_m1.Value = sl.StGeorgeTestDate.Value;
               // txtGeorgeFile_m1.Text = sl.StGeorgeTestFile;
                txtCatTest_m1.Text = sl.CATTest.ToString();
                dtpCatTest_m1.Value = sl.CATTestDate.Value;
                txtMmrcTest_m1.Text = sl.MMRCTest.ToString();
                dtpMmrc_m1.Value = sl.MMRCTestDate.Value;
                this.uploaddicomGeorge_m1.SetFile(sl.StGeorgeTestFile);
            }
        }
        private void BindSportLifeInfo_m3()
        {
            Maticsoft.BLL.SportLife bll = new Maticsoft.BLL.SportLife();

            List<SportLife> lungList = bll.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.AfterTreatment.ThreeMonth.ToString());
            if (lungList != null && lungList.Count > 0)
            {
                SportLife sl = lungList[0];
                //lblSportLifeID.Tag = sl.ID;
                txtSixTest_m3.Text = sl.SixMinuteTest.ToString();
                dtpSixTest_m3.Value = sl.SixMinuteTestDate.Value;
                txtGeorgeTest_m3.Text = sl.StGeorgeTest.ToString();
                dtpGeorgeTest_m3.Value = sl.StGeorgeTestDate.Value;
              //  txtGeorgeFile_m3.Text = sl.StGeorgeTestFile;
                txtCatTest_m3.Text = sl.CATTest.ToString();
                dtpCatTest_m3.Value = sl.CATTestDate.Value;
                txtMmrcTest_m3.Text = sl.MMRCTest.ToString();
                dtpMmrc_m3.Value = sl.MMRCTestDate.Value;
                this.uploaddicomGeorge_m3.SetFile(sl.StGeorgeTestFile);
            }
        }
        private void BindSportLifeInfo_m6()
        {
            Maticsoft.BLL.SportLife bll = new Maticsoft.BLL.SportLife();

            List<SportLife> lungList = bll.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.AfterTreatment.SixMonth.ToString());
            if (lungList != null && lungList.Count > 0)
            {
                SportLife sl = lungList[0];
                //lblSportLifeID.Tag = sl.ID;
                txtSixTest_m6.Text = sl.SixMinuteTest.ToString();
                dtpSixTest_m6.Value = sl.SixMinuteTestDate.Value;
                txtGeorgeTest_m6.Text = sl.StGeorgeTest.ToString();
                dtpGeorgeTest_m6.Value = sl.StGeorgeTestDate.Value;
                //txtGeorgeFile_m6.Text = sl.StGeorgeTestFile;
                txtCatTest_m6.Text = sl.CATTest.ToString();
                dtpCatTest_m6.Value = sl.CATTestDate.Value;
                txtMmrcTest_m6.Text = sl.MMRCTest.ToString();
                dtpMmrc_m6.Value = sl.MMRCTestDate.Value;
                this.uploaddicomGeorge_m6.SetFile(sl.StGeorgeTestFile);
            }
        }
        private void BindSportLifeInfo_y1()
        {
            Maticsoft.BLL.SportLife bll = new Maticsoft.BLL.SportLife();

            List<SportLife> lungList = bll.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.AfterTreatment.OneYear.ToString());
            if (lungList != null && lungList.Count > 0)
            {
                SportLife sl = lungList[0];
                //lblSportLifeID.Tag = sl.ID;
                txtSixTest_y1.Text = sl.SixMinuteTest.ToString();
                dtpSixTest_y1.Value = sl.SixMinuteTestDate.Value;
                txtGeorgeTest_y1.Text = sl.StGeorgeTest.ToString();
                dtpGeorgeTest_y1.Value = sl.StGeorgeTestDate.Value;
                //txtGeorgeFile_y1.Text = sl.StGeorgeTestFile;
                txtCatTest_y1.Text = sl.CATTest.ToString();
                dtpCatTest_y1.Value = sl.CATTestDate.Value;
                txtMmrcTest_y1.Text = sl.MMRCTest.ToString();
                dtpMmrc_y1.Value = sl.MMRCTestDate.Value;
                this.uploaddicomGeorge_y1.SetFile(sl.StGeorgeTestFile);
            }
        }

        private void BindDicomInfo_m1()
        {
            Maticsoft.BLL.dicom dicomBLL = new Maticsoft.BLL.dicom();

            List<dicom> lungList = dicomBLL.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.AfterTreatment.OneMonth.ToString());
            if (lungList != null && lungList.Count > 0)
            {
                dicom dicom = lungList[0];
                txtLobeVolumn_m1.Text = dicom.TreatLobeVolumn;
                txtLobeVolumn2_m1.Text = dicom.UnTreatLobeVolumn;
                dtpDicom_m1.Value = dicom.CheckDate.Value;
              //  txtDicomFile_m1.Text = dicom.File;
                this.uploaddicom_m1.SetFile(dicom.File);
            }
        }
        private void BindDicomInfo_m3()
        {
            Maticsoft.BLL.dicom dicomBLL = new Maticsoft.BLL.dicom();

            List<dicom> lungList = dicomBLL.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.AfterTreatment.ThreeMonth.ToString());
            if (lungList != null && lungList.Count > 0)
            {
                dicom dicom = lungList[0];
                txtLobeVolumn_m3.Text = dicom.TreatLobeVolumn;
                txtLobeVolumn2_m3.Text = dicom.UnTreatLobeVolumn;
                dtpDicom_m3.Value = dicom.CheckDate.Value;
              //  txtDicomFile_m3.Text = dicom.File;
                this.uploaddicom_m3.SetFile(dicom.File);
            }
        }
        private void BindDicomInfo_m6()
        {
            Maticsoft.BLL.dicom dicomBLL = new Maticsoft.BLL.dicom();

            List<dicom> lungList = dicomBLL.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.AfterTreatment.SixMonth.ToString());
            if (lungList != null && lungList.Count > 0)
            {
                dicom dicom = lungList[0];
                txtLobeVolumn_m6.Text = dicom.TreatLobeVolumn;
                txtLobeVolumn2_m6.Text = dicom.UnTreatLobeVolumn;
                dtpDicom_m6.Value = dicom.CheckDate.Value;
                //txtDicomFile_m3.Text = dicom.File;
                this.uploaddicom_m6.SetFile(dicom.File);
            }
        }
        private void BindDicomInfo_y1()
        {
            Maticsoft.BLL.dicom dicomBLL = new Maticsoft.BLL.dicom();

            List<dicom> lungList = dicomBLL.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.AfterTreatment.OneYear.ToString());
            if (lungList != null && lungList.Count > 0)
            {
                dicom dicom = lungList[0];
                txtLobeVolumn_y1.Text = dicom.TreatLobeVolumn;
                txtLobeVolumn2_y1.Text = dicom.UnTreatLobeVolumn;
                dtpDicom_y1.Value = dicom.CheckDate.Value;
                //txtDicomFile_m3.Text = dicom.File;
                this.uploaddicom_y1.SetFile(dicom.File);
            }
        }

        private void BindLungInfo_m1()
        {
            Maticsoft.BLL.Lung bll = new Maticsoft.BLL.Lung();

            List<Lung> lungList = bll.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.AfterTreatment.OneMonth.ToString());
            if (lungList != null && lungList.Count > 0)
            {
                Lung lung = lungList[0];
                txtFev1_m1.Text = lung.fev1;
                txtFev1pre_m1.Text = lung.fev1pre;
                txtFvc_m1.Text = lung.fvc;
                txtFvcpre_m1.Text = lung.fvcpre;
                txtFev1fvc_m1.Text = lung.fev1fvc;
                txtTlc_m1.Text = lung.tlc;
                txtTlvpre_m1.Text = lung.tlvpre;
                txtRV_m1.Text = lung.rv;
                txtRvpre_m1.Text = lung.rvpre;
                txtRvtlc_m1.Text = lung.rvtlc;
                dtpLung_m1.Value = lung.CheckTime.Value;
                this.uploaddicomLung_m1.SetFile(lung.File);
            }
        }
        private void BindLungInfo_m3()
        {
            Maticsoft.BLL.Lung bll = new Maticsoft.BLL.Lung();

            List<Lung> lungList = bll.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.AfterTreatment.ThreeMonth.ToString());
            if (lungList != null && lungList.Count > 0)
            {
                Lung lung = lungList[0];
                txtFev1_m3.Text = lung.fev1;
                txtFev1pre_m3.Text = lung.fev1pre;
                txtFvc_m3.Text = lung.fvc;
                txtFvcpre_m3.Text = lung.fvcpre;
                txtFev1fvc_m3.Text = lung.fev1fvc;
                txtTlc_m3.Text = lung.tlc;
                txtTlvpre_m3.Text = lung.tlvpre;
                txtRV_m3.Text = lung.rv;
                txtRvpre_m3.Text = lung.rvpre;
                txtRvtlc_m3.Text = lung.rvtlc;
                dtpLung_m3.Value = lung.CheckTime.Value;
                this.uploaddicomLung_m3.SetFile(lung.File);
            }
        }
        private void BindLungInfo_m6()
        {
            Maticsoft.BLL.Lung bll = new Maticsoft.BLL.Lung();

            List<Lung> lungList = bll.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.AfterTreatment.SixMonth.ToString());
            if (lungList != null && lungList.Count > 0)
            {
                Lung lung = lungList[0];
                txtFev1_m6.Text = lung.fev1;
                txtFev1pre_m6.Text = lung.fev1pre;
                txtFvc_m6.Text = lung.fvc;
                txtFvcpre_m6.Text = lung.fvcpre;
                txtFev1fvc_m6.Text = lung.fev1fvc;
                txtTlc_m6.Text = lung.tlc;
                txtTlvpre_m6.Text = lung.tlvpre;
                txtRV_m6.Text = lung.rv;
                txtRvpre_m6.Text = lung.rvpre;
                txtRvtlc_m6.Text = lung.rvtlc;
                dtpLung_m6.Value = lung.CheckTime.Value;
                this.uploaddicomLung_m6.SetFile(lung.File);
            }
        }
        private void BindLungInfo_y1()
        {
            Maticsoft.BLL.Lung bll = new Maticsoft.BLL.Lung();

            List<Lung> lungList = bll.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.AfterTreatment.OneYear.ToString());
            if (lungList != null && lungList.Count > 0)
            {
                Lung lung = lungList[0];
                txtFev1_y1.Text = lung.fev1;
                txtFev1pre_y1.Text = lung.fev1pre;
                txtFvc_y1.Text = lung.fvc;
                txtFvcpre_y1.Text = lung.fvcpre;
                txtFev1fvc_y1.Text = lung.fev1fvc;
                txtTlc_y1.Text = lung.tlc;
                txtTlvpre_y1.Text = lung.tlvpre;
                txtRV_y1.Text = lung.rv;
                txtRvpre_y1.Text = lung.rvpre;
                txtRvtlc_y1.Text = lung.rvtlc;
                dtpLung_y1.Value = lung.CheckTime.Value;
                this.uploaddicomLung_y1.SetFile(lung.File);
            }
        }
        private void BindBloodGasInfo_m1()
        {
            Maticsoft.BLL.BloodGas bll = new Maticsoft.BLL.BloodGas();

            List<BloodGas> bloodList = bll.GetModelList("PID=" + pid.ToString() + " AND Period=" + TreatPeroid.AfterTreatment.OneWeek.ToString());
            if (bloodList != null && bloodList.Count > 0)
            {
                BloodGas bg = bloodList[0];
                //this.lblBloodID.Tag = bg.ID;
                this.txtPH_m1.Text = bg.pH;
                this.txtPaO2_m1.Text = bg.PaO2;
                this.txtSaO2_m1.Text = bg.SaO2;
                this.txtPcO2_m1.Text = bg.PaCO2;
                this.txtRealHCO3_m1.Text = bg.RealHCO3;
                this.txtStandHCO3_m1.Text = bg.StandHCO3;
                this.txtAB_m1.Text = bg.AB;
                this.txtBE_m1.Text = bg.BE;
                this.txtAG_m1.Text = bg.AG;
                this.dtpBloodGas_m1.Value = bg.CheckDate.Value;
            }
        }
        internal static void ChangeControlEditable(Control f, bool isEditable)
        {
            foreach (Control c in f.Controls)
            {

                // MessageBox.Show(c.GetType().ToString());
                if (c.HasChildren)
                {
                    ChangeControlEditable(c, isEditable);
                }
                else if (c is TextBox)
                {
                    TextBox lll = (TextBox)c;
                    if (lll.Name != "txtLungPicFile")
                    {
                        lll.ReadOnly = !isEditable;
                    }
                    
                }
                else if (c is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)c;
                    dtp.Enabled = isEditable;
                }
                else if (c is Button)
                {
                    Button b = (Button)c;
                    if (b.Text != "编辑" && b.Text !="查看")
                    {
                        b.Enabled = isEditable;
                    }
                }
                else if (c is UltraGrid)
                {
                    UltraGrid ug = (UltraGrid)c;
                    ug.DisplayLayout.Bands[0].Columns["DELETE"].Hidden = !isEditable;
                    ug.DisplayLayout.Bands[0].Columns["EDIT"].Hidden = !isEditable;

                    //if (isEditable)
                    //{
                    //    ug.DisplayLayout.Bands[0].Columns["DELETE"].CellActivation = Activation.NoEdit;
                    //    ug.DisplayLayout.Bands[0].Columns["EDIT"].CellActivation = Activation.NoEdit;
                    //}
                    //else
                    //{
                    //    ug.DisplayLayout.Bands[0].Columns["DELETE"].CellActivation = Activation.Disabled;
                    //    ug.DisplayLayout.Bands[0].Columns["EDIT"].CellActivation = Activation.Disabled;
                    //}
                }
            }
        }
        private List<Control> GetAllControls(Control container)
        {
            List<Control> ControlList = new List<Control>();
            foreach (Control c in container.Controls)
            {
                GetAllControls(c);
                if (c is TextBox) ControlList.Add(c);
            }
            return ControlList;
        }
        private void DisableWeekPage(bool p)
        {
            List<Control> txtBoxList = GetAllControls(this.tpWeek);
            foreach (Control c in txtBoxList)
            {
                c.Enabled = p;
            }
        }

        private void BindWeekInfo()
        {
            this.BindBloodGasInfo_w();
            this.BindLungInfo_w();
            this.BindDicomInfo_w();
            this.BindSportLifeInfo_w();
            this.bindBadAction_w();
        }

        private void bindBadAction_w()
        {
            Maticsoft.BLL.BadReaction bll = new Maticsoft.BLL.BadReaction();
            DataSet ds = bll.GetList("PID=" + pid.ToString() + " AND Peroid=" + TreatPeroid.AfterTreatment.OneWeek.ToString());
            this.ugBadAction_w.DataSource = ds;
            this.ugBadAction_w.DataBind();

        }

        private void BindTreatInfo()
        {
            Maticsoft.BLL.TreatInfo treatInfoBll = new Maticsoft.BLL.TreatInfo();
            List<TreatInfo> trList = treatInfoBll.GetModelList("PID=" + pid.ToString());
            if (trList != null && trList.Count > 0)
            {

                TreatInfo tr = trList[0];

                this.dtpTreatDate.Value = tr.TreatDate.Value;
                txtTreatLobe.Text = tr.TreatLobe;
                txtValveCount.Text = tr.ValveCount.ToString();
                txtValvePosition.Text = tr.ValvePosition;
                dtpSergeryDate.Value = tr.SurgeryDate.Value;

            }

            Maticsoft.BLL.BadReaction bll = new Maticsoft.BLL.BadReaction();
            DataSet ds = bll.GetList("PID=" + pid.ToString() + " AND Peroid=" + TreatPeroid.InTreatment);
            this.ugBadReaction.DataSource = ds;
            this.ugBadReaction.DataBind();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            //DisableTreatInfoEdit(false);
            FreezeForm.ChangeControlEditable(this.tabPage4, true);
        }

        private void ugBadReaction_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            this.ugBadReaction.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;

            // Enable updating on the root band. This will override the displayLayout setting
            this.ugBadReaction.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.False;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            bool result = false;

            BloodGas bg = new BloodGas();
            bg.PID = pid;
            bg.Period = TreatPeroid.AfterTreatment.OneWeek;
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
            lung.Period = TreatPeroid.AfterTreatment.OneWeek;
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
            di.Period = TreatPeroid.AfterTreatment.OneWeek;
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
            sl.Period = TreatPeroid.AfterTreatment.OneWeek;
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
                th.Peroid = TreatPeroid.AfterTreatment.OneWeek;
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
                MessageBox.Show(this, "治疗后一周数据保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                ChangeControlEditable(this.tpWeek, false);
            }
            else
            {
                MessageBox.Show(this, "治疗后一周数据保存失败,请重试!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            ChangeControlEditable(this.tpWeek, true);
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

         

        private void button14_Click(object sender, EventArgs e)
        {
            ChangeControlEditable(this.tpMonth1, true);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            ChangeControlEditable(this.tpMonth3, true);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            ChangeControlEditable(this.tpMonth6, true);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            ChangeControlEditable(this.tpYear1, true);
        }

        //保存治疗后一月数据
        private void button13_Click(object sender, EventArgs e)
        {
            int period = TreatPeroid.AfterTreatment.OneMonth;
            bool result = false;

            BloodGas bg = new BloodGas();
            bg.PID = pid;
            bg.Period = period;
            bg.pH = txtPH_m1.Text;
            bg.PaO2 = txtPcO2_m1.Text;
            bg.SaO2 = txtSaO2_m1.Text;
            bg.PaCO2 = txtPcO2_m1.Text;
            bg.RealHCO3 = txtRealHCO3_m1.Text;
            bg.StandHCO3 = txtStandHCO3_m1.Text;
            bg.AB = txtAB_m1.Text;
            bg.BE = txtBE_m1.Text;
            bg.AG = txtAG_m1.Text;
            bg.CheckDate = dtpBloodGas_m1.Value;
            Maticsoft.BLL.BloodGas bgBll = new Maticsoft.BLL.BloodGas();
            result = bgBll.SaveOrUpdate(bg);

            Lung lung = new Lung();
            lung.PID = pid;
            lung.Period = period;
            lung.fev1 = txtFev1_m1.Text;
            lung.fev1pre = txtFev1pre_m1.Text;
            lung.fvc = txtFvc_m1.Text;
            lung.fvcpre = txtFvcpre_m1.Text;
            lung.fev1fvc = txtFev1fvc_m1.Text;
            lung.tlc = txtTlc_m1.Text;
            lung.tlvpre = txtTlvpre_m1.Text;
            lung.rv = txtRV_m1.Text;
            lung.rvpre = txtRvpre_m1.Text;
            lung.rvtlc = txtRvtlc_m1.Text;
            lung.CheckTime = dtpLung_m1.Value;
            lung.File = this.uploaddicomLung_m1.File;
            Maticsoft.BLL.Lung lungBLL = new Maticsoft.BLL.Lung();
            result = lungBLL.SaveOrUpdate(lung);

            dicom di = new dicom();
            di.PID = pid;
            di.Period = period;
            di.TreatLobeVolumn = txtLobeVolumn_m1.Text;
            di.UnTreatLobeVolumn = txtLobeVolumn2_m1.Text;

            di.File = this.uploaddicom_m1.File;
            di.CheckDate = dtpDicom_m1.Value;
            di.Heterogeneity = "";
            di.LobeSplit = "";

            Maticsoft.BLL.dicom diBLL = new Maticsoft.BLL.dicom();
            result = diBLL.SaveOrUpdate(di);

            SportLife sl = new SportLife();
            sl.PID = pid;
            sl.Period = period;
            int sixTest = 0;
            int.TryParse(txtSixTest_m1.Text, out sixTest);

            sl.SixMinuteTest = sixTest;
            sl.SixMinuteTestDate = dtpSixTest_m1.Value;
            sixTest = 0;
            int.TryParse(txtGeorgeTest_m1.Text, out sixTest);

            sl.StGeorgeTest = sixTest;// int.Parse(txtGeorgeTest_m1.Text);
            sl.StGeorgeTestDate = dtpGeorgeTest_m1.Value;
            sl.StGeorgeTestFile = this.uploaddicomGeorge_m1.File;
            sixTest = 0;
            int.TryParse(txtCatTest_m1.Text, out sixTest);

            sl.CATTest = sixTest;// int.Parse(txtCatTest_m1.Text);
            sl.CATTestDate = dtpCatTest_m1.Value;
            sixTest = 0;
            int.TryParse(txtMmrcTest_m1.Text, out sixTest);


            sl.MMRCTest = sixTest;// int.Parse(this.txtMmrc_m1.Text);
            sl.MMRCTestDate = dtpMmrc_m1.Value;
            Maticsoft.BLL.SportLife slBLL = new Maticsoft.BLL.SportLife();
            result = slBLL.SaveOrUpdate(sl);

            List<BadReaction> treatHistories = new List<BadReaction>();
            foreach (UltraGridRow r in this.ugBadAction_m1.Rows)
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
                MessageBox.Show(this, "治疗后一月数据保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                ChangeControlEditable(this.tpMonth1, false);
            }
            else
            {
                MessageBox.Show(this, "治疗后一月数据保存失败,请重试!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            BadReactionDetail openDialog = new BadReactionDetail(null);

            openDialog.ShowDialog();
            if (openDialog.DialogResult == DialogResult.OK)
            {
                if (openDialog.returnValue != null)
                {
                    BadReaction bd = openDialog.returnValue;

                    ugBadAction_y1.DisplayLayout.Rows.Band.AddNew();
                    ugBadAction_y1.Rows[ugBadAction_y1.Rows.Count - 1].Cells["PID"].Value = this.pid;


                    ugBadAction_y1.Rows[ugBadAction_y1.Rows.Count - 1].Cells["Peroid"].Value = bd.Peroid;
                    ugBadAction_y1.Rows[ugBadAction_y1.Rows.Count - 1].Cells["ReactionName"].Value = bd.ReactionName;
                    ugBadAction_y1.Rows[ugBadAction_y1.Rows.Count - 1].Cells["OccurDate"].Value = bd.OccurDate.Value.ToString("yyyy-MM-dd");
                    ugBadAction_y1.Rows[ugBadAction_y1.Rows.Count - 1].Cells["Severity"].Value = bd.Severity;
                    ugBadAction_y1.Rows[ugBadAction_y1.Rows.Count - 1].Cells["TreatMethod"].Value = bd.TreatMethod;
                    ugBadAction_y1.Rows[ugBadAction_y1.Rows.Count - 1].Cells["TreatResult"].Value = bd.TreatResult;
                }
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            BadReactionDetail openDialog = new BadReactionDetail(null);

            openDialog.ShowDialog();
            if (openDialog.DialogResult == DialogResult.OK)
            {
                if (openDialog.returnValue != null)
                {
                    BadReaction bd = openDialog.returnValue;

                    ugBadAction_m6.DisplayLayout.Rows.Band.AddNew();
                    ugBadAction_m6.Rows[ugBadAction_m6.Rows.Count - 1].Cells["PID"].Value = this.pid;


                    ugBadAction_m6.Rows[ugBadAction_m6.Rows.Count - 1].Cells["Peroid"].Value = bd.Peroid;
                    ugBadAction_m6.Rows[ugBadAction_m6.Rows.Count - 1].Cells["ReactionName"].Value = bd.ReactionName;
                    ugBadAction_m6.Rows[ugBadAction_m6.Rows.Count - 1].Cells["OccurDate"].Value = bd.OccurDate.Value.ToString("yyyy-MM-dd");
                    ugBadAction_m6.Rows[ugBadAction_m6.Rows.Count - 1].Cells["Severity"].Value = bd.Severity;
                    ugBadAction_m6.Rows[ugBadAction_m6.Rows.Count - 1].Cells["TreatMethod"].Value = bd.TreatMethod;
                    ugBadAction_m6.Rows[ugBadAction_m6.Rows.Count - 1].Cells["TreatResult"].Value = bd.TreatResult;
                }
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            BadReactionDetail openDialog = new BadReactionDetail(null);

            openDialog.ShowDialog();
            if (openDialog.DialogResult == DialogResult.OK)
            {
                if (openDialog.returnValue != null)
                {
                    BadReaction bd = openDialog.returnValue;

                    ugBadAction_m3.DisplayLayout.Rows.Band.AddNew();
                    ugBadAction_m3.Rows[ugBadAction_m3.Rows.Count - 1].Cells["PID"].Value = this.pid;


                    ugBadAction_m3.Rows[ugBadAction_m3.Rows.Count - 1].Cells["Peroid"].Value = bd.Peroid;
                    ugBadAction_m3.Rows[ugBadAction_m3.Rows.Count - 1].Cells["ReactionName"].Value = bd.ReactionName;
                    ugBadAction_m3.Rows[ugBadAction_m3.Rows.Count - 1].Cells["OccurDate"].Value = bd.OccurDate.Value.ToString("yyyy-MM-dd");
                    ugBadAction_m3.Rows[ugBadAction_m3.Rows.Count - 1].Cells["Severity"].Value = bd.Severity;
                    ugBadAction_m3.Rows[ugBadAction_m3.Rows.Count - 1].Cells["TreatMethod"].Value = bd.TreatMethod;
                    ugBadAction_m3.Rows[ugBadAction_m3.Rows.Count - 1].Cells["TreatResult"].Value = bd.TreatResult;
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            BadReactionDetail openDialog = new BadReactionDetail(null);

            openDialog.ShowDialog();
            if (openDialog.DialogResult == DialogResult.OK)
            {
                if (openDialog.returnValue != null)
                {
                    BadReaction bd = openDialog.returnValue;

                    ugBadAction_m1.DisplayLayout.Rows.Band.AddNew();
                    ugBadAction_m1.Rows[ugBadAction_m1.Rows.Count - 1].Cells["PID"].Value = this.pid;


                    ugBadAction_m1.Rows[ugBadAction_m1.Rows.Count - 1].Cells["Peroid"].Value = bd.Peroid;
                    ugBadAction_m1.Rows[ugBadAction_m1.Rows.Count - 1].Cells["ReactionName"].Value = bd.ReactionName;
                    ugBadAction_m1.Rows[ugBadAction_m1.Rows.Count - 1].Cells["OccurDate"].Value = bd.OccurDate.Value.ToString("yyyy-MM-dd");
                    ugBadAction_m1.Rows[ugBadAction_m1.Rows.Count - 1].Cells["Severity"].Value = bd.Severity;
                    ugBadAction_m1.Rows[ugBadAction_m1.Rows.Count - 1].Cells["TreatMethod"].Value = bd.TreatMethod;
                    ugBadAction_m1.Rows[ugBadAction_m1.Rows.Count - 1].Cells["TreatResult"].Value = bd.TreatResult;
                }
            }
        }

        //保存治疗后三月数据
        private void button16_Click(object sender, EventArgs e)
        {
            int period = TreatPeroid.AfterTreatment.ThreeMonth;
            bool result = false;

            BloodGas bg = new BloodGas();
            bg.PID = pid;
            bg.Period = period;
            bg.pH = txtPH_m3.Text;
            bg.PaO2 = txtPcO2_m3.Text;
            bg.SaO2 = txtSaO2_m3.Text;
            bg.PaCO2 = txtPcO2_m3.Text;
            bg.RealHCO3 = txtRealHCO3_m3.Text;
            bg.StandHCO3 = txtStandHCO3_m3.Text;
            bg.AB = txtAB_m3.Text;
            bg.BE = txtBE_m3.Text;
            bg.AG = txtAG_m3.Text;
            bg.CheckDate = dtpBloodGas_m3.Value;
            Maticsoft.BLL.BloodGas bgBll = new Maticsoft.BLL.BloodGas();
            result = bgBll.SaveOrUpdate(bg);

            Lung lung = new Lung();
            lung.PID = pid;
            lung.Period = period;
            lung.fev1 = txtFev1_m3.Text;
            lung.fev1pre = txtFev1pre_m3.Text;
            lung.fvc = txtFvc_m3.Text;
            lung.fvcpre = txtFvcpre_m3.Text;
            lung.fev1fvc = txtFev1fvc_m3.Text;
            lung.tlc = txtTlc_m3.Text;
            lung.tlvpre = txtTlvpre_m3.Text;
            lung.rv = txtRV_m3.Text;
            lung.rvpre = txtRvpre_m3.Text;
            lung.rvtlc = txtRvtlc_m3.Text;
            lung.CheckTime = dtpLung_m3.Value;
            lung.File = this.uploaddicomLung_m3.File;
            Maticsoft.BLL.Lung lungBLL = new Maticsoft.BLL.Lung();
            result = lungBLL.SaveOrUpdate(lung);

            dicom di = new dicom();
            di.PID = pid;
            di.Period = period;
            di.TreatLobeVolumn = txtLobeVolumn_m3.Text;
            di.UnTreatLobeVolumn = txtLobeVolumn2_m3.Text;

            di.File = this.uploaddicom_m3.File;
            di.CheckDate = dtpDicom_m3.Value;
            di.Heterogeneity = "";
            di.LobeSplit = "";

            Maticsoft.BLL.dicom diBLL = new Maticsoft.BLL.dicom();
            result = diBLL.SaveOrUpdate(di);

            SportLife sl = new SportLife();
            sl.PID = pid;
            sl.Period = period;
            int sixTest = 0;
            int.TryParse(txtSixTest_m3.Text, out sixTest);

            sl.SixMinuteTest = sixTest;
            sl.SixMinuteTestDate = dtpSixTest_m3.Value;
            sixTest = 0;
            int.TryParse(txtGeorgeTest_m3.Text, out sixTest);

            sl.StGeorgeTest = sixTest;// int.Parse(txtGeorgeTest_m3.Text);
            sl.StGeorgeTestDate = dtpGeorgeTest_m3.Value;
            sl.StGeorgeTestFile = this.uploaddicomGeorge_m3.File;
            sixTest = 0;
            int.TryParse(txtCatTest_m3.Text, out sixTest);

            sl.CATTest = sixTest;// int.Parse(txtCatTest_m3.Text);
            sl.CATTestDate = dtpCatTest_m3.Value;
            sixTest = 0;
            int.TryParse(txtMmrcTest_m3.Text, out sixTest);


            sl.MMRCTest = sixTest;// int.Parse(this.txtMmrc_m3.Text);
            sl.MMRCTestDate = dtpMmrc_m3.Value;
            Maticsoft.BLL.SportLife slBLL = new Maticsoft.BLL.SportLife();
            result = slBLL.SaveOrUpdate(sl);

            List<BadReaction> treatHistories = new List<BadReaction>();
            foreach (UltraGridRow r in this.ugBadAction_m3.Rows)
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
                MessageBox.Show(this, "治疗后三月数据保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                ChangeControlEditable(this.tpMonth3, false);
            }
            else
            {
                MessageBox.Show(this, "治疗后三月数据保存失败,请重试!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        //保存治疗后六个月数据
        private void button19_Click(object sender, EventArgs e)
        {
            int period = TreatPeroid.AfterTreatment.SixMonth;
            bool result = false;

            BloodGas bg = new BloodGas();
            bg.PID = pid;
            bg.Period = period;
            bg.pH = txtPH_m6.Text;
            bg.PaO2 = txtPao2_m6.Text;
            bg.SaO2 = txtSaO2_m6.Text;
            bg.PaCO2 = txtPaco2_m6.Text;
            bg.RealHCO3 = txtRealHCO3_m6.Text;
            bg.StandHCO3 = txtStandHCO3_m6.Text;
            bg.AB = txtAB_m6.Text;
            bg.BE = txtBE_m6.Text;
            bg.AG = txtAG_m6.Text;
            bg.CheckDate = dtpBloodGas_m6.Value;
            Maticsoft.BLL.BloodGas bgBll = new Maticsoft.BLL.BloodGas();
            result = bgBll.SaveOrUpdate(bg);

            Lung lung = new Lung();
            lung.PID = pid;
            lung.Period = period;
            lung.fev1 = txtFev1_m6.Text;
            lung.fev1pre = txtFev1pre_m6.Text;
            lung.fvc = txtFvc_m6.Text;
            lung.fvcpre = txtFvcpre_m6.Text;
            lung.fev1fvc = txtFev1fvc_m6.Text;
            lung.tlc = txtTlc_m6.Text;
            lung.tlvpre = txtTlvpre_m6.Text;
            lung.rv = txtRV_m6.Text;
            lung.rvpre = txtRvpre_m6.Text;
            lung.rvtlc = txtRvtlc_m6.Text;
            lung.CheckTime = dtpLung_m6.Value;
            lung.File = this.uploaddicomLung_m6.File;
            Maticsoft.BLL.Lung lungBLL = new Maticsoft.BLL.Lung();
            result = lungBLL.SaveOrUpdate(lung);

            dicom di = new dicom();
            di.PID = pid;
            di.Period = period;
            di.TreatLobeVolumn = txtLobeVolumn_m6.Text;
            di.UnTreatLobeVolumn = txtLobeVolumn2_m6.Text;

            di.File = this.uploaddicom_m6.File;
            di.CheckDate = dtpDicom_m6.Value;
            di.Heterogeneity = "";
            di.LobeSplit = "";

            Maticsoft.BLL.dicom diBLL = new Maticsoft.BLL.dicom();
            result = diBLL.SaveOrUpdate(di);

            SportLife sl = new SportLife();
            sl.PID = pid;
            sl.Period = period;
            int sixTest = 0;
            int.TryParse(txtSixTest_m6.Text, out sixTest);

            sl.SixMinuteTest = sixTest;
            sl.SixMinuteTestDate = dtpSixTest_m6.Value;
            sixTest = 0;
            int.TryParse(txtGeorgeTest_m6.Text, out sixTest);

            sl.StGeorgeTest = sixTest;// int.Parse(txtGeorgeTest_m6.Text);
            sl.StGeorgeTestDate = dtpGeorgeTest_m6.Value;
            sl.StGeorgeTestFile = this.uploaddicomGeorge_m6.File;
            sixTest = 0;
            int.TryParse(txtCatTest_m6.Text, out sixTest);

            sl.CATTest = sixTest;// int.Parse(txtCatTest_m6.Text);
            sl.CATTestDate = dtpCatTest_m6.Value;
            sixTest = 0;
            int.TryParse(txtMmrcTest_m6.Text, out sixTest);


            sl.MMRCTest = sixTest;// int.Parse(this.txtMmrc_m6.Text);
            sl.MMRCTestDate = dtpMmrc_m6.Value;
            Maticsoft.BLL.SportLife slBLL = new Maticsoft.BLL.SportLife();
            result = slBLL.SaveOrUpdate(sl);

            List<BadReaction> treatHistories = new List<BadReaction>();
            foreach (UltraGridRow r in this.ugBadAction_m6.Rows)
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
                MessageBox.Show(this, "治疗后六月数据保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                ChangeControlEditable(this.tpMonth6, false);
            }
            else
            {
                MessageBox.Show(this, "治疗后六月数据保存失败,请重试!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            int period = TreatPeroid.AfterTreatment.OneYear;
            bool result = false;

            BloodGas bg = new BloodGas();
            bg.PID = pid;
            bg.Period = period;
            bg.pH = txtPH_y1.Text;
            bg.PaO2 = txtPao2_y1.Text;
            bg.SaO2 = txtSaO2_y1.Text;
            bg.PaCO2 = txtPaco2_y1.Text;
            bg.RealHCO3 = txtRealHCO3_y1.Text;
            bg.StandHCO3 = txtStandHCO3_y1.Text;
            bg.AB = txtAB_y1.Text;
            bg.BE = txtBE_y1.Text;
            bg.AG = txtAG_y1.Text;
            bg.CheckDate = dtpBloodGas_y1.Value;
            Maticsoft.BLL.BloodGas bgBll = new Maticsoft.BLL.BloodGas();
            result = bgBll.SaveOrUpdate(bg);

            Lung lung = new Lung();
            lung.PID = pid;
            lung.Period = period;
            lung.fev1 = txtFev1_y1.Text;
            lung.fev1pre = txtFev1pre_y1.Text;
            lung.fvc = txtFvc_y1.Text;
            lung.fvcpre = txtFvcpre_y1.Text;
            lung.fev1fvc = txtFev1fvc_y1.Text;
            lung.tlc = txtTlc_y1.Text;
            lung.tlvpre = txtTlvpre_y1.Text;
            lung.rv = txtRV_y1.Text;
            lung.rvpre = txtRvpre_y1.Text;
            lung.rvtlc = txtRvtlc_y1.Text;
            lung.CheckTime = dtpLung_y1.Value;
            lung.File = this.uploaddicomLung_y1.File;
            Maticsoft.BLL.Lung lungBLL = new Maticsoft.BLL.Lung();
            result = lungBLL.SaveOrUpdate(lung);

            dicom di = new dicom();
            di.PID = pid;
            di.Period = period;
            di.TreatLobeVolumn = txtLobeVolumn_y1.Text;
            di.UnTreatLobeVolumn = txtLobeVolumn2_y1.Text;

            di.File = this.uploaddicom_y1.File;
            di.CheckDate = dtpDicom_y1.Value;
            di.Heterogeneity = "";
            di.LobeSplit = "";

            Maticsoft.BLL.dicom diBLL = new Maticsoft.BLL.dicom();
            result = diBLL.SaveOrUpdate(di);

            SportLife sl = new SportLife();
            sl.PID = pid;
            sl.Period = period;
            int sixTest = 0;
            int.TryParse(txtSixTest_y1.Text, out sixTest);

            sl.SixMinuteTest = sixTest;
            sl.SixMinuteTestDate = dtpSixTest_y1.Value;
            sixTest = 0;
            int.TryParse(txtGeorgeTest_y1.Text, out sixTest);

            sl.StGeorgeTest = sixTest;// int.Parse(txtGeorgeTest_y1.Text);
            sl.StGeorgeTestDate = dtpGeorgeTest_y1.Value;
            sl.StGeorgeTestFile = this.uploaddicomGeorge_y1.File;
            sixTest = 0;
            int.TryParse(txtCatTest_y1.Text, out sixTest);

            sl.CATTest = sixTest;// int.Parse(txtCatTest_y1.Text);
            sl.CATTestDate = dtpCatTest_y1.Value;
            sixTest = 0;
            int.TryParse(txtMmrcTest_y1.Text, out sixTest);


            sl.MMRCTest = sixTest;// int.Parse(this.txtMmrc_y1.Text);
            sl.MMRCTestDate = dtpMmrc_y1.Value;
            Maticsoft.BLL.SportLife slBLL = new Maticsoft.BLL.SportLife();
            result = slBLL.SaveOrUpdate(sl);

            List<BadReaction> treatHistories = new List<BadReaction>();
            foreach (UltraGridRow r in this.ugBadAction_y1.Rows)
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
                MessageBox.Show(this, "治疗后一年数据保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                ChangeControlEditable(this.tpYear1, false);
            }
            else
            {
                MessageBox.Show(this, "治疗后一年数据保存失败,请重试!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void ugBadAction_m1_ClickCellButton(object sender, CellEventArgs e)
        {
            if (e.Cell.Text == "删除")
            {
                DialogResult dr = MessageBox.Show(this, "确定要删除此行?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                {
                    if (ugBadAction_m1.ActiveRow.Cells["ID"].Text != "")
                    {
                        Maticsoft.BLL.BadReaction bll = new Maticsoft.BLL.BadReaction();
                        bll.Delete(int.Parse(ugBadAction_m1.ActiveRow.Cells["ID"].Text));
                    }
                    ugBadAction_m1.ActiveRow.Delete(false);//删除所选行
                }
            }
            if (e.Cell.Text == "编辑")
            {
                BadReaction bd = new BadReaction();
                // bd.ID = int.Parse(ugBadReaction.ActiveRow.Cells["ID"].Text);
                bd.ReactionName = ugBadAction_m1.ActiveRow.Cells["ReactionName"].Text;
                bd.OccurDate = DateTime.Parse(ugBadAction_m1.ActiveRow.Cells["OccurDate"].Text);
                bd.Severity = ugBadAction_m1.ActiveRow.Cells["Severity"].Text;
                bd.TreatMethod = ugBadAction_m1.ActiveRow.Cells["TreatMethod"].Text;
                bd.TreatResult = ugBadAction_m1.ActiveRow.Cells["TreatResult"].Text;

                BadReactionDetail openDialog = new BadReactionDetail(bd);
                //Butcher_EnterregisterDetail openDialog = new Butcher_EnterregisterDetail(0, 0, "");
                openDialog.ShowDialog();
                if (openDialog.DialogResult == DialogResult.OK)
                {
                    if (openDialog.returnValue != null)
                    {
                        BadReaction returnAction = openDialog.returnValue;

                        ugBadAction_m1.ActiveRow.Cells["ReactionName"].Value = returnAction.ReactionName;
                        ugBadAction_m1.ActiveRow.Cells["OccurDate"].Value = returnAction.OccurDate.Value.ToString("yyyy-MM-dd");
                        ugBadAction_m1.ActiveRow.Cells["Severity"].Value = returnAction.Severity;
                        ugBadAction_m1.ActiveRow.Cells["TreatMethod"].Value = returnAction.TreatMethod;
                        ugBadAction_m1.ActiveRow.Cells["TreatResult"].Value = returnAction.TreatResult;

                    }
                }
            }
        }

        private void ugBadAction_m3_ClickCellButton(object sender, CellEventArgs e)
        {
            if (e.Cell.Text == "删除")
            {
                DialogResult dr = MessageBox.Show(this, "确定要删除此行?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                {
                    if (ugBadAction_m3.ActiveRow.Cells["ID"].Text != "")
                    {
                        Maticsoft.BLL.BadReaction bll = new Maticsoft.BLL.BadReaction();
                        bll.Delete(int.Parse(ugBadAction_m3.ActiveRow.Cells["ID"].Text));
                    }
                    ugBadAction_m3.ActiveRow.Delete(false);//删除所选行
                }
            }
            if (e.Cell.Text == "编辑")
            {
                BadReaction bd = new BadReaction();
                // bd.ID = int.Parse(ugBadReaction.ActiveRow.Cells["ID"].Text);
                bd.ReactionName = ugBadAction_m3.ActiveRow.Cells["ReactionName"].Text;
                bd.OccurDate = DateTime.Parse(ugBadAction_m3.ActiveRow.Cells["OccurDate"].Text);
                bd.Severity = ugBadAction_m3.ActiveRow.Cells["Severity"].Text;
                bd.TreatMethod = ugBadAction_m3.ActiveRow.Cells["TreatMethod"].Text;
                bd.TreatResult = ugBadAction_m3.ActiveRow.Cells["TreatResult"].Text;

                BadReactionDetail openDialog = new BadReactionDetail(bd);
                //Butcher_EnterregisterDetail openDialog = new Butcher_EnterregisterDetail(0, 0, "");
                openDialog.ShowDialog();
                if (openDialog.DialogResult == DialogResult.OK)
                {
                    if (openDialog.returnValue != null)
                    {
                        BadReaction returnAction = openDialog.returnValue;

                        ugBadAction_m3.ActiveRow.Cells["ReactionName"].Value = returnAction.ReactionName;
                        ugBadAction_m3.ActiveRow.Cells["OccurDate"].Value = returnAction.OccurDate.Value.ToString("yyyy-MM-dd");
                        ugBadAction_m3.ActiveRow.Cells["Severity"].Value = returnAction.Severity;
                        ugBadAction_m3.ActiveRow.Cells["TreatMethod"].Value = returnAction.TreatMethod;
                        ugBadAction_m3.ActiveRow.Cells["TreatResult"].Value = returnAction.TreatResult;

                    }
                }
            }
        }

        private void ugBadAction_m6_ClickCellButton(object sender, CellEventArgs e)
        {
            if (e.Cell.Text == "删除")
            {
                DialogResult dr = MessageBox.Show(this, "确定要删除此行?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                {
                    if (ugBadAction_m6.ActiveRow.Cells["ID"].Text != "")
                    {
                        Maticsoft.BLL.BadReaction bll = new Maticsoft.BLL.BadReaction();
                        bll.Delete(int.Parse(ugBadAction_m6.ActiveRow.Cells["ID"].Text));
                    }
                    ugBadAction_m6.ActiveRow.Delete(false);//删除所选行
                }
            }
            if (e.Cell.Text == "编辑")
            {
                BadReaction bd = new BadReaction();
                // bd.ID = int.Parse(ugBadReaction.ActiveRow.Cells["ID"].Text);
                bd.ReactionName = ugBadAction_m6.ActiveRow.Cells["ReactionName"].Text;
                bd.OccurDate = DateTime.Parse(ugBadAction_m6.ActiveRow.Cells["OccurDate"].Text);
                bd.Severity = ugBadAction_m6.ActiveRow.Cells["Severity"].Text;
                bd.TreatMethod = ugBadAction_m6.ActiveRow.Cells["TreatMethod"].Text;
                bd.TreatResult = ugBadAction_m6.ActiveRow.Cells["TreatResult"].Text;

                BadReactionDetail openDialog = new BadReactionDetail(bd);
                //Butcher_EnterregisterDetail openDialog = new Butcher_EnterregisterDetail(0, 0, "");
                openDialog.ShowDialog();
                if (openDialog.DialogResult == DialogResult.OK)
                {
                    if (openDialog.returnValue != null)
                    {
                        BadReaction returnAction = openDialog.returnValue;

                        ugBadAction_m6.ActiveRow.Cells["ReactionName"].Value = returnAction.ReactionName;
                        ugBadAction_m6.ActiveRow.Cells["OccurDate"].Value = returnAction.OccurDate.Value.ToString("yyyy-MM-dd");
                        ugBadAction_m6.ActiveRow.Cells["Severity"].Value = returnAction.Severity;
                        ugBadAction_m6.ActiveRow.Cells["TreatMethod"].Value = returnAction.TreatMethod;
                        ugBadAction_m6.ActiveRow.Cells["TreatResult"].Value = returnAction.TreatResult;

                    }
                }
            }
        }

        private void ugBadAction_y1_ClickCellButton(object sender, CellEventArgs e)
        {
            if (e.Cell.Text == "删除")
            {
                DialogResult dr = MessageBox.Show(this, "确定要删除此行?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                {
                    if (ugBadAction_y1.ActiveRow.Cells["ID"].Text != "")
                    {
                        Maticsoft.BLL.BadReaction bll = new Maticsoft.BLL.BadReaction();
                        bll.Delete(int.Parse(ugBadAction_y1.ActiveRow.Cells["ID"].Text));
                    }
                    ugBadAction_y1.ActiveRow.Delete(false);//删除所选行
                }
            }
            if (e.Cell.Text == "编辑")
            {
                BadReaction bd = new BadReaction();
                // bd.ID = int.Parse(ugBadReaction.ActiveRow.Cells["ID"].Text);
                bd.ReactionName = ugBadAction_y1.ActiveRow.Cells["ReactionName"].Text;
                bd.OccurDate = DateTime.Parse(ugBadAction_y1.ActiveRow.Cells["OccurDate"].Text);
                bd.Severity = ugBadAction_y1.ActiveRow.Cells["Severity"].Text;
                bd.TreatMethod = ugBadAction_y1.ActiveRow.Cells["TreatMethod"].Text;
                bd.TreatResult = ugBadAction_y1.ActiveRow.Cells["TreatResult"].Text;

                BadReactionDetail openDialog = new BadReactionDetail(bd);
                //Butcher_EnterregisterDetail openDialog = new Butcher_EnterregisterDetail(0, 0, "");
                openDialog.ShowDialog();
                if (openDialog.DialogResult == DialogResult.OK)
                {
                    if (openDialog.returnValue != null)
                    {
                        BadReaction returnAction = openDialog.returnValue;

                        ugBadAction_y1.ActiveRow.Cells["ReactionName"].Value = returnAction.ReactionName;
                        ugBadAction_y1.ActiveRow.Cells["OccurDate"].Value = returnAction.OccurDate.Value.ToString("yyyy-MM-dd");
                        ugBadAction_y1.ActiveRow.Cells["Severity"].Value = returnAction.Severity;
                        ugBadAction_y1.ActiveRow.Cells["TreatMethod"].Value = returnAction.TreatMethod;
                        ugBadAction_y1.ActiveRow.Cells["TreatResult"].Value = returnAction.TreatResult;

                    }
                }
            }
        }

        private void ugAfterManyYears_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            int period = int.Parse(ugAfterManyYears.ActiveRow.Cells["Period"].Text);
             
            AfterTreatTrace openDialog = new AfterTreatTrace(pid,period); 
            openDialog.ShowDialog();
            //if (openDialog.DialogResult == DialogResult.OK)
            //{
            //    if (openDialog.returnValue != null)
            //    {
            //        BadReaction returnAction = openDialog.returnValue;

            //        ugBadAction_y1.ActiveRow.Cells["ReactionName"].Value = returnAction.ReactionName;
            //        ugBadAction_y1.ActiveRow.Cells["OccurDate"].Value = returnAction.OccurDate.Value.ToString("yyyy-MM-dd");
            //        ugBadAction_y1.ActiveRow.Cells["Severity"].Value = returnAction.Severity;
            //        ugBadAction_y1.ActiveRow.Cells["TreatMethod"].Value = returnAction.TreatMethod;
            //        ugBadAction_y1.ActiveRow.Cells["TreatResult"].Value = returnAction.TreatResult;

            //    }
            //}
        }

        private void button27_Click(object sender, EventArgs e)
        {  
            //if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    string fileName = openFileDialog1.FileName;
            //    string newFilename = GetUploadFilePathAndPrefixName() + System.IO.Path.GetFileName(fileName);
            //    try
            //    {
            //        System.IO.File.Copy(fileName, newFilename, true);
            //        this.txtLungPicFile.Tag = newFilename;
            //        txtLungPicFile.Text = System.IO.Path.GetFileName(newFilename);
            //      //  MessageBox.Show(this, "上传成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //        this.btnViewDicom.Visible = true;
            //        this.btnDeleteDicom.Visible = true;
            //        this.btnUploadDicom.Visible = false; 
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(this, "上传失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    }
            //}
        }

        private string GetUploadFilePathAndPrefixName()
        { 
            StringBuilder sb = new StringBuilder();
            sb.Append(Directory.GetCurrentDirectory());
            sb.Append("\\upload\\"); 

            return sb.Append(pid.ToString()).Append("_").Append(DateTime.Now.ToString("yyyyMMddHHmmss")).Append("_").ToString();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            //string dicomFile = txtLungPicFile.Tag.ToString();
           
            //VierDicomFile(dicomFile);            
        }

        private  void VierDicomFile(string dicomFile)
        {


            if (dicomFile == "" || !System.IO.File.Exists(dicomFile)) 
            {
                MessageBox.Show(this, "无效的dicom文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(Directory.GetCurrentDirectory());
            sb.Append("\\viewer\\");
            if (WindowsVersion.is64BitOperatingSystem)
            {
                sb.Append("\\microdicom-081-x64\\");
                string cPath = sb.ToString();
                string filename = Path.Combine(cPath, "mDicom.exe");
                ProcessStartInfo ps = new ProcessStartInfo(filename, dicomFile);
                ps.CreateNoWindow = true;
                Process.Start(ps);
            }
            else
            {
                sb.Append("\\microdicom-081-win32\\");
                string cPath = sb.ToString();
                string filename = Path.Combine(cPath, "mDicom.exe");
                ProcessStartInfo ps = new ProcessStartInfo(filename, dicomFile);
                ps.CreateNoWindow = true;
                Process.Start(ps);
            }
        }

        private void btnDeleteDicom_Click(object sender, EventArgs e)
        {
            //string dicomFile = txtLungPicFile.Tag.ToString();
            //if (dicomFile == "" || !System.IO.File.Exists(dicomFile))
            //{
            //    MessageBox.Show(this, "无效的dicom文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    return;
            //}
            //DialogResult dr = MessageBox.Show(this, "确定要删除此文件么?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //if (dr == DialogResult.Yes)
            //{
            //    System.IO.File.Delete(dicomFile);
            //    this.btnUploadDicom.Visible = true;
            //    this.btnViewDicom.Visible = false;
            //    this.btnDeleteDicom.Visible = false;
            //    txtLungPicFile.Text = "";
            //    txtLungPicFile.Tag = "";
                
            //}
        }

        private void uploaddicom1_Load(object sender, EventArgs e)
        {
            this.uploaddicom1.Pid = pid.ToString();
        }

        private void uploaddicom2_Load(object sender, EventArgs e)
        {
            this.uploaddicom2.Pid = pid.ToString();
        }

        private void uploaddicom3_Load(object sender, EventArgs e)
        {
            this.uploaddicom3.Pid = pid.ToString();
        }

        private void uploaddicom4_Load(object sender, EventArgs e)
        {
            this.uploaddicom4.Pid = pid.ToString();
        }

        private void uploaddicom5_Load(object sender, EventArgs e)
        {
            this.uploaddicom_w.Pid = pid.ToString();
        }

        private void uploaddicom6_Load(object sender, EventArgs e)
        {
            this.uploaddicomGeorge_w.Pid = pid.ToString();
        }

        private void uploaddicom7_Load(object sender, EventArgs e)
        {
            this.uploaddicomLung_w.Pid = pid.ToString();
        }

        private void uploaddicomLung_m1_Load(object sender, EventArgs e)
        {
            uploaddicomLung_m1.Pid = pid.ToString();
        }

        private void uploaddicom_m1_Load(object sender, EventArgs e)
        {
            uploaddicom_m1.Pid = pid.ToString();
        }

        private void uploaddicomGeorge_m1_Load(object sender, EventArgs e)
        {
            uploaddicomGeorge_m1.Pid = pid.ToString();
        }

        private void uploaddicomLung_m3_Load(object sender, EventArgs e)
        {
            uploaddicomLung_m3.Pid = pid.ToString();
        }

        private void uploaddicom_m3_Load(object sender, EventArgs e)
        {
            uploaddicom_m3.Pid = pid.ToString();
        }

        private void uploaddicomGeorge_m3_Load(object sender, EventArgs e)
        {
            uploaddicomGeorge_m3.Pid = pid.ToString();
        }

        private void uploaddicomLung_m6_Load(object sender, EventArgs e)
        {
            uploaddicom_m6.Pid = pid.ToString();
        }

        private void uploaddicom_m6_Load(object sender, EventArgs e)
        {
            uploaddicom_m6.Pid = pid.ToString();
        }

        private void uploaddicomGeorge_m6_Load(object sender, EventArgs e)
        {
            uploaddicomGeorge_m6.Pid = pid.ToString();
        }

        private void uploaddicomLung_y1_Load(object sender, EventArgs e)
        {
            uploaddicomLung_y1.Pid = pid.ToString();
        }

        private void uploaddicom_y1_Load(object sender, EventArgs e)
        {
            uploaddicom_y1.Pid = pid.ToString();
        }

        private void uploaddicomGeorge_y1_Load(object sender, EventArgs e)
        {
            uploaddicomGeorge_y1.Pid = pid.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FreezeForm.ChangeControlEditable(this.tabPage1, true);
        }

         

        





    }
}
