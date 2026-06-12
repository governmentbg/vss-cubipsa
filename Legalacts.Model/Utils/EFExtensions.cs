using Legalacts.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Legalacts.Model.Utils
{
    public static class EFExtensions
    {
        public static List<SqlParameter> ExtractSqlParameters<T>(this IQueryable<T> acts)
        {
            var internalQueryField = acts.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(f => f.Name.Equals("_internalQuery")).FirstOrDefault();

            var internalQuery = internalQueryField.GetValue(acts);

            var objectQueryField = internalQuery.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(f => f.Name.Equals("_objectQuery")).FirstOrDefault();

            var objectQuery = objectQueryField.GetValue(internalQuery) as ObjectQuery<T>;

            objectQuery.ToTraceString();

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            foreach (var parameter in objectQuery.Parameters)
            {
                SqlParameter sp = 
                    new SqlParameter(parameter.Name.Contains("@") ? 
                        parameter.Name : "@" + parameter.Name, parameter.Value);

                sqlParameters.Add(sp);
            }

            return sqlParameters;
        }
    }
}
