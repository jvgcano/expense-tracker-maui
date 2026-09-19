using ExpenseTracker.Infrastructure.Persistence.Sqlite;

namespace ExpenseTracker.Infrastructure.Tests;

public sealed class SqliteTestDatabase : IAsyncDisposable
{
    private readonly string _databasePath;

    public SqliteConnectionFactory ConnectionFactory { get; }

    public SqliteTestDatabase()
    {
        _databasePath = Path.Combine(
            Path.GetTempPath(),
            $"expense-tracker-test-{Guid.NewGuid():N}.db");

        var connectionString =
            $"Data Source={_databasePath};Pooling=False";

        ConnectionFactory = new SqliteConnectionFactory(connectionString);
    }

    public async Task InitializeAsync(
        CancellationToken cancellationToken = default)
    {
        var initializer = new SqliteDatabaseInitializer(ConnectionFactory);

        await initializer.InitializeAsync(cancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        if (File.Exists(_databasePath))
        {
            File.Delete(_databasePath);
        }

        return ValueTask.CompletedTask;
    }
}
