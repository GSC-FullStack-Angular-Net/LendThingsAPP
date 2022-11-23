using System.ComponentModel.DataAnnotations;

namespace LendThingsCommonClasses.DTO
{
        public class PersonForPartialUpdateDTO
        {
        public PersonForPartialUpdateDTO()
        {
        }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string? Name { get; set; }
            public string? PhoneNumber { get; set; }
            [EmailAddress(ErrorMessage = "The email is not valid.")]
            public string? Email { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        }
    
}
