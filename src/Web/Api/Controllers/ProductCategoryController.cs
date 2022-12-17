using AutoMapper;
using Library.DataPersistence;
using Library.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Api.Models.ProductDtos;
using Web.Pages.Shared;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("api/productCategories")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductCategoryController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductCategory(ProductCategoryForUpdateDto productCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _dbContext.ProductCategories.AnyAsync(r => r.Name.ToLower() == productCategoryDto.Name.ToLower()))
            {
                return BadRequest("Such productCategory already exists");
            }
            var newProductCategory = _mapper.Map<ProductCategory>(productCategoryDto);

            await _dbContext.ProductCategories.AddAsync(newProductCategory);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet]
        public ActionResult<IEnumerable<ProductCategoryDto>> GetProductCategories()
        {
            var categories = _dbContext.ProductCategories;
            var categoriesDtos = _mapper.Map<IEnumerable<ProductCategoryDto>>(categories);
            return Ok(categoriesDtos);
        }
    }
}
