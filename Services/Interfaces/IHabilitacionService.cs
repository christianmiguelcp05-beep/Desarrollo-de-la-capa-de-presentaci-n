using GestionHotelera.Models.Entities;

namespace GestionHotelera.Services.Interfaces;

public interface IHabilitacionService
{
    Task<List<Habilitacion>> ListarAsync();
    Task<List<Habilitacion>> ListarPorPisoAsync(int pisoId);
    Task<Habilitacion?> ObtenerAsync(int id);
    Task<Habilitacion> CrearAsync(Habilitacion habilitacion);
    Task<Habilitacion> ActualizarAsync(Habilitacion habilitacion);
    Task EliminarAsync(int id);
}
