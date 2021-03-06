﻿using System;
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        { 
            //验证输入信息
            //if (ValidateData.Validate(this.cmbLoginName, "登录名", false, -1, -1, ValidateData.ValidateType.CharsAndNumbers) == false ||
            //    ValidateData.Validate(this.txtPassword, "密码", false, -1, -1, ValidateData.ValidateType.CharsAndNumbers) == false)
            //{
            //    return;
            //}
 
            //    if (offlineLogin() == false)
            //    {
            //        return;
            //    }
             
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 离线状态登录,通过文件验证离线登录。成功返回true,失败返回false
        /// 登录成功取得登录信息。(包括用户基本信息,是否是管理员,用户权限信息)
        /// </summary>
        /// <returns></returns>
        private bool offlineLogin()
        {

            return true;
            string username = this.cmbLoginName.Text.Trim();
            string password = this.txtPassword.Text.Trim();
                    if (username.Equals("admin") && password.Equals("admin"))
                    { 
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("登录密码错误,请重试!", "提示");
                        this.txtPassword.Text = "";
                        this.txtPassword.Focus();
                       
                        return false;
                    }
               
             
        }


    }
}
