namespace Duocare2.ViewModels;

public class ForgotPasswordViewModel : BaseViewModel
{
    public string Email { get; set; }

    public Command SendCommand { get; }

    public ForgotPasswordViewModel()
    {
        SendCommand = new Command(OnSend);
    }

    private async void OnSend()
    {
        if (string.IsNullOrWhiteSpace(Email))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Introduce tu correo", "OK");
            return;
        }

        await Application.Current.MainPage.DisplayAlert(
            "Correo enviado",
            "Si el correo existe, recibirás instrucciones para recuperar tu contraseña.",
            "OK");

        await Shell.Current.GoToAsync("login");
    }
}
