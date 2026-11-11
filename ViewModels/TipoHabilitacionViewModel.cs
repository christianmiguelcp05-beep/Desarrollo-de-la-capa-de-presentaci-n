using System.ComponentModel.DataAnnotations;

namespace GestionHotelera.ViewModels;

public class TipoHabilitacionViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(300, ErrorMessage = "La descripción no puede exceder 300 caracteres")]
    [Display(Name = "Descripción")]
    public string Descripcion { get; set; } = string.Empty;

    [Required(ErrorMessage = "El precio base es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio base debe ser mayor a 0")]
    [Display(Name = "Precio Base")]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    public decimal PrecioBase { get; set; }

    [Display(Name = "Activo")]
    public bool Activo { get; set; } = true;
}
