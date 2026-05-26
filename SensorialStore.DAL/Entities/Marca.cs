using System.ComponentModel.DataAnnotations;

namespace SensorialStore.DAL.Entities
{
    public class Marca
    {
        [Key]
        public int MarcaId { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string PaisOrigen { get; set; }

        // Relación: Una marca tiene muchos productos
        public ICollection<Producto> Productos { get; set; }
    }
}