using GestionHotelera.Models.Entities;

namespace GestionHotelera.Services.Interfaces;

public interface IBloqueService
{
    Task<List<Bloque>> ListarAsync();
    Task<List<Bloque>> ListarPorHotelAsync(int hotelId);
    Task<Bloque?> ObtenerAsync(int id);
    Task<Bloque> CrearAsync(Bloque bloque);
    Task<Bloque> ActualizarAsync(Bloque bloque);
    Task EliminarAsync(int id);
}
