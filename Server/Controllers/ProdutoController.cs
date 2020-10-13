using blazor_mysql2.Server;
using Microsoft.AspNetCore.Mvc;
using blazor_mysql2.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class ProdutoController : Controller
{
    private readonly AppDbContext db;

    public ProdutoController(AppDbContext db)
    {
        this.db = db;
    }

    [HttpGet]
    [Route("List")]
    public async Task<IActionResult> Get()
    {
        var produto = await db.Produto.ToListAsync();
        return Ok(produto);
    }

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult> Post([FromBody] Produto produto)
    {
        try
        {
            var newProduct = new Product
            {
                Nome = product.Nome,
                Descricao = product.Descricao,
                Preco = product.Preco
            };

            db.Add(newProduct);
            await db.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return View(e);
        }
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> Put([FromBody] Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        db.Entry(product).State = EntityState.Modified;
        try
        {
            await db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw (ex);
        }
        return NoContent();
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var prod = await db.Produto.FindAsync(id);
        if(prod == null)
        {
            return NotFound();
        }
        db.Produto.Remove(prod);
        await db.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    [Route("GetByID")]
    public async Task<ActionResult> Get([FromQuery] int id)
    {
        var produto = await db.Produto.SingleOrDefaultAsync(x => x.ID == Convert.ToInt32(id));
        return Ok(produto);
    }

}