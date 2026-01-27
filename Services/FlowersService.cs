using Coursework.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.Models;

namespace Coursework.Services
{
    internal class FlowersService
    {

        public IServiceProvider _db;



        public FlowersService(IServiceProvider db) 
        {
            this._db = db;
        }

        public List<Models.Flowers> getListFlowers()
        {
            using (var scope = _db.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // create SELECT


                var flowers = dbContext.Flowers.OrderBy(p => p.Name).ToList();

                return flowers;
            }
        }

        public void addFlower(int CategoryId, 
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
                dbContext.SaveChanges();


            }

        }
    }
}
