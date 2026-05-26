using System.ComponentModel.DataAnnotations;

namespace SensorialStore.DAL.Entities
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        // Relación: Una categoría tiene muchos productos
        public ICollection<Producto> Productos { get; set; }
    }
}