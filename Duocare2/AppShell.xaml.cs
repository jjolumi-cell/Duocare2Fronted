using Duocare2.ViewModels;

namespace Duocare2;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // ViewModel para el saludo y la foto del menú lateral
        BindingContext = new ShellViewModel();

        // Rutas de navegación
        Routing.RegisterRoute("login", typeof(Views.LoginPage));
        Routing.RegisterRoute("register", typeof(Views.RegisterPage));
        Routing.RegisterRoute("home", typeof(Views.HomePage));
        Routing.RegisterRoute("ficha", typeof(Views.FichaPage));

        // Navegar al login cuando el Shell termine de cargarse
        Loaded += async (s, e) =>
        {
            await GoToAsync("login");
        };
    }
}

