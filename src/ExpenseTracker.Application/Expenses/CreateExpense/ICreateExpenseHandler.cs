using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Expenses.CreateExpense;

public interface ICreateExpenseHandler
{
    Task<Expense> HandleAsync(
        CreateExpenseCommand command,
        CancellationToken cancellationToken = default);
}
