﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace HIS.common
{
    public partial class uploaddicom : UserControl
    {
        string file = "";

         [Description("当前页号"), Category("自定义")]
        public string File
        {
            get { return file; }
            set { file = value; }
        }
        string pid = "0";

        public string Pid
        {
            get { return pid; }
            set { pid = value; }
        }

        public uploaddicom()
        {
            InitializeComponent();
        }

        public void SetFile(string file)
        {
            this.File = file;
            this.txtLungPicFile.Tag = this.File;
            this.txtLungPicFile.Text = System.IO.Path.GetFileName(this.File);
            this.btnViewDicom.Visible = true;
            this.btnDeleteDicom.Visible = true;
            this.btnUploadDicom.Visible = false;

        }
        //public void SetPid(string pid)
        //{
        //    this.Pid = pid; 

        //}
        private void btnViewDicom_Click(object sender, EventArgs e)
        {
            string dicomFile = txtLungPicFile.Tag.ToString();

            VierDicomFile(dicomFile);         
        }

        private void btnDeleteDicom_Click(object sender, EventArgs e)
        {
            string dicomFile = txtLungPicFile.Tag.ToString();
            if (dicomFile == "" || !System.IO.File.Exists(dicomFile))
            {
                MessageBox.Show(this, "无效的dicom文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            DialogResult dr = MessageBox.Show(this, "确定要删除此文件么?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.Yes)
            {
                System.IO.File.Delete(dicomFile);
                this.btnUploadDicom.Visible = true;
                this.btnViewDicom.Visible = false;
                this.btnDeleteDicom.Visible = false;
                txtLungPicFile.Text = "";
                txtLungPicFile.Tag = "";

            }
        }
         
        private void btnUploadDicom_Click(object sender, EventArgs e)
        {
           
            OpenDialogBox ob = new OpenDialogBox();
            ob.OpenDig();
           
                //fileDialog.InitialDirectory = @"c:\\"; //指定初始打开的目录
                //fileDialog.Title = "dicom文件选择";
              //  fileDialog.RestoreDirectory = true;


            if (ob._dialog == System.Windows.Forms.DialogResult.OK)
                {
                    string fileName = ob._selectedFile;//.FileName; 
                    string newFilename = GetUploadFilePathAndPrefixName() + System.IO.Path.GetFileName(fileName);
                    try
                    {
                        System.IO.File.Copy(fileName, newFilename, true);
                        this.txtLungPicFile.Tag = newFilename;
                        txtLungPicFile.Text = System.IO.Path.GetFileName(newFilename);
                        this.File = newFilename;
                        //  MessageBox.Show(this, "上传成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        this.btnViewDicom.Visible = true;
                        this.btnDeleteDicom.Visible = true;
                        this.btnUploadDicom.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, "上传失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                }
            
        }

        private void VierDicomFile(string dicomFile)
        {


            if (dicomFile == "" || !System.IO.File.Exists(dicomFile))
            {
                MessageBox.Show(this, "无效的dicom文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(Application.StartupPath);
            sb.Append(@"\viewer\");
            if (WindowsVersion.is64BitOperatingSystem)
            {
                sb.Append(@"\microdicom-081-x64\");
                string cPath = sb.ToString();
                string filename = Path.Combine(cPath, "mDicom.exe");
                ProcessStartInfo ps = new ProcessStartInfo(filename, dicomFile);
                ps.CreateNoWindow = true;
                Process.Start(ps);
            }
            else
            {
                sb.Append(@"\microdicom-081-win32\");
                string cPath = sb.ToString();
                string filename = Path.Combine(cPath, "mDicom.exe");
                ProcessStartInfo ps = new ProcessStartInfo(filename, dicomFile);
                ps.CreateNoWindow = true;
                Process.Start(ps);
            }
        }

        private string GetUploadFilePathAndPrefixName()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Application.StartupPath);
            sb.Append(@"\upload\");

            return sb.Append(Pid).Append("_").Append(DateTime.Now.ToString("yyyyMMddHHmmss")).Append("_").ToString();
        }

       
    }
}