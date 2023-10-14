using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
  [Route("v1/categories")]
  public class CategoryController : ControllerBase
  {
    [HttpGet]
    [Route("")]
    [AllowAnonymous]
    [ResponseCache(Duration = 30, VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any)]
    public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext db)
    {
      var categories = await db.Categories.AsNoTracking().ToListAsync();
      return Ok(categories);
    }

    [HttpGet]
    [Route("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<Category>> GetById(int id, [FromServices] DataContext db)
    {
      var category = await db.Categories.AsNoTracking().FirstOrDefaultAsync(category => category.Id == id);
      if (category == null) return NotFound(new { message = "Categoria não encontrada" });
      return Ok(category);
    }

    [HttpPost]
    [Route("")]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult<Category>> Post([FromBody] Category body, [FromServices] DataContext db)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      try
      {
        db.Categories.Add(body);
        await db.SaveChangesAsync();
        return Ok(body);
      }
      catch
      {
        return BadRequest(new { message = "Erro ao tentar criar categoria" });
      }
    }

    [HttpPut]
    [Route("{id:int}")]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult<Category>> Put(int id, [FromBody] Category body, [FromServices] DataContext db)
    {
      if (body.Id != id) return NotFound(new { message = "Categoria não encontrada" });
      if (!ModelState.IsValid) return BadRequest(ModelState);
      try
      {
        db.Categories.Entry(body).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return Ok(body);
      }
      catch (DbUpdateConcurrencyException)
      {
        return BadRequest(new { message = "Este registro já foi atualizado" });
      }
      catch (Exception)
      {
        return BadRequest(new { message = "Erro ao tentar criar categoria" });
      }
    }

    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult<Category>> Delete(int id, [FromServices] DataContext db)
    {
      var category = await db.Categories.FirstOrDefaultAsync(category => category.Id == id);
      if (category == null)
      {
        return NotFound(new { message = "Categoria não encontrada" });
      }
      try
      {
        db.Categories.Remove(category);
        await db.SaveChangesAsync();
        return Ok(new { message = "Categoria removida com sucesso" });
      }
      catch
      {
        return BadRequest(new { message = "Erro ao tentar deletar categoria" });
      }
    }
  }
}