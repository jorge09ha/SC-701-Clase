namespace JN_WEB.Entities
{
    public class Usuario
    {
        public string? Identificacion { get; set; } = string.Empty;
        public string? Nombre { get; set; } = string.Empty;
        public string? Correo { get; set; } = string.Empty;
        public string? Contrasenna { get; set; } = string.Empty;
        public string? Token { get; set; }
        public string? Descripcion { get; set; }
        public string? Estado { get; set; }
    }
}