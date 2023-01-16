using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public record Category
    {
        public int Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public static IEnumerable<Category> OfferStatusCaregories = new[]
        {
            new Category {Id = 1, Name = "offered"},
            new Category {Id = 2, Name = "waiting_for_acceptance"},
            new Category {Id = 3, Name = "accepted"},
            new Category {Id = 4, Name = "declined"},
        };

        public static IEnumerable<Category> UserJobCategories = new[]
        {
            new Category {Id = 1, Name = "architecture_engineering"},
            new Category {Id = 2, Name = "arts_culture_entertainment"},
            new Category {Id = 3, Name = "business_management_administration"},
            new Category {Id = 4, Name = "communications"},
            new Category {Id = 5, Name = "community_socialServices"},
        };

        public static IEnumerable<Category> UserRoleCategories = new[]
        {
            new Category {Id = 1, Name = "user"},
            new Category {Id = 2, Name = "admin"},
            new Category {Id = 3, Name = "employee"},
        };

        public static IEnumerable<Category> UserIdTypeCategories = new[]
        {
            new Category {Id = 1, Name = "ID"},
            new Category {Id = 2, Name = "passport"},
            new Category {Id = 3, Name = "PESEL"},
        };
    }
}
