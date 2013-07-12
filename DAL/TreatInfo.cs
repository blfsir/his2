using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:TreatInfo
	/// </summary>
	public partial class TreatInfo
	{
		public TreatInfo()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperOleDb.GetMaxID("ID", "TreatInfo"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from TreatInfo");
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
		public bool Add(Maticsoft.Model.TreatInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TreatInfo(");
			strSql.Append("PID,TreatDate,TreatLobe,ValveCount,ValvePosition,SurgeryDate)");
			strSql.Append(" values (");
			strSql.Append("@PID,@TreatDate,@TreatLobe,@ValveCount,@ValvePosition,@SurgeryDate)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@PID", OleDbType.Integer,4),
					new OleDbParameter("@TreatDate", OleDbType.Date),
					new OleDbParameter("@TreatLobe", OleDbType.VarChar,50),
					new OleDbParameter("@ValveCount", OleDbType.Integer,4),
					new OleDbParameter("@ValvePosition", OleDbType.VarChar,50),
					new OleDbParameter("@SurgeryDate", OleDbType.Date)};
			parameters[0].Value = model.PID;
			parameters[1].Value = model.TreatDate;
			parameters[2].Value = model.TreatLobe;
			parameters[3].Value = model.ValveCount;
			parameters[4].Value = model.ValvePosition;
			parameters[5].Value = model.SurgeryDate;

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
		public bool Update(Maticsoft.Model.TreatInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TreatInfo set ");
			strSql.Append("PID=@PID,");
			strSql.Append("TreatDate=@TreatDate,");
			strSql.Append("TreatLobe=@TreatLobe,");
			strSql.Append("ValveCount=@ValveCount,");
			strSql.Append("ValvePosition=@ValvePosition,");
			strSql.Append("SurgeryDate=@SurgeryDate");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@PID", OleDbType.Integer,4),
					new OleDbParameter("@TreatDate", OleDbType.Date),
					new OleDbParameter("@TreatLobe", OleDbType.VarChar,50),
					new OleDbParameter("@ValveCount", OleDbType.Integer,4),
					new OleDbParameter("@ValvePosition", OleDbType.VarChar,50),
					new OleDbParameter("@SurgeryDate", OleDbType.Date),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
			parameters[0].Value = model.PID;
			parameters[1].Value = model.TreatDate;
			parameters[2].Value = model.TreatLobe;
			parameters[3].Value = model.ValveCount;
			parameters[4].Value = model.ValvePosition;
			parameters[5].Value = model.SurgeryDate;
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
			strSql.Append("delete from TreatInfo ");
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
			strSql.Append("delete from TreatInfo ");
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
		public Maticsoft.Model.TreatInfo GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,PID,TreatDate,TreatLobe,ValveCount,ValvePosition,SurgeryDate from TreatInfo ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			Maticsoft.Model.TreatInfo model=new Maticsoft.Model.TreatInfo();
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
		public Maticsoft.Model.TreatInfo DataRowToModel(DataRow row)
		{
			Maticsoft.Model.TreatInfo model=new Maticsoft.Model.TreatInfo();
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
				if(row["TreatDate"]!=null && row["TreatDate"].ToString()!="")
				{
					model.TreatDate=DateTime.Parse(row["TreatDate"].ToString());
				}
				if(row["TreatLobe"]!=null)
				{
					model.TreatLobe=row["TreatLobe"].ToString();
				}
				if(row["ValveCount"]!=null && row["ValveCount"].ToString()!="")
				{
					model.ValveCount=int.Parse(row["ValveCount"].ToString());
				}
				if(row["ValvePosition"]!=null)
				{
					model.ValvePosition=row["ValvePosition"].ToString();
				}
				if(row["SurgeryDate"]!=null && row["SurgeryDate"].ToString()!="")
				{
					model.SurgeryDate=DateTime.Parse(row["SurgeryDate"].ToString());
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
			strSql.Append("select ID,PID,TreatDate,TreatLobe,ValveCount,ValvePosition,SurgeryDate ");
			strSql.Append(" FROM TreatInfo ");
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
			strSql.Append("select count(1) FROM TreatInfo ");
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
			strSql.Append(")AS Row, T.*  from TreatInfo T ");
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
			parameters[0].Value = "TreatInfo";
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
            strSql.Append(@"select    p.ID AS 编号, p.IDCode AS ID编号,p.Name AS 姓名, 
                        g.TreatDate AS 治疗日期,
                        g.TreatLobe AS 治疗肺叶,
                        g.ValveCount AS 放置活瓣个数,
                        g.ValvePosition AS 放置位置,
                        g.SurgeryDate AS  手术时间 ");
            strSql.Append(" FROM TreatInfo g, Patient p ");
            strSql.Append(" where g.PID = p.ID " );
            if (strWhere.Trim() != "")
            {
                strSql.Append( strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }
    }
}

