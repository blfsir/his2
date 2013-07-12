using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:COPDMedication
	/// </summary>
	public partial class COPDMedication
	{
		public COPDMedication()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperOleDb.GetMaxID("ID", "COPDMedication"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from COPDMedication");
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
		public bool Add(Maticsoft.Model.COPDMedication model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into COPDMedication(");
			strSql.Append("PID,COPDTypeID,COPDTypeName,DrugName,Dose,[Usage])");
			strSql.Append(" values (");
			strSql.Append("@PID,@COPDTypeID,@COPDTypeName,@DrugName,@Dose,@Usage)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@PID", OleDbType.Integer,4),
					new OleDbParameter("@COPDTypeID", OleDbType.Integer,4),
					new OleDbParameter("@COPDTypeName", OleDbType.VarChar,50),
					new OleDbParameter("@DrugName", OleDbType.VarChar,50),
					new OleDbParameter("@Dose", OleDbType.VarChar,50),
					new OleDbParameter("@Usage", OleDbType.VarChar,50)};
			parameters[0].Value = model.PID;
			parameters[1].Value = model.COPDTypeID;
			parameters[2].Value = model.COPDTypeName;
			parameters[3].Value = model.DrugName;
			parameters[4].Value = model.Dose;
			parameters[5].Value = model.Usage;

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
		public bool Update(Maticsoft.Model.COPDMedication model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update COPDMedication set ");
			strSql.Append("PID=@PID,");
			strSql.Append("COPDTypeID=@COPDTypeID,");
			strSql.Append("COPDTypeName=@COPDTypeName,");
			strSql.Append("DrugName=@DrugName,");
			strSql.Append("Dose=@Dose,");
			strSql.Append("[Usage]=@Usage");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@PID", OleDbType.Integer,4),
					new OleDbParameter("@COPDTypeID", OleDbType.Integer,4),
					new OleDbParameter("@COPDTypeName", OleDbType.VarChar,50),
					new OleDbParameter("@DrugName", OleDbType.VarChar,50),
					new OleDbParameter("@Dose", OleDbType.VarChar,50),
					new OleDbParameter("@Usage", OleDbType.VarChar,50),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
			parameters[0].Value = model.PID;
			parameters[1].Value = model.COPDTypeID;
			parameters[2].Value = model.COPDTypeName;
			parameters[3].Value = model.DrugName;
			parameters[4].Value = model.Dose;
			parameters[5].Value = model.Usage;
			parameters[6].Value = model.ID;

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
			strSql.Append("delete from COPDMedication ");
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
			strSql.Append("delete from COPDMedication ");
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
		public Maticsoft.Model.COPDMedication GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,PID,COPDTypeID,COPDTypeName,DrugName,Dose,[Usage] from COPDMedication ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			Maticsoft.Model.COPDMedication model=new Maticsoft.Model.COPDMedication();
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
		public Maticsoft.Model.COPDMedication DataRowToModel(DataRow row)
		{
			Maticsoft.Model.COPDMedication model=new Maticsoft.Model.COPDMedication();
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
				if(row["COPDTypeID"]!=null && row["COPDTypeID"].ToString()!="")
				{
					model.COPDTypeID=int.Parse(row["COPDTypeID"].ToString());
				}
				if(row["COPDTypeName"]!=null)
				{
					model.COPDTypeName=row["COPDTypeName"].ToString();
				}
				if(row["DrugName"]!=null)
				{
					model.DrugName=row["DrugName"].ToString();
				}
				if(row["Dose"]!=null)
				{
					model.Dose=row["Dose"].ToString();
				}
				if(row["Usage"]!=null)
				{
					model.Usage=row["Usage"].ToString();
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
			strSql.Append("select ID,PID,COPDTypeID,COPDTypeName,DrugName,Dose,[Usage] ");
			strSql.Append(" FROM COPDMedication ");
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
			strSql.Append("select count(1) FROM COPDMedication ");
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
			strSql.Append(")AS Row, T.*  from COPDMedication T ");
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
			parameters[0].Value = "COPDMedication";
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

