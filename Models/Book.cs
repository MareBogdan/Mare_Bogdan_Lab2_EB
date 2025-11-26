using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mare_Bogdan_Lab2_EB.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }

        // === GEN ===
        public int? GenreID { get; set; }
        public Genre? Genre { get; set; }

        // === AUTHOR (nou) ===
        public int? AuthorID { get; set; }     // cheia străină
        public Author? Author { get; set; }    // navigation property

        public ICollection<Order>? Orders { get; set; }
    }
}
