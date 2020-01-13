using System;

namespace Ecommerce.Sales.Application.Queries.ViewModels
{
    public class CartItemViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitValue { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
