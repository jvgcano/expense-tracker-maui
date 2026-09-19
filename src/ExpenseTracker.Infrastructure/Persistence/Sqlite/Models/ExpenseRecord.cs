namespace ExpenseTracker.Infrastructure.Persistence.Sqlite.Models;

public sealed class ExpenseRecord
{
    public string Id { get; set; } = string.Empty;

    public string Amount { get; set; } = string.Empty;

    public int Category { get; set; }

    public string Date { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}
