using ExpenseTracker.Maui.Features.Expenses.CreateExpense;

namespace ExpenseTracker.Maui.Features.Expenses.CreateExpense;

public partial class CreateExpensePage : ContentPage
{
    public CreateExpensePage(
        CreateExpenseViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}
