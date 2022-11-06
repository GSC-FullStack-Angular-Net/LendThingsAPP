namespace LendThingsAPI.Models
{
    public class Loan
    {
        public Loan()
        {
        }

        public Loan(int idLoan, DateTime returnDate, Thing? thing, Person? person)
        {
            Id = idLoan;
            ReturnDate = returnDate;
            Thing = thing;
            Person = person;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public DateTime ReturnDate { get; set; }
        public Thing? Thing { get; set; }
        public Person? Person { get; set; }
    }
}
