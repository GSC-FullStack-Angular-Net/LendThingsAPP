namespace LendThingsAPI.Models
{
    public class Category
    {
        public Category()
        {
        }

        public Category(string description, DateOnly creationDate)
        {
            Description = description;
            CreationDate = creationDate;
        }

        public string Description { get; set; }
        public DateOnly CreationDate { get;} = new DateOnly();

    }
}
