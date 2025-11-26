using Microsoft.EntityFrameworkCore;
using Mare_Bogdan_Lab2_EB.Models;

namespace Mare_Bogdan_Lab2_EB.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Mare_Bogdan_Lab2_EBContext(
                serviceProvider.GetRequiredService<DbContextOptions<Mare_Bogdan_Lab2_EBContext>>()))
            {
                if (context.Book.Any())
                {
                    return; // BD a fost creata anterior
                }

                // 1) Autori
                var authors = new Author[]
                {
                    new Author { FirstName = "Mihail",  LastName = "Sadoveanu" },
                    new Author { FirstName = "George",  LastName = "Calinescu" },
                    new Author { FirstName = "Mircea",  LastName = "Eliade" }
                };

                context.Author.AddRange(authors);
                context.SaveChanges(); // ca sa aiba ID-uri

                // 2) Cărți (cu AuthorID)
                context.Book.AddRange(
                    new Book { Title = "Baltagul", Price = Decimal.Parse("22"), AuthorID = authors[0].ID },
                    new Book { Title = "Enigma Otiliei", Price = Decimal.Parse("18"), AuthorID = authors[1].ID },
                    new Book { Title = "Maytrei", Price = Decimal.Parse("27"), AuthorID = authors[2].ID }
                );

                // 3) Genuri
                context.Genre.AddRange(
                    new Genre { Name = "Roman" },
                    new Genre { Name = "Nuvela" },
                    new Genre { Name = "Poezie" }
                );

                // 4) Clienți
                context.Customer.AddRange(
                    new Customer
                    {
                        Name = "Popescu Marcela",
                        Adress = "Str. Plopilor, nr. 24",
                        BirthDate = DateTime.Parse("1979-09-01")
                    },
                    new Customer
                    {
                        Name = "Mihailescu Cornel",
                        Adress = "Str. Bucuresti, nr. 45, ap. 2",
                        BirthDate = DateTime.Parse("1969-07-08")
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
