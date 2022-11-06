﻿namespace LendThingsAPI.Models
{
    public class Category : BaseEntity
    {
        public Category()
        {
        }

        public Category(int idCategory, string description)
        {
            Id = idCategory;
            Description = description;
        }

        public string Description { get; set; }
        public DateTime CreationDate { get;} = new DateTime();

    }
}
