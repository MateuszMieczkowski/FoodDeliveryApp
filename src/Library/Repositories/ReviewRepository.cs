using Library.DataPersistence;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _dbContext;
    private const int _maxPageSize = 30;
    public IEnumerable<RestaurantReview>? Reviews { get; set; }

    public ReviewRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        Reviews = _dbContext.RestaurantReviews;
    }

    public async Task AddReviewAsync(RestaurantReview review)
    {
        await _dbContext.RestaurantReviews.AddAsync(review);
    }

    public void DeleteReview(RestaurantReview review)
    {
        _dbContext.RestaurantReviews.Remove(review);
    }

    public async Task<List<RestaurantReview>> GetAllReviewsAsync()
    {
        return await _dbContext.RestaurantReviews.ToListAsync();
    }

    public async Task<RestaurantReview?> GetReviewAsync(int reviewId)
    {
        return await _dbContext.RestaurantReviews.FindAsync(reviewId);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<List<RestaurantReview>> GetRestaurantReviewsAsync(int restaurantId, int pageNumber = 1, int pageSize = 10)
    {
        if(pageSize > _maxPageSize)
        {
            pageSize = _maxPageSize;
        }
        if(pageSize <= 0)
        {
            pageSize = 10;
        }

        if(pageNumber <= 0)
        {
            pageNumber = 1;
        }

        var reviews = await _dbContext.RestaurantReviews.Where(r => r.RestaurantId == restaurantId)
                                                  .Skip((pageNumber - 1) * pageSize)
                                                  .Take(pageSize)
                                                  .ToListAsync();

        return reviews;
    }

    public async Task<int> GetReviewsCount(int restaurantId)
    {
        return await _dbContext.RestaurantReviews.Where(r => r.RestaurantId == restaurantId).CountAsync();
    }
}
