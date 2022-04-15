using System;
using System.Collections.Generic;
using System.Text;
using AppFinal.Models;

namespace AppFinal.DB
{
    internal interface UserDBInterface
    {
        public LinkedList<User> GetUsers();

        public LinkedList<User> GetUsers(Dictionary<string, string> filters);

        public LinkedList<User> GetUserFriends(string userId);

        public User GetUser(string userId);

        public User GetUser(Dictionary<string, string> filters);

        public bool UpdateUser(User user);

        public bool DeleteUser(string userId);

        public bool InsertUser(User user);
    }
}
