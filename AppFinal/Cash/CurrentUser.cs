﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AppFinal.DB.AccessClasses;
using AppFinal.Models;

namespace AppFinal.Cash
{
    static class CurrentUser
    {
        private static User _currentUser;

        public static Boolean IsLoggedIn(User user)
        {
            if (_currentUser == user)
                return true;
            return false;
        }

        public static void SetUser(User user)
        {
            _currentUser = user;
        }

        public static void LogOff()
        {
            _currentUser = null;
        }


        public static User GetUser()
        {
            return _currentUser;
        }

        public static async Task<LinkedList<GameMatch>> GetMatches()
        {
          return  await _currentUser.GetGameMatches();
        }

        public static async Task<LinkedList<User>> GetFriends()
        {
            return await new UserDbAccess().GetUserFriends(CurrentUser.GetUser().id);
        }
        public static async Task<User> GetFriend(string id)
        {
            foreach (var user in await GetFriends())
            {
                if (user.id == id)
                    return user;
            }

            return null;
        }
    }
}
