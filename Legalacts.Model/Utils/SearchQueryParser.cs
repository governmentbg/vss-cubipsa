using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Legalacts.Model.Utils
{
    /// <summary>
    /// Конвертира въведен от потребителя текст във вид подходящ за търсене от SQL Server. 
    /// Ако потребителят е посочил търсене с ключови думи, се изпълнява метода ParseLuceneQuotedQuery, който взима предвид извлечените от Lucene думи.
    /// В противен случай, независимо от това дали са въведени или не кавички в търсенето, се търсят документи които съдържат точно зададената фраза.
    /// </summary>
    public static class SearchQueryParser
    {
        private static char DoubleQuotation = '"';
        private static char SingleQuotation = '\'';
        private static char StarSuffix = '*';

        private const int DefaultNearClauseDistance = 3;

        /// <summary>
        /// Конвертира текст във вид, подходящ за търсене от SQL Server
        /// </summary>
        /// <param name="query">Въведеният текст</param>
        /// <returns>Преработен текст за търсене</returns>
        public static ParsedQueryInfo ParseQuery(string query)
        {
            SearchQueryType type = GetQueryType(query);

            string parsedQuery = ParseQuery(query, type);

            return new ParsedQueryInfo
            {
                Body = parsedQuery,
                IsValid = (type != SearchQueryType.NoiseOrEmpty)
            };
        }

        /// <summary>
        /// Метод за търсене с производни думи (корени на думи, извлeчени с Lucene)
        /// </summary>
        /// <param name="query">Филтриран текст от Lucene</param>
        /// <param name="nearDistance">Максимално сумарно разстояние между думите в текста</param>
        /// <returns>Обект съдържащ конвертирания резултат и флаг дали заявката е валидна</returns>
        public static ParsedQueryInfo ParseLuceneQuotedQuery(string query, int nearDistance = DefaultNearClauseDistance)
        {
            SearchQueryType type = GetQueryType(query);

            if (type != SearchQueryType.Quoted)
                return new ParsedQueryInfo
                {
                    Body = ParseQuery(query, type),
                    IsValid = type != SearchQueryType.NoiseOrEmpty
                };

            string[] words = query.Trim(StringExtensions.QuoteSymbols).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string queryBody;

            if (words.Length == 1)
            {
                queryBody = ParseSingleWordQuery(words[0]);
            }
            else
            {
                var parsedSegments = words.Select(w => ParseSingleWordQuery(w));
                string nearClause = string.Join(", ", parsedSegments);
                queryBody = string.Format("NEAR(({0}), {1}, TRUE)", nearClause, nearDistance);
                //queryBody = string.Join(" NEAR ", parsedSegments);
            }

            return new ParsedQueryInfo
            {
                Body = queryBody,
                IsValid = true
            };
        }

        private static string ParseQuery(string query, SearchQueryType queryType)
        {
            string result;

            switch (queryType)
            {
                //case SearchQueryType.SingleWordSuffix:
                //    result = ParseSingleWordQuery(query);
                //    break;
                //case SearchQueryType.ManyWordsSuffix:
                //    result = ParseManyWordsWithSuffixQuery(query);
                //    break;
                case SearchQueryType.Quoted:
                    result = ParseExactPhrase(query);
                    break;
                case SearchQueryType.NoiseOrEmpty:
                    result = query;
                    break;
                default:
                    throw new InvalidOperationException("Unknown query type.");
            }

            return result;
        }

        private static SearchQueryType GetQueryType(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return SearchQueryType.NoiseOrEmpty;

            string[] elements = query.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (elements.Length == 0 || elements.All(el => el.All(c => Char.IsPunctuation(c) || Char.IsSeparator(c) || Char.IsSymbol(c))))
                return SearchQueryType.NoiseOrEmpty;

            //if (query.IsQuoted())
                return SearchQueryType.Quoted;

            //return elements.Length == 1 ? SearchQueryType.SingleWordSuffix : SearchQueryType.ManyWordsSuffix;
        }

        private static string ParseSingleWordQuery(string query, bool includeStarSuffix = true)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(DoubleQuotation);

            foreach (char c in query)
            {
                if (c == DoubleQuotation || c == SingleQuotation)
                    builder.Append(c, 2); // insert two times to escape quotes in query
                else
                    builder.Append(c);
            }

            if (includeStarSuffix)
                builder.Append(StarSuffix); // for exact phrase no * is needed and includeStarSuffix is false

            builder.Append(DoubleQuotation);

            return builder.ToString();
        }

        private static string ParseManyWordsWithSuffixQuery(string query)
        {
            string[] segments = query.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            List<string> transformedSegments = new List<string>(segments.Length);

            foreach (string seg in segments.Distinct())
            {
                string transformedWord = ParseSingleWordQuery(seg); // the result is many words with * separated by AND so the single word method is reused
                transformedSegments.Add(transformedWord);
            }

            string parsedQuery = string.Join(" AND ", transformedSegments);
            return parsedQuery;
        }

        private static string ParseExactPhrase(string query)
        {
            string strippedOfQuotes = query.Trim(StringExtensions.QuoteSymbols); // ParseSingleWordQuery uses quotes so these are stripped

            return ParseSingleWordQuery(strippedOfQuotes, includeStarSuffix: false);
        }
    }
}
