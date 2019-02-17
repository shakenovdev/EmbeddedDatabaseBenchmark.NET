using System;
using Foundation;
using Foundation.Core;
using Foundation.SQL;
using LiteDB;

namespace LiteDBunit
{
    class Program
    {
        static void Main(string[] args)
        {
            var litedb = new LiteDBUnit();
            var benchmark = new Benchmark<CoreDataUnit>(litedb);
            benchmark.Execute();
            Console.ReadKey();
        }
    }

    class LiteDBUnit : IDataBaseUnit<CoreDataUnit>
    {
        private LiteDatabase _database;
        private LiteCollection<CoreDataUnit> _collection;

        public LiteDBUnit()
        {
            Name = "LiteDB";

            OpenConnection();
            DropTable();
            CreateTestTable();
        }

        public string Name { get; }

        public void OpenConnection()
        {
            _database = new LiteDatabase("database.db");
        }

        public void CloseConnection()
        {
            _database.Dispose();
        }
        
        public void Create(CoreDataUnit dataUnit)
        {
            _collection.Insert(dataUnit);
        }

        public void Read(int id)
        {
            _collection.FindById(id);
        }

        public void Update(CoreDataUnit dataUnit)
        {
            _collection.Update(dataUnit);
        }

        public void Delete(int id)
        {
            _collection.Delete(id);
        }

        public CoreDataUnit[] ConvertData(SQLDataUnit[] dataUnits)
        {
            return Array.ConvertAll(dataUnits, x => (CoreDataUnit) x);
        }

        public CoreDataUnit[] ShuffleAndUpdateData(CoreDataUnit[] dataUnits)
        {
            Helper.Shuffle(dataUnits);
            foreach (var dataUnit in dataUnits)
            {
                dataUnit.UpdateName();
            }

            return dataUnits;
        }

        private void DropTable()
        {
            if (_database.CollectionExists(Helper.TABLE_NAME))
            {
                _database.DropCollection(Helper.TABLE_NAME);
            }
        }

        private void CreateTestTable()
        {
            _collection = _database.GetCollection<CoreDataUnit>(Helper.TABLE_NAME);
        }

        // transactions are not supported
        public void BeginTransaction()
        {
        }

        public void CommitTransaction()
        {
        }
    }
}
