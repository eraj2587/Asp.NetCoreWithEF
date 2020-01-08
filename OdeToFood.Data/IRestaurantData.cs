using System;
using System.Collections.Generic;
using System.Text;
using OdeToFood.Core;
using System.Linq;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAllRestaurantByName(string name = null);
        Restaurant GetRestaurantById(int Id);
        Restaurant Update(Restaurant UpdatedRestaurant);
        void Add(Restaurant NewRestaurant);
        int Commit();

    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>
            {
                new Restaurant { Id=1, Name="Scott's Pizza", Location="Sydney", Cuisine=Cuisinetype.Italian},
                new Restaurant { Id=2, Name="Ornesto Cafe", Location="Melbourne", Cuisine=Cuisinetype.Mexican},
                new Restaurant { Id=3, Name="Shivam Restaurant", Location="Perth", Cuisine=Cuisinetype.Indian}
            };
        }

        public void Add(Restaurant NewRestaurant)
        {
            restaurants.Add(NewRestaurant);
        }

        public int Commit()
        {
            return 0;
        }

        public IEnumerable<Restaurant> GetAllRestaurantByName(string name=null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public Restaurant GetRestaurantById(int Id)
        {
            return restaurants.SingleOrDefault(x => x.Id == Id);
        }

        public Restaurant Update(Restaurant UpdatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(x => x.Id == UpdatedRestaurant.Id);

            if (restaurant != null)
            {
                restaurant.Name = UpdatedRestaurant.Name;
                restaurant.Location = UpdatedRestaurant.Location;
                restaurant.Cuisine = UpdatedRestaurant.Cuisine;
            }

            return restaurant;
        }
    }
}
