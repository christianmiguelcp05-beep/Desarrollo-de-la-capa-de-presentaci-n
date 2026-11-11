using GestionHotelera.Models.Entities;

namespace GestionHotelera.Services.Interfaces;

public interface IPisoService
{
    Task<List<Piso>> ListarAsync();
    Task<List<Piso>> ListarPorBloqueAsync(int bloqueId);
    Task<Piso?> ObtenerAsync(int id);
    Task<Piso> CrearAsync(Piso piso);
    Task<Piso> ActualizarAsync(Piso piso);
    Task EliminarAsync(int id);
}
