using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensorialStore.DAL.Entities
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [Required]
        public int MarcaId { get; set; }

        // Relaciones (Llaves foráneas)
        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }

        [ForeignKey("MarcaId")]
        public Marca Marca { get; set; }

        public string? ImagenUrl { get; set; }
    }
}