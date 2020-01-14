using System;
using System.Collections.Generic;

namespace Ecommerce.Core.DomainObjects.DTO
{
    public class ListOrderProducts
    {
        public Guid OrderId { get; set; }
        public ICollection<Item> Itens { get; set; }
    }

    public class Item
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
