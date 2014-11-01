using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Windows.Forms;
using PasswordKeeper.User;
using PasswordKeeper.Passwords;

namespace PasswordKeeper.DB
{
    class DBConnection
    {

        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public DBConnection()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "localhost";
            database = "password_keeper";
            uid = "root";
            password = "stralezelja";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server. Contact administrator.");
                        break;
                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again.");
                        break;
                }
                return false;
            }

        }

        private bool CloseConnection()
        {
            try
            {
                connection.Clone();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void AddUser(User.User user)
        {
            string query = "INSERT INTO USER (username, password) VALUES ('" + user.Username + "', '" + user.Password + "')";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public User.User GetUser(string username)
        {
            string query = "SELECT id, username, password FROM USER WHERE username='" + username + "'";

            User.User newUser = null;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    data.Read();
                    newUser = new User.User(data.GetInt32(0), data.GetString(1), data.GetString(2));
                    return newUser;
                }
                data.Close();
                this.CloseConnection();
            }
            return null;
        }

        public List<string> GetAllTypes()
        {
            List<string> allTypes = new List<string>();

            string query = "SELECT name FROM UNIT_TYPE";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        allTypes.Add(data.GetString(0));
                    }
                }
                data.Close();
                this.CloseConnection();
            }

            return allTypes;
        }

        public void InsertItem(Item item)
        {            
            if (this.OpenConnection() == true)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO PASS_UNIT(title, username, password, url, description, expires, UNIT_TYPE_id, USER_id) VALUES(@title, @username, @password, @url, @description, @expires, @UNIT_TYPE_id, @USER_id)";
                command.Parameters.AddWithValue("@title", item.Title);
                command.Parameters.AddWithValue("@username", item.Username);
                command.Parameters.AddWithValue("@password", item.Password);
                command.Parameters.AddWithValue("@url", item.Url);
                command.Parameters.AddWithValue("@description", item.Description);
                command.Parameters.AddWithValue("@expires", item.Expires.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@UNIT_TYPE_id", item.Type);
                command.Parameters.AddWithValue("@USER_id", item.Id);
                command.ExecuteNonQuery();
            }
            this.CloseConnection();
        }


        public List<Item> SelectItems(int userId, int unitTypeId)
        {
            string query = @"SELECT title, username, password, url, description, DATE_FORMAT(expires, '%d-%m-%Y') AS expires, UNIT_TYPE_id, USER_id FROM pass_unit WHERE USER_id=" + userId + " AND UNIT_TYPE_id=" + unitTypeId;

            List<Item> list = new List<Item>();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        string title = data.GetString(0);
                        string username = data.GetString(1);
                        string password = data.GetString(2);
                        int type = data.GetInt16(6);
                        string url = data.GetString(3);
                        string description = data.GetString(4);
                        DateTime expires = data.GetDateTime(5);
                        list.Add(new Item(0, title, username, password, type, url, description, expires));
                    }
                }
                data.Close();
                this.CloseConnection();
                return list;
            }
            else
            {
                return list;
            }

        }
    }
}
