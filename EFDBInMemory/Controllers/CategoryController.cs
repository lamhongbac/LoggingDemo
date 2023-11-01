using EFDBInMemory.Database;
using EFDBInMemory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFDBInMemory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        InMemoryDbContext inMemoryDbContext;
        public CategoryController(InMemoryDbContext inMemoryDbContext) 
        {
            this.inMemoryDbContext = inMemoryDbContext;

        }
        /// <summary>
        /// Task<List<ProductCategory>>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCategories")]
        public async Task<IActionResult> GetCatgories()
        {
            return Ok(inMemoryDbContext.Categories.ToList());
        }

        [HttpPost]
        [Route("CreateCat")]
        public async Task<IActionResult> CreateCat(ProductCategory model)
        {
            model.Id = Guid.NewGuid();
            inMemoryDbContext.Categories.Add(model);
            inMemoryDbContext.SaveChanges();
            return Ok(inMemoryDbContext.Categories.ToList());
        }
    }
}
