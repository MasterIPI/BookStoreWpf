using BookStore.Api.Entities.Enums;
using System;

namespace BookStore.Api.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public long UserRoleId { get; set; }

        public UserGender Gender { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
