using System.Data;
using System.Xml;
using MySql.Data.MySqlClient;

namespace LJK_CSharpLinkMySQLDemo_20210129
{
    public static class LjkSqlHelper
    {
        #region 返回要连接的sql语句

        public static string GetConnectSqliteString()
        {
            #region 从配置文件中获取

            //     string connStr = "";
            //     string configPath = Application.dataPath;
            //     int num = configPath.LastIndexOf("/");
            //     configPath = configPath.Substring(0, num);
            //     string url = configPath + "/config/systemconfig.xml";

            //     /**
            //* 开始解析XML文件 
            //*/
            //     XmlDocument xmlDoc = new XmlDocument();
            //     xmlDoc.Load(url);

            //     if (xmlDoc.HasChildNodes)
            //     {
            //         XmlNodeList nodeList = xmlDoc.GetElementsByTagName("ConnetionStringSetting");
            //         foreach (XmlNode node in nodeList)
            //         {
            //             connStr = node.SelectSingleNode("ConnetionString").InnerText;
            //         }
            //     }
            //     return connStr;

            #endregion

            // string configPath = Application.dataPath;
            // int num = configPath.LastIndexOf("/");
            // configPath = configPath.Substring(0, num);
            // string url = configPath + "/config/" + "CameraServer.db;";
            // string connectionString = " Data Source= " + url + " version=3";
            // connectionString=connectionString.Replace("/", "\\");
            // return connectionString;
            string connectionString = "server=127.0.0.1;port=3306;database=MyGameDB;user=root;password=liang521";
            return connectionString;
        }

        #endregion

        //执行命令的封装：增、删、改、查

        #region ExcuteNonQuery 执行非查询语句的方法

        /// <summary>
        ///  执行非查询语句的方法，执行的sql为insert、update、delete的时候会调用
        /// </summary>
        /// <returns>返回影响的行数</returns>
        public static int LJK_ExcuteNonQuery(string sql, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(GetConnectSqliteString()))
            {
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region ExcuteScalar 返回首行首列的方法

        /// <summary>
        /// 返回首行首列的方法
        /// </summary>
        /// <param name="sql">获取要执行的sql语句</param>
        /// <param name="parameters">添加sql语句中的参数</param>
        /// <returns>返回首行首列的方法的值</returns>
        public static object LJK_ExcuteScalar(string sql, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(GetConnectSqliteString()))
            {
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    //直接返回的是首行首列的值
                    return cmd.ExecuteScalar();
                }
            }
        }

        #endregion

        #region 从数据库中查询，获得内存表DataTable

        /// <summary>
        /// 通过sql查询，获取DataTable
        /// </summary>
        /// <param name="sql">要查询的sql脚本</param>
        /// <param name="parameters">sql脚本中的参数</param>
        /// <returns>返回内存表DataTable</returns>
        public static DataTable LJK_GetDataTable(string sql, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(GetConnectSqliteString()))
            {
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn))
                    {
                        DataTable dataTable = new DataTable();
                        //千万记住添加参数,查询命令
                        adapter.SelectCommand.Parameters.AddRange(parameters);
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        #endregion
        
    }
}