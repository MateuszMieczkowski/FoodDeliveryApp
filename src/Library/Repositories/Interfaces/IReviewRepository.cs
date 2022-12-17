using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.Interfaces;

public interface IReviewRepository
{
    IEnumerable<RestaurantReview>? Reviews{ get; set; }

    Task AddReviewAsync(RestaurantReview review);

    void DeleteReview(RestaurantReview review);

    Task<RestaurantReview?> GetReviewAsync(int reviewId);

    IEnumerable<RestaurantReview>? GetRestaurantReviews(int restaurantId, int pageNumber=1, int pageSize = 10);

    Task<List<RestaurantReview>> GetAllReviewsAsync();

    Task<int> SaveChangesAsync();
}
