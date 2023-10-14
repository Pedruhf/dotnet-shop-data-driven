using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
  [Table("[usuario]")]
  public class User
  {
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("login")]
    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MaxLength(20, ErrorMessage = "Este campo deve ter no máximo 20 caracteres")]
    [MinLength(3, ErrorMessage = "Este campo deve ter no mínimo 3 caracteres")]
    public string Username { get; set; }

    [Column("senha")]
    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MaxLength(20, ErrorMessage = "Este campo deve ter no máximo 20 caracteres")]
    [MinLength(3, ErrorMessage = "Este campo deve ter no mínimo 3 caracteres")]
    public string Password { get; set; }

    [Column("permissao")]
    public string Role { get; set; }
  }
}