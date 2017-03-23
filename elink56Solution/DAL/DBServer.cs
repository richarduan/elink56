using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DAL
{

    #region 数据库操作类
    /// <summary>
    /// 数据库操作类
    /// </summary>
    public class DBServer : IDisposable
    {

        #region 属性 - 数据库连接属性 只读
        /// <summary>
        /// 数据库连接属性 只读
        /// </summary>
        private string connectionString
        {
            get
            {
                //return System.Configuration.ConfigurationSettings.AppSettings["CnHT_System_SqlServer"];
                //return @"Server=192.168.1.66;Database=CnhtDB;uid=sa;pwd=dc8668";

                //return @"Server=203.156.200.69;Database=CnhtDB;uid=sa;pwd=dc8668";

                //return System.Configuration.ConfigurationSettings.AppSettings["CnHT_System_SqlServer"];
                return System.Configuration.ConfigurationManager.ConnectionStrings["toohuadbases"].ConnectionString;
            }
        }

        private SqlConnection dbConnection;
        #endregion


        #region 构造函数



        public void Dispose()
        {
            //释放对象的方法
            dbConnection.Dispose();
        }



        /// <summary>
        /// 构造函数
        /// </summary>
        public DBServer()
        {
            if (this.dbConnection == null)
            {
                dbConnection = new SqlConnection(this.connectionString);
                if (dbConnection.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                if (dbConnection.State == System.Data.ConnectionState.Broken)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }

                if (dbConnection.State == System.Data.ConnectionState.Closed)
                {

                    dbConnection.Open();

                }
            }

        }
        #endregion

        #region DataReader方法
        /// <summary>
        /// DataReader 传入参数查询方法
        /// </summary>
        /// <param name="commandType">执行类型</param>
        /// <param name="sql">存储过程名称/执行语句</param>
        /// <param name="param">数据组合</param>
        /// <returns></returns>
        /// <summary>



        public SqlDataReader ExecuteReader(CommandType commandType, string sql, params SqlParameter[] param)
        {


            SqlCommand sqlcomm = this.dbConnection.CreateCommand();
            sqlcomm.CommandTimeout = 180;
            sqlcomm.CommandText = sql;
            sqlcomm.Connection = this.dbConnection;
            sqlcomm.CommandType = commandType;
            for (int i = 0; i < param.Length; i++)
            {
                sqlcomm.Parameters.Add(param[i]);

            }
            return sqlcomm.ExecuteReader(CommandBehavior.CloseConnection);



        }

        /// <summary>
        /// DataReader 无传入参数查询方法
        /// </summary>
        /// <param name="commandType">执行类型</param>
        /// <param name="safesql">传入执行SQL</param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(CommandType commandType, string safesql)
        {
            SqlCommand sqlcomm = this.dbConnection.CreateCommand();
            sqlcomm.CommandTimeout = 180;
            sqlcomm.CommandText = safesql;
            sqlcomm.Connection = this.dbConnection;
            sqlcomm.CommandType = commandType;
            return sqlcomm.ExecuteReader(CommandBehavior.CloseConnection);
        }




        #endregion

        #region DataTable方法
        /// <summary>
        /// 数据集操作 返回DataTable 不带参数
        /// </summary>
        /// <param name="safeSql"></param>
        /// <returns></returns>
        public DataTable GetDataTable(CommandType commandType, string safeSql)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = this.dbConnection.CreateCommand();
            cmd.CommandType = commandType;
            cmd.CommandText = safeSql;
            cmd.Connection = this.dbConnection;
            cmd.CommandTimeout = 180;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            this.dbConnection.Close();
            this.dbConnection.Dispose();
            cmd.Dispose();
            return ds.Tables[0];
        }

        /// <summary>
        /// 数据集操作 返回DataTable 带参数
        /// </summary>
        /// <param name="safeSql"></param>
        /// <returns></returns>
        public DataTable GetDataTable(CommandType commandType, string sql, params SqlParameter[] param)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = this.dbConnection.CreateCommand();
            cmd.CommandType = commandType;
            cmd.CommandText = sql;
            cmd.Connection = dbConnection;
            cmd.CommandTimeout = 180;
            //cmd.Parameters.AddRange(values);
            for (int i = 0; i < param.Length; i++)
            {
                cmd.Parameters.Add(param[i]);

            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            cmd.Dispose();
            this.dbConnection.Close();
            this.dbConnection.Dispose();
            return ds.Tables[0];
        }
        #endregion

        #region ExecuteNonQuery方法


        /// <summary>
        /// ExecuteNonQuery 带传入参数
        /// </summary>
        /// <param name="commandType">执行类型</param>
        /// <param name="safeSql">存储过程名称/SQL语句</param>
        /// <param name="param">参数数组</param>
        /// <returns></returns>
        public int ExecuteNonQuery(CommandType commandType, string safeSql, params SqlParameter[] param)
        {

            SqlCommand comm = this.dbConnection.CreateCommand();
            comm.CommandType = commandType;
            comm.CommandText = safeSql;
            comm.Connection = this.dbConnection;
            comm.CommandTimeout = 180;
            for (int i = 0; i < param.Length; i++)
            {
                comm.Parameters.Add(param[i]);
            }

            int intValue = comm.ExecuteNonQuery();
            comm.Dispose();
            this.dbConnection.Close();
            this.dbConnection.Dispose();
            return intValue;

        }

        /// <summary>
        /// ExecuteNonQuery 不带传入参数
        /// </summary>
        /// <param name="commandType">执行类型</param>
        /// <param name="safeSql">执行语句</param>
        /// <returns></returns>
        public int ExecuteNonQuery(CommandType commandType, string safeSql)
        {
            SqlCommand comm = this.dbConnection.CreateCommand();
            comm.CommandType = commandType;
            comm.CommandText = safeSql;
            comm.Connection = this.dbConnection;
            comm.CommandTimeout = 180;
            int intValue = comm.ExecuteNonQuery();
            comm.Dispose();
            this.dbConnection.Close();
            this.dbConnection.Dispose();
            return intValue;

        }


        #endregion

        #region ExecuteScalar方法

        /// <summary>
        ///  ExecuteScalar 带参数方法
        /// </summary>
        /// <param name="commandType">执行方法</param>
        /// <param name="safeSql">存储过程名称/SQL语句</param>
        /// <param name="Parem">参数数组</param>
        /// <returns></returns>
        public int ExecuteScalar(CommandType commandType, string safeSql, params SqlParameter[] Parem)
        {
            SqlCommand comm = this.dbConnection.CreateCommand();
            comm.CommandText = safeSql;
            comm.Connection = this.dbConnection;
            comm.CommandType = commandType;
            comm.CommandTimeout = 180;
            for (int i = 0; i < Parem.Length; i++)
            {
                comm.Parameters.Add(Parem[i]);
            }
            int intValue = Convert.ToInt32(comm.ExecuteScalar());
            comm.Dispose();
            this.dbConnection.Close();
            this.dbConnection.Dispose();
            return intValue;

        }
        /// <summary>
        /// ExecuteScalar 不带参数方法
        /// </summary>
        /// <param name="commandType">执行方法</param>
        /// <param name="safeSql">存储过程名称/SQL语句</param>
        /// <returns></returns>
        public int ExecuteScalar(CommandType commandType, string safeSql)
        {
            SqlCommand comm = this.dbConnection.CreateCommand();
            comm.CommandType = commandType;
            comm.CommandText = safeSql;
            comm.Connection = this.dbConnection;
            int intValue = Convert.ToInt32(comm.ExecuteScalar());
            comm.Dispose();
            this.dbConnection.Close();
            this.dbConnection.Dispose();
            return intValue;
        }





        #endregion

        #region 生成参数
        /// <summary>
        /// 数字入
        /// </summary>
        /// <param name="ParamName">字段名</param>
        /// <param name="DbType">数据类型</param>
        /// <param name="Size">数据长度</param>
        /// <returns></returns>
        public SqlParameter MakeInParam(string ParamName, SqlDbType SqlType, int Size, object Value)
        {
            return MakeParam(ParamName, SqlType, Size, ParameterDirection.Input, Value);
        }

        /// <summary>
        /// 数字输出
        /// </summary>
        /// <param name="ParamName">字段名</param>
        /// <param name="DbType">数据类型</param>
        /// <param name="Size">数据长度</param>
        /// <returns></returns>
        public SqlParameter MakeOutParam(string ParamName, SqlDbType SqlType, int Size)
        {
            return MakeParam(ParamName, SqlType, Size, ParameterDirection.Output, null);
        }

        /// <summary>
        /// 字符输入输出通用方法
        /// </summary>
        /// <param name="ParamName">字段名称</param>
        /// <param name="DbType"></param>
        /// <param name="Size">字符长度</param>
        /// <param name="Direction">输出/输入</param>
        /// <param name="Value">输入值</param>
        /// <returns></returns>
        public SqlParameter MakeParam(string ParamName, SqlDbType SqlType, Int32 Size, ParameterDirection Direction, object Value)
        {

            SqlParameter param = new SqlParameter(ParamName, SqlType, Size);


            if (Direction == ParameterDirection.Output || Value == null)
            {
                Value = null;
            }



            param.Direction = Direction;
            param.Value = Value;

            return param;

        }

        #endregion 生成参数结束

    }
    #endregion

}