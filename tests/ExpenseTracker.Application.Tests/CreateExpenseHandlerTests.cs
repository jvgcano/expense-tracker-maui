using ExpenseTracker.Application.Expenses.CreateExpense;
using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Application.Tests;

public class CreateExpenseHandlerTests
{
    [Fact]
    public void Handle_WithValidCommand_ReturnsExpense()
    {
        // Arrange
        var command = new CreateExpenseCommand(
            100.50m,
            ExpenseCategory.Food,
            new DateTime(2026, 8, 29),
            "Lunch");

        var handler = new CreateExpenseHandler();

        // Act
        var result = handler.Handle(command);

        // Assert
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(command.Amount, result.Amount);
        Assert.Equal(command.Category, result.Category);
        Assert.Equal(command.Date, result.Date);
        Assert.Equal(command.Description, result.Description);
    }
}