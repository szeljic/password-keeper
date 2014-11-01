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
        private int id, type;
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

        public int Id
        {
            get { return id; }
            set { id = value; }
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

        public Item(int id, string title, string username, string password, int type, string url, string description, DateTime expires)
        {
            this.id = id;
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
