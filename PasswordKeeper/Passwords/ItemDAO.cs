﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper.Passwords
{
    interface ItemDAO
    {
        void AddItem(Item item);
        List<Item> SelectItemsForType(int userId, int unitTypeId);
        Item SelectItem(int itemId);
        void UpdateItem(Item item);
        void RemoveItem(int itemId);
    }
}
