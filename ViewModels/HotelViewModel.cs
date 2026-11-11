using System.ComponentModel.DataAnnotations;

namespace GestionHotelera.ViewModels;

public class HotelViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "La dirección no puede exceder 200 caracteres")]
    [Display(Name = "Dirección")]
    public string Direccion { get; set; } = string.Empty;

    [StringLength(20)]
    [Display(Name = "Teléfono")]
    [RegularExpression(@"^\+?[\d\s\-\(\)]{7,}$", ErrorMessage = "Formato de teléfono inválido")]
    public string Telefono { get; set; } = string.Empty;

    [Range(1, 5, ErrorMessage = "Las estrellas deben estar entre 1 y 5")]
    [Display(Name = "Estrellas")]
    public int Estrellas { get; set; } = 3;

    [Display(Name = "Activo")]
    public bool Activo { get; set; } = true;
}
