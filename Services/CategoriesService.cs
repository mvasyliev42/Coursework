using Coursework.Data;
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
