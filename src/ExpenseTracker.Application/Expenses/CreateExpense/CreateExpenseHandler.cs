using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Expenses.CreateExpense;

public sealed class CreateExpenseHandler
{
    private readonly IExpenseRepository _repository;

    public CreateExpenseHandler(IExpenseRepository repository)
    {
        _repository = repository;
    }

    public async Task<Expense> HandleAsync(
        CreateExpenseCommand command,
        CancellationToken cancellationToken = default)
    {
        var expense = new Expense(
            command.Amount,
            command.Category,
            command.Date,
            command.Description);

        await _repository.AddAsync(expense, cancellationToken);

        return expense;
    }
}
