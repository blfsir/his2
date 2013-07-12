﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
 
using System.Data.OleDb;
using Maticsoft.Model;

namespace DataAccessLayer
{
    public class PatientDAL
    {
        public System.Data.DataTable GetPatientByID(string PatientID)
        {
            string strSql = "select * from [Patient] where id=" + PatientID;
            try
            {
                DataSet ds = OLEDBHelper.Query(strSql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }

        public int EditPatientInfo(Patient patientInfo)
        {
            int strResult = 0;
            #region 更新数据语句
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [Patient] ");
            strSql.Append("set Name=@name,IDCode=@idcode,ModifyDate=@modifydate ");
            strSql.Append(" where ID=@id ");
            OleDbParameter[] parameters = {
				new OleDbParameter("@name", OleDbType.WChar,50),
				new OleDbParameter("@idcode", OleDbType.WChar,50),
				new OleDbParameter("@modifydate", OleDbType.Date), 
                new OleDbParameter("@id",  OleDbType.Integer)
                };
            parameters[0].Value = patientInfo.Name ;
            parameters[1].Value = patientInfo.IDCode;
            parameters[2].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            parameters[3].Value = patientInfo.ID;
            #endregion
            try
            {
                strResult = OLEDBHelper.ExecuteSql(strSql.ToString(), parameters);
                return strResult;
            }
            catch (Exception ex)
            {
                return strResult;
            }
        }

        public int AddPatientInfo(Patient patientInfo)
        {
            int strResult = 0;
            #region 插入数据语句
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [Patient](");
            strSql.Append("Name,IDCode,SignDate) ");
            strSql.Append(" values (");
            strSql.Append("@name,@idcode,@signdate)");
            OleDbParameter[] parameters = {
				
				new OleDbParameter("@name", OleDbType.WChar,50),
				new OleDbParameter("@idcode", OleDbType.WChar,50),
				new OleDbParameter("@signdate", OleDbType.Date) 
                };

            parameters[0].Value = patientInfo.Name;
            parameters[1].Value = patientInfo.IDCode;
            parameters[2].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            #endregion
            try
            {
                strResult = OLEDBHelper.ExecuteSql(strSql.ToString(), parameters);

                return strResult;
            }
            catch (Exception ex)
            {
                return strResult;
            }
        }

        public DataTable GetPatientList()
        {
            string strSql = "select * from [Patient] ";
            try
            {
                DataSet ds = OLEDBHelper.Query(strSql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }
    }
}