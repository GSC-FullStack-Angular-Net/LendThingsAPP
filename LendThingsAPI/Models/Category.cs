namespace LendThingsAPI.Models
{
    public class Category
    {
        public Category()
        {
        }

        public Category(int idCategory, string description)
        {
            Id = idCategory;
            Description = description;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get;} = new DateTime();

    }
}
