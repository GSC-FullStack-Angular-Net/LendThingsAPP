namespace LendThingsAPI.Models
{
    public class Person : BaseEntity
    {
        public Person()
        {
        }

        public Person(int idPerson, string name, string phoneNumber, string email)
        {
            Id = idPerson;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
