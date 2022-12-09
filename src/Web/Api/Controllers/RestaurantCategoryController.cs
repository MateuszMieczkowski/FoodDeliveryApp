using AutoMapper;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Web.Api.Models.RestaurantDtos;

namespace Web.Api.Controllers;

[ApiController]
[Route("/api/category")]
public class RestaurantCategoryController : ControllerBase
{
    private readonly IRestaurantCategoryRepository _restaurantCategoryRepository;
    private readonly IMapper _mapper;

    public RestaurantCategoryController(IRestaurantCategoryRepository restaurantCategoryRepository, IMapper mapper)
    {
        _restaurantCategoryRepository = restaurantCategoryRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<RestaurantCategoryDto>> GetCategories()
    {
        var categoryDtos = _mapper.Map<IEnumerable<RestaurantCategoryDto>>(_restaurantCategoryRepository.Categories);
        return Ok(categoryDtos);
    }
}
