using Web.Api.Models.RestaurantDtos;

namespace Web.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<RestaurantDto>? Restaurants { get; set; }

        public HomeViewModel(IEnumerable<RestaurantDto>? restaurants)
        {
            Restaurants = restaurants;
        }
    }
}
