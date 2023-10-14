using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
  [Table("[produto]")]
  public class Product
  {
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("titulo")]
    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MaxLength(60, ErrorMessage = "Este campo deve ter no máximo 60 caracteres")]
    [MinLength(3, ErrorMessage = "Este campo deve ter no mínimo 3 caracteres")]
    public string Title { get; set; }

    [Column("descricao")]
    [MaxLength(1024, ErrorMessage = "Este campo deve ter no máximo 1024 caracteres")]
    public string Description { get; set; }

    [Column("preco")]
    [Required(ErrorMessage = "Este campo é obrigatório")]
    [Range(0.01, int.MaxValue, ErrorMessage = "O preco deve ser maior que 0")]
    public decimal Price { get; set; }

    [Column("id_categoria")]
    [Required(ErrorMessage = "Este campo é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "Categoria inválida")]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
  }
}