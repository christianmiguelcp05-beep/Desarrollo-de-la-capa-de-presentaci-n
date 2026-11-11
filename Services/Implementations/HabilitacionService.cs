using GestionHotelera.Models.Entities;
using GestionHotelera.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using GestionHotelera.Data;

namespace GestionHotelera.Services.Implementations;

public class HabilitacionService : IHabilitacionService
{
    private readonly HotelDbContext _context;

    public HabilitacionService(HotelDbContext context)
    {
        _context = context;
    }

    public async Task<List<Habilitacion>> ListarAsync()
    {
        return await _context.Habilitaciones
            .Include(h => h.Piso).ThenInclude(p => p!.Bloque).ThenInclude(b => b!.Hotel)
            .Include(h => h.TipoHabilitacion)
            .ToListAsync();
    }

    public async Task<List<Habilitacion>> ListarPorPisoAsync(int pisoId)
    {
        return await _context.Habilitaciones
            .Where(h => h.PisoId == pisoId)
            .Include(h => h.Piso)
            .Include(h => h.TipoHabilitacion)
            .ToListAsync();
    }

    public async Task<Habilitacion?> ObtenerAsync(int id)
    {
        return await _context.Habilitaciones
            .Include(h => h.Piso).ThenInclude(p => p!.Bloque).ThenInclude(b => b!.Hotel)
            .Include(h => h.TipoHabilitacion)
            .FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<Habilitacion> CrearAsync(Habilitacion habilitacion)
    {
        _context.Habilitaciones.Add(habilitacion);
        await _context.SaveChangesAsync();
        return habilitacion;
    }

    public async Task<Habilitacion> ActualizarAsync(Habilitacion habilitacion)
    {
        _context.Habilitaciones.Update(habilitacion);
        await _context.SaveChangesAsync();
        return habilitacion;
    }

    public async Task EliminarAsync(int id)
    {
        var habilitacion = await ObtenerAsync(id);
        if (habilitacion == null)
            throw new InvalidOperationException("Habilitación no encontrada");

        _context.Habilitaciones.Remove(habilitacion);
        await _context.SaveChangesAsync();
    }
}
