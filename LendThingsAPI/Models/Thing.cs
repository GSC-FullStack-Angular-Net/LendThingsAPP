namespace LendThingsAPI.Models
{
    public class Thing
    {
        public Thing()
        {
        }

        public Thing(string description, DateOnly creationDate, Category? category)
        {
            Description = description;
            CreationDate = creationDate;
            Category = category;
        }

        public string Description {get; set;}
        public DateOnly CreationDate { get; } = new DateOnly();

        public Category? Category { get; set; }
    }
}
