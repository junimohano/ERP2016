using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Linq;

namespace Erp2016.Lib.Extensions
{
    /// <summary>
    /// Summary description for DataContextExtension
    /// </summary>
    public static class DataContextExtension
    {
        public static IEnumerable<Dictionary<string, object>> ExecuteQueryToDictionary(this DataContext dataContext, string query)
        {
            using (DbCommand command = dataContext.Connection.CreateCommand())
            {
                command.CommandText = query;
                dataContext.Connection.Open();

                using (DbDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        Dictionary<string, object> dictionary = new Dictionary<string, object>();

                        for (int i = 0; i < reader.FieldCount; i++)
                            dictionary.Add(reader.GetName(i), reader.GetValue(i));

                        yield return dictionary;
                    }
                }
            }
        }
    }
}