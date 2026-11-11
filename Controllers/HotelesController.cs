using GestionHotelera.Models.Entities;
using GestionHotelera.Services.Interfaces;
using GestionHotelera.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GestionHotelera.Controllers;

public class HotelesController : Controller
{
    private readonly IHotelService _service;
    private readonly IBloqueService _bloqueService;

    public HotelesController(IHotelService service, IBloqueService bloqueService)
    {
        _service = service;
        _bloqueService = bloqueService;
    }

    public async Task<IActionResult> Index(string searchString)
    {
        var hoteles = await _service.ListarAsync();
        if (!string.IsNullOrEmpty(searchString))
            hoteles = hoteles.Where(h => h.Nombre.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();

        var viewModels = hoteles.Select(ToViewModel).ToList();
        ViewData["SearchString"] = searchString;
        return View(viewModels);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(HotelViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);

        try
        {
            var hotel = ToEntity(viewModel);
            await _service.CrearAsync(hotel);
            TempData["Success"] = "Hotel creado exitosamente";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(viewModel);
        }
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var hotel = await _service.ObtenerAsync(id.Value);
        if (hotel == null) return NotFound();
        return View(ToViewModel(hotel));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, HotelViewModel viewModel)
    {
        if (id != viewModel.Id) return NotFound();
        if (!ModelState.IsValid) return View(viewModel);

        try
        {
            var hotel = ToEntity(viewModel);
            await _service.ActualizarAsync(hotel);
            TempData["Success"] = "Hotel actualizado exitosamente";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(viewModel);
        }
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var hotel = await _service.ObtenerAsync(id.Value);
        if (hotel == null) return NotFound();
        return View(ToViewModel(hotel));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _service.EliminarAsync(id);
            TempData["Success"] = "Hotel eliminado exitosamente";
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

    private HotelViewModel ToViewModel(Hotel hotel)
        => new() { Id = hotel.Id, Nombre = hotel.Nombre, Direccion = hotel.Direccion, Telefono = hotel.Telefono, Estrellas = hotel.Estrellas, Activo = hotel.Activo };

    private Hotel ToEntity(HotelViewModel vm)
        => new() { Id = vm.Id, Nombre = vm.Nombre, Direccion = vm.Direccion, Telefono = vm.Telefono, Estrellas = vm.Estrellas, Activo = vm.Activo };
}
