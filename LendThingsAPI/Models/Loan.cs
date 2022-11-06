namespace LendThingsAPI.Models
{
    public class Loan
    {
        public DateOnly Date { get; set; }
        public DateOnly ReturnDate { get; set; }
        public Thing? Thing { get; set; }
        public Person? Person { get; set; }
    }
}
