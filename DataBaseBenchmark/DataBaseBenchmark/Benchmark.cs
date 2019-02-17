using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Foundation.Core;
using Foundation.SQL;

namespace Foundation
{
    public class Benchmark<T> where T : IDataUnit
    {
        private readonly IDataBaseUnit<T> _dataBaseUnit;

        public Benchmark(IDataBaseUnit<T> dataBaseUnit)
        {
            _dataBaseUnit = dataBaseUnit;
        }

        public void Execute()
        {
            var iterations = new[] { 100, 1_000, 10_000, 100_000 };

            foreach (var count in iterations)
            {
                var data = GenerateData(count);
                var properData = _dataBaseUnit.ConvertData(data);

                OperateByModel(properData, _dataBaseUnit.Create, "CREATE", count);
                properData = _dataBaseUnit.ShuffleAndUpdateData(properData);
                var ids = data.Select(x => x.Id).ToArray();
                OperateById(ids, _dataBaseUnit.Read, "READ", count);
                OperateByModel(properData, _dataBaseUnit.Update, "UPDATE", count);
                OperateById(ids, _dataBaseUnit.Delete, "DELETE", count);
            }

            Finish();
        }

        private static SQLDataUnit[] GenerateData(int count)
        {
            return Enumerable.Range(1, count)
                .Select(x => new SQLDataUnit()
                {
                    Id = x,
                    Name = Helper.RandomString(10),
                    Sum = Helper.RandomInteger(100_000)
                }).ToArray();
        }

        private void OperateByModel(T[] dataUnits, Action<T> operation, string operationName, int currentIteration)
        {
            void MultipleOperation()
            {
                foreach (var dataUnit in dataUnits)
                {
                    operation(dataUnit);
                }
            }
            
            Operate(MultipleOperation, operationName, currentIteration);
        }

        private void OperateById(int[] ids, Action<int> operation, string operationName, int currentIteration)
        {
            void MultipleOperation()
            {
                foreach (var id in ids)
                {
                    operation(id);
                }
            }

            Operate(MultipleOperation, operationName, currentIteration);
        }

        private void Operate(Action multipleOperation, string operationName, int currentIteration)
        {
            var timer = Stopwatch.StartNew();

            _dataBaseUnit.BeginTransaction();
            multipleOperation();
            _dataBaseUnit.CommitTransaction();

            Console.WriteLine($"{_dataBaseUnit.Name} | {currentIteration} | {operationName} | {timer.ElapsedMilliseconds} ms");
        }

        private void Finish()
        {
            Console.WriteLine("Benchmark is done!");
            _dataBaseUnit.CloseConnection();
        }
    }
}