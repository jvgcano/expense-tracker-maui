using System.Globalization;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure.Persistence.Sqlite.Models;

namespace ExpenseTracker.Infrastructure.Persistence.Sqlite.Mappers;

public static class ExpenseRecordMapper
{
    public static ExpenseRecord ToRecord(Expense expense)
    {
        return new ExpenseRecord
        {
            Id = expense.Id.ToString("D"),
            Amount = expense.Amount.ToString(CultureInfo.InvariantCulture),
            Category = (int)expense.Category,
            Date = expense.Date.ToString("O", CultureInfo.InvariantCulture),
            Description = expense.Description
        };
    }
}
