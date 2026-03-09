using Coursework.Data;
using Coursework.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Coursework.Services
{
    internal class CategoriesService: Service
    {


        public string name;

        public int inserted_id;


        public IServiceProvider _db;

        public CategoriesService(IServiceProvider db)
        {
            this._db = db;
        }

        public override bool save()
        {
            using (var scope = _db.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // create INSERT
                var category = new Categories
                {
                    Name = this.name
                };
                dbContext.Categories.Add(category);
                dbContext.SaveChanges();
                this.inserted_id = category.Id;
                return true;
            }
        }

        public override bool update(int id)
        {
            using (var scope = _db.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // create SELECT
                var category = dbContext.Categories.FirstOrDefault(f => f.Id == id);

                if (category == null)
                {
                    return false;
                }

                category.Name = this.name;

                var count = dbContext.SaveChanges();

                return count > 0 ? true : false;
            }
        }

        public override List<IModel> getList()
        {
            using (var scope = _db.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // create SELECT

                var categories = dbContext.Categories
                                          .OrderBy(p => p.Name)
                                          .ToList<IModel>(); // Explicitly cast to List<IModel>

                return categories;
            }
        }
    }
}
