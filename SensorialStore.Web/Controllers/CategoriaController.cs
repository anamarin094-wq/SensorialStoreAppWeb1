using Microsoft.AspNetCore.Mvc;
using SensorialStore.BLL;
using SensorialStore.DAL.Entities;
using System.Threading.Tasks;

namespace SensorialStore.Web.Controllers
{
    // Este controlador maneja todo lo que pasa en la pantalla de "Gestión de Categorías"
    public class CategoriaController : Controller
    {
        // Traemos el servicio de lógica para poder comunicarnos con la base de datos
        private readonly ProductoService _productoService;

        // El constructor recibe el servicio configurado para usarlo en los métodos de abajo
        public CategoriaController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        // Este método carga la página principal de categorías
        public async Task<IActionResult> Index()
        {
            // Le pide al servicio la lista de todas las categorías guardadas
            var categorias = await _productoService.ObtenerCategoriasAsync();
            // Le manda esa lista a la vista de HTML para que las dibuje en la tabla
            return View(categorias);
        }

        // Este método recibe los datos del formulario cuando creamos una categoría nueva
        [HttpPost]
        public async Task<IActionResult> Crear(Categoria categoria)
        {
            // CORRECCIÓN: Quitamos la lista interna de productos de la validación
            // Esto se hace para que C# no ponga problema porque la lista de productos llega vacía desde el formulario
            ModelState.Remove("Productos");

            // Si los datos obligatorios (como el Nombre) se llenaron bien
            if (ModelState.IsValid)
            {
                // Llama al servicio para guardar la nueva categoría en la base de datos
                await _productoService.CrearCategoriaAsync(categoria);
                // Recarga la misma página para que veamos la categoría recién agregada
                return RedirectToAction(nameof(Index));
            }
            // Si algo falló, igual nos devuelve al inicio de categorías
            return RedirectToAction(nameof(Index));
        }

        // Este método recibe los datos de la categoría que modificamos en el modal de edición
        [HttpPost]
        public async Task<IActionResult> Editar(Categoria categoria)
        {
            // También quitamos la lista interna de productos para evitar errores de validación
            ModelState.Remove("Productos");

            // Si el formulario es válido y tiene los datos correctos
            if (ModelState.IsValid)
            {
                // Llama al servicio para actualizar los datos viejos por los nuevos en SQL
                await _productoService.ActualizarCategoriaAsync(categoria);
                // Nos regresa a la lista de categorías actualizada
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // Este método recibe el ID de la categoría cuando le damos al botón de la papelera
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            // Llama al servicio para borrar ese registro usando su número de ID único
            await _productoService.EliminarCategoriaAsync(id);
            // Nos regresa a la pantalla principal para ver los cambios
            return RedirectToAction(nameof(Index));
        }
    }
}