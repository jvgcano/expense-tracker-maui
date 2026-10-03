using ExpenseTracker.Application.Expenses;
using ExpenseTracker.Application.Expenses.CreateExpense;
using ExpenseTracker.Infrastructure.Persistence.Sqlite;
using ExpenseTracker.Maui.Features.Expenses.CreateExpense;
using Microsoft.Extensions.DependencyInjection;
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

        builder.Services.AddTransient<CreateExpenseHandler>();
        builder.Services.AddTransient<CreateExpenseViewModel>();
        builder.Services.AddTransient<CreateExpensePage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        var initializer =
            app.Services.GetRequiredService<SqliteDatabaseInitializer>();

        initializer
            .InitializeAsync()
            .GetAwaiter()
            .GetResult();

        return app;
    }
}
