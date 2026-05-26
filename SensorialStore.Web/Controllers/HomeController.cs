using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SensorialStore.BLL;
using SensorialStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SensorialStore.Web.Controllers
{
    // Este es el controlador principal que maneja la pantalla de inicio y el catálogo de productos
    public class HomeController : Controller
    {
        // Traemos nuestro servicio de lógica de negocio para interactuar con los productos
        private readonly ProductoService _productoService;

        // El constructor recibe el servicio para poder usar sus métodos aquí adentro
        public HomeController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        // Este método privado sirve para llenar los combos de Categorías y Marcas en los formularios
        private async Task CargarListasDesplegablesAsync()
        {
            // Guardamos las listas en el ViewBag para que la vista de HTML las pueda leer fácilmente
            ViewBag.Categorias = await _productoService.ObtenerCategoriasAsync() ?? new List<Categoria>();
            ViewBag.Marcas = await _productoService.ObtenerMarcasAsync() ?? new List<Marca>();
        }

        // Este método carga la página principal del catálogo de maquillaje
        public async Task<IActionResult> Index()
        {
            // Primero cargamos las listas para los modales de crear y editar
            await CargarListasDesplegablesAsync();
            // Traemos todos los productos guardados con sus marcas y categorías asociadas
            var productos = await _productoService.ObtenerTodosLosProductosAsync();
            // Le pasamos los productos a la vista para que los muestre en la tabla principal
            return View(productos);
        }

        // Este método recibe los datos cuando creamos un nuevo producto desde el modal
        [HttpPost]
        public async Task<IActionResult> Crear(Producto producto, string ImagenUrl)
        {
            // Quitamos Categoria y Marca de la validación porque son objetos complejos
            // Solo nos interesa validar sus IDs (CategoriaId y MarcaId)
            ModelState.Remove("Categoria");
            ModelState.Remove("Marca");

            // Si todos los campos obligatorios del formulario están bien diligenciados
            if (ModelState.IsValid)
            {
                // Asigna directamente el link de internet que pegamos al producto
                producto.ImagenUrl = ImagenUrl;

                // Guardamos el objeto producto pasándolo dentro de los paréntesis del servicio
                await _productoService.CrearProductoAsync(producto);

                // Recargamos la vista principal para ver el nuevo producto con su foto cargada
                return RedirectToAction(nameof(Index));
            }

            // Si la validación falla por alguna razón, volvemos a cargar las listas y los productos para no romper la pantalla
            await CargarListasDesplegablesAsync();
            var productos = await _productoService.ObtenerTodosLosProductosAsync();
            return View("Index", productos);
        }

        // Este método recibe los datos modificados cuando editamos un producto existente
        [HttpPost]
        public async Task<IActionResult> Editar(Producto producto, string ImagenUrl)
        {
            // Quitamos los objetos complejos de la validación para evitar errores
            ModelState.Remove("Categoria");
            ModelState.Remove("Marca");

            // Si el formulario es válido con los nuevos cambios
            if (ModelState.IsValid)
            {
                // Mantiene el manejo por link de internet también al editar el producto
                producto.ImagenUrl = ImagenUrl;

                // Llama al servicio para guardar la actualización en la base de datos
                await _productoService.ActualizarProductoAsync(producto);
                // Nos regresa al inicio para ver los cambios reflejados
                return RedirectToAction(nameof(Index));
            }

            // Si los datos no son válidos, recargamos la página de inicio mostrando los productos actuales
            await CargarListasDesplegablesAsync();
            var productos = await _productoService.ObtenerTodosLosProductosAsync();
            return View("Index", productos);
        }

        // Este método se activa al darle clic al botón de eliminar un producto
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            // Le pasa el ID al servicio para que busque el producto en SQL y lo borre
            await _productoService.EliminarProductoAsync(id);
            // Nos redirige al inicio para actualizar la lista de la tabla
            return RedirectToAction(nameof(Index));
        }
    }
}