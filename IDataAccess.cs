/*

In this file, we define the interface for the DataAccess class. 
This interface will be used to define the methods that will be used to interact with the database. 
The methods will be used to read, create, update, and delete data from the database. 
The methods will be implemented in the DataAccess class.

*/

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary;

public interface IDataAccess 
{
    // Read from the database
    Task<List<T>> ReadDataFromDBAsync<T, U>(string sql, U parameters);

    // Create, Delete, and Update in the database
    Task<int> SaveDataToDBAsync<T>(string sql, T parameters);
}