namespace GestionHotelera.Models.Entities;

public class Bloque
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int HotelId { get; set; }

    // Relaciones
    public Hotel? Hotel { get; set; }
    public ICollection<Piso> Pisos { get; set; } = new List<Piso>();
}
