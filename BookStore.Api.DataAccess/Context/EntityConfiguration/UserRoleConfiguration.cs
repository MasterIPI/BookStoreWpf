using BookStore.Api.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Api.DataAccess.Context.EntityConfiguration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasData
            (
                new UserRole()
                {
                    Id = 1,
                    Role = Role.Admin
                },
                new UserRole()
                {
                    Id = 2,
                    Role = Role.User
                }
            );
        }
    }
}
