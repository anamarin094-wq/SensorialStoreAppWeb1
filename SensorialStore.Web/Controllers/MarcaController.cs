using Microsoft.AspNetCore.Mvc;
using SensorialStore.BLL;
using SensorialStore.DAL.Entities;
using System.Threading.Tasks;

namespace SensorialStore.Web.Controllers
{
    // Este controlador maneja la pantalla donde se gestionan las marcas de maquillaje (como Vogue o Maybelline)
    public class MarcaController : Controller
    {
        // Traemos el servicio de lógica de negocio para conectar esta pantalla con la base de datos
        private readonly ProductoService _productoService;

        // El constructor recibe el servicio para poder usar todas sus funciones aquí adentro
        public MarcaController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        // Este método carga la página principal de marcas
        public async Task<IActionResult> Index()
        {
            // Le pide al servicio la lista de todas las marcas registradas en SQL
            var marcas = await _productoService.ObtenerMarcasAsync();
            // Le envía esa lista a la vista para que las muestre ordenadas en la tabla
            return View(marcas);
        }

        // Este método recibe los datos del formulario cuando agregamos una marca nueva
        [HttpPost]
        public async Task<IActionResult> Crear(Marca marca)
        {
            // CORRECCIÓN: Quitamos la lista interna de productos de la validación
            // Hacemos esto para que C# no tire error porque la lista de productos relacionados llega vacía
            ModelState.Remove("Productos");

            // Si los campos obligatorios del formulario están bien digitados
            if (ModelState.IsValid)
            {
                // Llama al servicio para guardar la nueva marca en la base de datos
                await _productoService.CrearMarcaAsync(marca);
                // Nos refresca la pantalla de marcas para ver los cambios
                return RedirectToAction(nameof(Index));
            }
            // Si algo falla, igual nos deja en la misma página de marcas sin dañar nada
            return RedirectToAction(nameof(Index));
        }

        // Este método recibe los cambios cuando editamos una marca existente en el modal
        [HttpPost]
        public async Task<IActionResult> Editar(Marca marca)
        {
            // También sacamos la lista interna de productos de la validación por seguridad
            ModelState.Remove("Productos");

            // Si los nuevos datos modificados son válidos
            if (ModelState.IsValid)
            {
                // Llama al servicio para actualizar el registro viejo con los datos nuevos en SQL
                await _productoService.ActualizarMarcaAsync(marca);
                // Nos devuelve a la lista principal de marcas ya actualizada
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // Este método se activa cuando le damos al botón de eliminar una marca
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            // Le pasa el ID seleccionado al servicio para que busque la marca y la borre de la base de datos
            await _productoService.EliminarMarcaAsync(id);
            // Nos regresa a la vista principal para ver que ya no aparece en la lista
            return RedirectToAction(nameof(Index));
        }
    }
}