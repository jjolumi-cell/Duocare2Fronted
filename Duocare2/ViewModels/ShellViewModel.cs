using Duocare2.Services;

namespace Duocare2.ViewModels;

public class ShellViewModel : BaseViewModel
{
    public string Saludo => $"Hola {NombreMostrar}";
    public string FotoPerfil => PerfilService.Instancia.Perfil?.Foto;

    public string NombreMostrar
    {
        get
        {
            var perfil = PerfilService.Instancia.Perfil;
            if (perfil == null || string.IsNullOrWhiteSpace(perfil.Nombre))
                return "Usuario";

            return perfil.Nombre;
        }
    }
}
