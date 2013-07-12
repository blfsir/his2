//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using System.Data;
using System.Text.RegularExpressions;
namespace DataAccessLayer
{
    public abstract class OLEDBHelper
    {
        static Database db = DatabaseFactory.CreateDatabase("AccessConn");

        public OLEDBHelper()
        {
        }

        #region  执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static void ExecuteSql(string SQLString)
        {

            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand(SQLString);
                db.ExecuteNonQuery(dbCommand);

            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static void ExecuteSqlTran(ArrayList SQLStringList)
        {

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            db.ExecuteNonQuery(strsql, trans);
                        }
                    }
                    trans.Commit();
                }
                catch (Exception E)
                {
                    trans.Rollback();
                    throw new Exception(E.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString)
        {

            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand(SQLString);

                object obj = db.ExecuteScalar(dbCommand);
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    return null;
                }
                else
                {
                    return obj;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {

            DataSet ds = null;
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand(SQLString);
                ds = db.ExecuteDataSet(dbCommand);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ds;

        }

        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static bool Exists(string strSql)
        {
            object obj = GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="paras">参数集合</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, DbParameter[] paras)
        {
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand(SQLString);

                for (int i = 0; i < paras.Length; i++)
                {
                    db.AddInParameter(dbCommand, paras[i].ParameterName, paras[i].DbType, paras[i].Value);
                }

                return db.ExecuteNonQuery(dbCommand);

            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的DbParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    //循环
                    foreach (DictionaryEntry myDE in SQLStringList)
                    {
                        string cmdText = myDE.Key.ToString();
                        DbParameter[] cmdParms = (DbParameter[])myDE.Value;

                        DbCommand dbCommand = db.GetSqlStringCommand(cmdText);

                        for (int i = 0; i < cmdParms.Length; i++)
                        {
                            db.AddInParameter(dbCommand, cmdParms[i].ParameterName, cmdParms[i].DbType, cmdParms[i].Value);
                        }

                        db.ExecuteNonQuery(dbCommand, trans);
                    }
                    trans.Commit();
                }
                catch (Exception E)
                {

                    trans.Rollback();

                    throw new Exception(E.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        ///  执行多条SQL语句，实现数据库事务。带参数[添加人：钟伟 20110314]
        /// </summary>
        /// <param name="SQLStringList">sql语句列表</param>
        /// <param name="param">参数列表 注意此项应与sql语句中的项一一对应</param>
        public static void ExecuteSqlTran(ArrayList SQLStringList, List<DbParameter[]> param)
        {
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    if (SQLStringList != null && param != null && SQLStringList.Count > 0 && SQLStringList.Count == param.Count)
                    {
                        //循环
                        for (int n = 0; n < SQLStringList.Count; n++)
                        {
                            string cmdText = SQLStringList[n].ToString();
                            DbParameter[] cmdParms = param[n];
                            if (cmdText.Trim().Length > 0 && cmdParms != null && cmdParms.Length > 0)
                            {
                                DbCommand dbCommand = db.GetSqlStringCommand(cmdText);

                                for (int i = 0; i < cmdParms.Length; i++)
                                {
                                    db.AddInParameter(dbCommand, cmdParms[i].ParameterName, cmdParms[i].DbType, cmdParms[i].Value);
                                }

                                db.ExecuteNonQuery(dbCommand, trans);
                            }
                        }

                        trans.Commit();
                    }
                }
                catch (Exception E)
                {
                    trans.Rollback();
                    throw new Exception(E.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, DbParameter[] cmdParms)
        {
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand(SQLString);
                for (int i = 0; i < cmdParms.Length; i++)
                {
                    db.AddInParameter(dbCommand, cmdParms[i].ParameterName, cmdParms[i].DbType, cmdParms[i].Value);
                }
                object obj = db.ExecuteScalar(dbCommand);
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    return null;
                }
                else
                {
                    return obj;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, DbParameter[] cmdParms)
        {
            DataSet ds = null;
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand(SQLString);
                for (int i = 0; i < cmdParms.Length; i++)
                {
                    db.AddInParameter(dbCommand, cmdParms[i].ParameterName, cmdParms[i].DbType, cmdParms[i].Value);
                }
                ds = db.ExecuteDataSet(dbCommand);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ds;
        }

        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns></returns>
        public static bool Exists(string strSql, DbParameter[] cmdParms)
        {
            object obj = GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region 真分页20110609钟伟

        /// <summary>
        /// 执行对默认数据库有自定义排序的分页的查询 返回一个DataSet
        /// </summary>
        /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="OrderFields">排序语句，带order by</param>
        /// <param name="PageIndex">当前页的页码 从1开始</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="FieldList">用以取需要统计的数量的字段名，如果是多表查询，请带上表名或别名，如:a.num</param>
        /// <param name="PageCount">输出参数，返回查询的总页数</param>
        /// <param name="valueList">输出参数，返回统计的数量列表 注：返回的总记录数为最后一个</param>
        /// <returns></returns>
        public static DataSet ExecutePage(string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, List<string> FieldList, out int PageCount, out List<double> valueList)
        {
            try
            {
                string sql = GetPageSql(SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, FieldList, out PageCount, out valueList, null);
                return Query(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 执行对默认数据库有自定义排序的分页的查询 返回一个DataSet
        /// </summary>
        /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="OrderFields">排序语句，带order by</param>
        /// <param name="PageIndex">当前页的页码 从1开始</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="FieldList">用以取需要统计的数量的字段名，如果是多表查询，请带上表名或别名，如:a.num</param>
        /// <param name="PageCount">输出参数，返回查询的总页数</param>
        /// <param name="valueList">输出参数，返回统计的数量列表 注：返回的总记录数为最后一个</param>
        /// <param name="commandParameters">参数集合</param>
        /// <returns></returns>
        public static DataSet ExecutePage(string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, List<string> FieldList, out int PageCount, out List<double> valueList, DbParameter[] commandParameters)
        {
            try
            {
                string sql = GetPageSql(SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, FieldList, out PageCount, out valueList, commandParameters);
                return Query(sql, commandParameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 取得分页的SQL语句
        /// </summary>
        /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="OrderFields">排序语句，带order by</param>
        /// <param name="PageIndex">当前页的页码</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="FieldList">用以取需要统计的数量的字段名，如果是多表查询，请带上表名或别名，如:a.num</param>
        /// <param name="PageCount">输出参数，返回查询的总页数</param>
        /// <param name="valueList">输出参数，返回统计的数量列表 注：返回的总记录数为最后一个</param>
        /// <param name="cmdParms">参数集合</param>
        /// <returns></returns>
        private static string GetPageSql(string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, List<string> FieldList, out int PageCount, out List<double> valueList, DbParameter[] cmdParms)
        {
            int RecordCount = 0;
            PageCount = 0;
            valueList = new List<double>();
            if (PageSize <= 0)
            {
                PageSize = 10;
            }

            #region 取统计数量

            if (FieldList != null && FieldList.Count > 0)
            {
                foreach (string str in FieldList)
                {
                    string Sqlnum = "select sum(" + str + ") from " + SqlTablesAndWhere;
                    double num = 0;
                    if (cmdParms != null && cmdParms.Length > 0)
                    {
                        num = GetSingle(Sqlnum, cmdParms) == null ? 0 : Convert.ToDouble(GetSingle(Sqlnum, cmdParms).ToString());
                    }
                    else
                    {
                        num = GetSingle(Sqlnum) == null ? 0 : Convert.ToDouble(GetSingle(Sqlnum).ToString());
                    }
                    valueList.Add(num);
                }
            }

            #endregion

            #region 取总记录数

            string SqlCount = "select count(" + IndexField + ") from " + SqlTablesAndWhere;
            if (cmdParms != null && cmdParms.Length > 0)
            {
                RecordCount = GetSingle(SqlCount, cmdParms) == null ? 0 : Convert.ToInt32(GetSingle(SqlCount, cmdParms).ToString());
            }
            else
            {
                RecordCount = GetSingle(SqlCount) == null ? 0 : Convert.ToInt32(GetSingle(SqlCount).ToString());
            }
            valueList.Add(RecordCount);

            #endregion

            if (RecordCount % PageSize == 0)
            {
                PageCount = RecordCount / PageSize;
            }
            else
            {
                PageCount = RecordCount / PageSize + 1;
            }
            if (PageIndex > PageCount)
                PageIndex = PageCount;
            if (PageIndex < 1)
                PageIndex = 1;
            string Sql = null;
            if (PageIndex == 1)
            {
                Sql = "select top " + PageSize + " " + SqlAllFields + " from " + SqlTablesAndWhere + " " + OrderFields;
            }
            else
            {
                Sql = "select top " + PageSize + " " + SqlAllFields + " from ";
                if (SqlTablesAndWhere.ToLower().IndexOf(" where ") > 0)
                {
                    string _where = Regex.Replace(SqlTablesAndWhere, @"\ where\ ", " where (", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                    Sql += _where + ") and (";
                }
                else
                {
                    Sql += SqlTablesAndWhere + " where (";
                }
                Sql += IndexField + " not in (select top " + (PageIndex - 1) * PageSize + " " + IndexField + " from " + SqlTablesAndWhere + " " + OrderFields;
                Sql += ")) " + OrderFields;
            }
            return Sql;
        }

        #endregion
    }
}
