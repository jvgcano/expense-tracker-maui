using ExpenseTracker.Application.Expenses;
using ExpenseTracker.Infrastructure.Persistence.Sqlite;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        var databasePath = Path.Combine(
            FileSystem.AppDataDirectory,
            "expenses.db");

        var connectionString =
            $"Data Source={databasePath}";

        builder.Services.AddSingleton(
            new SqliteConnectionFactory(connectionString));

        builder.Services.AddSingleton<SqliteDatabaseInitializer>();

        builder.Services.AddSingleton<IExpenseRepository, SqliteExpenseRepository>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
