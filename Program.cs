using GestionHotelera.Data;
using GestionHotelera.Services.Interfaces;
using GestionHotelera.Services.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar DbContext
builder.Services.AddDbContext<HotelDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? 
        "Server=(localdb)\\mssqllocaldb;Database=GestionHotelera;Trusted_Connection=true;"));

// Agregar servicios de MVC
builder.Services.AddControllersWithViews();

// Registrar servicios de la lógica de negocio
builder.Services.AddScoped<ITipoHabilitacionService, TipoHabilitacionService>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IBloqueService, BloqueService>();
builder.Services.AddScoped<IPisoService, PisoService>();
builder.Services.AddScoped<IHabilitacionService, HabilitacionService>();

var app = builder.Build();

// Configurar el pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

