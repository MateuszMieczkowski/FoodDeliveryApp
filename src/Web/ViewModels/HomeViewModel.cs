using Library.Entities;
using Web.Api.Models.RestaurantDtos;

namespace Web.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Restaurant>? Restaurants { get; set; }

        public HomeViewModel(IEnumerable<Restaurant>? restaurants)
        {
            Restaurants = restaurants;
        }
    }
}
