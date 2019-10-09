using BookStore.Api.Entities;
using BookStore.Api.Entities.Enums;

namespace BookStore.Api
{
    public class UserRole : BaseEntity
    {
        public Role Role { get; set; }
    }
}
