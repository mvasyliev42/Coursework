using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Models
{
    public class Flowers
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public float Price { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public DateTime CreatedAt { get; set; }

        public Categories Category { get; set; }
    }
}
