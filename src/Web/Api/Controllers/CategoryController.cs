using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Web.Api.Controllers;

[ApiController]
[Route("/api/category")]
public class CategoryController : ControllerBase
{
    private readonly IRestaurantCategoryRepository _restaurantCategoryRepository;

    public CategoryController(IRestaurantCategoryRepository restaurantCategoryRepository)
    {
        _restaurantCategoryRepository = restaurantCategoryRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<RestaurantCategory>> GetCategories()
    {
        return Ok(_restaurantCategoryRepository.Categories);
    }
}
