## Shallow comparative analysis of CRUD operations performance among SQL/NoSQL Embedded databases in .NET Core

[SQLite](https://www.sqlite.org/index.html) / [SQL Server Express LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15) / [Realm .NET](https://realm.io/docs/dotnet/latest/) / [LiteDB](https://www.litedb.org)

Versions of compared database that have been used in analysis can be found in source code.

The analysis shows the execution time to perform N operations (Create/Update/Read/Delete) using same entity type.

Databases` settings are not modified after installation, no performance adjusments were made.

For time measurements tool `System.Diagnostic.Stopwatch` was chosen. The time unit is `ms`. Timer starts when transaction opens and ends once transaction is closed.

Entity type:
```
public class DataUnit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Sum { get; set; }
}
```

In order to complete the analysis, queries are executed in separate iterations of *1_000*, *10_000* and *100_000* operations.
***
| N=1\_000           | CREATE | READ | UPDATE | DELETE |
|--------------------|--------|------|--------|--------|
| SQLite             | 16     | 10   | 20     | 19     |
| SQL Server LocalDB | 122    | 139  | 159    | 142    |
| Realm              | 14     | 19   | 12     | 25     |
| LiteDB             | 822    | 26   | 529    | 813    |
***
| N=10\_000          | CREATE | READ | UPDATE | DELETE |
|--------------------|--------|------|--------|--------|
| SQLite             | 75     | 108  | 94     | 76     |
| SQL Server LocalDB | 1257   | 1645 | 1662   | 1199   |
| Realm              | 210    | 93   | 130    | 1159   |
| LiteDB             | 8864   | 300  | 10653  | 11298  |
***
| N=100\_000         | CREATE | READ  | UPDATE | DELETE |
|--------------------|--------|-------|--------|--------|
| SQLite             | 966    | 1610  | 1214   | 1090   |
| SQL Server LocalDB | 14545  | 15903 | 15872  | 12738  |
| Realm              | 1242   | 554   | 738    | 321127 |
| LiteDB             | 106493 | 3506  | 118285 | 119198 |
***

The comparative analysis doesn't pretend to exalt one over others, it represents what you can expect from database performance in most simple ways. The database effiency can be different in various situation with various data. The conclusion what to choose is up to you.