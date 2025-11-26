using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mare_Bogdan_Lab2_EB.Models;

namespace Mare_Bogdan_Lab2_EB.Data
{
    public class Mare_Bogdan_Lab2_EBContext : DbContext
    {
        public Mare_Bogdan_Lab2_EBContext (DbContextOptions<Mare_Bogdan_Lab2_EBContext> options)
            : base(options)
        {
        }

        public DbSet<Mare_Bogdan_Lab2_EB.Models.Book> Book { get; set; } = default!;
    }
}
