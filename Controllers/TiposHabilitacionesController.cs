using GestionHotelera.Models.Entities;
using GestionHotelera.Services.Interfaces;
using GestionHotelera.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GestionHotelera.Controllers;

public class TiposHabilitacionesController : Controller
{
    private readonly ITipoHabilitacionService _service;

    public TiposHabilitacionesController(ITipoHabilitacionService service)
    {
        _service = service;
    }

    // GET: TiposHabilitaciones
    public async Task<IActionResult> Index(string searchString)
    {
        var tipos = await _service.ListarAsync();

        if (!string.IsNullOrEmpty(searchString))
        {
            tipos = tipos.Where(t => t.Nombre.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        var viewModels = tipos.Select(ToViewModel).ToList();
        ViewData["SearchString"] = searchString;
        return View(viewModels);
    }

    // GET: TiposHabilitaciones/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: TiposHabilitaciones/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TipoHabilitacionViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        try
        {
            var tipo = ToEntity(viewModel);
            await _service.CrearAsync(tipo);
            TempData["Success"] = "Tipo de habilitación creado exitosamente";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(viewModel);
        }
    }

    // GET: TiposHabilitaciones/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var tipo = await _service.ObtenerAsync(id.Value);
        if (tipo == null) return NotFound();

        var viewModel = ToViewModel(tipo);
        return View(viewModel);
    }

    // POST: TiposHabilitaciones/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TipoHabilitacionViewModel viewModel)
    {
        if (id != viewModel.Id) return NotFound();

        if (!ModelState.IsValid)
            return View(viewModel);

        try
        {
            var tipo = ToEntity(viewModel);
            await _service.ActualizarAsync(tipo);
            TempData["Success"] = "Tipo de habilitación actualizado exitosamente";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(viewModel);
        }
    }

    // GET: TiposHabilitaciones/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var tipo = await _service.ObtenerAsync(id.Value);
        if (tipo == null) return NotFound();

        var viewModel = ToViewModel(tipo);
        return View(viewModel);
    }

    // POST: TiposHabilitaciones/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _service.EliminarAsync(id);
            TempData["Success"] = "Tipo de habilitación eliminado exitosamente";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction(nameof(Delete), new { id });
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    private TipoHabilitacionViewModel ToViewModel(TipoHabilitacion tipo)
    {
        return new TipoHabilitacionViewModel
        {
            Id = tipo.Id,
            Nombre = tipo.Nombre,
            Descripcion = tipo.Descripcion,
            PrecioBase = tipo.PrecioBase,
            Activo = tipo.Activo
        };
    }

    private TipoHabilitacion ToEntity(TipoHabilitacionViewModel viewModel)
    {
        return new TipoHabilitacion
        {
            Id = viewModel.Id,
            Nombre = viewModel.Nombre,
            Descripcion = viewModel.Descripcion,
            PrecioBase = viewModel.PrecioBase,
            Activo = viewModel.Activo
        };
    }
}
