using System;
using System.Linq;
using Foundation;
using Foundation.Core;
using Foundation.SQL;
using Realms;

namespace RealmUnit
{
    class Program
    {
        static void Main(string[] args)
        {
            var realm = new RealmUnit();
            var benchmark = new Benchmark<RealmDataUnit>(realm);
            benchmark.Execute();
            Console.ReadKey();
        }
    }

    class RealmUnit : IDataBaseUnit<RealmDataUnit>
    {
        private readonly Realm _database;
        private Transaction _transaction;

        public RealmUnit()
        {
            Name = "Realm";
            _database = Realm.GetInstance();

            BeginTransaction();
            _database.RemoveAll();
            CommitTransaction();
        }

        public string Name { get; }

        public void CloseConnection()
        {
            _database.Dispose();
        }

        public void BeginTransaction()
        {
            _transaction = _database.BeginWrite();
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
        }

        public void Create(RealmDataUnit dataUnit)
        {
            _database.Add(dataUnit);
        }

        public void Delete(int id)
        {
            var row = _database.Find<RealmDataUnit>(id);
            _database.Remove(row);
        }

        public void Read(int id)
        {
            _database.Find<RealmDataUnit>(id);
        }

        public void Update(RealmDataUnit dataUnit)
        {
            _database.Add(dataUnit, update: true);
        }

        public RealmDataUnit[] ConvertData(SQLDataUnit[] dataUnits)
        {
            return dataUnits.Select(x => new RealmDataUnit()
            {
                Id = x.Id,
                Name = x.Name,
                Sum = x.Sum
            }).ToArray();
        }

        public RealmDataUnit[] ShuffleAndUpdateData(RealmDataUnit[] dataUnits)
        {
            Helper.Shuffle(dataUnits);
            var result = dataUnits.Select(x => x.ToStandaloneWithUpdatedName()).ToArray();
            return result;
        }
    }
}
