using Microsoft.EntityFrameworkCore;
using SensorialStore.DAL;
using SensorialStore.DAL.Entities;

namespace SensorialStore.BLL
{
    public class ProductoService
    {
        // Esta variable es la que nos da acceso directo a la base de datos
        private readonly SensorialDbContext _context;

        // El constructor recibe la conexión de la base de datos para que podamos usarla en toda esta clase
        public ProductoService(SensorialDbContext context)
        {
            _context = context;
        }

        // Trae toda la lista de productos de la base de datos y les pega la información de su Categoría y Marca
        public async Task<List<Producto>> ObtenerTodosLosProductosAsync()
        {
            return await _context.Productos
                .Include(p => p.Categoria) // Une los datos de la tabla Categorias
                .Include(p => p.Marca)     // Une los datos de la tabla Marcas
                .ToListAsync();            // Convierte todo ese resultado en una lista
        }

        // Busca un producto específico usando su ID único (sirve para cargar sus datos al ir a editar o eliminar)
        public async Task<Producto> ObtenerProductoPorIdAsync(int id)
        {
            return await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .FirstOrDefaultAsync(p => p.ProductoId == id); // Devuelve el primero que coincida con el ID
        }

        // Recibe un nuevo producto lleno desde el formulario y lo inserta en la base de datos
        public async Task<bool> CrearProductoAsync(Producto producto)
        {
            _context.Productos.Add(producto); // Lo prepara en la lista para insertar
            var insertados = await _context.SaveChangesAsync(); // Guarda físicamente los cambios en SQL
            return insertados > 0; // Si guardó al menos un registro, devuelve true (verdadero)
        }

        // Recibe un producto que modificamos y actualiza sus valores en la base de datos
        public async Task<bool> ActualizarProductoAsync(Producto producto)
        {
            _context.Productos.Update(producto); // Le avisa a Entity Framework que este producto cambió
            var modificados = await _context.SaveChangesAsync(); // Impacta los cambios en la base de datos
            return modificados > 0; // Si modificó con éxito, devuelve true
        }

        // Busca un producto por su ID y si lo encuentra, lo borra permanentemente
        public async Task<bool> EliminarProductoAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id); // Primero lo busca en la base de datos
            if (producto == null) return false; // Si no existe (por si acaso), se sale y dice false

            _context.Productos.Remove(producto); // Lo marca para borrar
            var eliminados = await _context.SaveChangesAsync(); // Lo borra de verdad en la base de datos
            return eliminados > 0; // Si borró algo, devuelve true
        }

        // Trae todas las categorías limpias para poder llenar los combos/selects del formulario
        public async Task<List<Categoria>> ObtenerCategoriasAsync()
        {
            return await _context.Categorias.ToListAsync();
        }

        // Trae todas las marcas limpias para poder llenar los combos/selects del formulario
        public async Task<List<Marca>> ObtenerMarcasAsync()
        {
            return await _context.Marcas.ToListAsync();
        }

        // ================= OPERACIONES PARA CATEGORÍAS =================
        // (Esto hace exactamente lo mismo de arriba, pero operando sobre la tabla de Categorías)

        // Busca una categoría específica por su ID
        public async Task<Categoria> ObtenerCategoriaPorIdAsync(int id)
        {
            return await _context.Categorias.FindAsync(id);
        }

        // Guarda una nueva categoría que creemos en su respectiva vista
        public async Task<bool> CrearCategoriaAsync(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            return await _context.SaveChangesAsync() > 0;
        }

        // Guarda los cambios de una categoría que hayamos editado
        public async Task<bool> ActualizarCategoriaAsync(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            return await _context.SaveChangesAsync() > 0;
        }

        // Elimina una categoría seleccionada usando su ID
        public async Task<bool> EliminarCategoriaAsync(int id)
        {
            var cat = await _context.Categorias.FindAsync(id);
            if (cat == null) return false;
            _context.Categorias.Remove(cat);
            return await _context.SaveChangesAsync() > 0;
        }

        // ================= OPERACIONES PARA MARCAS =================
        // (Esto hace lo mismo, pero operando sobre la tabla de Marcas)

        // Busca una marca específica por su ID
        public async Task<Marca> ObtenerMarcaPorIdAsync(int id)
        {
            return await _context.Marcas.FindAsync(id);
        }

        // Guarda una nueva marca (por ejemplo: Vogue, Maybelline) en la base de datos
        public async Task<bool> CrearMarcaAsync(Marca marca)
        {
            _context.Marcas.Add(marca);
            return await _context.SaveChangesAsync() > 0;
        }

        // Guarda los cambios de una marca que hayamos editado (como arreglar el país de origen)
        public async Task<bool> ActualizarMarcaAsync(Marca marca)
        {
            _context.Marcas.Update(marca);
            return await _context.SaveChangesAsync() > 0;
        }

        // Elimina una marca de la base de datos por su ID
        public async Task<bool> EliminarMarcaAsync(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null) return false;
            _context.Marcas.Remove(marca);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}