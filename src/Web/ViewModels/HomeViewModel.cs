using Library.Entities;

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
