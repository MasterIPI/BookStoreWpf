using BookStore.Api.DataAccess;
using BookStore.Api.DataAccess.Repositories;
using BookStore.Api.Entities;
using BookStore.Api.SeparateRepositories.Interfaces;

namespace BookStore.Api.SeparateRepositories.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
