using JN_WEB.Entities;

namespace JN_WEB.Models
{
    public class UsuarioModel (HttpClient httpClient) : IUsuarioModel
    {
        public void RegistrarUsuario(Usuario ent)
        {
            using (httpClient)
            {
                string url = "https://localhost:7009/api/Usuario/RegistrarUsuario";
                JsonContent body = JsonContent.Create(ent);
                var resp = httpClient.PostAsJsonAsync(url, body).Result;
            }
        }

        public void IniciarSesion(Usuario ent)
        {
            using (httpClient)
            {
                string url = "https://localhost:7009/api/Usuario/IniciarSesion";
                JsonContent body = JsonContent.Create(ent);
                var resp = httpClient.PostAsJsonAsync(url, body).Result;
            }
        }
    }
}