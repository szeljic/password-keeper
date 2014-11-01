using PasswordKeeper.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper.Passwords
{
    class ItemDAOImpl : ItemDAO
    {
        public void AddItem(Item item)
        {
            DBConnection connection = new DBConnection();
            connection.InsertItem(item);
        }

        public List<Item> SelectItemsForType(int userId, int unitTypeId)
        {
            DBConnection connection = new DBConnection();
            return connection.SelectItems(userId, unitTypeId);
        }
    }
}
