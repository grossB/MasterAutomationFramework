using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RepositorySQL.RetryAssert
{
    class Sql
    {
        protected internal const int DefaultRetryCount = 15;

        const string connString = "";

        public static void Execute(string query, object element = null)
        {
            using (var connection = new SqlConnection(connString))
            {
                connection.Execute(query, element);
            }
        }

        public static int ExecuteScalar(string query, object element = null)
        {
            using (var connection = new SqlConnection(connString))
            {
                return connection.ExecuteScalar<int>(query, element);
            }
        }

        public static T ExecuteScalarStoredProcedure<T>(string storedProcName, object element = null)
        {
            using (var connection = new SqlConnection(connString))
            {
                var dynamicParameters = new DynamicParameters(element);
                dynamicParameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                var rowsAffected = connection.ExecuteScalar<int>(storedProcName, dynamicParameters, commandType: CommandType.StoredProcedure);

                var ret = dynamicParameters.Get<T>("ReturnValue");
                return ret;
            }
        }

        public static IEnumerable<T> Query<T>(string query, object element = null, int retryCount = DefaultRetryCount)
        {
            return Query(x => x.Query<T>(query, element), retryCount: retryCount);
        }

        public static IEnumerable<T> AssertMultiple<T>(IEnumerable<T> expectedResult, string query, object element = null, int retryCount = DefaultRetryCount, List<string> ignorePropertiesCompare = default)
        {
            return Query(x => x.Query<T>(query, element), x => Compare(expectedResult, x, ignorePropertiesCompare), retryCount);
        }

        public static T QuerySingle<T>(string query, object element = null, int retryCount = DefaultRetryCount)
        {
            return Query(x => x.QueryFirst<T>(query, element));
        }

        public static T AssertSingle<T>(T expectedResult, string query, object element = null, int retryCount = DefaultRetryCount, List<string> ignorePropertiesCompare = default)
        {
            return Query(x => x.QueryFirst<T>(query, element), x => Compare(expectedResult, x, ignorePropertiesCompare), retryCount);
        }

        public static T AssertSingleOrDefault<T>(T expectedResult, string query, object element = null, int retryCount = DefaultRetryCount, List<string> ignorePropertiesCompare = default)
        {
            return Query(x => x.QueryFirstOrDefault<T>(query, element), x => Compare(expectedResult, x, ignorePropertiesCompare), retryCount);
        }

        public static T AssertSingle<T>(Func<T, bool> expectedResult, string query, object element = null, int retryCount = DefaultRetryCount)
        {
            return Query(x => x.QueryFirst<T>(query, element), x => (expectedResult(x), string.Empty), retryCount);
        }

        private static T Query<T>(Func<SqlConnection, T> method, Func<T, (bool result, string message)> comparer = null, int retryCount = DefaultRetryCount)
        {
            return RetryAssert.Assert(() =>
            {
                using (var connection = new SqlConnection(connString))
                {
                    var queryResult = method(connection);
                    if (comparer != null)
                    {
                        var (comparisonResult, message) = comparer(queryResult);
                        Assert.IsTrue(comparisonResult, message);
                    }

                    return queryResult;
                }
            }, retryCount);
        }

        private static (bool, string) Compare<T>(T expectedResult, T queryResult, List<string> ignoredPropertiesCompare)
        {
            var compareLogic = new CompareLogic
            {
                Config = new ComparisonConfig
                {
                    MaxDifferences = 10,
                    MembersToIgnore = ignoredPropertiesCompare ?? new List<string>()
                }
            };
            var comparisonResult = compareLogic.Compare(expectedResult, queryResult);
            return !comparisonResult.AreEqual ? (false, comparisonResult.DifferencesString) : (true, string.Empty);
        }


    }
}
