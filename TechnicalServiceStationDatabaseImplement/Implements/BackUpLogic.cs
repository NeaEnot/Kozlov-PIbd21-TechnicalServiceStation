using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection; 
using TechnicalServiceStationBusinessLogic.BusinessLogic;

namespace TechnicalServiceStationDatabaseImplement.Implements
{
    public class BackUpLogic : BackUpAbstractLogic
    {
        protected override Assembly GetAssembly()
        {
            return typeof(BackUpLogic).Assembly;
        }

        protected override List<PropertyInfo> GetFullList()
        {
            using (var context = new TechnicalServiceStationDatabase())
            {
                Type type = context.GetType();
                return type.GetProperties().Where(x => x.PropertyType.FullName.Contains("Order")).ToList();
            }
        }

        protected override List<T> GetList<T>()
        {
            using (var context = new TechnicalServiceStationDatabase())
            {
                return context.Set<T>().ToList();
            }
        }
    }
}