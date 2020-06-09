using OdeToFood.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Data.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {

        private List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>
            {
                new Restaurant() { Id = 1, Name = "Pizza's Juan", Cuisine = CuisineType.Italian },
                new Restaurant() { Id = 2, Name = "Tersiguels", Cuisine = CuisineType.French },
                new Restaurant() { Id = 3, Name = "Mango Grove", Cuisine = CuisineType.Indian }
            };
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return restaurants.OrderBy(o => o.Name);
        }
    }
}
