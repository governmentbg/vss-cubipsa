using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using Legalacts.Model.Entities;
using Legalacts.Model.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Legalacts.Model.Repositories
{
    #region Interface

    public interface ILegalactsRepository : IRepository
    {
        #region Act

        Act CreateAct();
        Act GetActById(int id);
        Act GetActByUID(string uid);
        Act GetActByEcli(string ecli);
        void RemoveActCascading(Act act);
        IQueryable<Act> GetAllActs();
        List<Act> GetActsByKeywords(string keywords, string query, List<SqlParameter> sqlParameters, bool isLuceneActivated, bool isLastCondition);
        DeletedAct CreateDeletedAct();

        #endregion

        #region ConnectedCases

        ConnectedCase CreateConnectedCase();

        #endregion

        #region HigherCourts

        HigherCourt CreateHigherCourt();

        #endregion

        #region Logs

        Log CreateLog();
        IQueryable<Log> GetAllLogs();

        #endregion
    }

    #endregion

    #region Class

    public class LegalactsRepository : RepositoryBase, ILegalactsRepository
    {
        #region Act

        public Act CreateAct()
        {
            Act act = new Act();
            DataContext.Acts.Add(act);

            return act;
        }

        public Act GetActById(int id)
        {
            return DataContext.Acts.FirstOrDefault(e => e.ActId == id);
        }

        public Act GetActByUID(string uid)
        {
            return DataContext.Acts.FirstOrDefault(e => e.UID.Equals(uid));
        }

        public Act GetActByEcli(string ecli)
        {
            return DataContext.Acts.FirstOrDefault(e => e.EcliCode.Equals(ecli) || e.PreviousEcliCode.Equals(ecli));
        }

        public void RemoveActCascading(Act act)
        {
            if (act.HigherCourt != null)
            {
                DataContext.HigherCourts.Remove(act.HigherCourt);
            }

            if (act.ConnectedCases != null)
            {
                foreach (var cc in act.ConnectedCases.ToList())
                {
                    DataContext.ConnectedCases.Remove(cc);
                }
            }

            if (act.ConnectedActs != null)
            {
                act.ConnectedActs.Clear();
            }

            var connectedActs = DataContext.Acts.Where(a => a.ConnectedActs.Select(c => c.ActId).Contains(act.ActId)).ToList();

            foreach (var connectedAct in connectedActs)
            {
                if (connectedAct.ConnectedActs != null)
                {
                    connectedAct.ConnectedActs.Clear();
                }
            }

            // delete act document
            if(act.ActDocumentId != null)
            {
                var actDocument = DataContext.ActDocuments.Single(e => e.ActDocumentId == act.ActDocumentId);
                DataContext.ActDocuments.Remove(actDocument);
            }

            // delete motive document
            if (act.MotiveDocumentId != null)
            {
                var motiveDocument = DataContext.MotiveDocuments.Single(e => e.MotiveDocumentId == act.MotiveDocumentId);
                DataContext.MotiveDocuments.Remove(motiveDocument);
            }

            DataContext.Acts.Remove(act);
        }

        public IQueryable<Act> GetAllActs()
        {
            return DataContext.Acts;
        }

        public List<Act> GetActsByKeywords(string keywords, string query, List<SqlParameter> sqlParameters, bool isLuceneActivated, bool isLastCondition)
        {
            if (string.IsNullOrWhiteSpace(keywords))
                throw new ArgumentNullException("Input must not be null.");

            var aliasStartIndex = query.IndexOf("[Acts] AS") + 10;
            var alias = query.Substring(aliasStartIndex, query.IndexOf(']', aliasStartIndex) - aliasStartIndex + 1).Replace(System.Environment.NewLine, string.Empty);

            string FTS_CONTAINS_QUERY = @"CONTAINS([dbo].[ActDocuments].[Content], @keywords)";
            string FTS_INNER_JOIN_QUERY = @"INNER JOIN [dbo].[ActDocuments] ON " + alias + ".[ActDocumentId] = [dbo].[ActDocuments].[ActDocumentId] WHERE";

            string dbQuery = isLastCondition ?
                                query + " " + FTS_INNER_JOIN_QUERY + " " + FTS_CONTAINS_QUERY :
                                query.Replace("WHERE", FTS_INNER_JOIN_QUERY) + " AND " + FTS_CONTAINS_QUERY;
            try
            {
                //bool isQuoted = keywords.IsQuoted();

                string luceneFilteredQuery = isLuceneActivated ? Lucene.LuceneFilter.Apply(keywords) : keywords;

                ParsedQueryInfo parsedQueryInfo = (isLuceneActivated) ?
                                    SearchQueryParser.ParseLuceneQuotedQuery(luceneFilteredQuery.Quote()) :
                                    SearchQueryParser.ParseQuery(luceneFilteredQuery);

                //ParsedQueryInfo parsedQueryInfo = (isQuoted && isLuceneActivated) ? 
                //                    SearchQueryParser.ParseLuceneQuotedQuery(luceneFilteredQuery.Quote()) : 
                //                    SearchQueryParser.ParseQuery(luceneFilteredQuery);

                if (parsedQueryInfo.IsValid)
                {
                    var ftsSqlParameters = new List<SqlParameter>();

                    foreach (var sqlParam in sqlParameters)
                    {
                        ftsSqlParameters.Add(new SqlParameter(sqlParam.ParameterName, sqlParam.Value));
                    }

                    ftsSqlParameters.Add(new SqlParameter("@keywords", parsedQueryInfo.Body));

                    var sqlQuery = DataContext.Database.SqlQuery<Act>(dbQuery, ftsSqlParameters.ToArray());

                    var actIds = sqlQuery.Select(e => e.ActId).ToList();

                    return DataContext.Acts.Where(e => actIds.Contains(e.ActId)).ToList();
                }
                else
                {
                    throw new ValidationException("Parsed query not valid.");
                }
            }
            catch
            {
                var sqlQuery = DataContext.Database.SqlQuery<Act>(query, sqlParameters.ToArray());

                var actIds = sqlQuery.Select(e => e.ActId).ToList();

                return DataContext.Acts.Where(e => actIds.Contains(e.ActId)).ToList();
            }
        }

        public DeletedAct CreateDeletedAct()
        {
            DeletedAct deletedAct = new DeletedAct();
            DataContext.DeletedActs.Add(deletedAct);

            return deletedAct;
        }

        #endregion

        #region ConnectedCases

        public ConnectedCase CreateConnectedCase()
        {
            ConnectedCase connectedCase = new ConnectedCase();
            DataContext.ConnectedCases.Add(connectedCase);

            return connectedCase;
        }

        #endregion

        #region HigherCourts

        public HigherCourt CreateHigherCourt()
        {
            HigherCourt higherCourt = new HigherCourt();
            DataContext.HigherCourts.Add(higherCourt);

            return higherCourt;
        }

        #endregion

        #region Log

        public Log CreateLog()
        {
            Log log = new Log();
            DataContext.Logs.Add(log);

            return log;
        }

        public IQueryable<Log> GetAllLogs()
        {
            return DataContext.Logs;
        }

        #endregion

    }

    #endregion
}
