using JN_WEB.Entities;
using System.Net.Http;
using JN_WEB.Interface;
using System.Net.Http.Headers;

namespace JN_WEB.Models
{
    public class UsuarioModel(HttpClient httpClient, IConfiguration iConfiguration, IHttpContextAccessor iContextAccessor) : IUsuarioModel
    {

        public Respuesta RegistrarUsuario(Usuario ent)
        {
            using (httpClient)
            {
                string url = iConfiguration.GetSection("Keys:UrlApi").Value + "Usuario/RegistrarUsuario";
                JsonContent body = JsonContent.Create(ent);
                var resp = httpClient.PostAsync(url, body).Result;
                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<Respuesta>().Result!;
                }
                else
                {
                    return new Respuesta();
                }
            }
        }

        public Respuesta IniciarSesion(Usuario ent)
        {
            using (httpClient)
            {
                string url = iConfiguration.GetSection("Keys:UrlApi").Value +  "Usuario/IniciarSesion";
                JsonContent body = JsonContent.Create(ent);
                var resp = httpClient.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<Respuesta>().Result!;
                }
                else
                {
                    return new Respuesta();
                }
            }
        }

        public Respuesta ConsultarUsuarios()
        {
            using (httpClient)
            {
                string url = iConfiguration.GetSection("Keys:UrlApi").Value + "Usuario/ConsultarUsuarios";
                string token = iContextAccessor.HttpContext!.Session.GetString("TOKEN")!.ToString()!; //OJO

                httpClient.DefaultRequestHeaders.Authorization =  new AuthenticationHeaderValue("Bearer", token);
                var resp = httpClient.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<Respuesta>().Result!;
                }
                else
                {
                    return new Respuesta();
                }
            }
        }
    }
}