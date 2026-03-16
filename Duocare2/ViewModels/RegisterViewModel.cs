using Duocare2.Services;
using Duocare2.Models;

namespace Duocare2.ViewModels;

public class RegisterViewModel : BaseViewModel
{
    public string Nombre { get; set; }
    public string Email { get; set; }

    private string password;
    public string Password
    {
        get => password;
        set
        {
            password = value;
            OnPropertyChanged();
            ValidatePasswords();
        }
    }

    private string confirmPassword;
    public string ConfirmPassword
    {
        get => confirmPassword;
        set
        {
            confirmPassword = value;
            OnPropertyChanged();
            ValidatePasswords();
        }
    }

    private string confirmPasswordMessage;
    public string ConfirmPasswordMessage
    {
        get => confirmPasswordMessage;
        set { confirmPasswordMessage = value; OnPropertyChanged(); }
    }

    private Color confirmPasswordMessageColor;
    public Color ConfirmPasswordMessageColor
    {
        get => confirmPasswordMessageColor;
        set { confirmPasswordMessageColor = value; OnPropertyChanged(); }
    }

    private Color confirmPasswordBorderColor;
    public Color ConfirmPasswordBorderColor
    {
        get => confirmPasswordBorderColor;
        set { confirmPasswordBorderColor = value; OnPropertyChanged(); }
    }

    public Command RegisterCommand { get; }
    public Command IrALoginCommand { get; }

    public RegisterViewModel()
    {
        RegisterCommand = new Command(OnRegister);
        IrALoginCommand = new Command(OnIrALogin);
    }

    private void ValidatePasswords()
    {
        if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
        {
            ConfirmPasswordMessage = "";
            ConfirmPasswordBorderColor = Colors.Transparent;
            return;
        }

        if (Password == ConfirmPassword)
        {
            ConfirmPasswordMessage = "Coincide ✔";
            ConfirmPasswordMessageColor = Colors.Green;
            ConfirmPasswordBorderColor = Colors.Green;
        }
        else
        {
            ConfirmPasswordMessage = "No coincide ✘";
            ConfirmPasswordMessageColor = Colors.Red;
            ConfirmPasswordBorderColor = Colors.Red;
        }
    }

    private async void OnRegister()
    {
        if (string.IsNullOrWhiteSpace(Nombre) ||
            string.IsNullOrWhiteSpace(Email) ||
            string.IsNullOrWhiteSpace(Password) ||
            string.IsNullOrWhiteSpace(ConfirmPassword))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
            return;
        }

        if (Password != ConfirmPassword)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Las contraseñas no coinciden", "OK");
            return;
        }

        await Application.Current.MainPage.DisplayAlert("Registro", "Registro exitoso", "OK");
        await Shell.Current.GoToAsync("login");
    }

    private async void OnIrALogin()
    {
        await Shell.Current.GoToAsync("login");
    }
}
