using GestionHotelera.Models.Entities;

namespace GestionHotelera.Services.Interfaces;

public interface IHotelService
{
    Task<List<Hotel>> ListarAsync();
    Task<Hotel?> ObtenerAsync(int id);
    Task<Hotel> CrearAsync(Hotel hotel);
    Task<Hotel> ActualizarAsync(Hotel hotel);
    Task EliminarAsync(int id);
}
