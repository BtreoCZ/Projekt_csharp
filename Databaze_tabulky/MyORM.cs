using System;
using System.Data;
using System.Reflection;
using System.Text;
using Dapper;
using Microsoft.Data.Sqlite;
namespace Databaze_tabulky;

public class MyORM
{
    private readonly string connectionToDatabase;
    private readonly ReflectionLoader reflectionLoader;
    private IDbConnection app_database;
    private IDbTransaction? transaction;

    public MyORM(string connectionToDatabase, string assemblyPath)
    {
        this.connectionToDatabase = connectionToDatabase;
        this.reflectionLoader = new ReflectionLoader(assemblyPath);
        this.app_database = new SqliteConnection(connectionToDatabase);
    }

    public void BeginTransaction()
    {
        if (app_database.State != ConnectionState.Open)
            app_database.Open();

        transaction = app_database.BeginTransaction();
    }

    public void Commit()
    {
        transaction?.Commit();
        Dispose();
    }

    public void Rollback()
    {
        transaction?.Rollback();
        Dispose();
    }

    private void Dispose()
    {
        app_database?.Close();
        transaction = null;
    }

    public async Task InsertIntoAsync(object entity)
    {
        Type type = entity.GetType();
        var tableName = type.Name;

        PropertyInfo[] properties = reflectionLoader.GetProperties(type).Where(p => p.Name != $"{tableName}Id").ToArray();
        var columnNames = string.Join(", ", properties.Select(p => p.Name));
        var values = string.Join(", ", properties.Select(p => $"@{p.Name}"));
        foreach(var name in columnNames)
        {
            Console.WriteLine(name);
        }

        string query = $"INSERT INTO {tableName}s ({columnNames}) VALUES ({values}); SELECT last_insert_rowid();";

        var newId = await app_database.ExecuteScalarAsync<long>(query, entity, transaction);

        var idProperty = type.GetProperty($"{tableName}Id");

        if (idProperty != null && idProperty.CanWrite)
        {
            idProperty.SetValue(entity, Convert.ToInt32(newId));
        }
    }

    public async Task<IEnumerable<T>> SelectAllAsync<T>() where T : class
    {
        Type type = typeof(T);

        string tableName = type.Name;

        string query = $"SELECT * FROM {tableName}s";

        return await app_database.QueryAsync<T>(query, transaction);
    }

    public async Task<T> SelectFromAsync<T>(int id) where T : class
    {
        Type type = typeof(T);
        string tableName = type.Name;
        string query = $"SELECT * FROM {tableName}s WHERE {tableName}Id = @Id";

        return await app_database.QueryFirstOrDefaultAsync<T>(query, new { Id = id }, transaction);
    }

    public async Task<IEnumerable<T>> SelectWhereAsync<T>(Dictionary<string, object> conditions, bool useLike = false) where T : class
    {
        Type type = typeof(T);
        string tableName = type.Name;

        var whereClause = conditions.Select(kvp =>
            useLike && kvp.Key == "Name"
            ? $"LOWER({kvp.Key}) LIKE LOWER(@{kvp.Key})"
            : $"{kvp.Key} = @{kvp.Key}"
        ).ToArray();

        string query = $"SELECT * FROM {tableName}s WHERE {string.Join(" AND ", whereClause)}";
        return await app_database.QueryAsync<T>(query, conditions, transaction);
    }

    public async Task<IEnumerable<T>> SelectAllWhereAsync<T>(Dictionary<string, object> conditions, bool useLike = false) where T : class
    {
        Type type = typeof(T);

        string tableName = type.Name;

        var whereClause = conditions.Select(kvp =>
            useLike && kvp.Key == "Name"
            ? $"LOWER({kvp.Key}) LIKE LOWER(@{kvp.Key})"
            : $"{kvp.Key} = @{kvp.Key}"
        ).ToArray();

        string query = $"SELECT * FROM {tableName}s WHERE {string.Join(" AND ", whereClause)}";

        return await app_database.QueryAsync<T>(query, conditions, transaction);
    }

    public async Task DeleteFromAsync<T>(int id) where T : class
    {
        Type type = typeof(T);
        string tableName = type.Name;
        string query = $"DELETE FROM {tableName}s WHERE {tableName}Id = @Id";

        await app_database.ExecuteAsync(query, new { Id = id }, transaction);
    }


    public async Task DeleteFromWhereAsync<T>(Dictionary<string, object> conditions) where T : class
    {
        Type type = typeof(T);
        string tableName = type.Name;

        var whereClause = conditions.Select(kvp => $"{kvp.Key} = @{kvp.Key}").ToArray();

        string query = $"DELETE FROM {tableName}s WHERE {string.Join(" AND ", whereClause)}";

        await app_database.ExecuteAsync(query, conditions, transaction);
    }

    public async Task UpdateInAsync<T>(T entity) where T : class
    {
        Type type = typeof(T);
        var tableName = type.Name;

        PropertyInfo[] properties = reflectionLoader.GetProperties(type).Where(p => p.Name != $"{tableName}Id").ToArray();
        var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));

        string query = $"UPDATE {tableName}s SET {setClause} WHERE {tableName}Id = @{tableName}Id";

        await app_database.ExecuteAsync(query, entity, transaction);
    }
}