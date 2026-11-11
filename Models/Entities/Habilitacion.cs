namespace GestionHotelera.Models.Entities;

public class Habilitacion
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public int Capacidad { get; set; }
    public string Estado { get; set; } = "Disponible"; // "Disponible", "Ocupada", "Mantenimiento"
    public int PisoId { get; set; }
    public int TipoHabilitacionId { get; set; }

    // Relaciones
    public Piso? Piso { get; set; }
    public TipoHabilitacion? TipoHabilitacion { get; set; }
}
