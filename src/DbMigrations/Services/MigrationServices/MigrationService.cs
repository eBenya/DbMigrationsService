using DbMigrations.Options;
using DbMigrations.Services.DbOperationServices;
using Microsoft.Extensions.Logging;

namespace DbMigrations.Services.MigrationServices
{
    public class MigrationService : IMigrationService
    {
        private readonly IDbOperationService _dbMigrationService;
        private readonly DbMigraionOptions _dbMigraionOptions;
        private readonly ILogger _logger;

        public MigrationService(IDbOperationService dbMigrationService, ILogger logger, DbMigraionOptions dbMigraionOptions)
        {
            _dbMigrationService = dbMigrationService ?? throw new ArgumentNullException(nameof(dbMigrationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dbMigraionOptions = dbMigraionOptions ?? throw new ArgumentNullException(nameof(dbMigraionOptions));
        }

        async Task<bool> IMigrationService.TryMakeMigrationAsync(CancellationToken token)
        {
            var isSuccessOperation = !_dbMigraionOptions.DoNeedMigration;
            if (_dbMigraionOptions.DoNeedMigration)
                isSuccessOperation = await TryMakeMigrationAsync(token);

            return isSuccessOperation;
        }

        private bool IsFromSqlRaw => _dbMigraionOptions.MigrationType?.IsFromSqlRaw ?? false;
        private bool IsFromSqlFile => _dbMigraionOptions.MigrationType?.SqlRawMigration?.IsFromFile ?? false;


        private async Task<bool> TryMakeMigrationAsync(CancellationToken token)
        {
            try
            {
                var isMigrationComplete = await MakeMigrationAsync(token);

                if (!isMigrationComplete)
                    _logger.LogWarning("Migration failed. {@MigrationOptions}", _dbMigraionOptions);

                return isMigrationComplete;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Migration failed. {@MigrationOptions}", _dbMigraionOptions);
                return false;
            }
        }

        private async Task<bool> MakeMigrationAsync(CancellationToken token)
        {
            const string migrationScripDefaultFilePath = "./Script/MigrationScript.sql";

            if (IsFromSqlRaw)
                return await RunMigrationByGeneratedSqlAsync(token);
                
            if (IsFromSqlFile)
                return await RunMigrationBySqlFromFileAsync(_dbMigraionOptions.MigrationType!.SqlRawMigration!.SqlFilePath ?? migrationScripDefaultFilePath, token);

            return await RunMigrationAsync(token);
        }

        private async Task<bool> RunMigrationAsync(CancellationToken token)
        {
            _logger.LogInformation("Try to make migration by migration file.");
            return await _dbMigrationService.DoMigrationFromSqlRaw(token);
        }

        private async Task<bool> RunMigrationByGeneratedSqlAsync(CancellationToken token)
        {
            _logger.LogInformation("Try to make migration by generated sql raw.");
            return await _dbMigrationService.DoMigrationFromSqlRaw(token);
        }

        private async Task<bool> RunMigrationBySqlFromFileAsync(string sqlFilePath, CancellationToken token)
        {
            _logger.LogInformation("Try to make migration by sql raw from file.");
            return await _dbMigrationService.DoMigrationFromSqlRaw(sqlFilePath, token);
        }
    }
}
