using PasswordKeeper.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper.Type
{
    class UnitTypeDAOImpl : UnitTypeDAO
    {
        public UnitType GetAllTypes()
        {
            DBConnection connection = new DBConnection();
            UnitType unitType = new UnitType();
            unitType.AllTypes = connection.GetAllTypes();
            return unitType;
        }
    }
}
