using BookStore.Api.DataAccess.Interfaces;
using BookStore.Api.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Api.DataAccess.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
