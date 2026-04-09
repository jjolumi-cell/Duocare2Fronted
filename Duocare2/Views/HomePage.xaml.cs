using Duocare2.ViewModels;
using Duocare2.Views;

namespace Duocare2.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
        BindingContext = new HomeViewModel();
    }

    private async void AbrirCalendarioNiño(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("calendarioNiño");
    }

    private async void AbrirCalendarioMascota(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("calendarioMascota");
    }


    // MÉTODOS ANTIGUOS (solo se usan si sigues usando botones dentro del calendario)
    private async void DiaNiño_Clicked(object sender, EventArgs e)
    {
        var boton = sender as Button;
        string dia = boton.Text;

        var descripcion = await DisplayPromptAsync("Nuevo evento", $"Evento para el día {dia}:");
        if (string.IsNullOrWhiteSpace(descripcion)) return;

        var vm = BindingContext as HomeViewModel;
        vm.ChildEvents.Add($"{dia}: {descripcion}");
    }

    private async void DiaMascota_Clicked(object sender, EventArgs e)
    {
        var boton = sender as Button;
        string dia = boton.Text;

        var descripcion = await DisplayPromptAsync("Nuevo evento", $"Evento para el día {dia}:");
        if (string.IsNullOrWhiteSpace(descripcion)) return;

        var vm = BindingContext as HomeViewModel;
        vm.PetEvents.Add($"{dia}: {descripcion}");
    }

    private async void AñadirEventoNiño_Clicked(object sender, EventArgs e)
    {
        var descripcion = await DisplayPromptAsync("Nuevo evento", "Descripción:");
        if (string.IsNullOrWhiteSpace(descripcion)) return;

        var vm = BindingContext as HomeViewModel;
        vm.ChildEvents.Add(descripcion);
    }

    private async void AñadirEventoMascota_Clicked(object sender, EventArgs e)
    {
        var descripcion = await DisplayPromptAsync("Nuevo evento", "Descripción:");
        if (string.IsNullOrWhiteSpace(descripcion)) return;

        var vm = BindingContext as HomeViewModel;
        vm.PetEvents.Add(descripcion);
    }
}
