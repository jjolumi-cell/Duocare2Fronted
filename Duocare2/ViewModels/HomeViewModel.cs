namespace Duocare2.ViewModels;

public class HomeViewModel : BaseViewModel
{
    public string NombreNiño { get; set; } = "No registrado";
    public string NombreMascota { get; set; } = "No registrado";

    public bool HayPerfilCompleto
    {
        get
        {
            bool tieneNiño = NombreNiño != "No registrado";
            bool tieneMascota = NombreMascota != "No registrado";
            return tieneNiño || tieneMascota;
        }
    }
}
