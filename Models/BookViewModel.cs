namespace Mare_Bogdan_Lab2_EB.Models
{
    public class BookViewModel
    {
        public int ID { get; set; }

        public string Title { get; set; } = string.Empty;

        public decimal Price { get; set; }

        // numele complet al autorului (din Author.FullName)
        public string FullName { get; set; } = string.Empty;
    }
}
