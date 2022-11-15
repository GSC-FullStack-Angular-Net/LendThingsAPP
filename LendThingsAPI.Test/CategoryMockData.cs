using LendThingsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendThingsAPI.Test
{
    public static class CategoryMockData
    {
        private static List<Category> Categories { get; set; }

        static public List<Category> GetCategoriesList()
        {
            if (Categories is null)
            {
                Categories = new List<Category>() {
                new Category { Id = 1, Description = "Machinery" },
                new Category { Id = 2, Description = "School" },
                new Category { Id = 3, Description = "Computer" }
            };
            }
            return Categories;
        }
    }
}
