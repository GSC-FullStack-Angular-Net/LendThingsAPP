namespace LendThingsAPI.Controllers
{
    public class ThingForPartialUpdateDTO
    {
        public int? Id { get; set; }
        public string? Description { get; set; }
        public DateTime? CreationDate { get; } = DateTime.UtcNow;
        public int? Category { get; }
    }
}