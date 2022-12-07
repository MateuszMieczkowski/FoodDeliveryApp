using Library.DataPersistence;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _dbContext;

    public IEnumerable<RestaurantReview> Reviews { get; set; }

    public ReviewRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        Reviews = _dbContext.RestaurantReviews;
    }

    public async Task AddReviewAsync(RestaurantReview review)
    {
        await _dbContext.RestaurantReviews.AddAsync(review);
    }

    public async Task<bool> DeleteReviewAsync(int reviewId)
    {
        var review = await GetReviewAsync(reviewId);
        if(review is null)
        {
            return false;
        }
        _dbContext.RestaurantReviews.Remove(review);
        return true;
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
}
