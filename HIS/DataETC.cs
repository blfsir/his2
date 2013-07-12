﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExportToExcel;
using System.IO;
using System.Diagnostics;
using HIS.common;

namespace HIS
{
    public partial class DataETC : Form
    {
        public DataETC()
        {
            InitializeComponent();
            
        }

        private void BindCheckBoxCheckedChangeEvent(Control f)
        {
            foreach (Control c in f.Controls)
            {
                if (c.HasChildren)
                {
                    BindCheckBoxCheckedChangeEvent(c);
                }
                else if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    cb.CheckedChanged += new System.EventHandler(subCheck_CheckedChanges);
                }
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //export data to excel
        private void btnOK_Click(object sender, EventArgs e)
        {
            Maticsoft.BLL.Patient patientBLL = new Maticsoft.BLL.Patient();
            DataSet ds = patientBLL.GetAllListWithTitle();
            ds.Tables[0].TableName = "患者信息";

            //人口学数据
            Maticsoft.BLL.Demographic demoBLL = new Maticsoft.BLL.Demographic();
            DataTable dtDemo = demoBLL.GetAllListWithTitle().Tables[0].Copy();
            dtDemo.TableName = "人口学数据";
            //一般情况
            Maticsoft.BLL.GeneralInfo geneBLL = new Maticsoft.BLL.GeneralInfo();
            DataTable dtGene = geneBLL.GetAllListWithTitle().Tables[0].Copy();
            dtGene.TableName = "一般情况";

            ds.Tables.Add(dtDemo);
            ds.Tables.Add(dtGene);
            DialogResult result = this.folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string foldername = this.folderBrowserDialog1.SelectedPath;
                try
                {
                    if (cbTreatInfo.Checked || cbTreatInfoBadAction.Checked)
                    {
                        DataSet dsTreatInfo = GetTreatInfo();
                        CreateExcelFile.CreateExcelDocument(dsTreatInfo, foldername + @"\治疗情况.xlsx");
                    }
                    if (cbCOPD.Checked || cbBlood.Checked || cbLung.Checked || cbDicom.Checked || cbChartis.Checked || cbSport.Checked)
                    {
                        DataSet dsBeforeTreatInfo = GetBeforeTreatInfo();
                        CreateExcelFile.CreateExcelDocument(dsBeforeTreatInfo, foldername + @"\治疗前基线指标.xlsx");
                    }
                    
                    CreateExcelFile.CreateExcelDocument(ds, foldername+@"\患者基本信息.xlsx");
                    MessageBox.Show("数据提取成功!");
                    if (File.Exists(foldername))
                    {
                        Process.Start("explorer.exe", foldername);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Couldn't create Excel file.\r\nException: " + ex.Message);
                    return;
                }
                // the code here will be executed if the user presses Open in
                // the dialog.
            }

        }

        private DataSet GetBeforeTreatInfo()
        {
            throw new NotImplementedException();
        }

        private DataSet GetTreatInfo()
        {
            DataSet ds = new DataSet();
            if (cbTreatInfo.Checked)
            {
                Maticsoft.BLL.TreatInfo bll = new Maticsoft.BLL.TreatInfo();
                DataSet dsTreatInfo = bll.GetListWithTitle("");
                dsTreatInfo.Tables[0].TableName = "治疗情况";
                ds.Merge(dsTreatInfo);
            }
            if (cbTreatInfoBadAction.Checked)
            {
                Maticsoft.BLL.BadReaction badBLL = new Maticsoft.BLL.BadReaction();
                DataSet dsBad = badBLL.GetListWithTitle(" AND g.peroid=" + TreatPeroid.InTreatment.ToString());
                dsBad.Tables[0].TableName = "术中不良事件";
                ds.Merge(dsBad);
            }
            

            return ds;
        }

        //private void checkAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    FreezeForm.ChangeCheckBoxStatus(this.gbexport, this.checkAll.Checked);
        //}
        //   this.checkAll.CheckedChanged += new System.EventHandler(this.checkAll_CheckedChanged);
        private void subCheck_CheckedChanges(object sender, EventArgs e)
        {
            //var c = GetAll(this.gbexport, typeof(CheckBox));
            //bool ischecked = true;
            //foreach (Control cb in c)
            //{
            //    CheckBox myCB = (CheckBox)cb;
            //    if (!myCB.Checked)
            //    {
            //        ischecked = false;
            //        break;
            //    }
            //}
            //this.checkAll.Checked = ischecked;
           //this.checkAll.Checked = FreezeForm.IsAllCheckBoxChecked(this.gbexport);
        }

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        private void DataETC_Load(object sender, EventArgs e)
        {
            //BindCheckBoxCheckedChangeEvent(this.gbexport);
            //var c = GetAll(this.gbexport, typeof(CheckBox));
            //foreach (Control cb in c)
            //{
            //    CheckBox myCB = (CheckBox)cb;
            //    myCB.CheckedChanged += new System.EventHandler(subCheck_CheckedChanges);
            //}
            //MessageBox.Show("Total Controls: " + c.Count());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FreezeForm.ChangeCheckBoxStatus(this.gbexport, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FreezeForm.ChangeCheckBoxStatus(this.gbexport, false);
        }
    }
}
