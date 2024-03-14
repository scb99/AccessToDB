using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataLibrary;

public class DataLoadAndSave : IDataAccess //
{
    // Read

    public async Task<List<T>> LoadDataAsync<T, U>(string sql, U parameters, string connectionString)
    {
        using IDbConnection connection = new MySqlConnection(connectionString);

        var commandType = sql.EndsWith("SP") ? CommandType.StoredProcedure : CommandType.Text;

        var rows = await connection.QueryAsync<T>(sql, parameters, commandType: commandType);

        return rows.ToList();

        //if (sql.EndsWith("SP")) Suggestion made by GitHub Copilot to replace the commented code below with the code above.
        //{
        //    // sql = stored procedure name
        //    var rowsFromSP = await connection.QueryAsync<T>(sql, parameters, commandType: CommandType.StoredProcedure);

        //    return rowsFromSP.ToList();
        //}

        //var rows = await connection.QueryAsync<T>(sql, parameters);

        //return rows.ToList();
    }

    // Create, Delete, and Update

    public async Task<int> SaveDataAsync<T>(string sql, T parameters, string connectionString)
    {
        using IDbConnection connection = new MySqlConnection(connectionString);

        var commandType = sql.EndsWith("SP") ? CommandType.StoredProcedure : CommandType.Text;

        return await connection.ExecuteAsync(sql, parameters, commandType: commandType);

        //if (sql.EndsWith("SP")) Suggestion made by GitHub Copilot to replace the commented code below with the code above.
        //{
        //{
        //    // sql = Stored Proceudre
        //    return await connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);
        //}

        //return await connection.ExecuteAsync(sql, parameters);
    }
}