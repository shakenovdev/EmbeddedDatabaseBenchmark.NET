using System;
using System.Data.Common;
using System.Data.SQLite;
using Foundation;
using Foundation.SQL;

namespace SQLite
{
    class Program
    {
        static void Main(string[] args)
        {
            var sqlite = new SQLiteUnit();
            var benchmark = new Benchmark<SQLDataUnit>(sqlite);
            benchmark.Execute();
            Console.ReadKey();
        }
    }

    class SQLiteUnit : SQLUnit
    {
        public SQLiteUnit() 
            : base("SQLite")
        {
        }

        protected override DbConnection CreateConnection()
        {
            return new SQLiteConnection("" + new SQLiteConnectionStringBuilder { DataSource = "database.db" });
        }

        protected override void CreateTestTable()
        {
            Command.CommandText = $"CREATE TABLE {Helper.TABLE_NAME} (Id integer primary key, Name nvarchar(255), Sum integer);";
            Command.ExecuteNonQuery();
        }
    }
}