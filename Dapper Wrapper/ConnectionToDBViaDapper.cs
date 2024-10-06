using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DataAbstractions.Dapper;

public class ConnectionToDBViaDapper(IDbConnection connection) : IDataAccessor
{
    private readonly IDbConnection _connection = connection ?? throw new ArgumentNullException(); 

    public IDbConnection GetUnderlyingConnection() 
        => _connection;

    public string ConnectionString
    {
        get => _connection.ConnectionString;
        set => _connection.ConnectionString = value;
    }

    public int ConnectionTimeout
        => _connection.ConnectionTimeout;

    public string Database
        => _connection.Database;

    public ConnectionState State
        => _connection.State;

    public IDbTransaction BeginTransaction()
        => _connection.BeginTransaction();

    public IDbTransaction BeginTransaction(IsolationLevel il)
        => _connection.BeginTransaction(il);

    public void Close()
        => _connection.Close();

    public void ChangeDatabase(string databaseName)
        => _connection.ChangeDatabase(databaseName);

    public IDbCommand CreateCommand()
        => _connection.CreateCommand();

    public void Open()
    {
        if (_connection.State == ConnectionState.Closed)
        {
            _connection.Open();
        }
    }

    public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        => _connection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);

    public Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        => _connection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);

    public void Dispose()
    {
        if (_connection.State != ConnectionState.Closed)
        {
            _connection.Close();
        }
        _connection.Dispose();
    }
}