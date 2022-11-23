using System.ComponentModel.DataAnnotations;

namespace LendThingsCommonClasses.DTO
{
    public class CategoryBaseDTO
    {
        public CategoryBaseDTO()
        {
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is mandatory")]
        public string Description { get; set; }
    }
}