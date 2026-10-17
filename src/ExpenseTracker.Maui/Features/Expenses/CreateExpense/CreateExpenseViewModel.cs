using ExpenseTracker.Application.Expenses.CreateExpense;
using ExpenseTracker.Domain.Enums;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ExpenseTracker.Maui.Features.Expenses.CreateExpense;

public sealed class CreateExpenseViewModel : INotifyPropertyChanged
{
    private readonly ICreateExpenseHandler _handler;
    private readonly ILogger<CreateExpenseViewModel> _logger;

    private string _amountText = string.Empty;
    private ExpenseCategory _category;
    private DateTime _date = DateTime.Today;
    private string _description = string.Empty;
    private string _errorMessage = string.Empty;
    private string _successMessage = string.Empty;
    private bool _isSaving;
    private int _saveInProgress;

    public CreateExpenseViewModel(
        ICreateExpenseHandler handler,
        ILogger<CreateExpenseViewModel> logger)
    {
        _handler = handler;
        _logger = logger;

        SaveCommand = new Command(
            async () => await SaveAsync(),
            () => !IsSaving);
    }

    public IReadOnlyList<ExpenseCategory> Categories { get; } =
        Enum.GetValues<ExpenseCategory>();

    public string AmountText
    {
        get => _amountText;
        set
        {
            if (SetProperty(ref _amountText, value))
            {
                ErrorMessage = string.Empty;
                SuccessMessage = string.Empty;
            }
        }
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

    public string ErrorMessage
    {
        get => _errorMessage;
        private set => SetProperty(ref _errorMessage, value);
    }

    public string SuccessMessage
    {
        get => _successMessage;
        private set => SetProperty(ref _successMessage, value);
    }

    public bool IsSaving
    {
        get => _isSaving;
        private set
        {
            if (SetProperty(ref _isSaving, value))
            {
                SaveCommand.ChangeCanExecute();
            }
        }
    }

    public Command SaveCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private async Task SaveAsync()
    {
        // An atomic guard prevents overlapping saves, even if the command
        // is invoked again before the UI updates its enabled state.
        if (Interlocked.Exchange(ref _saveInProgress, 1) == 1)
        {
            return;
        }

        try
        {
            ErrorMessage = string.Empty;
            SuccessMessage = string.Empty;

            if (!decimal.TryParse(
                    AmountText,
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out var amount))
            {
                ErrorMessage = "Enter a valid amount.";
                return;
            }

            if (amount <= 0)
            {
                ErrorMessage = "Enter an amount greater than zero.";
                return;
            }

            ErrorMessage = string.Empty;
            SuccessMessage = string.Empty;
            IsSaving = true;

            var command = new CreateExpenseCommand(
                amount,
                Category,
                Date,
                Description);

            await _handler.HandleAsync(command);

            // Reset only after the handler confirms the save succeeded.
            AmountText = string.Empty;
            Description = string.Empty;
            Date = DateTime.Today;

            SuccessMessage = "Expense saved successfully.";
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to save expense.");
            ErrorMessage = "Unable to save the expense. Please try again.";
        }
        finally
        {
            IsSaving = false;
            Interlocked.Exchange(ref _saveInProgress, 0);
        }
    }

    private bool SetProperty<T>(
        ref T storage,
        T value,
        [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
        {
            return false;
        }

        storage = value;

        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));

        return true;
    }
}
