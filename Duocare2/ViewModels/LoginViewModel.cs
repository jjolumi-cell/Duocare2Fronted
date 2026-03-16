using System.Windows.Input;

namespace Duocare2.ViewModels;

public class LoginViewModel : BaseViewModel
{
    public string Email { get; set; }
    public string Password { get; set; }

    public Command LoginCommand { get; }
    public Command GoRegisterCommand { get; }
    public Command ForgotPasswordCommand { get; }

    public LoginViewModel()
    {
        LoginCommand = new Command(OnLogin);
        GoRegisterCommand = new Command(() => Shell.Current.GoToAsync("register"));
        ForgotPasswordCommand = new Command(OnForgotPassword);
    }

    private async void OnLogin()
    {
        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Rellena todos los campos", "OK");
            return;
        }

        await Shell.Current.GoToAsync("//home");
    }

    private async void OnForgotPassword()
    {
        await Shell.Current.GoToAsync("///forgotpassword");
    }
}
