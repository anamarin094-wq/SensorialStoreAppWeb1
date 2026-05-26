using Microsoft.EntityFrameworkCore;
using SensorialStore.DAL; // Se importa la capa de datos para poder utilizar el DbContext

var builder = WebApplication.CreateBuilder(args);

// ================= CONEXIÓN A LA BASE DE DATOS =================
// Se registra el DbContext en el contenedor de servicios y se le pasa la configuración de SQL
// obtenida directamente desde el archivo appsettings.json bajo el nombre "DefaultConnection"
builder.Services.AddDbContext<SensorialDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Se conecta la capa de negocio (BLL) mediante inyección de dependencias para que los controladores
// puedan acceder a las funciones de los productos de manera ordenada y sin duplicar código
builder.Services.AddScoped<SensorialStore.BLL.ProductoService>();

// Se configura la aplicación para que soporte la arquitectura de MVC (Vistas y Controladores)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ================= CONFIGURACIÓN DEL PIPELINE (MIDDLEWARES) =================
// Se define el comportamiento de la aplicación ante las peticiones de los usuarios

// Se valida si la aplicación no se encuentra en el entorno de desarrollo (modo producción)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Se redirige a una vista de error amigable si ocurre un fallo crítico
    app.UseHsts(); // Se activa el protocolo de seguridad estricta para el navegador
}

// Se fuerza a la aplicación a redirigir cualquier petición hacia el protocolo seguro HTTPS
app.UseHttpsRedirection();

// Se habilita el acceso a los archivos físicos de la carpeta wwwroot (imágenes, estilos CSS y scripts)
app.UseStaticFiles();

// Se activa el sistema de enrutamiento para que la aplicación aprenda a interpretar las URLs ingresadas
app.UseRouting();

// Se añade el middleware de autorización para el control de accesos en el futuro
app.UseAuthorization();

// ================= RUTA POR DEFECTO =================
// Se establece la ruta inicial del proyecto. Si el usuario ingresa a la URL raíz,
// el sistema lo redirige automáticamente al HomeController para ejecutar la acción Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Se inicia oficialmente la ejecución de la aplicación web
app.Run();