namespace Duocare2.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
        BindingContext = new ViewModels.HomeViewModel();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var vm = BindingContext as ViewModels.HomeViewModel;

        // Si no hay niño ni mascota → mostrar alerta
        if (!vm.HayPerfilCompleto)
        {
            await DisplayAlert(
                "Perfil incompleto",
                "Debes registrar un niño, un perro o ambos para completar tu perfil.",
                "OK");

            await Shell.Current.GoToAsync("ficha");
        }
    }
}
