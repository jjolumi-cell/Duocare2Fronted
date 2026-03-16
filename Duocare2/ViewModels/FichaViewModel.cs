using Duocare2.ViewModels;
using System.Collections.ObjectModel;

public class NiñoModel
{
    public string Nombre { get; set; }
    public DateTime FechaNacimiento { get; set; } = DateTime.Now;
    public string Notas { get; set; }
}

public class FichaViewModel : BaseViewModel
{
    public Command CambiarTabCommand { get; }
    public Command GuardarCommand { get; }
    public Command AñadirNiñoCommand { get; }

    public ObservableCollection<NiñoModel> ListaNiños { get; set; }

    public bool MostrarNiño { get; set; }
    public bool MostrarMascota { get; set; }
    public bool MostrarAmbos { get; set; }

    public Color TabNiñoBackground { get; set; }
    public Color TabMascotaBackground { get; set; }
    public Color TabAmbosBackground { get; set; }

    public Color TabNiñoBorder { get; set; }
    public Color TabMascotaBorder { get; set; }
    public Color TabAmbosBorder { get; set; }

    public Color TabNiñoText { get; set; }
    public Color TabMascotaText { get; set; }
    public Color TabAmbosText { get; set; }

    public string NombreMascota { get; set; }
    public string NotasMascota { get; set; }

    public ObservableCollection<string> TiposMascota { get; } =
        new ObservableCollection<string>
        {
            "Perro", "Gato", "Ave", "Reptil", "Roedor", "Otro"
        };

    private string tipoMascota;
    public string TipoMascota
    {
        get => tipoMascota;
        set
        {
            tipoMascota = value;
            OnPropertyChanged();
            MostrarOtro = tipoMascota == "Otro";
        }
    }

    private bool mostrarOtro;
    public bool MostrarOtro
    {
        get => mostrarOtro;
        set
        {
            mostrarOtro = value;
            OnPropertyChanged();
        }
    }

    public string MascotaOtro { get; set; }

    public FichaViewModel()
    {
        ListaNiños = new ObservableCollection<NiñoModel>();
        ListaNiños.Add(new NiñoModel());

        AñadirNiñoCommand = new Command(() =>
        {
            ListaNiños.Add(new NiñoModel());
        });

        CambiarTabCommand = new Command<string>(CambiarTab);
        GuardarCommand = new Command(OnGuardar);

        CambiarTab("Niño");
    }

    private void CambiarTab(string tab)
    {
        MostrarNiño = tab == "Niño";
        MostrarMascota = tab == "Mascota";
        MostrarAmbos = tab == "Ambos";

        string active = "#5A4FCF";
        string inactive = "#C7C7CC";
        string activeBg = "#EDE7FF";

        TabNiñoBackground = MostrarNiño ? Color.FromArgb(activeBg) : Colors.White;
        TabMascotaBackground = MostrarMascota ? Color.FromArgb(activeBg) : Colors.White;
        TabAmbosBackground = MostrarAmbos ? Color.FromArgb(activeBg) : Colors.White;

        TabNiñoBorder = MostrarNiño ? Color.FromArgb(active) : Color.FromArgb(inactive);
        TabMascotaBorder = MostrarMascota ? Color.FromArgb(active) : Color.FromArgb(inactive);
        TabAmbosBorder = MostrarAmbos ? Color.FromArgb(active) : Color.FromArgb(inactive);

        TabNiñoText = MostrarNiño ? Color.FromArgb(active) : Colors.Black;
        TabMascotaText = MostrarMascota ? Color.FromArgb(active) : Colors.Black;
        TabAmbosText = MostrarAmbos ? Color.FromArgb(active) : Colors.Black;

        OnPropertyChanged(nameof(MostrarNiño));
        OnPropertyChanged(nameof(MostrarMascota));
        OnPropertyChanged(nameof(MostrarAmbos));

        OnPropertyChanged(nameof(TabNiñoBackground));
        OnPropertyChanged(nameof(TabMascotaBackground));
        OnPropertyChanged(nameof(TabAmbosBackground));

        OnPropertyChanged(nameof(TabNiñoBorder));
        OnPropertyChanged(nameof(TabMascotaBorder));
        OnPropertyChanged(nameof(TabAmbosBorder));

        OnPropertyChanged(nameof(TabNiñoText));
        OnPropertyChanged(nameof(TabMascotaText));
        OnPropertyChanged(nameof(TabAmbosText));
    }

    private async void OnGuardar()
    {
        await Application.Current.MainPage.DisplayAlert("Guardado", "Datos guardados correctamente", "OK");
    }
}
