using System;

namespace LJK_NHibernateLinkMySQLDemo_20210202.Model
{
    public class User
    {
        public virtual  int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual DateTime RegisterDate { get; set; }
    }
    
    
}