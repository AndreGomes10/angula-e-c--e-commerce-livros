using APIBookstore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace livraria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class livrariaController : ControllerBase
    {
        private readonly TodoContext _context;  // essa é variavel que vai armazenar o banco



        public livrariaController(TodoContext context)
        {

            _context = context;

            foreach (Product x in _context.todoProducts)
                _context.todoProducts.Remove(x);
            _context.SaveChanges();


            _context.todoProducts.Add(new Product { Id = "1", Name = "Book1", Price = 24, Quantity = 1, Category = "action", Img = "img1" });
            _context.todoProducts.Add(new Product { Id = "2", Name = "Book2", Price = 50, Quantity = 1, Category = "action", Img = "img2" });
            _context.todoProducts.Add(new Product { Id = "3", Name = "Book3", Price = 20, Quantity = 2, Category = "action", Img = "img3" });
            _context.todoProducts.Add(new Product { Id = "4", Name = "Book4", Price = 10, Quantity = 1, Category = "action", Img = "img1" });
            _context.todoProducts.Add(new Product { Id = "5", Name = "Book5", Price = 15, Quantity = 5, Category = "action", Img = "img1" });

            _context.SaveChanges();

        }

        // adicionar um produto no banco
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.todoProducts.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProdut), new { id = product.Id }, product);  // get, pra retornar o produto pra ver se inseriu certo o produto no banco
        }

        // aqui vai pegar todos os produtos do banco
        // GET: api/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProdutos()  // retornar uma lista de produtos
        {
            return await _context.todoProducts.ToListAsync();  // pegar todo do banco em forma de lista

        }

        // aqui vai pegar só um produto do banco
        // GET: api/bookstore/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProdut(int id)
        {
            var todoItem = await _context.todoProducts.FindAsync(id.ToString());  // pegar o item no banco

            if (todoItem == null)  // verificar se o item existe
            {
                return NotFound();
            }

            return todoItem;
        }

    }
}
