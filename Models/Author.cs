using System.Collections.Generic;

namespace Mare_Bogdan_Lab2_EB.Models
{
    public class Author
    {
        public int ID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => FirstName + " " + LastName;


        // un autor poate avea mai multe cărți
        public ICollection<Book>? Books { get; set; }
    }
}
