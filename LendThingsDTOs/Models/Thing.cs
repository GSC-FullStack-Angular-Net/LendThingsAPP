namespace LendThingsCommonClasses.Models
{
    public class Thing:BaseEntity
    {
        public Thing()
        {
        }

        public Thing(int idThing, string description,  Category category)
        {
            Id = idThing;
            Description = description;
            Category = category;
        }

        public string Description {get; set;}
        public DateTime CreationDate { get; } = DateTime.UtcNow;

        public Category Category { get; set; }
    }
}
