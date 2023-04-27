namespace Project.App;

public record DALOptions
{
    public SqliteOptions Sqlite { get; init; }
}
public record SqliteOptions
{
    public bool Enabled { get; init; } = true;

    public string DatabaseName { get; init; } = null!;

    public bool RecreateDatabaseEachTime { get; init; } = false;

    public bool SeedDemoData { get; init; } = false;
}
