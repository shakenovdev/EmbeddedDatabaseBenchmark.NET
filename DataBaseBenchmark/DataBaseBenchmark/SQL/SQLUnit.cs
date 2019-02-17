using System.Data.Common;
using System.Linq;
using Foundation.Core;

namespace Foundation.SQL
{
    public abstract class SQLUnit : IDataBaseUnit<SQLDataUnit>
    {
        protected readonly DbConnection Connection;
        protected DbCommand Command;
        protected DbTransaction Transaction;

        public string Name { get; }

        protected abstract DbConnection CreateConnection();
        protected abstract void CreateTestTable();

        protected SQLUnit(string name)
        {
            Name = name;
            Connection = CreateConnection();
            OpenConnection();
            BeginTransaction();
            DropTable();
            CreateTestTable();
            CommitTransaction();
        }

        public void CloseConnection()
        {
            Connection.Close();
            Transaction.Dispose();
            Command.Dispose();
            Connection.Dispose();
        }

        public void BeginTransaction()
        {
            Transaction = Connection.BeginTransaction();
            Command.Transaction = Transaction;
        }

        public void CommitTransaction()
        {
            Transaction.Commit();
        }

        public void Create(SQLDataUnit dataUnit)
        {
            Command.CommandText = $"INSERT INTO {Helper.TABLE_NAME} (Name, Sum) values ('{dataUnit.Name}', {dataUnit.Sum});";
            Command.ExecuteNonQuery();
        }

        public void Read(int id)
        {
            Command.CommandText = $"SELECT * FROM {Helper.TABLE_NAME} WHERE Id = {id}";
            Command.ExecuteNonQuery();
        }

        public void Update(SQLDataUnit dataUnit)
        {
            Command.CommandText = $"UPDATE {Helper.TABLE_NAME} SET Name = '{dataUnit.Name}' WHERE Id = {dataUnit.Id}";
            Command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            Command.CommandText = $"DELETE FROM {Helper.TABLE_NAME} WHERE Id = {id}";
            Command.ExecuteNonQuery();
        }

        public SQLDataUnit[] ConvertData(SQLDataUnit[] dataUnits)
        {
            return dataUnits;
        }

        public SQLDataUnit[] ShuffleAndUpdateData(SQLDataUnit[] dataUnits)
        {
            Helper.Shuffle(dataUnits);

            foreach (var dataUnit in dataUnits)
            {
                dataUnit.UpdateName();
            }
            
            return dataUnits;
        }

        private void OpenConnection()
        {
            Connection.Open();
            Command = Connection.CreateCommand();
        }

        private void DropTable()
        {
            Command.CommandText = $"DROP TABLE IF EXISTS {Helper.TABLE_NAME}";
            Command.ExecuteNonQuery();
        }
    }
}