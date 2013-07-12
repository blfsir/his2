using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:Patient
	/// </summary>
	public partial class Patient
	{
		public Patient()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperOleDb.GetMaxID("ID", "Patient"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Patient");
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
		public bool Add(Maticsoft.Model.Patient model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Patient(");
			strSql.Append("Name,IDCode,SignDate,ModifyDate)");
			strSql.Append(" values (");
			strSql.Append("@Name,@IDCode,@SignDate,@ModifyDate)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Name", OleDbType.VarChar,50),
					new OleDbParameter("@IDCode", OleDbType.VarChar,50),
					new OleDbParameter("@SignDate", OleDbType.Date),
					new OleDbParameter("@ModifyDate", OleDbType.Date)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.IDCode;
			parameters[2].Value = model.SignDate;
			parameters[3].Value = model.ModifyDate;

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
		public bool Update(Maticsoft.Model.Patient model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Patient set ");
			strSql.Append("Name=@Name,");
			strSql.Append("IDCode=@IDCode,");
			strSql.Append("SignDate=@SignDate,");
			strSql.Append("ModifyDate=@ModifyDate");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Name", OleDbType.VarChar,50),
					new OleDbParameter("@IDCode", OleDbType.VarChar,50),
					new OleDbParameter("@SignDate", OleDbType.Date),
					new OleDbParameter("@ModifyDate", OleDbType.Date),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.IDCode;
			parameters[2].Value = model.SignDate;
			parameters[3].Value = model.ModifyDate;
			parameters[4].Value = model.ID;

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
			strSql.Append("delete from Patient ");
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
			strSql.Append("delete from Patient ");
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
		public Maticsoft.Model.Patient GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,Name,IDCode,SignDate,ModifyDate from Patient ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			Maticsoft.Model.Patient model=new Maticsoft.Model.Patient();
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
		public Maticsoft.Model.Patient DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Patient model=new Maticsoft.Model.Patient();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["Name"]!=null)
				{
					model.Name=row["Name"].ToString();
				}
				if(row["IDCode"]!=null)
				{
					model.IDCode=row["IDCode"].ToString();
				}
				if(row["SignDate"]!=null && row["SignDate"].ToString()!="")
				{
					model.SignDate=DateTime.Parse(row["SignDate"].ToString());
				}
				if(row["ModifyDate"]!=null && row["ModifyDate"].ToString()!="")
				{
					model.ModifyDate=DateTime.Parse(row["ModifyDate"].ToString());
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
			strSql.Append("select ID,Name,IDCode,SignDate,ModifyDate ");
			strSql.Append(" FROM Patient ");
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
			strSql.Append("select count(1) FROM Patient ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            object obj = DbHelperOleDb.GetSingle(strSql.ToString());
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
            strSql.Append(@"SELECT *,
                            (SELECT Count([ID]) AS C
                            FROM [patient] AS A
                            WHERE A.[ID] < [patient].[ID]) AS [Row]
                            FROM [patient]");
            //strSql.Append(" SELECT ROW_NUMBER() OVER (");
            //if (!string.IsNullOrEmpty(orderby.Trim()))
            //{
            //    strSql.Append("order by T." + orderby);
            //}
            //else
            //{
            //    strSql.Append("order by T.ID desc");
            //}
            //strSql.Append(")AS Row, T.*  from Patient T ");
            //if (!string.IsNullOrEmpty(strWhere.Trim()))
            //{
            //    strSql.Append(" WHERE " + strWhere);
            //}
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            //strSql.Append(" AND  " + strWhere);
            //strSql.Append("SELECT * FROM Patient T ");
            //strSql.AppendFormat(" WHERE T.ID between {0} and {1}", startIndex, endIndex);
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
			parameters[0].Value = "Patient";
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

        public DataSet GetTracedPatient()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"  SELECT  p.ID,p.IDCode,p.Name,1 as Period,'治疗后第1周' as PeriodName  from patient as p , treatinfo as t
                            where Date() between DateAdd('d',-2,DateAdd('ww', 1,treatdate)) and  DateAdd('ww', 1,treatdate)
                            and p.ID =t.PID
 
                            and NOT EXISTS
                                (SELECT ID  
                                 FROM BloodGas
                                 WHERE BloodGas.PID = 
                                        p.ID
                                    AND BloodGas.Period =1)
                            union all
                            SELECT  p.ID,p.IDCode,p.Name,2 as Period,'治疗后1月' as PeriodName  from patient as p , treatinfo as t
                                                       where Date() between DateAdd('d',-2,DateAdd('m', 1,treatdate)) and  DateAdd('m', 1,treatdate)
                                                        and p.ID =t.PID 
                                                        and NOT EXISTS
                                                            (SELECT ID  
                                                             FROM BloodGas
                                                             WHERE BloodGas.PID = 
                                                                    p.ID
                                                                AND BloodGas.Period =1)
									
                            union all
                            SELECT  p.ID,p.IDCode,p.Name,3 as Period,'治疗后3月' as PeriodName  from patient as p , treatinfo as t
                                                       where Date() between DateAdd('d',-7,DateAdd('m', 3,treatdate)) and  DateAdd('m', 3,treatdate)
                                                        and p.ID =t.PID 
                                                        and NOT EXISTS
                                                            (SELECT ID  
                                                             FROM BloodGas
                                                             WHERE BloodGas.PID = 
                                                                    p.ID
                                                                AND BloodGas.Period =1)
                            union all
                            SELECT  p.ID,p.IDCode,p.Name,4 as Period,'治疗后6月' as PeriodName  from patient as p , treatinfo as t
                                                      where Date() between DateAdd('d',-7,DateAdd('m', 6,treatdate)) and  DateAdd('m', 6,treatdate)
                                                        and p.ID =t.PID 
                                                        and NOT EXISTS
                                                            (SELECT ID  
                                                             FROM BloodGas
                                                             WHERE BloodGas.PID = 
                                                                    p.ID
                                                                AND BloodGas.Period =1)
                            union all									
                            SELECT  p.ID,p.IDCode,p.Name,5 as Period,'治疗后1年' as PeriodName  from patient as p , treatinfo as t
                                                      where Date() between DateAdd('d',-15,DateAdd('yyyy', 1,treatdate)) and  DateAdd('yyyy', 1,treatdate)
                                                        and p.ID =t.PID 
                                                        and NOT EXISTS
                                                            (SELECT ID  
                                                             FROM BloodGas
                                                             WHERE BloodGas.PID = 
                                                                    p.ID
                                                                AND BloodGas.Period =1)
                            "); 
            //if (strWhere.Trim() != "")
            //{
            //    strSql.Append(" where " + strWhere);
            //}
            return DbHelperOleDb.Query(strSql.ToString());
        }

        public DataSet GetListWithTitle(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID AS 编号,Name AS 姓名,IDCode AS ID编号,SignDate AS 入库日期,ModifyDate AS 修改日期 ");
            strSql.Append(" FROM Patient ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }
    }
}

