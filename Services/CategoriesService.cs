using Coursework.Data;
using Coursework.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Services
{
    internal class CategoriesService
    {
        public IServiceProvider _db;

        public CategoriesService(IServiceProvider db)
        {
            this._db = db;
        }

        public Categories addCategory(string name)
        {
            using (var scope = _db.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // create INSERT
                var category = new Categories
                {
                    Name = name
                };
                dbContext.Categories.Add(category);
                dbContext.SaveChanges();
                return category;
            }
        }

        public List<Models.Categories> getListCategories()
        {
            using (var scope = _db.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // create SELECT


                var categories = dbContext.Categories.OrderBy(p => p.Name).ToList();

                return categories;
            }

        }
    }
}
