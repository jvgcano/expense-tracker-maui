using ExpenseTracker.Application.Expenses;
using ExpenseTracker.Application.Expenses.CreateExpense;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Application.Tests;

public class CreateExpenseHandlerTests
{
    [Fact]
    public async Task HandleAsync_WithValidCommand_CreatesAndPersistsExpense()
    {
        // Arrange
        var command = new CreateExpenseCommand(
            100.50m,
            ExpenseCategory.Food,
            new DateTime(2026, 8, 29),
            "Lunch");

        var repository = new TestExpenseRepository();
        var handler = new CreateExpenseHandler(repository);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(command.Amount, result.Amount);
        Assert.Equal(command.Category, result.Category);
        Assert.Equal(command.Date, result.Date);
        Assert.Equal(command.Description, result.Description);

        Assert.Same(result, repository.AddedExpense);
    }

    private sealed class TestExpenseRepository : IExpenseRepository
    {
        public Expense? AddedExpense { get; private set; }

        public Task AddAsync(
            Expense expense,
            CancellationToken cancellationToken = default)
        {
            AddedExpense = expense;
            return Task.CompletedTask;
        }
    }
}
