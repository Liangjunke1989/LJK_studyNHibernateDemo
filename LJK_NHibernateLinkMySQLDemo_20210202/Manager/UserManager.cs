using System.Collections.Generic;
using LJK_NHibernateLinkMySQLDemo_20210202.Common;
using LJK_NHibernateLinkMySQLDemo_20210202.Model;
using NHibernate;
using NHibernate.Criterion;
using Org.BouncyCastle.Asn1.IsisMtt.X509;

namespace LJK_NHibernateLinkMySQLDemo_20210202.Manager
{
    public class UserManager : IUserManager
    {
        private static UserManager _instance;
        public static UserManager Instance => _instance ?? (_instance = new UserManager());

        public void Add(User user)
        {
            // ISession session = NHibernateHelper.OpenSession();
            // session.Save(user);
            // session.Close();
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction()) //需要释放，最好使用using，
                    //只有大括号里面执行完了，transaction才会释放
                {
                    session.Save(user);
                    transaction.Commit();
                }
            }
        }

        //更新
        public void Update(User user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction()) //需要释放，最好使用using，
                    //只有大括号里面执行完了，transaction才会释放
                {
                    session.Update(user);
                    transaction.Commit();
                }
            }
        }

        public void Remove(User user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction()) //需要释放，最好使用using，
                    //只有大括号里面执行完了，transaction才会释放
                {
                    session.Delete(user);
                    transaction.Commit();
                }
            }
        }

        public User GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                User user = session.Get<User>(id);
                return user;
            }
        }

        //使用CreateCriteria查询
        public User GetByUserName(string username)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                // ICriteria criteria = session.CreateCriteria(typeof(User));//criteria标准
                // criteria.Add(Restrictions.Eq("Username", username));//Restrictions限制条件，查询条件封装成了对象
                // return criteria.UniqueResult<User>();//Unique独一无二

                ICriteria criteria = session.CreateCriteria(typeof(User)).Add(Restrictions.Eq("Username", username));//Username属性名
                User user = criteria.UniqueResult<User>();
                return user;
            }
        }

        public ICollection<User> GetAllUsers()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<User> users = session.CreateCriteria(typeof(User)).List<User>();
                return users;
            }
        }

        public bool VerifyUser(string username, string password)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                // 方式一：
                ICriteria criteria = session.CreateCriteria(typeof(User)).Add(Restrictions.Eq("Username", username));//Username属性名
                User user = criteria.UniqueResult<User>();
                return user.Password.Equals(password);
                
                //方式二：
                // User user = session
                //     .CreateCriteria(typeof(User))
                //     .Add(Restrictions.Eq("Username", username))
                //     .Add(Restrictions.Eq("Password", password))
                //     .UniqueResult<User>();
                // if (user==null)
                // {
                //     return false;
                // }
                // return true;
            }
        }
    }
}