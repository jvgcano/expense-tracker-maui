using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Expenses.CreateExpense;

public sealed class CreateExpenseHandler
{
    public Expense Handle(CreateExpenseCommand command)
    {
        return new Expense(
            command.Amount,
            command.Category,
            command.Date,
            command.Description);
    }
}