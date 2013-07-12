using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:BloodGas
	/// </summary>
	public partial class BloodGas
	{
		public BloodGas()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperOleDb.GetMaxID("ID", "BloodGas"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from BloodGas");
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
		public bool Add(Maticsoft.Model.BloodGas model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into BloodGas(");
			strSql.Append("PID,Period,pH,PaO2,SaO2,PaCO2,RealHCO3,StandHCO3,AB,BE,AG,CheckDate)");
			strSql.Append(" values (");
			strSql.Append("@PID,@Period,@pH,@PaO2,@SaO2,@PaCO2,@RealHCO3,@StandHCO3,@AB,@BE,@AG,@CheckDate)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@PID", OleDbType.Integer,4),
					new OleDbParameter("@Period", OleDbType.Integer,4),
					new OleDbParameter("@pH", OleDbType.VarChar,50),
					new OleDbParameter("@PaO2", OleDbType.VarChar,50),
					new OleDbParameter("@SaO2", OleDbType.VarChar,50),
					new OleDbParameter("@PaCO2", OleDbType.VarChar,50),
					new OleDbParameter("@RealHCO3", OleDbType.VarChar,50),
					new OleDbParameter("@StandHCO3", OleDbType.VarChar,50),
					new OleDbParameter("@AB", OleDbType.VarChar,50),
					new OleDbParameter("@BE", OleDbType.VarChar,50),
					new OleDbParameter("@AG", OleDbType.VarChar,50),
					new OleDbParameter("@CheckDate", OleDbType.Date)};
			parameters[0].Value = model.PID;
			parameters[1].Value = model.Period;
			parameters[2].Value = model.pH;
			parameters[3].Value = model.PaO2;
			parameters[4].Value = model.SaO2;
			parameters[5].Value = model.PaCO2;
			parameters[6].Value = model.RealHCO3;
			parameters[7].Value = model.StandHCO3;
			parameters[8].Value = model.AB;
			parameters[9].Value = model.BE;
			parameters[10].Value = model.AG;
			parameters[11].Value = model.CheckDate;

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
		public bool Update(Maticsoft.Model.BloodGas model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update BloodGas set ");
			strSql.Append("PID=@PID,");
			strSql.Append("Period=@Period,");
			strSql.Append("pH=@pH,");
			strSql.Append("PaO2=@PaO2,");
			strSql.Append("SaO2=@SaO2,");
			strSql.Append("PaCO2=@PaCO2,");
			strSql.Append("RealHCO3=@RealHCO3,");
			strSql.Append("StandHCO3=@StandHCO3,");
			strSql.Append("AB=@AB,");
			strSql.Append("BE=@BE,");
			strSql.Append("AG=@AG,");
			strSql.Append("CheckDate=@CheckDate");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@PID", OleDbType.Integer,4),
					new OleDbParameter("@Period", OleDbType.Integer,4),
					new OleDbParameter("@pH", OleDbType.VarChar,50),
					new OleDbParameter("@PaO2", OleDbType.VarChar,50),
					new OleDbParameter("@SaO2", OleDbType.VarChar,50),
					new OleDbParameter("@PaCO2", OleDbType.VarChar,50),
					new OleDbParameter("@RealHCO3", OleDbType.VarChar,50),
					new OleDbParameter("@StandHCO3", OleDbType.VarChar,50),
					new OleDbParameter("@AB", OleDbType.VarChar,50),
					new OleDbParameter("@BE", OleDbType.VarChar,50),
					new OleDbParameter("@AG", OleDbType.VarChar,50),
					new OleDbParameter("@CheckDate", OleDbType.Date),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
			parameters[0].Value = model.PID;
			parameters[1].Value = model.Period;
			parameters[2].Value = model.pH;
			parameters[3].Value = model.PaO2;
			parameters[4].Value = model.SaO2;
			parameters[5].Value = model.PaCO2;
			parameters[6].Value = model.RealHCO3;
			parameters[7].Value = model.StandHCO3;
			parameters[8].Value = model.AB;
			parameters[9].Value = model.BE;
			parameters[10].Value = model.AG;
			parameters[11].Value = model.CheckDate;
			parameters[12].Value = model.ID;

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
			strSql.Append("delete from BloodGas ");
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
			strSql.Append("delete from BloodGas ");
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
		public Maticsoft.Model.BloodGas GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,PID,Period,pH,PaO2,SaO2,PaCO2,RealHCO3,StandHCO3,AB,BE,AG,CheckDate from BloodGas ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			Maticsoft.Model.BloodGas model=new Maticsoft.Model.BloodGas();
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
		public Maticsoft.Model.BloodGas DataRowToModel(DataRow row)
		{
			Maticsoft.Model.BloodGas model=new Maticsoft.Model.BloodGas();
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
				if(row["Period"]!=null && row["Period"].ToString()!="")
				{
					model.Period=int.Parse(row["Period"].ToString());
				}
				if(row["pH"]!=null)
				{
					model.pH=row["pH"].ToString();
				}
				if(row["PaO2"]!=null)
				{
					model.PaO2=row["PaO2"].ToString();
				}
				if(row["SaO2"]!=null)
				{
					model.SaO2=row["SaO2"].ToString();
				}
				if(row["PaCO2"]!=null)
				{
					model.PaCO2=row["PaCO2"].ToString();
				}
				if(row["RealHCO3"]!=null)
				{
					model.RealHCO3=row["RealHCO3"].ToString();
				}
				if(row["StandHCO3"]!=null)
				{
					model.StandHCO3=row["StandHCO3"].ToString();
				}
				if(row["AB"]!=null)
				{
					model.AB=row["AB"].ToString();
				}
				if(row["BE"]!=null)
				{
					model.BE=row["BE"].ToString();
				}
				if(row["AG"]!=null)
				{
					model.AG=row["AG"].ToString();
				}
				if(row["CheckDate"]!=null && row["CheckDate"].ToString()!="")
				{
					model.CheckDate=DateTime.Parse(row["CheckDate"].ToString());
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
			strSql.Append("select ID,PID,Period,pH,PaO2,SaO2,PaCO2,RealHCO3,StandHCO3,AB,BE,AG,CheckDate ");
			strSql.Append(" FROM BloodGas ");
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
			strSql.Append("select count(1) FROM BloodGas ");
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
			strSql.Append(")AS Row, T.*  from BloodGas T ");
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
			parameters[0].Value = "BloodGas";
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
	}
}

