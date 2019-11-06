using BookStore.Api.DataAccess.Interfaces;
using BookStore.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Api.DataAccess.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private ApplicationContext _context;
        private DbSet<TEntity> _set;

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        public async Task<long> Create(TEntity entity)
        {
            await _set.AddAsync(entity);
            return 0;
        }
    }
}
