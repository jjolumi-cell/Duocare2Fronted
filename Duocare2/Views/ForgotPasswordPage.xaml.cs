namespace Duocare2.Views;

public partial class ForgotPasswordPage : ContentPage
{
    public ForgotPasswordPage()
    {
        InitializeComponent();
        BindingContext = new ViewModels.ForgotPasswordViewModel();
    }
}
