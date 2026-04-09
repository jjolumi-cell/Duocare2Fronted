using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Duocare2.ViewModels;

public class LoginViewModel : BaseViewModel
{
    private string email;
    public string Email
    {
        get => email;
        set
        {
            email = value;
            OnPropertyChanged();
        }
    }

    private string password;
    public string Password
    {
        get => password;
        set
        {
            password = value;
            OnPropertyChanged();
        }
    }

    private bool rememberMe;
    public bool RememberMe
    {
        get => rememberMe;
        set
        {
            rememberMe = value;
            OnPropertyChanged();
        }
    }

    public Command LoginCommand { get; }
    public Command GoRegisterCommand { get; }
    public Command ForgotPasswordCommand { get; }

    public LoginViewModel()
    {
        LoginCommand = new Command(OnLogin);
        GoRegisterCommand = new Command(() => Shell.Current.GoToAsync("registro"));
        ForgotPasswordCommand = new Command(OnForgotPassword);

        LoadSavedCredentials();
    }

    private async void LoadSavedCredentials()
    {
        Email = await SecureStorage.GetAsync("saved_email");
        Password = await SecureStorage.GetAsync("saved_password");

        if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            RememberMe = true;
    }

    private async void OnLogin()
    {
        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Rellena todos los campos", "OK");
            return;
        }

        if (RememberMe)
        {
            await SecureStorage.SetAsync("saved_email", Email);
            await SecureStorage.SetAsync("saved_password", Password);
        }
        else
        {
            SecureStorage.Remove("saved_email");
            SecureStorage.Remove("saved_password");
        }

        // EXTRAER SOLO EL NOMBRE (antes del @)
        var nombre = Email.Split('@')[0];

        // GUARDAR NOMBRE
        Preferences.Set("NombrePadre", nombre);

        // ENVIAR MENSAJE AL MENÚ
        WeakReferenceMessenger.Default.Send(new ParentNameMessage(nombre));

        // 🔥 COMPROBAR SI TIENE FICHA
        bool tieneFicha = Preferences.Get("TieneFicha", false);

        if (!tieneFicha)
        {
            bool crear = await Application.Current.MainPage.DisplayAlert(
                "Ficha necesaria",
                "Aún no has creado la ficha del niño. ¿Quieres crearla ahora?",
                "Sí",
                "No"
            );

            if (crear)
            {
                await Shell.Current.GoToAsync("ficha");
                return;
            }
        }

        // NAVEGAR A HOME
        await Shell.Current.GoToAsync("//home");
    }

    private async void OnForgotPassword()
    {
        await Shell.Current.GoToAsync("forgotpassword");
    }
}