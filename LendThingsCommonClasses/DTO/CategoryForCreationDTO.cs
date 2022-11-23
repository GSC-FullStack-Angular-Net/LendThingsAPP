using System.ComponentModel.DataAnnotations;

namespace LendThingsCommonClasses.DTO
{
    public class CategoryForCreationDTO
    {
        public CategoryForCreationDTO()
        {
        }
        [Required(ErrorMessage = "Description is mandatory")]
        public string Description { get; set; }
    }
}