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
            password = "123456";
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
                command.Parameters.AddWithValue("@USER_id", item.IdUser);
                command.ExecuteNonQuery();
            }
            this.CloseConnection();
        }


        public List<Item> SelectItems(int userId, int unitTypeId)
        {
            string query = "SELECT id, title, username, password, url, description, expires, UNIT_TYPE_id, USER_id FROM pass_unit WHERE USER_id=" + userId + " AND UNIT_TYPE_id=" + unitTypeId;

            List<Item> list = new List<Item>();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        int id = data.GetInt32(0);
                        string title = data.GetString(1);
                        string username = data.GetString(2);
                        string password = data.GetString(3);
                        int type = data.GetInt32(7);
                        string url = data.GetString(4);
                        string description = data.GetString(5);
                        DateTime expires = data.GetDateTime(6);
                        int idUser = data.GetInt32(8);
                        list.Add(new Item(id, idUser, title, username, password, type, url, description, expires));
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

        public Item SelectItem(int itemId)
        {
            string query = "SELECT * FROM pass_unit WHERE id=" + itemId;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    data.Read();
                    int id = data.GetInt32(0);
                    string title = data.GetString(1);
                    string username = data.GetString(2);
                    string password = data.GetString(3);
                    int type = data.GetInt32(7);
                    string url = data.GetString(4);
                    string description = data.GetString(5);
                    DateTime expires = data.GetDateTime(6);
                    int idUser = data.GetInt32(8);
                    return new Item(id, idUser, title, username, password, type, url, description, expires);
                }
            }
            return null;
        }

        public void UpdateItem(Item item)
        {
            string query = "UPDATE pass_unit SET title='" + item.Title + "', username='" + item.Username + "', password='" + item.Password + "', url='" + item.Url + "', description='" + item.Description + "', expires='" + item.Expires.ToString("yyyy-MM-dd HH:mm:ss") + "', UNIT_TYPE_id='" + item.Type + "', USER_ID='" + item.IdUser + "' WHERE id='" + item.IdPass + "'";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void RemoveItem(int itemId)
        {
            string query = "DELETE FROM pass_unit WHERE id='" + itemId + "'";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
    }
}
