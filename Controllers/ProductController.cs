using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
  [Route("v1/products")]
  public class ProductController : ControllerBase
  {
    [HttpGet]
    [Route("")]
    [AllowAnonymous]
    public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext db)
    {
      var products = await db
        .Products
        .Include(product => product.Category)
        .AsNoTracking()
        .ToListAsync();
      return Ok(products);
    }

    [HttpGet]
    [Route("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<Product>> GetById(int id, [FromServices] DataContext db)
    {
      var product = await db
      .Products
      .Include(product => product.Category)
      .AsNoTracking()
      .FirstOrDefaultAsync(product => product.Id == id);
      if (product == null) return NotFound(new { message = "Produto não encontrada" });
      return Ok(product);
    }

    [HttpGet]
    [Route("products/{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<Product>> GetByCategory(int CategoryId, [FromServices] DataContext db)
    {
      var products = await db
      .Products
      .Include(product => product.Category)
      .AsNoTracking()
      .Where(procut => procut.CategoryId == CategoryId)
      .ToListAsync();
      return Ok(products);
    }

    [HttpPost]
    [Route("")]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult<Product>> Post([FromBody] Product body, [FromServices] DataContext db)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      try
      {
        db.Products.Add(body);
        await db.SaveChangesAsync();
        return Ok(body);
      }
      catch
      {
        return BadRequest(new { message = "Erro ao tentar criar product" });
      }
    }

    [HttpPut]
    [Route("{id:int}")]
    [Authorize(Roles = "manager")]
    public async Task<ActionResult<Product>> Put(int id, [FromBody] Product body, [FromServices] DataContext db)
    {
      if (body.Id != id) return NotFound(new { message = "Produto não encontrada" });
      if (!ModelState.IsValid) return BadRequest(ModelState);
      try
      {
        db.Products.Entry(body).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return Ok(body);
      }
      catch (DbUpdateConcurrencyException)
      {
        return BadRequest(new { message = "Este registro já foi atualizado" });
      }
      catch (Exception)
      {
        return BadRequest(new { message = "Erro ao tentar criar product" });
      }
    }

    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = "manager")]
    public async Task<ActionResult<Product>> Delete(int id, [FromServices] DataContext db)
    {
      var product = await db.Products.FirstOrDefaultAsync(product => product.Id == id);
      if (product == null)
      {
        return NotFound(new { message = "Produto não encontrada" });
      }
      try
      {
        db.Products.Remove(product);
        await db.SaveChangesAsync();
        return Ok(new { message = "Produto removida com sucesso" });
      }
      catch
      {
        return BadRequest(new { message = "Erro ao tentar deletar product" });
      }
    }
  }
}