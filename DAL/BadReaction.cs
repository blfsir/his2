﻿using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:BadReaction
	/// </summary>
	public partial class BadReaction
	{
		public BadReaction()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperOleDb.GetMaxID("ID", "BadReaction"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from BadReaction");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			return DbHelperOleDb.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.BadReaction model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into BadReaction(");
			strSql.Append("PID,Peroid,ReactionName,OccurDate,Severity,TreatMethod,TreatResult)");
			strSql.Append(" values (");
			strSql.Append("@PID,@Peroid,@ReactionName,@OccurDate,@Severity,@TreatMethod,@TreatResult)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@PID", OleDbType.Integer,4),
					new OleDbParameter("@Peroid", OleDbType.Integer,4),
					new OleDbParameter("@ReactionName", OleDbType.VarChar,50),
					new OleDbParameter("@OccurDate", OleDbType.Date),
					new OleDbParameter("@Severity", OleDbType.VarChar,50),
					new OleDbParameter("@TreatMethod", OleDbType.VarChar,50),
					new OleDbParameter("@TreatResult", OleDbType.VarChar,50)};
			parameters[0].Value = model.PID;
			parameters[1].Value = model.Peroid;
			parameters[2].Value = model.ReactionName;
			parameters[3].Value = model.OccurDate;
			parameters[4].Value = model.Severity;
			parameters[5].Value = model.TreatMethod;
			parameters[6].Value = model.TreatResult;

			int rows=DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.BadReaction model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update BadReaction set ");
			strSql.Append("PID=@PID,");
			strSql.Append("Peroid=@Peroid,");
			strSql.Append("ReactionName=@ReactionName,");
			strSql.Append("OccurDate=@OccurDate,");
			strSql.Append("Severity=@Severity,");
			strSql.Append("TreatMethod=@TreatMethod,");
			strSql.Append("TreatResult=@TreatResult");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@PID", OleDbType.Integer,4),
					new OleDbParameter("@Peroid", OleDbType.Integer,4),
					new OleDbParameter("@ReactionName", OleDbType.VarChar,50),
					new OleDbParameter("@OccurDate", OleDbType.Date),
					new OleDbParameter("@Severity", OleDbType.VarChar,50),
					new OleDbParameter("@TreatMethod", OleDbType.VarChar,50),
					new OleDbParameter("@TreatResult", OleDbType.VarChar,50),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
			parameters[0].Value = model.PID;
			parameters[1].Value = model.Peroid;
			parameters[2].Value = model.ReactionName;
			parameters[3].Value = model.OccurDate;
			parameters[4].Value = model.Severity;
			parameters[5].Value = model.TreatMethod;
			parameters[6].Value = model.TreatResult;
			parameters[7].Value = model.ID;

			int rows=DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from BadReaction ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			int rows=DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from BadReaction ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperOleDb.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.BadReaction GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,PID,Peroid,ReactionName,OccurDate,Severity,TreatMethod,TreatResult from BadReaction ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			Maticsoft.Model.BadReaction model=new Maticsoft.Model.BadReaction();
			DataSet ds=DbHelperOleDb.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.BadReaction DataRowToModel(DataRow row)
		{
			Maticsoft.Model.BadReaction model=new Maticsoft.Model.BadReaction();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["PID"]!=null && row["PID"].ToString()!="")
				{
					model.PID=int.Parse(row["PID"].ToString());
				}
				if(row["Peroid"]!=null && row["Peroid"].ToString()!="")
				{
					model.Peroid=int.Parse(row["Peroid"].ToString());
				}
				if(row["ReactionName"]!=null)
				{
					model.ReactionName=row["ReactionName"].ToString();
				}
				if(row["OccurDate"]!=null && row["OccurDate"].ToString()!="")
				{
					model.OccurDate=DateTime.Parse(row["OccurDate"].ToString());
				}
				if(row["Severity"]!=null)
				{
					model.Severity=row["Severity"].ToString();
				}
				if(row["TreatMethod"]!=null)
				{
					model.TreatMethod=row["TreatMethod"].ToString();
				}
				if(row["TreatResult"]!=null)
				{
					model.TreatResult=row["TreatResult"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,PID,Peroid,ReactionName,OccurDate,Severity,TreatMethod,TreatResult ");
			strSql.Append(" FROM BadReaction ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperOleDb.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM BadReaction ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from BadReaction T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperOleDb.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			OleDbParameter[] parameters = {
					new OleDbParameter("@tblName", OleDbType.VarChar, 255),
					new OleDbParameter("@fldName", OleDbType.VarChar, 255),
					new OleDbParameter("@PageSize", OleDbType.Integer),
					new OleDbParameter("@PageIndex", OleDbType.Integer),
					new OleDbParameter("@IsReCount", OleDbType.Boolean),
					new OleDbParameter("@OrderType", OleDbType.Boolean),
					new OleDbParameter("@strWhere", OleDbType.VarChar,1000),
					};
			parameters[0].Value = "BadReaction";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperOleDb.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod

        public DataSet GetListWithTitle(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select   p.ID AS 编号, p.IDCode AS ID编号,p.Name AS 姓名, 
                            iif(g.Peroid=0,'术中',iif(g.Peroid=1,'治疗后1周',iif(g.Peroid=2,'治疗后1月',iif(g.Peroid=3,'治疗后3月',iif(g.Peroid=4,'治疗后6月',iif(g.Peroid=5,'治疗后1年',iif(g.Peroid=6,'治疗后2年',iif(g.Peroid=7,'治疗后3年',iif(g.Peroid=8,'治疗后4年',iif(g.Peroid=9,'治疗后5年','其它')))))))))) as 不良事件发生区间,
                            g.ReactionName AS 副反应名称,
                            Format(g.OccurDate,'yyyy-mm-dd') AS 发生日期,
                            g.Severity AS 严重程度,
                            g.TreatMethod AS 处理方法,
                            g.TreatResult AS  处理结果");
            strSql.Append(" FROM BadReaction g, Patient p ");
            strSql.Append(" where g.PID = p.ID");
            if (strWhere.Trim() != "")
            {
                strSql.Append( strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }
    }
}

