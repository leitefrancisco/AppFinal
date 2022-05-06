using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AppFinal.Models;

namespace AppFinal.Cash
{
    static class CurrentFriend
    {
        public static User _currentFriend;

    
        public static void SetUser(User user)
        {
            _currentFriend = user;
        }
        
        public static User GetUser()
        {
            return _currentFriend;
        }
        public static async Task<LinkedList<GameMatch>> GetMatches()
        {
            return await _currentFriend.GetGameMatches();
        }
    }
}
