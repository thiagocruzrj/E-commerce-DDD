using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Sales.Application.Queries.ViewModels
{
    public class CartViewModel
    {
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DescountValue { get; set; }
        public string VoucherCode { get; set; }

        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
        public CartPaymentViewModel Payment { get; set; }
    }
}
