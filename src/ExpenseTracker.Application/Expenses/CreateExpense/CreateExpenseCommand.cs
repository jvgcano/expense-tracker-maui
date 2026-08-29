using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Application.Expenses.CreateExpense;

public sealed record CreateExpenseCommand(
    decimal Amount,
    ExpenseCategory Category,
    DateTime Date,
    string? Description);
