using GestionHotelera.Models.Entities;
using GestionHotelera.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using GestionHotelera.Data;

namespace GestionHotelera.Services.Implementations;

public class HotelService : IHotelService
{
    private readonly HotelDbContext _context;

    public HotelService(HotelDbContext context)
    {
        _context = context;
    }

    public async Task<List<Hotel>> ListarAsync()
    {
        return await _context.Hoteles.Include(h => h.Bloques).ToListAsync();
    }

    public async Task<Hotel?> ObtenerAsync(int id)
    {
        return await _context.Hoteles.Include(h => h.Bloques).FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<Hotel> CrearAsync(Hotel hotel)
    {
        _context.Hoteles.Add(hotel);
        await _context.SaveChangesAsync();
        return hotel;
    }

    public async Task<Hotel> ActualizarAsync(Hotel hotel)
    {
        _context.Hoteles.Update(hotel);
        await _context.SaveChangesAsync();
        return hotel;
    }

    public async Task EliminarAsync(int id)
    {
        var hotel = await ObtenerAsync(id);
        if (hotel == null)
            throw new InvalidOperationException("Hotel no encontrado");

        // Verificar si tiene bloques
        if (hotel.Bloques.Count > 0)
            throw new InvalidOperationException("No se puede eliminar este hotel porque tiene bloques asociados");

        _context.Hoteles.Remove(hotel);
        await _context.SaveChangesAsync();
    }
}
