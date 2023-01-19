﻿namespace API.Dtos.User
{
    public class CreateUserDto
    {
        public int? RoleId { get; set; }

        public bool? Active { get; set; }

        public bool? Anonymous { get; set; }

        public string? Email { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int? JobTypeId { get; set; }

        public int? IncomeLevel { get; set; }

        public int? IdTypeId { get; set; }

        public string? IdNumber { get; set; }
    }
}
