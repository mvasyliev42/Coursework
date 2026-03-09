using Coursework.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.Models;
using System.Data.Entity;
using System.Diagnostics;
using System.Xml.Linq;

namespace Coursework.Services
{
    internal class FlowersService: Service
    {


        public int CategoryId;
        public string Name;
        public string Description;
        public int Count;
        public float Price;
        public string Type;
        public string Color;

        private IServiceProvider _db;



        public FlowersService(IServiceProvider db) 
        {
            this._db = db;
        }

        public override List<IModel> getList()
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
                }).OrderBy(p => p.Name).ToList<IModel>();

                return flowers;
            }
        }




        public bool save()
        {
            using (var scope = _db.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // create SELECT

                var flower = new Flowers()
                {
                    CategoryId = this.CategoryId,
                    Name = this.Name,
                    Description = this.Description,
                    Count = this.Count,
                    Price = this.Price,
                    Type = this.Type,
                    Color = this.Color,
                    CreatedAt = DateTime.UtcNow
                };


                dbContext.Flowers.Add(flower);
                var count = dbContext.SaveChanges();

                return count > 0 ? true : false;


            }
        }

        public override bool update(int id)
        {
            using (var scope = _db.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // create SELECT
                var flower = dbContext.Flowers.FirstOrDefault(f => f.Id == id);

                if (flower == null)
                {
                    return false;
                }

                flower.CategoryId = this.CategoryId;
                flower.Name = this.Name;
                flower.Description = this.Description;
                flower.Count = this.Count;
                flower.Price = this.Price;
                flower.Type = this.Type;
                flower.Color = this.Color;

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
                var flowers = dbContext.Flowers.OrderBy(p => p.Count).Take(5).ToList();
                return flowers;
            }
        }
    }
}
