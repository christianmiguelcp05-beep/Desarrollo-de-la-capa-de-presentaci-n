using GestionHotelera.Models.Entities;
using GestionHotelera.Services.Interfaces;
using GestionHotelera.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionHotelera.Controllers;

public class BloquesController : Controller
{
    private readonly IBloqueService _service;
    private readonly IHotelService _hotelService;

    public BloquesController(IBloqueService service, IHotelService hotelService)
    {
        _service = service;
        _hotelService = hotelService;
    }

    public async Task<IActionResult> Index()
    {
        var bloques = await _service.ListarAsync();
        var viewModels = bloques.Select(b => new BloqueViewModel
        {
            Id = b.Id,
            Nombre = b.Nombre, 
            HotelId = b.HotelId,
            HotelNombre = b.Hotel?.Nombre
        }).ToList();
        return View(viewModels);
    }

    public async Task<IActionResult> Create()
    {
        var hoteles = await _hotelService.ListarAsync();
        ViewBag.Hoteles = hoteles;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BloqueViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            var hoteles = await _hotelService.ListarAsync();
            ViewBag.Hoteles = hoteles;
            return View(viewModel);
        }

        try
        {
            var bloque = new Bloque { Nombre = viewModel.Nombre, HotelId = viewModel.HotelId };
            await _service.CrearAsync(bloque);
            TempData["Success"] = "Bloque creado exitosamente";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            var hoteles = await _hotelService.ListarAsync();
            ViewBag.Hoteles = hoteles;
            ModelState.AddModelError("", ex.Message);
            return View(viewModel);
        }
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var bloque = await _service.ObtenerAsync(id.Value);
        if (bloque == null) return NotFound();

        var hoteles = await _hotelService.ListarAsync();
        ViewBag.Hoteles = hoteles;

        var viewModel = new BloqueViewModel
        {
            Id = bloque.Id,
            Nombre = bloque.Nombre,
            HotelId = bloque.HotelId,
            HotelNombre = bloque.Hotel?.Nombre
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, BloqueViewModel viewModel)
    {
        if (id != viewModel.Id) return NotFound();
        if (!ModelState.IsValid)
        {
            var hoteles = await _hotelService.ListarAsync();
            ViewBag.Hoteles = hoteles;
            return View(viewModel);
        }

        try
        {
            var bloque = new Bloque { Id = viewModel.Id, Nombre = viewModel.Nombre, HotelId = viewModel.HotelId };
            await _service.ActualizarAsync(bloque);
            TempData["Success"] = "Bloque actualizado exitosamente";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            var hoteles = await _hotelService.ListarAsync();
            ViewBag.Hoteles = hoteles;
            ModelState.AddModelError("", ex.Message);
            return View(viewModel);
        }
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var bloque = await _service.ObtenerAsync(id.Value);
        if (bloque == null) return NotFound();

        var viewModel = new BloqueViewModel
        {
            Id = bloque.Id,
            Nombre = bloque.Nombre,
            HotelId = bloque.HotelId,
            HotelNombre = bloque.Hotel?.Nombre
        };
        return View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _service.EliminarAsync(id);
            TempData["Success"] = "Bloque eliminado exitosamente";
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

    [HttpGet]
    public async Task<IActionResult> PorHotel(int id)
    {
        var bloques = await _service.ListarPorHotelAsync(id);
        return Json(bloques.Select(b => new { id = b.Id, nombre = b.Nombre }).ToList());
    }
}
