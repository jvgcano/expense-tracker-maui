using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Domain.Tests;

public class ExpenseTests
{
    [Fact]
    public void Constructor_WithValidValues_CreatesExpense()
    {
        // Arrange
        var amount = 100.50m;
        var category = ExpenseCategory.Food;
        var date = new DateTime(2026, 8, 29);
        var description = "Lunch";

        // Act
        var expense = new Expense(
            amount,
            category,
            date,
            description);

        // Assert
        Assert.NotEqual(Guid.Empty, expense.Id);
        Assert.Equal(amount, expense.Amount);
        Assert.Equal(category, expense.Category);
        Assert.Equal(date, expense.Date);
        Assert.Equal(description, expense.Description);
    }

    [Fact]
    public void Constructor_WithZeroAmount_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var amount = 0m;
        var category = ExpenseCategory.Food;
        var date = new DateTime(2026, 8, 29);

        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => new Expense(amount, category, date));

        // Assert
        Assert.Equal("amount", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithInvalidCategory_ThrowsArgumentException()
    {
        // Arrange
        var amount = 100m;
        var category = (ExpenseCategory)999;
        var date = new DateTime(2026, 8, 29);

        // Act
        var exception = Assert.Throws<ArgumentException>(
            () => new Expense(amount, category, date));

        // Assert
        Assert.Equal("category", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithDefaultDate_ThrowsArgumentException()
    {
        // Arrange
        var amount = 100m;
        var category = ExpenseCategory.Food;
        var date = default(DateTime);

        // Act
        var exception = Assert.Throws<ArgumentException>(
            () => new Expense(amount, category, date));

        // Assert
        Assert.Equal("date", exception.ParamName);
    }

    [Fact]
    public void Constructor_With250CharacterDescription_CreatesExpense()
    {
        // Arrange
        var description = new string('A', 250);

        // Act
        var expense = new Expense(
            100m,
            ExpenseCategory.Food,
            new DateTime(2026, 8, 29),
            description);

        // Assert
        Assert.Equal(description, expense.Description);
    }

    [Fact]
    public void Constructor_With251CharacterDescription_ThrowsArgumentException()
    {
        // Arrange
        var description = new string('A', 251);

        // Act
        var exception = Assert.Throws<ArgumentException>(
            () => new Expense(
                100m,
                ExpenseCategory.Food,
                new DateTime(2026, 8, 29),
                description));

        // Assert
        Assert.Equal("description", exception.ParamName);
    }
}
