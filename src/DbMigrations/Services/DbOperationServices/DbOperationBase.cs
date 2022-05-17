using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DbMigrations.Services.DbOperationServices
{
    public abstract class DbOperationBase<TContext> : IDbOperationService
        where TContext : DbContext
    {
        private readonly TContext dbContext;
        private readonly ILogger<DbOperationBase<TContext>> logger;

        protected DbOperationBase(TContext dbContext, ILogger<DbOperationBase<TContext>> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public abstract Task<bool> DoMigrationAsync(CancellationToken token = default);

        public abstract Task<bool> DoMigrationFromSqlRaw(CancellationToken token = default);

        public abstract Task<bool> DoMigrationFromSqlRaw(string sqlFilePath, CancellationToken token = default);
    }
}
