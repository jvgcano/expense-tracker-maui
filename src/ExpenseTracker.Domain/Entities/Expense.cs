using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Domain.Entities;

public class Expense
{
    public Guid Id { get; private set; }

    public decimal Amount { get; private set; }

    public ExpenseCategory Category { get; private set; }

    public DateTime Date { get; private set; }

    public string Description { get; private set; } = string.Empty;

    public Expense(
        decimal amount,
        ExpenseCategory category,
        DateTime date,
        string? description = null)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Expense amount must be greater than zero.");

        if (!Enum.IsDefined(category))
            throw new ArgumentException(
                "Invalid expense category.",
                nameof(category));

        if (date == default)
            throw new ArgumentException(
                "Expense date is required.",
                nameof(date));

        if (description?.Length > 250)
            throw new ArgumentException(
                "Expense description cannot exceed 250 characters.",
                nameof(description));

        Id = Guid.NewGuid();
        Amount = amount;
        Category = category;
        Date = date;
        Description = description ?? string.Empty;
    }
}
