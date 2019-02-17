using Foundation.SQL;

namespace Foundation.Core
{
    public interface IDataBaseUnit<T> where T : IDataUnit
    {
        string Name { get; }

        void CloseConnection();
        void BeginTransaction();
        void CommitTransaction();

        void Create(T dataUnit);
        void Read(int id);
        void Update(T dataUnit);
        void Delete(int id);

        T[] ConvertData(SQLDataUnit[] dataUnits);
        T[] ShuffleAndUpdateData(T[] dataUnits);
    }
}