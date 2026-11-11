# Sistema de Gestión Hotelera — Capa de Presentación

Aplicación web para la gestión de la infraestructura de un hotel, desarrollada con **ASP.NET Core MVC** y arquitectura en capas. Este repositorio contiene el desarrollo de la **capa de presentación** (proyecto Web) sobre las capas de acceso a datos (DAL) y lógica de negocio (BL).

> Proyecto académico — Instituto Tecnológico de Las Américas (ITLA).

---

## Módulos

| Módulo | Descripción |
|---|---|
| **Gestión de Hoteles** | Registro y mantenimiento de hoteles (nombre, dirección, teléfono, estrellas, estado). |
| **Gestión de Bloques** | Bloques o edificios que pertenecen a un hotel. |
| **Gestión de Pisos** | Pisos de cada bloque, con filtro en cascada Hotel → Bloque. |
| **Gestión de Habilitaciones** | Habitaciones del hotel (número, capacidad, estado, tipo), con filtro en cascada Hotel → Bloque → Piso. |
| **Mantenimiento de Tipos de Habilitaciones** | Catálogo de tipos de habitación con su precio base. |

### Jerarquía del modelo

```
Hotel ──< Bloque ──< Piso ──< Habilitación >── TipoHabilitación
```

---

##  Arquitectura

```
Solución
├── DAL   → Acceso a datos: entidades, DbContext y repositorios (EF Core)
├── BL    → Lógica de negocio: servicios y reglas de negocio
└── Web   → Presentación: Controllers, ViewModels y Views (Razor)
```

**Flujo:** `Vista → Controller → Servicio (BL) → Repositorio (DAL) → Base de datos`

Principios aplicados:

- Los controllers **no acceden al `DbContext`**; solo dependen de interfaces de la BL (`IHotelService`, `IBloqueService`, etc.) mediante inyección de dependencias.
- Las vistas trabajan con **ViewModels**, nunca con las entidades de EF directamente.
- Las reglas de negocio se validan en la BL y se lanzan como `InvalidOperationException`; el controller las captura y las muestra al usuario.

---

##  Características

- CRUD completo en cada módulo (listado, crear, editar y eliminar con confirmación).
- Validación en cliente y servidor con `DataAnnotations`.
- Protección `[ValidateAntiForgeryToken]` en todos los formularios POST.
- Búsqueda rápida en los listados.
- Listas desplegables en cascada (Hotel → Bloque → Piso) mediante endpoints JSON y `cascade.js`.
- Mensajes de éxito y error con `TempData`.
- Reglas de integridad: no se puede eliminar un registro que tenga dependencias (hotel con bloques, bloque con pisos, piso con habilitaciones, tipo con habilitaciones).

---

## 🛠️ Tecnologías

- C# / .NET (ASP.NET Core MVC)
- Entity Framework Core
- SQL Server
- Razor Views + Bootstrap
- JavaScript (fetch API)
- Visual Studio y Git

---

##  Estructura del proyecto Web

```
Web/
├── Controllers/
│   ├── HotelesController.cs
│   ├── BloquesController.cs
│   ├── PisosController.cs
│   ├── HabilitacionesController.cs
│   └── TiposHabilitacionController.cs
├── ViewModels/
│   ├── HotelVM.cs
│   ├── BloqueVM.cs
│   ├── PisoVM.cs
│   ├── HabilitacionVM.cs
│   └── TipoHabilitacionVM.cs
├── Views/
│   ├── Hoteles/            (Index, Create, Edit, Delete, _Form)
│   ├── Bloques/
│   ├── Pisos/
│   ├── Habilitaciones/
│   ├── TiposHabilitacion/
│   └── Shared/             (_Layout, _Alerts, _MenuModulos)
└── wwwroot/js/
    └── cascade.js
```

---

##  Instalación y ejecución

### Requisitos previos

- Visual Studio 2022 o superior con la carga de trabajo **ASP.NET y desarrollo web**
- SDK de .NET correspondiente al proyecto
- SQL Server (o SQL Server LocalDB)
- Git

### Pasos

1. **Clonar el repositorio**
   ```bash
   git clone <URL-DEL-REPOSITORIO>
   cd <NOMBRE-DE-LA-CARPETA>
   ```

2. **Abrir la solución** (`.sln`) en Visual Studio.

3. **Configurar la cadena de conexión** en `appsettings.json` del proyecto Web:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=NombreBD;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```

4. **Crear la base de datos** (según el método usado por el proyecto):
   - Si usa migraciones, en la *Consola del Administrador de paquetes*:
     ```powershell
     Update-Database
     ```
   - Si la base de datos se proporciona con un script, ejecútalo en SQL Server Management Studio.

5. **Restaurar paquetes y compilar:** `Ctrl + Shift + B`.

6. **Ejecutar:** establece el proyecto Web como proyecto de inicio y presiona `F5`.

---

##  Flujo de trabajo con Git

Se trabaja con **una rama por módulo**:

| Rama | Módulo |
|---|---|
| `feature/hoteles` | Gestión de Hoteles |
| `feature/bloques` | Gestión de Bloques |
| `feature/pisos` | Gestión de Pisos |
| `feature/habilitaciones` | Gestión de Habilitaciones |
| `feature/tipos-habilitacion` | Mantenimiento de Tipos de Habilitaciones |

```bash
git checkout -b feature/hoteles
git add .
git commit -m "feat(hoteles): agrega CRUD de hoteles en la capa de presentación"
git push -u origin feature/hoteles
```

Cuando el módulo esté terminado, se abre un *Pull Request* hacia `main` para su revisión.

---

##  Reglas de negocio

| Entidad | Regla |
|---|---|
| Hotel | Nombre único. No se elimina si tiene bloques. |
| Bloque | Nombre único dentro del hotel. No se elimina si tiene pisos. |
| Piso | Número único dentro del bloque. No se elimina si tiene habilitaciones. |
| Habilitación | Número único dentro del hotel. |
| Tipo de habilitación | No se elimina si tiene habilitaciones asociadas. |
