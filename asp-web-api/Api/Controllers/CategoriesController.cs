using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public CategoriesController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _databaseContext.Categories.ToListAsync();

            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category categoryRequest)
        {
            categoryRequest.Id = Guid.NewGuid();

            await _databaseContext.Categories.AddAsync(categoryRequest);
            await _databaseContext.SaveChangesAsync();

            return Ok(categoryRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategory([FromRoute] Guid id)
        {
            var category = await _databaseContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, Category categoryRequest)
        {
            var category = await _databaseContext.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = categoryRequest.Name;
            //category.Transactions = categoryRequest.Transactions;

            await _databaseContext.SaveChangesAsync();

            return Ok(category);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category = await _databaseContext.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _databaseContext.Categories.Remove(category);
            await _databaseContext.SaveChangesAsync();

            return Ok();
        }
    }
}
