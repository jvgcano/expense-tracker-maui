using ExpenseTracker.Application.Expenses;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure.Persistence.Sqlite.Mappers;
using Microsoft.Data.Sqlite;

namespace ExpenseTracker.Infrastructure.Persistence.Sqlite;

public sealed class SqliteExpenseRepository : IExpenseRepository
{
    private readonly SqliteConnectionFactory _connectionFactory;

    public SqliteExpenseRepository(
        SqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task AddAsync(
        Expense expense,
        CancellationToken cancellationToken = default)
    {
        var record = ExpenseRecordMapper.ToRecord(expense);

        await using var connection = _connectionFactory.CreateConnection();

        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();

        command.CommandText =
            """
            INSERT INTO Expenses
                (Id, Amount, Category, Date, Description)
            VALUES
                ($id, $amount, $category, $date, $description);
            """;

        command.Parameters.AddWithValue("$id", record.Id);
        command.Parameters.AddWithValue("$amount", record.Amount);
        command.Parameters.AddWithValue("$category", record.Category);
        command.Parameters.AddWithValue("$date", record.Date);
        command.Parameters.AddWithValue("$description", record.Description);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
