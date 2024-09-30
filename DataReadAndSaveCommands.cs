﻿/*

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

We use the convention of naming stored procedures with the suffix "SP" to indicate that they are stored procedures.

*/

using DataAbstractions.Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataLibrary;

public class DataReadAndSaveCommands(IDataAccessor dataAccessor) : IDataAccess 
{
    private readonly IDataAccessor _dataAccessor = dataAccessor;

    // Read
    public async Task<List<T>> ReadDataFromDBAsync<T, U>(string sql, U parameters, string connectionString)
    {
        try
        {
            _dataAccessor.Open();

            var commandType = sql.EndsWith("SP") ? CommandType.StoredProcedure : CommandType.Text;

            var rows = await _dataAccessor.QueryAsync<T>(sql, parameters, commandType: commandType);

            return rows.ToList();
        }
        finally
        {
            _dataAccessor.Close();
        }
    }

    // Create, Delete, and Update
    public async Task<int> SaveDataToDBAsync<T>(string sql, T parameters, string connectionString)
    {
        try
        {
            _dataAccessor.Open();

            var commandType = sql.EndsWith("SP") ? CommandType.StoredProcedure : CommandType.Text;

            return await _dataAccessor.ExecuteAsync(sql, parameters, commandType: commandType);
        }
        finally
        {
            _dataAccessor.Close();
        }
    }
}