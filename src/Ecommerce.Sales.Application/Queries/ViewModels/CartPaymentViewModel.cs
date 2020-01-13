using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Sales.Application.Queries.ViewModels
{
    public class CartPaymentViewModel
    {
        public string CardName { get; set; }
        public string NumberCard { get; set; }
        public string ExpirationCard { get; set; }
        public string CvcCard { get; set; }
    }
}
