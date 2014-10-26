using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordKeeper.DB;
using System.Security.Cryptography;
using System.IO;

namespace PasswordKeeper.User
{
    class UserDAOImpl : UserDAO
    {
        public void addUser(string username, string password)
        {
            DBConnection connection = new DBConnection();

            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }           

            connection.AddUser(new User(username, sBuilder.ToString()));
        }

        public User getUser(string username, string password)
        {
            DBConnection connection = new DBConnection();
            User user = connection.GetUser(username);

            if (user != null)
            {
                MD5 md5Hash = MD5.Create();
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                string hashOfInput = sBuilder.ToString();
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                if (0 == comparer.Compare(hashOfInput, user.Password))
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public bool isExisting(string username)
        {
            DBConnection connection = new DBConnection();
            User user = connection.GetUser(username);
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


            
    }
}
