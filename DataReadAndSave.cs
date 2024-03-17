/*

This class is used as the interface to a database. The commands a database can perforn are divided into four categories:

Create
Read
Update
Delete

These four operations are often abbreviated as CRUD. 
The ReadDataFromDBAsync method is used to read data from the database. 
The SaveDataToDBAsync method is used to create, update, or delete data in the database.

This class implements the IDataAccess interface. The ReadDataFromDBAsync method uses the Dapper library to read data from the database. 
The SaveDataToDBAsync method uses the Dapper library to create, update, or delete data in the database.

A database can be accessed using a connection string. The connection string is a string that contains the information needed to connect to a database.
A reference for constructing a connection string for a MySQL database can be found at https://www.connectionstrings.com/mysql/. 

*/

using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataLibrary;

public class DataReadAndSave : IDataAccess 
{
    // Read
    public async Task<List<T>> ReadDataFromDBAsync<T, U>(string sql, U parameters, string connectionString)
    {
        using IDbConnection connection = new MySqlConnection(connectionString);

        var commandType = sql.EndsWith("SP") ? CommandType.StoredProcedure : CommandType.Text;

        var rows = await connection.QueryAsync<T>(sql, parameters, commandType: commandType);

        return rows.ToList();
    }

    // Create, Delete, and Update
    public async Task<int> SaveDataToDBAsync<T>(string sql, T parameters, string connectionString)
    {
        using IDbConnection connection = new MySqlConnection(connectionString);

        var commandType = sql.EndsWith("SP") ? CommandType.StoredProcedure : CommandType.Text;

        return await connection.ExecuteAsync(sql, parameters, commandType: commandType);
    }
}