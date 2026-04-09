using CommunityToolkit.Mvvm.Messaging;
using Duocare2.ViewModels;
using Duocare2.Views;

namespace Duocare2;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        var vm = new ShellViewModel();
        BindingContext = vm;

        // Si NO hay sesión iniciada, limpiamos datos antiguos
        if (!Preferences.ContainsKey("ParentName"))
        {
            Preferences.Remove("ChildName");
        }

        // Recibir nombre del padre cuando inicia sesión
        WeakReferenceMessenger.Default.Register<ParentNameMessage>(this, (r, m) =>
        {
            vm.ParentName = m.Value;
        });

        // Recibir nombre del niño cuando se registre o se cree ficha
        WeakReferenceMessenger.Default.Register<ChildNameMessage>(this, (r, m) =>
        {
            vm.ChildName = m.Value;
        });

        // Rutas
        Routing.RegisterRoute("login", typeof(LoginPage));
        Routing.RegisterRoute("registro", typeof(RegisterPage));
        Routing.RegisterRoute("home", typeof(HomePage));
        Routing.RegisterRoute("ficha", typeof(ProfilePage));
        Routing.RegisterRoute("forgotpassword", typeof(ForgotPasswordPage));
        Routing.RegisterRoute("calendarioNiño", typeof(ChildCalendarPage));
        Routing.RegisterRoute("calendarioMascota", typeof(PetCalendarPage));
        Routing.RegisterRoute("acerca", typeof(AboutPage));
    }

    private async void OnAboutClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("acerca");
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        // Borrar datos guardados
        Preferences.Remove("ParentName");
        Preferences.Remove("ChildName");

        // Avisar al ShellViewModel para que borre el nombre del menú
        WeakReferenceMessenger.Default.Send(new ParentNameMessage(string.Empty));
        WeakReferenceMessenger.Default.Send(new ChildNameMessage(string.Empty));

        // Cerrar menú lateral
        Shell.Current.FlyoutIsPresented = false;

        // Navegar al login (ruta absoluta)
        await Shell.Current.GoToAsync("//login");
    }
}
