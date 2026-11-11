using GestionHotelera.Models.Entities;
using GestionHotelera.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using GestionHotelera.Data;

namespace GestionHotelera.Services.Implementations;

public class BloqueService : IBloqueService
{
    private readonly HotelDbContext _context;

    public BloqueService(HotelDbContext context)
    {
        _context = context;
    }

    public async Task<List<Bloque>> ListarAsync()
    {
        return await _context.Bloques.Include(b => b.Hotel).Include(b => b.Pisos).ToListAsync();
    }

    public async Task<List<Bloque>> ListarPorHotelAsync(int hotelId)
    {
        return await _context.Bloques
            .Where(b => b.HotelId == hotelId)
            .Include(b => b.Hotel)
            .ToListAsync();
    }

    public async Task<Bloque?> ObtenerAsync(int id)
    {
        return await _context.Bloques.Include(b => b.Hotel).Include(b => b.Pisos).FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Bloque> CrearAsync(Bloque bloque)
    {
        _context.Bloques.Add(bloque);
        await _context.SaveChangesAsync();
        return bloque;
    }

    public async Task<Bloque> ActualizarAsync(Bloque bloque)
    {
        _context.Bloques.Update(bloque);
        await _context.SaveChangesAsync();
        return bloque;
    }

    public async Task EliminarAsync(int id)
    {
        var bloque = await ObtenerAsync(id);
        if (bloque == null)
            throw new InvalidOperationException("Bloque no encontrado");

        // Verificar si tiene pisos
        if (bloque.Pisos.Count > 0)
            throw new InvalidOperationException("No se puede eliminar este bloque porque tiene pisos asociados");

        _context.Bloques.Remove(bloque);
        await _context.SaveChangesAsync();
    }
}
