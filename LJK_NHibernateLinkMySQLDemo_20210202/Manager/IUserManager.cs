using System.Collections;
using System.Collections.Generic;
using LJK_NHibernateLinkMySQLDemo_20210202.Model;
namespace LJK_NHibernateLinkMySQLDemo_20210202.Manager
{
    //定义接口的好处？可以以后更换中间件
    public interface IUserManager
    {
        void Add(User user);
        void Update(User user);
        void Remove(User user);
        User GetById(int id);
        User GetByUserName(string username);
        ICollection<User> GetAllUsers();
        bool VerifyUser(string username, string password);

    }
}