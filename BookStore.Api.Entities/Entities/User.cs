
using BookStore.Api.Entities.Enums;

namespace BookStore.Api.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public UserGender Gender { get; set; }
    }
}
