namespace DbMigrations.Services.DbOperationServices;

public interface IDbOperationService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<bool> DoMigrationAsync(CancellationToken token = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<bool> DoMigrationFromSqlRaw(CancellationToken token = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sqlFilePath"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<bool> DoMigrationFromSqlRaw(string sqlFilePath, CancellationToken token = default);

}
