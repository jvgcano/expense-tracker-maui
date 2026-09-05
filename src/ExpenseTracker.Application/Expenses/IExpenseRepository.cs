using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Expenses;

public interface IExpenseRepository
{
    Task AddAsync(
        Expense expense,
        CancellationToken cancellationToken = default);
}
