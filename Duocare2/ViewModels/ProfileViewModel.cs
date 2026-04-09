using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Duocare2.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        // ---------------------------
        // VISIBILIDAD
        // ---------------------------
        private bool _mostrarNiño;
        public bool MostrarNiño
        {
            get => _mostrarNiño;
            set => SetProperty(ref _mostrarNiño, value);
        }

        private bool _mostrarMascota;
        public bool MostrarMascota
        {
            get => _mostrarMascota;
            set => SetProperty(ref _mostrarMascota, value);
        }

        private bool _mostrarAmbos;
        public bool MostrarAmbos
        {
            get => _mostrarAmbos;
            set => SetProperty(ref _mostrarAmbos, value);
        }

        // ---------------------------
        // COLORES DE TABS
        // ---------------------------
        public string TabNiñoBackground { get; set; }
        public string TabNiñoBorder { get; set; }
        public string TabNiñoText { get; set; }

        public string TabMascotaBackground { get; set; }
        public string TabMascotaBorder { get; set; }
        public string TabMascotaText { get; set; }

        public string TabAmbosBackground { get; set; }
        public string TabAmbosBorder { get; set; }
        public string TabAmbosText { get; set; }

        // ---------------------------
        // FOTOS
        // ---------------------------
        public ImageSource FotoNiño { get; set; }
        public ImageSource FotoMascota { get; set; }

        // ---------------------------
        // LISTAS
        // ---------------------------
        public ObservableCollection<NiñoModel> ListaNiños { get; set; }
        public ObservableCollection<MascotaModel> ListaMascotas { get; set; }

        // ---------------------------
        // CAMPOS GENERALES NIÑO
        // ---------------------------
        public string AlergiasNiño { get; set; }
        public string MedicacionNiño { get; set; }
        public string RutinasNiño { get; set; }
        public string ContactoNombreNiño { get; set; }
        public string ContactoTelefonoNiño { get; set; }

        // ---------------------------
        // CAMPOS GENERALES MASCOTA
        // ---------------------------
        public string AlergiasMascota { get; set; }
        public string MedicacionMascota { get; set; }
        public string RutinasMascota { get; set; }
        public string PaseoMañana { get; set; }
        public string PaseoTarde { get; set; }
        public string PaseoNoche { get; set; }
        public string ContactoNombreMascota { get; set; }
        public string ContactoTelefonoMascota { get; set; }

        // ---------------------------
        // COMANDOS
        // ---------------------------
        public ICommand CambiarTabCommand { get; }
        public ICommand SubirFotoNiñoCommand { get; }
        public ICommand SubirFotoMascotaCommand { get; }
        public ICommand AñadirNiñoCommand { get; }
        public ICommand AñadirMascotaCommand { get; }
        public ICommand EliminarNiñoCommand { get; }
        public ICommand EliminarMascotaCommand { get; }

        public ProfileViewModel()
        {
            ListaNiños = new ObservableCollection<NiñoModel>();
            ListaMascotas = new ObservableCollection<MascotaModel>();

            CambiarTabCommand = new Command<string>(CambiarTab);
            AñadirNiñoCommand = new Command(() => ListaNiños.Add(new NiñoModel()));
            AñadirMascotaCommand = new Command(() => ListaMascotas.Add(new MascotaModel()));
            EliminarNiñoCommand = new Command<NiñoModel>(n => ListaNiños.Remove(n));
            EliminarMascotaCommand = new Command<MascotaModel>(m => ListaMascotas.Remove(m));

            // Estado inicial
            ActivarTab("Niño");
        }

        // ---------------------------
        // LÓGICA DE TABS
        // ---------------------------
        private void CambiarTab(string tab)
        {
            ActivarTab(tab);
        }

        private void ActivarTab(string tab)
        {
            MostrarNiño = tab == "Niño";
            MostrarMascota = tab == "Mascota";
            MostrarAmbos = tab == "Ambos";

            // Colores
            TabNiñoBackground = tab == "Niño" ? "#5A4FCF" : "White";
            TabNiñoBorder = "#5A4FCF";
            TabNiñoText = tab == "Niño" ? "White" : "#5A4FCF";

            TabMascotaBackground = tab == "Mascota" ? "#FF8C42" : "White";
            TabMascotaBorder = "#FF8C42";
            TabMascotaText = tab == "Mascota" ? "White" : "#FF8C42";

            TabAmbosBackground = tab == "Ambos" ? "#2CB67D" : "White";
            TabAmbosBorder = "#2CB67D";
            TabAmbosText = tab == "Ambos" ? "White" : "#2CB67D";

            OnPropertyChanged(nameof(TabNiñoBackground));
            OnPropertyChanged(nameof(TabNiñoBorder));
            OnPropertyChanged(nameof(TabNiñoText));
            OnPropertyChanged(nameof(TabMascotaBackground));
            OnPropertyChanged(nameof(TabMascotaBorder));
            OnPropertyChanged(nameof(TabMascotaText));
            OnPropertyChanged(nameof(TabAmbosBackground));
            OnPropertyChanged(nameof(TabAmbosBorder));
            OnPropertyChanged(nameof(TabAmbosText));
        }
    }

    // ---------------------------
    // MODELOS
    // ---------------------------
    public class NiñoModel
    {
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; } = DateTime.Now;
        public string Notas { get; set; }
    }

    public class MascotaModel
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string OtroTipo { get; set; }
        public string Notas { get; set; }
    }
}
