using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary;

public interface IDataAccess //
{
    // Read

    Task<List<T>> LoadDataAsync<T, U>(string sql, U parameters, string connectionString);


    // Create, Delete, and Update

    Task<int> SaveDataAsync<T>(string sql, T parameters, string connectionString);
}