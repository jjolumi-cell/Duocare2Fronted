using Duocare2.Models;
using Duocare2.Services;
using Microsoft.Maui.Media;

namespace Duocare2.ViewModels;

public class FichaViewModel : BaseViewModel
{
    private string _foto;
    public string Foto
    {
        get => _foto;
        set
        {
            _foto = value;
            OnPropertyChanged();
        }
    }

    private string _tipo;
    public string Tipo
    {
        get => _tipo;
        set
        {
            _tipo = value;
            OnPropertyChanged();
        }
    }

    public string Nombre { get; set; }
    public DateTime FechaNacimiento { get; set; } = DateTime.Now;
    public string Notas { get; set; }

    public List<string> Tipos { get; } = new()
    {
        "Niño",
        "Mascota",
        "Ambos"
    };

    public Command GuardarCommand { get; }
    public Command SeleccionarFotoCommand { get; }

    public FichaViewModel()
    {
        // VALORES DE PRUEBA
        Foto = "dotnet_bot.png";   // alguna imagen que tengas en Resources/Images
        Tipo = "Niño";

        GuardarCommand = new Command(OnGuardar);
        SeleccionarFotoCommand = new Command(async () => await SeleccionarFoto());
    }

    private async Task SeleccionarFoto()
    {
        var resultado = await MediaPicker.PickPhotoAsync();

        if (resultado != null)
            Foto = resultado.FullPath;
    }

    private async void OnGuardar()
    {
        var perfil = new Perfil
        {
            Nombre = Nombre,
            Foto = Foto,
            Tipo = Tipo
        };

        PerfilService.Instancia.GuardarPerfil(perfil);
        await Shell.Current.GoToAsync("home");
    }
}
