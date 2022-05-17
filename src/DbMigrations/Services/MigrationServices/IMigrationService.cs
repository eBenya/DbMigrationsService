namespace DbMigrations.Services.MigrationServices
{
    public interface IMigrationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> TryMakeMigrationAsync(CancellationToken token);
    }
}
