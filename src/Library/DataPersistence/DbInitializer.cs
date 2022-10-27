using Library.Entities;

namespace Library.DataPersistence;

public class DbInitializer
{

    private static Dictionary<string, RestaurantCategory> _restaurantCategories = default!;
    private static Dictionary<string, RestaurantCategory> RestaurantCategories
    {
        get
        {
            if (_restaurantCategories is null)
            {
                _restaurantCategories = new Dictionary<string, RestaurantCategory>();
                _restaurantCategories.Add("Pizza", new RestaurantCategory() { Name = "Pizza" });
                _restaurantCategories.Add("Pasta", new RestaurantCategory() { Name = "Pasta" });
                _restaurantCategories.Add("Burgers", new RestaurantCategory() { Name = "Burgers" });
                _restaurantCategories.Add("Asian Cuisine", new RestaurantCategory() { Name = "Asian Cuisine" });
                _restaurantCategories.Add("Polish Cuisine", new RestaurantCategory() { Name = "Polish Cuisine" });
            }
            return _restaurantCategories;
        }
    }

    public static void Seed(ApplicationDbContext dbContext)
    {
        if (!dbContext.RestaurantCategories.Any())
        {
            dbContext.RestaurantCategories.AddRange(RestaurantCategories.Select(r => r.Value));
        }

        if(!dbContext.Restaurants.Any())
        {
            List<Restaurant> restaurants = new()
        {
            new()
            {
                Name = "Jaga Bistro",
                Description = "Serving Pizza, Burgers, Pasta, great cocktails and more!",
                RestaurantCategory = RestaurantCategories["Pizza"]
            },
             new()
            {
                Name = "Italiano",
                Description = "Italian food you will fall love with!",
                RestaurantCategory = RestaurantCategories["Pasta"]
            },
              new()
            {
                Name = "Sushi dushy",
                Description = "Traditional Japanese food.",
                RestaurantCategory = RestaurantCategories["Asian Cuisine"]
            },
               new()
            {
                Name = "Pierogarnia",
                Description = "Pierogis, kartacze, bigos",
                RestaurantCategory = RestaurantCategories["Polish Cuisine"]
            }
        };

            dbContext.Restaurants.AddRange(restaurants);
            dbContext.SaveChanges();
        }
       
    }
}
