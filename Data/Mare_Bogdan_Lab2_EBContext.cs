using Microsoft.EntityFrameworkCore;
using Mare_Bogdan_Lab2_EB.Models;

namespace Mare_Bogdan_Lab2_EB.Data
{
    public class Mare_Bogdan_Lab2_EBContext : DbContext
    {
        public Mare_Bogdan_Lab2_EBContext(
            DbContextOptions<Mare_Bogdan_Lab2_EBContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<Customer> Customer { get; set; } = default!;
        public DbSet<Genre> Genre { get; set; } = default!;

        public DbSet<Author> Author { get; set; } = default!;

        public DbSet<Order> Order { get; set; } = default!;

    }
}
