namespace GestionHotelera.Models.Entities;

public class Hotel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public int Estrellas { get; set; } // 1-5
    public bool Activo { get; set; } = true;

    // Relaciones
    public ICollection<Bloque> Bloques { get; set; } = new List<Bloque>();
}
