using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
  [Table("[categoria]")]
  public class Category
  {
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("titulo")]
    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MaxLength(60, ErrorMessage = "Este campo deve ter no máximo 60 caracteres")]
    [MinLength(3, ErrorMessage = "Este campo deve ter no mínimo 3 caracteres")]
    public string Title { get; set; }
  }
}