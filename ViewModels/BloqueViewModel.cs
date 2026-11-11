using System.ComponentModel.DataAnnotations;

namespace GestionHotelera.ViewModels;

public class BloqueViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El hotel es obligatorio")]
    [Display(Name = "Hotel")]
    public int HotelId { get; set; }

    [Display(Name = "Hotel")]
    public string? HotelNombre { get; set; }
}
