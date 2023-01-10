using AutoMapper;
using Library.DataPersistence;
using Library.Entities;
using Library.Models.ProductDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

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
    [Authorize(Roles = "admin,manager")]
    public async Task<ActionResult> CreateProductCategory(ProductCategoryForUpdateDto productCategoryDto)
    {
        var exists = await _dbContext.ProductCategories.AnyAsync(r => r.Name == productCategoryDto.Name);
        if (exists)
        {
            return BadRequest("Such productCategory already exists");
        }
        var newProductCategory = _mapper.Map<ProductCategory>(productCategoryDto);

        await _dbContext.ProductCategories.AddAsync(newProductCategory);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }


    [HttpGet]
    public async Task<ActionResult<List<ProductCategoryDto>>> GetProductCategories()
    {
        var categories = await _dbContext.ProductCategories.ToListAsync();
        var categoriesDtos = _mapper.Map<List<ProductCategoryDto>>(categories);
        return Ok(categoriesDtos);
    }
}