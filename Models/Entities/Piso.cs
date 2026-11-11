namespace GestionHotelera.Models.Entities;

public class Piso
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public int BloqueId { get; set; }

    // Relaciones
    public Bloque? Bloque { get; set; }
    public ICollection<Habilitacion> Habilitaciones { get; set; } = new List<Habilitacion>();
}
