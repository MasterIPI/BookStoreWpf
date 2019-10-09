namespace BookStore.Api.Entities
{
    public class UserBasket : BaseEntity
    {
        public long UserId { get; set; }
    }

    public class UserBasketProduct : BaseEntity
    {
        public long UserBasketId { get; set; }

        public long ProductId { get; set; }
    }
}
