using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Duocare2.ViewModels;

public class ShellViewModel : INotifyPropertyChanged
{
    private string parentName;
    public string ParentName
    {
        get => parentName;
        set
        {
            parentName = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(SesionIniciada));
            OnPropertyChanged(nameof(NoSesion));
        }
    }

    private string childName;
    public string ChildName
    {
        get => childName;
        set
        {
            childName = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(TieneFicha));
        }
    }

    public bool SesionIniciada => !string.IsNullOrEmpty(ParentName);
    public bool NoSesion => string.IsNullOrEmpty(ParentName);
    public bool TieneFicha => !string.IsNullOrEmpty(ChildName);

    public ICommand GoToAcercaCommand { get; }
    public ICommand CerrarSesionCommand { get; }

    public ShellViewModel()
    {
        // Cargar datos guardados (EN INGLÉS)
        ParentName = Preferences.Get("ParentName", "");
        ChildName = Preferences.Get("ChildName", "");

        GoToAcercaCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync("acerca");
        });

        CerrarSesionCommand = new Command(OnCerrarSesion);
    }

    private async void OnCerrarSesion()
    {
        // Borrar datos guardados (EN INGLÉS)
        Preferences.Remove("ParentName");
        Preferences.Remove("ChildName");

        ParentName = string.Empty;
        ChildName = string.Empty;

        await Shell.Current.GoToAsync("//login");
    }

    public event PropertyChangedEventHandler PropertyChanged;

    void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
