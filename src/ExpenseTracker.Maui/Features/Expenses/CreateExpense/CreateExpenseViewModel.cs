using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ExpenseTracker.Application.Expenses.CreateExpense;
using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Maui.Features.Expenses.CreateExpense;

public sealed class CreateExpenseViewModel : INotifyPropertyChanged
{
    private readonly CreateExpenseHandler _handler;

    private decimal _amount;
    private ExpenseCategory _category;
    private DateTime _date = DateTime.Today;
    private string _description = string.Empty;

    public CreateExpenseViewModel(CreateExpenseHandler handler)
    {
        _handler = handler;

        SaveCommand = new Command(
            async () => await SaveAsync());
    }

    public IReadOnlyList<ExpenseCategory> Categories { get; } =
        Enum.GetValues<ExpenseCategory>();

    public decimal Amount
    {
        get => _amount;
        set => SetProperty(ref _amount, value);
    }

    public ExpenseCategory Category
    {
        get => _category;
        set => SetProperty(ref _category, value);
    }

    public DateTime Date
    {
        get => _date;
        set => SetProperty(ref _date, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public ICommand SaveCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private async Task SaveAsync()
    {
        var command = new CreateExpenseCommand(
            Amount,
            Category,
            Date,
            Description);

        await _handler.HandleAsync(command);
    }

    private void SetProperty<T>(
        ref T storage,
        T value,
        [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
        {
            return;
        }

        storage = value;

        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }
}
