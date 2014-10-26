using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper.User
{
    interface UserDAO
    {
        void addUser(string username, string password);
        User getUser(string username, string password);
        bool isExisting(string username);
    }
}
