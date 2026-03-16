using Duocare2.Services;
using Duocare2.Models;

namespace Duocare2.ViewModels;

public class RegisterViewModel : BaseViewModel
{
    public string Nombre { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public Command RegisterCommand { get; }
    public Command IrALoginCommand { get; }

    public RegisterViewModel()
    {
        RegisterCommand = new Command(OnRegister);
        IrALoginCommand = new Command(OnIrALogin);
    }

    private async void OnRegister()
    {
        if (string.IsNullOrWhiteSpace(Nombre) ||
            string.IsNullOrWhiteSpace(Email) ||
            string.IsNullOrWhiteSpace(Password))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
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
