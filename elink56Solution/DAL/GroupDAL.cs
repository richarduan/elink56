using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Models;//请先添加引用

namespace DAL
{
    /// <summary>
    /// 数据访问类Group。
    /// </summary>
    public class GroupDAL
    {
        public GroupDAL() { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int InsertData(GroupInfo model)
        {
            using (DBServer db = new DBServer())
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into [elink_group] (");
                strSql.Append("group_name,group_subjectionId,group_subjectionName,group_subjectionIds,group_display)");
                strSql.Append(" values (");
                strSql.Append("@group_name,@group_subjectionId,@group_subjectionName,@group_subjectionIds,@group_display)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
                 db.MakeInParam("@group_name",SqlDbType.VarChar,200,model.Group_name),
                 db.MakeInParam("@group_subjectionId",SqlDbType.Int,4,model.Group_subjectionId),
                 db.MakeInParam("@group_subjectionName",SqlDbType.VarChar,200,model.Group_subjectionName),
                 db.MakeInParam("@group_subjectionIds",SqlDbType.VarChar,5000,model.Group_subjectionIds),
                 db.MakeInParam("@group_display",SqlDbType.TinyInt,1,model.Group_display),
             };
                return db.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int UpDate(GroupInfo model)
        {
            using (DBServer db = new DBServer())
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update [elink_group] set ");
                strSql.Append("group_name=@group_name,");
                strSql.Append("group_subjectionId=@group_subjectionId,");
                strSql.Append("group_subjectionName=@group_subjectionName,");
                strSql.Append("group_subjectionIds=@group_subjectionIds,");
                strSql.Append("group_display=@group_display");
                strSql.Append(" where group_id=@group_id ");
                SqlParameter[] parameters = {
                 db.MakeInParam("@group_id",SqlDbType.Int,4,model.Group_id),
                 db.MakeInParam("@group_name",SqlDbType.VarChar,200,model.Group_name),
                 db.MakeInParam("@group_subjectionId",SqlDbType.Int,4,model.Group_subjectionId),
                 db.MakeInParam("@group_subjectionName",SqlDbType.VarChar,200,model.Group_subjectionName),
                 db.MakeInParam("@group_subjectionIds",SqlDbType.VarChar,5000,model.Group_subjectionIds),
                 db.MakeInParam("@group_display",SqlDbType.TinyInt,1,model.Group_display)
             };
                return db.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int group_id)
        {
            using (DBServer db = new DBServer())
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete [elink_group] ");
                strSql.Append(" where group_id=@group_id");
                SqlParameter[] parameters = {
                 db.MakeInParam("@group_id",SqlDbType.Int,4,group_id)
             };
                return db.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public GroupInfo GetModel(int group_id)
        {
            using (DBServer db = new DBServer())
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  group_id, group_name, group_subjectionId, group_subjectionName, group_subjectionIds, group_display, Group_level from [elink_group] ");
                strSql.Append(" where group_id=@group_id ");
                using (SqlDataReader dr = db.ExecuteReader(CommandType.Text, strSql.ToString(), new System.Data.SqlClient.SqlParameter("@group_id", group_id)))
                {
                    GroupInfo model = null;
                    if (dr.Read())
                    {
                        model = new GroupInfo();
                        model.Group_id = (int)dr["group_id"];
                        model.Group_name = Convert.ToString(dr["group_name"]);
                        model.Group_subjectionId = (int)dr["group_subjectionId"];
                        model.Group_subjectionName = Convert.ToString(dr["group_subjectionName"]);
                        model.Group_subjectionIds = Convert.ToString(dr["group_subjectionIds"]);
                        model.Group_display = Convert.ToInt32(dr["group_display"]);
                    }
                    return model;
                }
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<GroupInfo> GetList(string strWhere)
        {
            using (DBServer db = new DBServer())
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  group_id, group_name, group_subjectionId, group_subjectionName, group_subjectionIds, group_display, Group_level");
                strSql.Append(" FROM [elink_group] ");
                if (!string.IsNullOrEmpty(strWhere.Trim()))
                {
                    strSql.Append(string.Format(" where {0}",strWhere));
                }
                List<GroupInfo> list = new List<GroupInfo>();
                using (DataTable table = db.GetDataTable(CommandType.Text, strSql.ToString()))
                {
                    foreach (DataRow row in table.Rows)
                    {
                        GroupInfo model = new GroupInfo();
                        model.Group_id = Convert.ToInt32(row["Group_id"]);
                        model.Group_name = Convert.ToString(row["group_name"]);
                        model.Group_subjectionId = (int)row["Group_subjectionId"];
                        model.Group_subjectionName = Convert.ToString(row["group_subjectionName"]);
                        model.Group_subjectionIds = Convert.ToString(row["group_subjectionIds"]);
                        model.Group_display =Convert.ToInt32(row["Group_display"]);
                        model.Group_level = Convert.ToInt32(row["Group_level"]);
                        list.Add(model);
                    }
                    table.Clear();
                    table.Dispose();
                    return list;
                }
            }
        }

        /// <summary>
        /// 存储过程读取
        /// </summary>
        public IList<GroupInfo> GetList(int PageNo, int PageSize, string strWhere, out int TotalPageNo)
        {
            using (DBServer db = new DBServer())
            {
                TotalPageNo = 0;
                strWhere = string.IsNullOrEmpty(strWhere) ? string.Empty : strWhere;
                SqlParameter[] SqlParam = {
		                            db.MakeParam("@tblName",SqlDbType.VarChar,100,ParameterDirection.Input,"[elink_group]"),
		                            db.MakeParam("@fldName",SqlDbType.VarChar,100,ParameterDirection.Input,"group_id"),
		                            db.MakeParam("@fldOut",SqlDbType.VarChar,1000,ParameterDirection.Input,"group_id,group_name,group_subjectionId,group_subjectionName,group_subjectionIds,group_display"),
		                            db.MakeInParam("@PageSize",SqlDbType.Int,4,PageSize),
		                            db.MakeInParam("@PageIndex",SqlDbType.Int,4,PageNo),
		                            db.MakeInParam("@OrderType",SqlDbType.Int,4,1),
		                            db.MakeParam("@strWhere",SqlDbType.VarChar,500,ParameterDirection.Input,strWhere)
	                            };
                List<GroupInfo> list = new List<GroupInfo>();
                using (DataTable table = db.GetDataTable(CommandType.StoredProcedure, "Usp_Paged", SqlParam))
                {
                    foreach (DataRow row in table.Rows)
                    {
                        GroupInfo model = new GroupInfo();
                        model.Group_id = (int)row["Group_id"];
                        model.Group_name = Convert.ToString(row["group_name"]);
                        model.Group_subjectionId = (int)row["Group_subjectionId"];
                        model.Group_subjectionName = Convert.ToString(row["group_subjectionName"]);
                        model.Group_subjectionIds = Convert.ToString(row["group_subjectionIds"]);
                        model.Group_display = (int)row["Group_display"];
                        list.Add(model);
                    }
                    TotalPageNo = this.TotalPageNo(strWhere);
                    table.Clear();
                    table.Dispose();
                    return list;
                }
            }
        }
        #region  返回查询条件下所有总记录数
        /// <summary>
        /// 返回查询条件下所有总记录数
        /// </summary>
        /// <param name="strSearch"></param>
        /// <param name="classid"></param>
        /// <param name="smallclassid"></param>
        /// <returns></returns>
        private int TotalPageNo(string strWhere)
        {
            DBServer db = new DBServer();
            SqlParameter[] SqlParame ={
		                                               db.MakeParam("@tblName",SqlDbType.VarChar,100,ParameterDirection.Input,"[elink_group]"),
		                                               db.MakeParam("@strWhere",SqlDbType.VarChar,500,ParameterDirection.Input,strWhere),
		                                               db.MakeParam("@fldName",SqlDbType.VarChar,100,ParameterDirection.Input,"group_id"),
	                                                 db.MakeParam("@getRec",SqlDbType.Int,4,ParameterDirection.Input,1)
	                                           };
            int intValue = db.ExecuteScalar(CommandType.StoredProcedure, "Usp_Paged", SqlParame);
            return intValue;
        }
        #endregion
        #endregion  成员方法
    }
}
