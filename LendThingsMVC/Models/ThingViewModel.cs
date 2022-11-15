using LendThingsAPI.Models;

namespace LendThingsMVC.Models
{
    public class ThingViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; } = DateTime.UtcNow;
        public Category Category { get; set; }
    }
}
