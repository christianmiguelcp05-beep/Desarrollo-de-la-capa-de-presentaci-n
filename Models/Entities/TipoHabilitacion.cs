namespace GestionHotelera.Models.Entities;

public class TipoHabilitacion
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal PrecioBase { get; set; }
    public bool Activo { get; set; } = true;

    // Relaciones
    public ICollection<Habilitacion> Habilitaciones { get; set; } = new List<Habilitacion>();
}
