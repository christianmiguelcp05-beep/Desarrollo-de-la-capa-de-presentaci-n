using GestionHotelera.Models.Entities;
using GestionHotelera.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using GestionHotelera.Data;

namespace GestionHotelera.Services.Implementations;

public class TipoHabilitacionService : ITipoHabilitacionService
{
    private readonly HotelDbContext _context;

    public TipoHabilitacionService(HotelDbContext context)
    {
        _context = context;
    }

    public async Task<List<TipoHabilitacion>> ListarAsync()
    {
        return await _context.TiposHabilitaciones.ToListAsync();
    }

    public async Task<TipoHabilitacion?> ObtenerAsync(int id)
    {
        return await _context.TiposHabilitaciones.FindAsync(id);
    }

    public async Task<TipoHabilitacion> CrearAsync(TipoHabilitacion tipoHabilitacion)
    {
        _context.TiposHabilitaciones.Add(tipoHabilitacion);
        await _context.SaveChangesAsync();
        return tipoHabilitacion;
    }

    public async Task<TipoHabilitacion> ActualizarAsync(TipoHabilitacion tipoHabilitacion)
    {
        _context.TiposHabilitaciones.Update(tipoHabilitacion);
        await _context.SaveChangesAsync();
        return tipoHabilitacion;
    }

    public async Task EliminarAsync(int id)
    {
        var tipo = await ObtenerAsync(id);
        if (tipo == null)
            throw new InvalidOperationException("Tipo de habilitación no encontrado");

        // Verificar si tiene habilitaciones asociadas
        var tieneHabilitaciones = await _context.Habilitaciones
            .AnyAsync(h => h.TipoHabilitacionId == id);

        if (tieneHabilitaciones)
            throw new InvalidOperationException("No se puede eliminar este tipo de habilitación porque tiene habilitaciones asociadas");

        _context.TiposHabilitaciones.Remove(tipo);
        await _context.SaveChangesAsync();
    }
}
