# DbOperation service

Что умеет/содержит:
##### Options:
* Настройки `DbMigrationOptions`
```csharp
 "DbMigrationOption": {
    "IsMigration": true,               // Флаг необходимости миграции (Если стоит только он, то миграции мудут проводиться из файла миграций) 
    "MigrationType": {
      "IsFromSqlRaw": false,           // Флаг проведения миграций из sql строки(Если активен он и IsMigration, то в коде будет генерироваться sql скрипт и миграция будет проведена из него)  
      "SqlRawMigration": {
        "IsFromFile": true,            // Флаг проведения миграций из sql скрипта сохраненного в файле(Для активации этого варианта, так же требуется IsMigration=true)
        "SqlFilePath": ".\\[pathTofile]\\[SqlScriptFile].sql"
      }
    }
  }
```
**Priority:** `IsFromSqlRaw` **>** `IsFromFile`

##### Services:
* `IDbOperation`
```csharp
    /// <summary>
    /// To do migration by sql script file
    /// </summary>
    /// <returns>true is operation success</returns>
    Task<bool> CreateDbBySqlAsync(string sqlFilePath, CancellationToken cancellationToken);

    /// <summary>
    /// To do migration by migration file
    /// </summary>
    /// <returns>true is operation success</returns>
    Task<bool> MigrateAsync(CancellationToken cancellationToken);

    /// <summary>
    /// To do migration by generated sql raw
    /// </summary>
    /// <returns>true is operation success</returns>
    Task<bool> CreateDbBySqlAsync(CancellationToken cTk);
```

Для добавления нового провайдера, необходимо отнаследоваться от абстрактного класса `DbOperationBase`</br>
На текущий момент реализованы: 
* `IMigrationService`
```csharp
    /// <summary>
    /// Try make migration
    /// </summary>
    /// <returns>true is operation success</returns>
    Task<bool> TryMakeMigrationAsync(CancellationToken token);
```
Использует `IDbOperation`

`DbMigrationBase`:
```csharp
public abstract class DbMigrationBase<TContext> : IDbOperation where TContext : DbContext
    {
        protected readonly TContext DbContext;
        protected readonly ILogger<DbMigrationBase<TContext>> Logger;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        protected DbMigrationBase(TContext dbContext, ILogger<DbMigrationBase<TContext>> logger)
        {
            DbContext = dbContext;
            Logger = logger;
        }

        public abstract Task<bool> CreateDbBySqlAsync(string sqlFilePath, CancellationToken cancellationToken);

        public abstract Task<bool> MigrateAsync(CancellationToken cancellationToken);

        public abstract Task<bool> CreateDbBySqlAsync(CancellationToken cTk);
    }
```