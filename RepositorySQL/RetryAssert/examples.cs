using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositorySQL.RetryAssert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorySQL.RetryAssert
{
    class examples
    {
        public static void AssertFaultDateTime(int aaa, DateTime faultTimeCreation, Fault fault)
        {
            var ignoredFields = new List<string>()
            {
                "WarningDateTime",
                "FaultOutsideEndLocation",
            };

            Sql.AssertSingle(fault,
                $@"SELECT  * FROM (
                        SELECT *  FROM [Table]) as innerTable
                WHERE aaaa = @@aaa order by [bbb] desc",
                new { aaa = aaa, priorit = fault.Priority}, retryCount: 20, ignoredFields);
        }

        public static int QuerySingle(int aaa, Fault fault)
        {
            return Sql.QuerySingle<int>(
                $@"SELECT COUNT(*) FROM [Table] where [aaa] = @aaa and
                            [Priority] = @priority and
                            [Location] = @location",
                new { aaa = aaa, priority = fault.Priority }, retryCount: 20);
        }

        public static int AfterFaultTest(int param1, int param2 = 0)
        {
            return Sql.ExecuteScalarStoredProcedure<int>("[ODL].[Stored_Procedure]", new { param1, param2 });
        }

        public static void ExampleOfQuertyBy(int param1 )
        {
            var dbCore = (Sql.Query<dynamic>($"select * from [Table.Name] where paramName = @aaa; ",
                new { aaa = param1 })
                as IEnumerable<object>).ToList();

            Assert.AreEqual(1, dbCore.Count);

            var dbCore0 = dbCore[0] as IDictionary<string, object>;
        }
    }

    public class Fault
    {
        public int Priority { get; set; }
        public int MyProperty2 { get; set; }

    }
}
