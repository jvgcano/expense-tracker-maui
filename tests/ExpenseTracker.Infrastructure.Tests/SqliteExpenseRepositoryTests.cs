using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Enums;
using ExpenseTracker.Infrastructure.Persistence.Sqlite;
using Microsoft.Data.Sqlite;

namespace ExpenseTracker.Infrastructure.Tests;

public class SqliteExpenseRepositoryTests
{
    [Fact]
    public async Task AddAsync_WithValidExpense_PersistsExpense()
    {
        // Arrange
        await using var database = new SqliteTestDatabase();

        await database.InitializeAsync();

        var repository = new SqliteExpenseRepository(
            database.ConnectionFactory);

        var expense = new Expense(
            100.50m,
            ExpenseCategory.Food,
            new DateTime(2026, 8, 29, 12, 30, 0),
            "Lunch");

        // Act
        await repository.AddAsync(expense);

        // Assert
        await using var connection =
            database.ConnectionFactory.CreateConnection();

        await connection.OpenAsync();

        await using var command = connection.CreateCommand();

        command.CommandText =
            """
            SELECT Id, Amount, Category, Date, Description
            FROM Expenses
            WHERE Id = $id;
            """;

        command.Parameters.AddWithValue(
            "$id",
            expense.Id.ToString("D"));

        await using var reader = await command.ExecuteReaderAsync();

        Assert.True(await reader.ReadAsync());

        Assert.Equal(
            expense.Id.ToString("D"),
            reader.GetString(0));

        Assert.Equal(
            "100.50",
            reader.GetString(1));

        Assert.Equal(
            (int)ExpenseCategory.Food,
            reader.GetInt32(2));

        Assert.Equal(
            expense.Date.ToString("O"),
            reader.GetString(3));

        Assert.Equal(
            "Lunch",
            reader.GetString(4));
    }
}
