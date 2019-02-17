using System;
using System.Data.Common;
using System.Data.SqlClient;
using Foundation;
using Foundation.Core;
using Foundation.SQL;

namespace SQLLocalDB
{
    class Program
    {
        static void Main(string[] args)
        {
            var localdb = new SQLLocalDBUnit();
            var benchmark = new Benchmark<SQLDataUnit>(localdb);
            benchmark.Execute();
            Console.ReadKey();
        }
    }

    class SQLLocalDBUnit : SQLUnit
    {
        public SQLLocalDBUnit() 
            : base("SQL Server LocalDB")
        {
        }

        protected override DbConnection CreateConnection()
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                IntegratedSecurity = true
            };
            
            return new SqlConnection(connectionStringBuilder.ConnectionString);
        }

        protected override void CreateTestTable()
        {
            Command.CommandText = $"CREATE TABLE {Helper.TABLE_NAME} (Id int identity not null primary key, Name nvarchar(255), Sum int);";
            Command.ExecuteNonQuery();
        }
    }
}