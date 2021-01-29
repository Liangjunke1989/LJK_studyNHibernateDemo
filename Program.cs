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
                conn.Open();//建立连接
                Console.WriteLine("已经建立连接了！");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            conn.Close();//关闭连接
            Console.ReadKey();
        }
    }
}