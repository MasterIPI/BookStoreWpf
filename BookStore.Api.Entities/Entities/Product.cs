using BookStore.Api.Entities.Enums;
using System;

namespace BookStore.Api.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }
        
        public decimal Price { get; set; }
        
        public ProductType ProductType { get; set; }

        public int QuantityAvailable { get; set; }

        public DateTime PublishDate { get; set; }

        public float Rating { get; set; }

        public int AgeRating { get; set; }

        public int PageNumber { get; set; }

        public CoverType CoverType { get; set; }

        public bool HasDustJacket { get; set; }
    }

    public class ProductAuthor : BaseEntity
    {
        public long ProductId { get; set; }

        public long AuthorId { get; set; }
    }

    public class ProductPublisher : BaseEntity
    {
        public long ProductId { get; set; }

        public long PublisherId { get; set; }
    }
}
