using Duocare2.Services;
using Duocare2.Models;
using CommunityToolkit.Mvvm.Messaging;

namespace Duocare2.ViewModels;

public class RegisterViewModel : BaseViewModel
{
    public string Name { get; set; }
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
        set
        {
            confirmPasswordMessage = value;
            OnPropertyChanged();
        }
    }

    private Color confirmPasswordMessageColor;
    public Color ConfirmPasswordMessageColor
    {
        get => confirmPasswordMessageColor;
        set
        {
            confirmPasswordMessageColor = value;
            OnPropertyChanged();
        }
    }

    private Color confirmPasswordBorderColor;
    public Color ConfirmPasswordBorderColor
    {
        get => confirmPasswordBorderColor;
        set
        {
            confirmPasswordBorderColor = value;
            OnPropertyChanged();
        }
    }

    public Command RegisterCommand { get; }
    public Command GoToLoginCommand { get; }

    public RegisterViewModel()
    {
        RegisterCommand = new Command(OnRegister);
        GoToLoginCommand = new Command(OnGoToLogin);
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
        if (string.IsNullOrWhiteSpace(Name) ||
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

        // Guardar nombre del padre para el menú
        Preferences.Set("ParentName", Name);
        WeakReferenceMessenger.Default.Send(new ParentNameMessage(Name));

        await Application.Current.MainPage.DisplayAlert("Registro", "Registro exitoso", "OK");

        // Ir al Home después de registrarse
        await Shell.Current.GoToAsync("//home");
    }

    private async void OnGoToLogin()
    {
        await Shell.Current.GoToAsync("//login");
    }
}
