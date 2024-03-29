﻿using AutoMapper;
using Library.Authorization;
using Library.Entities;
using Library.Exceptions;
using Library.Models;
using Library.Models.RestaurantDtos;
using Library.Repositories.Interfaces;
using Library.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Library.Services;

public class RestaurantService : IRestaurantService
{
	private readonly IRestaurantRepository _restaurantRepository;
	private readonly IRestaurantCategoryRepository _restaurantCategoryRepository;
	private readonly IMapper _mapper;
	private readonly IRequirementService _requirementService;

	private const int MaxPageSize = 50;

	public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper, IRestaurantCategoryRepository restaurantCategoryRepository, IRequirementService requirementService)
	{
		_restaurantRepository = restaurantRepository;
		_restaurantCategoryRepository = restaurantCategoryRepository;
		_requirementService = requirementService;

		_mapper = mapper;
	}

	public async Task<PagedResult<RestaurantDto>> GetRestaurantsAsync
		(string? name, string? city, string? category, string? searchQuery, int pageNumber = 1, int pageSize = 10)
	{
		name = name?.Trim();
		city = city?.Trim();
		category = category?.Trim();
		searchQuery = searchQuery?.Trim();

		if (pageSize > MaxPageSize || pageSize <= 0 || pageNumber <= 0)
		{
			throw new BadRequestException("Wrong page size or page number.");
		}

		(List<Restaurant> restaurants, int totalResultsCount) = await GetPaginatedResults
			(name, city, category, searchQuery, pageNumber, pageSize);

		if ((pageNumber - 1) * pageSize > totalResultsCount)
		{
			throw new BadRequestException("Wrong page size or page number.");
		}

		var restaurantDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
		var result = new PagedResult<RestaurantDto>(restaurantDtos, pageNumber, pageSize, totalResultsCount);
		return result;
	}

	private async Task<(List<Restaurant>, int)> GetPaginatedResults(string? name, string? city, string? category, string? searchQuery, int pageNumber, int pageSize)
	{
		var restaurants = _restaurantRepository.Restaurants;
		if (!string.IsNullOrEmpty(name))
		{
			restaurants = restaurants.Where(r => r.Name == name);
		}
		if (!string.IsNullOrEmpty(city))
		{
			restaurants = restaurants.Where(r => r.City == city);
		}
		if (!string.IsNullOrEmpty(category))
		{
			restaurants = restaurants.Where(r => r.RestaurantCategory.Name == category);
		}
		if (!string.IsNullOrEmpty(searchQuery))
		{
			restaurants = restaurants.Where(r => r.Name.Contains(searchQuery) || r.Description.Contains(searchQuery)
			   || r.City.Contains(searchQuery) || r.RestaurantCategory.Name.Contains(searchQuery));
		}
		var baseQuery = restaurants;

		var result = await restaurants.Skip(pageSize * (pageNumber - 1))
									  .Take(pageSize)
									  .OrderBy(x => x.Rating)
									  .ToListAsync();

		int totalResultsCount = await baseQuery.CountAsync();

		return (result, totalResultsCount);
	}

	public async Task<RestaurantDto> GetRestaurantAsync(int restaurantId)
	{
		var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId)
			?? throw new NotFoundException($"There's no such restaurant with id:{restaurantId}.");
		var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
		return restaurantDto;
	}

	public async Task DeleteRestaurantAsync(int restaurantId)
	{
		var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId)
			?? throw new NotFoundException($"There's no such restaurant with id:{restaurantId}.");
		await _restaurantRepository.DeleteRestaurantAsync(restaurant);
		await _restaurantRepository.SaveChangesAsync();
	}

	public async Task<RestaurantDto> CreateRestaurantAsync(RestaurantForUpdateDto dto)
	{
		var category = await _restaurantCategoryRepository.GetRestaurantCategory(dto.RestaurantCategoryName) ?? new RestaurantCategory { Name = dto.RestaurantCategoryName };

		var newRestaurant = _mapper.Map<Restaurant>(dto);
		newRestaurant.RestaurantCategory = category;

		await _restaurantRepository.AddRestaurantAsync(newRestaurant);
		await _restaurantRepository.SaveChangesAsync();

		var newRestaurantDto = _mapper.Map<RestaurantDto>(newRestaurant);
		return newRestaurantDto;
	}

	public async Task UpdateRestaurantAsync(int restaurantId, RestaurantForUpdateDto dto)
	{
		var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId)
			?? throw new NotFoundException($"There's no such restaurant with id:{restaurantId}.");
		await AuthorizeManager(restaurantId);

		var category = await _restaurantCategoryRepository.GetRestaurantCategory(dto.RestaurantCategoryName)
					   ?? new RestaurantCategory { Name = dto.RestaurantCategoryName };

		restaurant.Name = dto.Name;
		restaurant.Description = dto.Description;
		restaurant.RestaurantCategory = category;
		restaurant.City = dto.City;
		restaurant.ImageUrl = dto.ImageUrl;

		await _restaurantRepository.SaveChangesAsync();
	}

	public IEnumerable<RestaurantCategoryDto> GetRestaurantCategories()
	{
		var categories = _restaurantCategoryRepository.Categories;
		var categoryDtos = _mapper.Map<IEnumerable<RestaurantCategoryDto>>(categories);
		return categoryDtos;
	}

	private async Task AuthorizeManager(int restaurantId)
	{
		var authorizationResult =
			await _requirementService.AuthorizeAsync(new RestaurantManagerRequirement(restaurantId));
		if (!authorizationResult.Succeeded)
		{
			throw new ForbiddenException();
		}
	}

	public async Task<List<string>> GetCities()
	{
		var cities = await _restaurantRepository.Restaurants
												.Select(x => x.City)
												.Distinct()
												.ToListAsync();
		return cities;
	}
}
