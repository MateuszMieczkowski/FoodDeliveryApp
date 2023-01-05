using Library.Entities;

namespace Web.Api.Models.ShoppingCartDtos
{
    public class ShoppingCartItemDto
    {
        public int Id { get; set; }

        public Guid ShoppingCartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}