namespace DbMigrations.Options;

public class DbMigraionOptions
{
    public const string SectionName = "DbMigraionOptions";

    public bool DoNeedMigration { get; set; }

    public MigrationType? MigrationType { get; set; }
}

public class MigrationType
{
    public bool IsFromSqlRaw { get; set; }
    public SqlRawMigration? SqlRawMigration { get; set; }
}

public class SqlRawMigration
{
    public bool IsFromFile { get; set; }
    public string? SqlFilePath { get; set; }
}
