using NHibernate;
using NHibernate.Cfg;

namespace LJK_NHibernateLinkMySQLDemo_20210202.Common
{
    public static class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if(_sessionFactory == null)
                {
                    var configuration = new Configuration();//创建配置对象
                    configuration.Configure();//解析数据库连接配置（在默认路径中解析）
                    configuration.AddAssembly("LJK_NHibernateLinkMySQLDemo_20210202");
                    _sessionFactory = configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();//通过sessionFactory得到一个session。
        }
    }
}