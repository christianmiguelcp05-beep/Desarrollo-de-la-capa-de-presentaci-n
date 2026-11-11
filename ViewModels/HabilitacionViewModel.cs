using System.ComponentModel.DataAnnotations;

namespace GestionHotelera.ViewModels;

public class HabilitacionViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El número es obligatorio")]
    [Range(1, 9999, ErrorMessage = "El número debe ser válido")]
    [Display(Name = "Número")]
    public int Numero { get; set; }

    [Required(ErrorMessage = "La capacidad es obligatoria")]
    [Range(1, 20, ErrorMessage = "La capacidad debe estar entre 1 y 20")]
    [Display(Name = "Capacidad")]
    public int Capacidad { get; set; }

    [Display(Name = "Estado")]
    public string Estado { get; set; } = "Disponible";

    [Required(ErrorMessage = "El piso es obligatorio")]
    [Display(Name = "Piso")]
    public int PisoId { get; set; }

    [Display(Name = "Piso")]
    public string? PisoNombre { get; set; }

    [Required(ErrorMessage = "El tipo de habilitación es obligatorio")]
    [Display(Name = "Tipo de Habilitación")]
    public int TipoHabilitacionId { get; set; }

    [Display(Name = "Tipo de Habilitación")]
    public string? TipoHabilitacionNombre { get; set; }

    [Display(Name = "Hotel")]
    public int? HotelId { get; set; }

    [Display(Name = "Bloque")]
    public int? BloqueId { get; set; }
}
