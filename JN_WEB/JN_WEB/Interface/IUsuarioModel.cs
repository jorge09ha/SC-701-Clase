using JN_WEB.Entities;

namespace JN_WEB.Interface
{
    public interface IUsuarioModel
    {
        Respuesta RegistrarUsuario(Usuario ent);
        Respuesta IniciarSesion(Usuario ent);
        Respuesta ConsultarUsuarios();
    }
}