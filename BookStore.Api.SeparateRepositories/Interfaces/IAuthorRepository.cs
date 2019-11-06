using BookStore.Api.DataAccess.Interfaces;
using BookStore.Api.Entities;

namespace BookStore.Api.SeparateRepositories.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
    }
}
