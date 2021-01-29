using System;
using MySql.Data.MySqlClient;

namespace LJK_CSharpLinkMySQLDemo_20210129
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string connectStr = "server=127.0.0.1;port=3306;database=MyGameDB;user=root;password=liang521;";
            MySqlConnection conn = new MySqlConnection(connectStr);//创建连接对象
            try
            {
                conn.Open(); //建立连接
                
                string sql = "select * from users";
                MySqlCommand cmd = new MySqlCommand(sql,conn);
                //cmd.ExecuteReader();//执行一些查询（读取数据）
                //cmd.ExecuteNonQuery();//执行插入操作
                //cmd.ExecuteScalar();//执行一些查询，返回单个查询的值
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("用户名为:{0},  密码为:{1},  注册日期为:{2}",reader[1],reader[2],reader[3]);
                }
                Console.WriteLine("\n已经成功建立连接了！");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();//关闭连接
            }
           
            Console.ReadKey();
        }
    }
}