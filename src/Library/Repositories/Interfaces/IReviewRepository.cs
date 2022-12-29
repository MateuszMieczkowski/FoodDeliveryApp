using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.Interfaces;

public interface IReviewRepository
{
    IQueryable<RestaurantReview>? Reviews{ get; set; }

    Task<int> GetReviewsCount(int restaurantId);

    Task AddReviewAsync(RestaurantReview review);

    void DeleteReview(RestaurantReview review);

    Task<RestaurantReview?> GetReviewAsync(int reviewId);

    Task<List<RestaurantReview>> GetAllReviewsAsync();

    Task<int> SaveChangesAsync();
}
