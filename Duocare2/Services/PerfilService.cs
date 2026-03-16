using Duocare2.Models;

namespace Duocare2.Services;

public class PerfilService
{
    private static PerfilService _instancia;
    public static PerfilService Instancia => _instancia ??= new PerfilService();

    public Perfil Perfil { get; private set; }

    public void GuardarPerfil(Perfil perfil)
    {
        Perfil = perfil;
    }
}
