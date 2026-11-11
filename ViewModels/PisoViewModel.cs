using System.ComponentModel.DataAnnotations;

namespace GestionHotelera.ViewModels;

public class PisoViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El número es obligatorio")]
    [Range(0, 999, ErrorMessage = "El número debe ser válido")]
    [Display(Name = "Número")]
    public int Numero { get; set; }

    [StringLength(200, ErrorMessage = "La descripción no puede exceder 200 caracteres")]
    [Display(Name = "Descripción")]
    public string Descripcion { get; set; } = string.Empty;

    [Required(ErrorMessage = "El bloque es obligatorio")]
    [Display(Name = "Bloque")]
    public int BloqueId { get; set; }

    [Display(Name = "Bloque")]
    public string? BloqueNombre { get; set; }

    [Display(Name = "Hotel")]
    public int? HotelId { get; set; }
}
