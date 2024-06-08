using JN_WEB.Entities;

namespace JN_WEB.Models
{
    public interface IUsuarioModel
    {
        void RegistrarUsuario(Usuario ent);

        void IniciarSesion(Usuario ent);
    }
}
