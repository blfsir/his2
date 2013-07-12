using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:TreatHistory
	/// </summary>
	public partial class TreatHistory
	{
		public TreatHistory()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperOleDb.GetMaxID("ID", "TreatHistory"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from TreatHistory");
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
		public bool Add(Maticsoft.Model.TreatHistory model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TreatHistory(");
			strSql.Append("PID,GID,Disease,IllYear,TreatInfo,TreatResult,MedicateHistory)");
			strSql.Append(" values (");
			strSql.Append("@PID,@GID,@Disease,@IllYear,@TreatInfo,@TreatResult,@MedicateHistory)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@PID", OleDbType.Integer,4),
					new OleDbParameter("@GID", OleDbType.Integer,4),
					new OleDbParameter("@Disease", OleDbType.VarChar,50),
					new OleDbParameter("@IllYear", OleDbType.VarChar,50),
					new OleDbParameter("@TreatInfo", OleDbType.VarChar,50),
					new OleDbParameter("@TreatResult", OleDbType.VarChar,50),
					new OleDbParameter("@MedicateHistory", OleDbType.VarChar,50)};
			parameters[0].Value = model.PID;
			parameters[1].Value = model.GID;
			parameters[2].Value = model.Disease;
			parameters[3].Value = model.IllYear;
			parameters[4].Value = model.TreatInfo;
			parameters[5].Value = model.TreatResult;
			parameters[6].Value = model.MedicateHistory;

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
		public bool Update(Maticsoft.Model.TreatHistory model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TreatHistory set ");
			strSql.Append("PID=@PID,");
			strSql.Append("GID=@GID,");
			strSql.Append("Disease=@Disease,");
			strSql.Append("IllYear=@IllYear,");
			strSql.Append("TreatInfo=@TreatInfo,");
			strSql.Append("TreatResult=@TreatResult,");
			strSql.Append("MedicateHistory=@MedicateHistory");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@PID", OleDbType.Integer,4),
					new OleDbParameter("@GID", OleDbType.Integer,4),
					new OleDbParameter("@Disease", OleDbType.VarChar,50),
					new OleDbParameter("@IllYear", OleDbType.VarChar,50),
					new OleDbParameter("@TreatInfo", OleDbType.VarChar,50),
					new OleDbParameter("@TreatResult", OleDbType.VarChar,50),
					new OleDbParameter("@MedicateHistory", OleDbType.VarChar,50),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
			parameters[0].Value = model.PID;
			parameters[1].Value = model.GID;
			parameters[2].Value = model.Disease;
			parameters[3].Value = model.IllYear;
			parameters[4].Value = model.TreatInfo;
			parameters[5].Value = model.TreatResult;
			parameters[6].Value = model.MedicateHistory;
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
			strSql.Append("delete from TreatHistory ");
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
			strSql.Append("delete from TreatHistory ");
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
		public Maticsoft.Model.TreatHistory GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,PID,GID,Disease,IllYear,TreatInfo,TreatResult,MedicateHistory from TreatHistory ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			Maticsoft.Model.TreatHistory model=new Maticsoft.Model.TreatHistory();
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
		public Maticsoft.Model.TreatHistory DataRowToModel(DataRow row)
		{
			Maticsoft.Model.TreatHistory model=new Maticsoft.Model.TreatHistory();
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
				if(row["GID"]!=null && row["GID"].ToString()!="")
				{
					model.GID=int.Parse(row["GID"].ToString());
				}
				if(row["Disease"]!=null)
				{
					model.Disease=row["Disease"].ToString();
				}
				if(row["IllYear"]!=null)
				{
					model.IllYear=row["IllYear"].ToString();
				}
				if(row["TreatInfo"]!=null)
				{
					model.TreatInfo=row["TreatInfo"].ToString();
				}
				if(row["TreatResult"]!=null)
				{
					model.TreatResult=row["TreatResult"].ToString();
				}
				if(row["MedicateHistory"]!=null)
				{
					model.MedicateHistory=row["MedicateHistory"].ToString();
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
			strSql.Append("select ID,PID,GID,Disease,IllYear,TreatInfo,TreatResult,MedicateHistory ");
			strSql.Append(" FROM TreatHistory ");
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
			strSql.Append("select count(1) FROM TreatHistory ");
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
			strSql.Append(")AS Row, T.*  from TreatHistory T ");
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
			parameters[0].Value = "TreatHistory";
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

