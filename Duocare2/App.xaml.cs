namespace Duocare2;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Cargamos el Shell como página principal
        MainPage = new AppShell();
    }
}
