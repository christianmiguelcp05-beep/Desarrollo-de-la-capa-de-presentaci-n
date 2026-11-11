using GestionHotelera.Models.Entities;
using GestionHotelera.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using GestionHotelera.Data;

namespace GestionHotelera.Services.Implementations;

public class PisoService : IPisoService
{
    private readonly HotelDbContext _context;

    public PisoService(HotelDbContext context)
    {
        _context = context;
    }

    public async Task<List<Piso>> ListarAsync()
    {
        return await _context.Pisos.Include(p => p.Bloque).ThenInclude(b => b!.Hotel).Include(p => p.Habilitaciones).ToListAsync();
    }

    public async Task<List<Piso>> ListarPorBloqueAsync(int bloqueId)
    {
        return await _context.Pisos
            .Where(p => p.BloqueId == bloqueId)
            .Include(p => p.Bloque)
            .ToListAsync();
    }

    public async Task<Piso?> ObtenerAsync(int id)
    {
        return await _context.Pisos.Include(p => p.Bloque).Include(p => p.Habilitaciones).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Piso> CrearAsync(Piso piso)
    {
        _context.Pisos.Add(piso);
        await _context.SaveChangesAsync();
        return piso;
    }

    public async Task<Piso> ActualizarAsync(Piso piso)
    {
        _context.Pisos.Update(piso);
        await _context.SaveChangesAsync();
        return piso;
    }

    public async Task EliminarAsync(int id)
    {
        var piso = await ObtenerAsync(id);
        if (piso == null)
            throw new InvalidOperationException("Piso no encontrado");

        // Verificar si tiene habilitaciones
        if (piso.Habilitaciones.Count > 0)
            throw new InvalidOperationException("No se puede eliminar este piso porque tiene habilitaciones asociadas");

        _context.Pisos.Remove(piso);
        await _context.SaveChangesAsync();
    }
}
