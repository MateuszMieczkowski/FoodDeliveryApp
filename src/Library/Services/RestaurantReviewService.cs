using AutoMapper;
using Library.Entities;
using Library.Exceptions;
using Library.Models;
using Library.Models.RestaurantDtos;
using Library.Repositories.Interfaces;
using Library.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Services;

public class RestaurantReviewService : IRestaurantReviewService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;

    private const int MaxPageSize = 50;

    public RestaurantReviewService(IRestaurantRepository restaurantRepository, IReviewRepository reviewRepository, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }

    public async Task AddReviewAsync(int restaurantId, RestaurantReviewForUpdateDto dto)
    {
        var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId)
            ?? throw new NotFoundException($"There's no such restaurant with id: {restaurantId}.");
		var newReview = _mapper.Map<RestaurantReview>(dto);
        newReview.Restaurant = restaurant;

        await _reviewRepository.AddReviewAsync(newReview);
        await _reviewRepository.SaveChangesAsync();
    }

    public async Task DeleteReviewAsync(int restaurantId, int reviewId)
    {
		_ = await _restaurantRepository.GetRestaurantAsync(restaurantId)
			?? throw new NotFoundException($"There's no such restaurant with id: {restaurantId}.");
		var review = await _reviewRepository.GetReviewAsync(reviewId)
            ?? throw new NotFoundException($"There's no such review with id: {reviewId}.");
		if (review.RestaurantId != restaurantId)
        {
            throw new BadRequestException($"Review with id: {reviewId} does not belong to restaurant with id: {restaurantId}.");
        }

        _reviewRepository.DeleteReview(review);
        await _restaurantRepository.SaveChangesAsync();
    }

    public async Task<PagedResult<RestaurantReviewDto>> GetReviewsAsync(int restaurantId, int pageNumber, int pageSize)
    {
		_ = await _restaurantRepository.GetRestaurantAsync(restaurantId)
			?? throw new NotFoundException($"There's no such restaurant with id: {restaurantId}.");
		if (pageSize > MaxPageSize || pageSize <= 0 || pageNumber <= 0)
        {
            throw new BadRequestException("Wrong page size or page number.");
        }

        (List<RestaurantReview> reviews, int totalResultsCount) = await GetPaginatedResults(restaurantId, pageNumber, pageSize);

        if(pageSize * (pageNumber - 1) > totalResultsCount)
        {
            throw new BadRequestException("Wrong page size or page number.");
        }

        var reviewDtos = _mapper.Map<List<RestaurantReviewDto>>(reviews);
        var result = new PagedResult<RestaurantReviewDto>(reviewDtos, pageNumber, pageSize, totalResultsCount);
        return result;
    }

    private async Task<(List<RestaurantReview>, int)> GetPaginatedResults(int restaurantId, int pageNumber, int pageSize)
    {
        var reviews = _reviewRepository.Reviews!.Where(r => r.RestaurantId == restaurantId);
        var baseQuery = reviews;

        var result = await reviews.Skip(pageSize * (pageNumber - 1))
                                  .Take(pageSize)
                                  .ToListAsync();

        var totalResultsCount = await baseQuery.CountAsync();

        return (result, totalResultsCount);
    }
}
