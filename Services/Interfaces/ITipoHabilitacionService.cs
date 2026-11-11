using GestionHotelera.Models.Entities;

namespace GestionHotelera.Services.Interfaces;

public interface ITipoHabilitacionService
{
    Task<List<TipoHabilitacion>> ListarAsync();
    Task<TipoHabilitacion?> ObtenerAsync(int id);
    Task<TipoHabilitacion> CrearAsync(TipoHabilitacion tipoHabilitacion);
    Task<TipoHabilitacion> ActualizarAsync(TipoHabilitacion tipoHabilitacion);
    Task EliminarAsync(int id);
}
