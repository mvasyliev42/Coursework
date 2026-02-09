using Coursework.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.Models;
using System.Data.Entity;

namespace Coursework.Services
{
    internal class FlowersService
    {

        private IServiceProvider _db;



        public FlowersService(IServiceProvider db) 
        {
            this._db = db;
        }

        public List<Data.FlowerDto> getListFlowers()
        {
            using (var scope = _db.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // create SELECT


                var flowers = dbContext.Flowers.Include(p => p.Category).Select(f => new FlowerDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    CategoryName = f.Category != null ? f.Category.Name : "",
                    Type = f.Type,
                    Count = f.Count,
                    Price = f.Price,
                    Color = f.Color,
                    Description = f.Description
                }).OrderBy(p => p.Name).ToList();

                return flowers;
            }
        }

        public bool addFlower(int CategoryId, 
            string Name, 
            string Description, 
            int Count, 
            float Price, 
            string Type, 
            string Color)
        {
            using (var scope = _db.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // create SELECT

                var flower = new Flowers()
                {
                    CategoryId = CategoryId,
                    Name = Name,
                    Description = Description,
                    Count = Count,
                    Price = Price,
                    Type = Type,
                    Color = Color,
                    CreatedAt = DateTime.UtcNow
                };

                
                dbContext.Flowers.Add(flower);
                var count = dbContext.SaveChanges();

                return count > 0? true: false;


            }

        }

        public bool UpdateFlower(int Id,
            int CategoryId,
            string Name,
            string Description,
            int Count,
            float Price,
            string Type,
            string Color)
        {
            using (var scope = _db.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // create SELECT
                var flower = dbContext.Flowers.FirstOrDefault(f => f.Id == Id);

                if (flower == null)
                {
                    return false;
                }

                flower.CategoryId = CategoryId;
                flower.Name = Name;
                flower.Description = Description;
                flower.Count = Count;
                flower.Price = Price;
                flower.Type = Type;
                flower.Color = Color;

                var count = dbContext.SaveChanges();

                return count > 0 ? true : false;
            }
        }


        public List<Flowers> getTop5CountsFlowers()
        {
            using (var scope = _db.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // create SELECT

                var flowers = dbContext.Flowers.OrderByDescending(p => p.Count).Take(5).ToList();

                return flowers;
            }
        }

        public List<Flowers> getTop5FadingFlowers()
        {
            using (var scope = _db.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // create SELECT
                var flowers = dbContext.Flowers.OrderByDescending(p => p.Count).Take(5).ToList();
                return flowers;
            }
        }
    }
}
