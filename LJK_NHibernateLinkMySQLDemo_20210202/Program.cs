using System;
using System.Linq;
using LJK_NHibernateLinkMySQLDemo_20210202.Model;
using NHibernate;
using NHibernate.Cfg;

namespace LJK_NHibernateLinkMySQLDemo_20210202
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Configuration configuration = new Configuration();
            configuration.Configure(); //解析nHibernate.cfg.xml数据库的连接文件,默认路径默认名字解析
            configuration.AddAssembly("LJK_NHibernateLinkMySQLDemo_20210202"); //解析映射文件User.hbm.xml...
            ISessionFactory sessionFactory = null;
            ISession session = null;
            ITransaction transaction = null;
            try
            {
                sessionFactory = configuration.BuildSessionFactory(); //创建session工厂，用于与数据库的连接
                if (sessionFactory != null)
                {
                    session = sessionFactory.OpenSession(); //打开一个与数据库的会话（连接）
                    User user = new User()
                    {
                        Username = "KOKO",
                        Password = "5566",
                        RegisterDate = DateTime.Now
                    };
                    //插入
                    //session.Save(user);
                    
                    //删除
                    //session.Delete(user);
                    
                    //查询
                    // IQueryable<User> users = session.Query<User>();
                    // foreach (var u in users)
                    // {
                    //     Console.WriteLine(u.Id);
                    // }

                    //事务
                    //MySQL 事务主要用于处理操作量大，复杂度高的数据。
                    //比如说，在人员管理系统中，你删除一个人员，你既需要删除人员的基本资料，也要删除和该人员相关的信息，如信箱，文章等等，
                    //这样，这些数据库操作语句就构成一个事务！
                    
                    /*一般来说，事务是必须满足4个条件（ACID）：原子性（Atomicity，或称不可分割性）、一致性（Consistency）、隔离性（Isolation，又称独立性）、持久性（Durability）。
                    原子性：一个事务（transaction）中的所有操作，要么全部完成，要么全部不完成，不会结束在中间某个环节。
                         事务在执行过程中发生错误，会被回滚（Rollback）到事务开始前的状态，就像这个事务从来没有执行过一样。
                    一致性：在事务开始之前和事务结束以后，数据库的完整性没有被破坏。
                         这表示写入的资料必须完全符合所有的预设规则，这包含资料的精确度、串联性以及后续数据库可以自发性地完成预定的工作。
                    隔离性：数据库允许多个并发事务同时对其数据进行读写和修改的能力，隔离性可以防止多个事务并发执行时由于交叉执行而导致数据的不一致。
                          事务隔离分为不同级别，包括读未提交（Read uncommitted）、读提交（read committed）、可重复读（repeatable read）和串行化（Serializable）。
                    持久性：事务处理结束后，对数据的修改就是永久的，即便系统故障也不会丢失。*/
                    
                    /*a少50，b多50*/    //数据的完整性和统一性,事务回滚
                    
                    //创建事务
                    transaction = session.BeginTransaction();
                    //进行操作
                    User user01 = new User()
                    {
                        Username = "dk01",
                        Password = "123",
                        RegisterDate = DateTime.Now
                    };
                    User user02 = new User()
                    {
                        Username = "dk01",
                        Password = "1989",
                        RegisterDate = DateTime.Now
                    };
                    session.Save(user01);
                    session.Save(user02);
                    //事务提交
                    transaction.Commit();
                    //事务关闭
                }
                Console.WriteLine("已经初步解析了！");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                transaction?.Dispose();//事务关闭/释放
                session?.Close();
                if (sessionFactory != null)
                {
                    sessionFactory.Close();
                }
            }
        }
    }
}