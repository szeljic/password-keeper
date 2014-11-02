using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper.Passwords
{
    class Item
    {
        private string title, username, password, url, description;
        private int idUser, idPass, type;

        public int IdPass
        {
            get { return idPass; }
            set { idPass = value; }
        }
        private DateTime expires;

        public DateTime Expires
        {
            get { return expires; }
            set { expires = value; }
        }

        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        public int IdUser
        {
            get { return idUser; }
            set { idUser = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public Item(int idPass, int idUser, string title, string username, string password, int type, string url, string description, DateTime expires)
        {
            this.idPass = idPass;
            this.idUser = idUser;
            this.title = title;
            this.username = username;
            this.password = password;
            this.type = type;
            this.url = url;
            this.description = description;
            this.expires = expires;
        }
    }
}
