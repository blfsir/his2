using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:Lung
	/// </summary>
	public partial class Lung
	{
		public Lung()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperOleDb.GetMaxID("ID", "Lung"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Lung");
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
		public bool Add(Maticsoft.Model.Lung model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Lung(");
			strSql.Append("PID,Period,fev1,fev1pre,fvc,fvcpre,fev1fvc,tlc,tlvpre,rv,rvpre,rvtlc,CheckTime,File)");
			strSql.Append(" values (");
			strSql.Append("@PID,@Period,@fev1,@fev1pre,@fvc,@fvcpre,@fev1fvc,@tlc,@tlvpre,@rv,@rvpre,@rvtlc,@CheckTime,@File)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@PID", OleDbType.Integer,4),
					new OleDbParameter("@Period", OleDbType.Integer,4),
					new OleDbParameter("@fev1", OleDbType.VarChar,50),
					new OleDbParameter("@fev1pre", OleDbType.VarChar,50),
					new OleDbParameter("@fvc", OleDbType.VarChar,50),
					new OleDbParameter("@fvcpre", OleDbType.VarChar,50),
					new OleDbParameter("@fev1fvc", OleDbType.VarChar,50),
					new OleDbParameter("@tlc", OleDbType.VarChar,50),
					new OleDbParameter("@tlvpre", OleDbType.VarChar,50),
					new OleDbParameter("@rv", OleDbType.VarChar,50),
					new OleDbParameter("@rvpre", OleDbType.VarChar,50),
					new OleDbParameter("@rvtlc", OleDbType.VarChar,50),
					new OleDbParameter("@CheckTime", OleDbType.Date),
					new OleDbParameter("@File", OleDbType.VarChar,255)};
			parameters[0].Value = model.PID;
			parameters[1].Value = model.Period;
			parameters[2].Value = model.fev1;
			parameters[3].Value = model.fev1pre;
			parameters[4].Value = model.fvc;
			parameters[5].Value = model.fvcpre;
			parameters[6].Value = model.fev1fvc;
			parameters[7].Value = model.tlc;
			parameters[8].Value = model.tlvpre;
			parameters[9].Value = model.rv;
			parameters[10].Value = model.rvpre;
			parameters[11].Value = model.rvtlc;
			parameters[12].Value = model.CheckTime;
			parameters[13].Value = model.File;

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
		public bool Update(Maticsoft.Model.Lung model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Lung set ");
			strSql.Append("PID=@PID,");
			strSql.Append("Period=@Period,");
			strSql.Append("fev1=@fev1,");
			strSql.Append("fev1pre=@fev1pre,");
			strSql.Append("fvc=@fvc,");
			strSql.Append("fvcpre=@fvcpre,");
			strSql.Append("fev1fvc=@fev1fvc,");
			strSql.Append("tlc=@tlc,");
			strSql.Append("tlvpre=@tlvpre,");
			strSql.Append("rv=@rv,");
			strSql.Append("rvpre=@rvpre,");
			strSql.Append("rvtlc=@rvtlc,");
			strSql.Append("CheckTime=@CheckTime,");
			strSql.Append("File=@File");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@PID", OleDbType.Integer,4),
					new OleDbParameter("@Period", OleDbType.Integer,4),
					new OleDbParameter("@fev1", OleDbType.VarChar,50),
					new OleDbParameter("@fev1pre", OleDbType.VarChar,50),
					new OleDbParameter("@fvc", OleDbType.VarChar,50),
					new OleDbParameter("@fvcpre", OleDbType.VarChar,50),
					new OleDbParameter("@fev1fvc", OleDbType.VarChar,50),
					new OleDbParameter("@tlc", OleDbType.VarChar,50),
					new OleDbParameter("@tlvpre", OleDbType.VarChar,50),
					new OleDbParameter("@rv", OleDbType.VarChar,50),
					new OleDbParameter("@rvpre", OleDbType.VarChar,50),
					new OleDbParameter("@rvtlc", OleDbType.VarChar,50),
					new OleDbParameter("@CheckTime", OleDbType.Date),
					new OleDbParameter("@File", OleDbType.VarChar,255),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
			parameters[0].Value = model.PID;
			parameters[1].Value = model.Period;
			parameters[2].Value = model.fev1;
			parameters[3].Value = model.fev1pre;
			parameters[4].Value = model.fvc;
			parameters[5].Value = model.fvcpre;
			parameters[6].Value = model.fev1fvc;
			parameters[7].Value = model.tlc;
			parameters[8].Value = model.tlvpre;
			parameters[9].Value = model.rv;
			parameters[10].Value = model.rvpre;
			parameters[11].Value = model.rvtlc;
			parameters[12].Value = model.CheckTime;
			parameters[13].Value = model.File;
			parameters[14].Value = model.ID;

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
			strSql.Append("delete from Lung ");
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
			strSql.Append("delete from Lung ");
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
		public Maticsoft.Model.Lung GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,PID,Period,fev1,fev1pre,fvc,fvcpre,fev1fvc,tlc,tlvpre,rv,rvpre,rvtlc,CheckTime,File from Lung ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			Maticsoft.Model.Lung model=new Maticsoft.Model.Lung();
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
		public Maticsoft.Model.Lung DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Lung model=new Maticsoft.Model.Lung();
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
				if(row["fev1"]!=null)
				{
					model.fev1=row["fev1"].ToString();
				}
				if(row["fev1pre"]!=null)
				{
					model.fev1pre=row["fev1pre"].ToString();
				}
				if(row["fvc"]!=null)
				{
					model.fvc=row["fvc"].ToString();
				}
				if(row["fvcpre"]!=null)
				{
					model.fvcpre=row["fvcpre"].ToString();
				}
				if(row["fev1fvc"]!=null)
				{
					model.fev1fvc=row["fev1fvc"].ToString();
				}
				if(row["tlc"]!=null)
				{
					model.tlc=row["tlc"].ToString();
				}
				if(row["tlvpre"]!=null)
				{
					model.tlvpre=row["tlvpre"].ToString();
				}
				if(row["rv"]!=null)
				{
					model.rv=row["rv"].ToString();
				}
				if(row["rvpre"]!=null)
				{
					model.rvpre=row["rvpre"].ToString();
				}
				if(row["rvtlc"]!=null)
				{
					model.rvtlc=row["rvtlc"].ToString();
				}
				if(row["CheckTime"]!=null && row["CheckTime"].ToString()!="")
				{
					model.CheckTime=DateTime.Parse(row["CheckTime"].ToString());
				}
				if(row["File"]!=null)
				{
					model.File=row["File"].ToString();
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
			strSql.Append("select ID,PID,Period,fev1,fev1pre,fvc,fvcpre,fev1fvc,tlc,tlvpre,rv,rvpre,rvtlc,CheckTime,File ");
			strSql.Append(" FROM Lung ");
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
			strSql.Append("select count(1) FROM Lung ");
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
			strSql.Append(")AS Row, T.*  from Lung T ");
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
			parameters[0].Value = "Lung";
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

