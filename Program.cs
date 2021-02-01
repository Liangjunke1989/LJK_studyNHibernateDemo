using System;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using Renci.SshNet.Common;

namespace LJK_CSharpLinkMySQLDemo_20210129
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //01、创建连接字符串
            string connectStr = "server=127.0.0.1;port=3306;database=MyGameDB;user=root;password=liang521;";
            //02、//创建Connection连接对象
            MySqlConnection conn = new MySqlConnection(connectStr);//创建连接对象
            try
            {
                //03、打开连接
                conn.Open(); //建立连接
                
                //04、创建sql连接字符串
                //查询Sql语句
                //string sql = "select * from users";
                
                //插入Sql语句
                //string sql = "insert into users(username,password,registerdate)values('ljk4','1233211',"+"'"+DateTime.Now+"'"+")";
                
                //修改Sql语句
                //string sql = "update mygamedb.users set username='dk',password='1234' where id=4";//指定那个数据库下的哪张表进行操作
                
                //删除Sql语句
                string sql = "delete from users where id = 4";
                
                //05、执行利用command对象
                MySqlCommand cmd = new MySqlCommand(sql,conn);
                //06、根据常用的cmd命令执行sql数据库的增删改查。
                
                //与数据库交互的三种方法：
                //cmd.ExecuteScalar();//执行一些查询，返回单个查询的值（返回第一行第一列的值的查询）
                //cmd.ExecuteReader();//执行一些查询（读取数据，常连接，while（reader.read()）逐条读取数据）
                //cmd.ExecuteNonQuery();//执行插入操作
                
                #region 01、查询练习
                //01、利用cmd.ExecuteScalar()查询
                object o = cmd.ExecuteScalar();//读取第一行第一列的数据
                if (o!=null)
                {
                    Console.WriteLine("存在值，查找到有数据！");
                }
                
                //02、利用cmd.ExecuteReader()查询
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())//逐条读取
                {
                    Console.WriteLine("用户ID为:{0}, 用户名为:{1},  密码为:{2},  注册日期为:{3}",reader[0],reader[1],reader[2],reader[3]);
                }
                Console.WriteLine("\n已经成功建立连接了！");
                #endregion

                #region 02、插入练习

                // int executeNonQuery = cmd.ExecuteNonQuery();//影响了几行记录
                // if (executeNonQuery>0)
                // {
                //     Console.WriteLine("插入成功！影响的行数为："+executeNonQuery);
                // }
                #endregion

                #region 03、修改练习

                // int executeNonQuery2 = cmd.ExecuteNonQuery();//影响了几行记录
                // if (executeNonQuery2>0)
                // {
                //     Console.WriteLine("修改成功！影响的行数为："+executeNonQuery2);
                // }
                
                #endregion

                #region 04、删除练习

                int executeNonQuery3 = cmd.ExecuteNonQuery();//影响了几行记录
                if (executeNonQuery3>0)
                {
                    Console.WriteLine("删除成功！影响的行数为："+executeNonQuery3);
                }

                #endregion

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