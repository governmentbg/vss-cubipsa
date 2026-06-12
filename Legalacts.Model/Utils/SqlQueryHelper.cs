using System;
using System.Collections.Generic;
using System.Linq;

namespace Legalacts.Model.Utils
{
    public static class SqlQueryHelper
    {
        public static string AddIn(string query, string columnName, bool isFirstCondition, IEnumerable<int> elements)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentException("Query must not be empty.");

            if (string.IsNullOrWhiteSpace(columnName))
                throw new ArgumentException("Column name must not be empty.");

            if (elements == null || elements.Count() == 0)
                throw new ArgumentException("You must specifiy at least one IN parameter.");

            string newQuery;

            if (isFirstCondition)
                newQuery = string.Format("{0} WHERE {1} IN ({2})", query, columnName, string.Join(", ", elements));
            else
                newQuery = string.Format("{0} AND {1} IN ({2})", query, columnName, string.Join(", ", elements));

            return newQuery;
        }

        public static string InsertIn(string query, string columnName, bool isFirstCondition, IEnumerable<int> elements) 
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentException("Query must not be empty.");

            if (string.IsNullOrWhiteSpace(columnName))
                throw new ArgumentException("Column name must not be empty.");

            if (elements == null || elements.Count() == 0)
                throw new ArgumentException("You must specifiy at least one IN parameter.");

            string newQuery;

            if (isFirstCondition)
            {
                int whereIndex = query.IndexOf("WHERE", StringComparison.InvariantCultureIgnoreCase);

                int inClauseIndex = whereIndex + 5;

                string inClause = string.Format(" {0} IN ({1}) AND ", columnName, string.Join(", ", elements));

                newQuery = query.Insert(inClauseIndex, inClause);
            }
            else
                newQuery = string.Format("{0} AND {1} IN ({2})", query, columnName, string.Join(", ", elements));

            return newQuery;
        }
    }
}
