using Ecommerce.Core.DomainObjects;
using Ecommerce.Sales.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Sales.Domain.Entities
{
    public class Order : Entity, IAggregateRoot
    {
        public int Code { get; private set; }
        public Guid ClientId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool VoucherUsed { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public OrderStatus OrderStatus { get; private set; }

        private readonly List<OrderItem> _orderItems;

        public IReadOnlyCollection<OrderItem> OrderItem => _orderItems;

        // EF Relation
        public virtual Voucher Voucher { get; private set; }

        public Order(Guid clientId, bool voucherUsed, decimal discount, decimal totalPrice)
        {
            ClientId = clientId;
            VoucherUsed = voucherUsed;
            Discount = discount;
            TotalPrice = totalPrice;
            _orderItems = new List<OrderItem>();
        }

        protected Order() { _orderItems = new List<OrderItem>(); }

        public void ApplyVoucher(Voucher voucher)
        {
            Voucher = voucher;
            VoucherUsed = true;
            CalculateOrderValue();
        }

        public void CalculateOrderValue()
        {
            TotalPrice = OrderItem.Sum(p => p.CalculatePrice());
            CalculateTotalDiscountValue();
        }

        public void CalculateTotalDiscountValue()
        {
            if (!VoucherUsed) return;

            decimal discount = 0;
            var value = TotalPrice;

            if(Voucher.VoucherDiscountType == VoucherDiscountType.Percentage)
            {
                if(Voucher.Percentage.HasValue)
                {
                    discount = (value * Voucher.Percentage.Value) / 100;
                    value -= discount;
                }
            } else
            {
                if(Voucher.DiscountValue.HasValue)
                {
                    discount = Voucher.DiscountValue.Value;
                    value -= discount;
                }
            }
            TotalPrice = value < 0 ? 0 : value;
            Discount = discount;
        }

        public bool OrderItemExist(OrderItem item)
        {
            return _orderItems.Any(p => p.ProductId == item.ProductId);
        }

        public void AddItem(OrderItem item)
        {
            if (!item.IsValid()) return;

            item.AssociateOrder(Id);

            if (OrderItemExist(item))
            {
                var existentItem = _orderItems.FirstOrDefault(p => p.ProductId == item.ProductId);
                existentItem.AddUnits(item.Quantity);
                item = existentItem;

                _orderItems.Remove(existentItem);
            }
            item.CalculatePrice();
            _orderItems.Add(item);

            CalculateOrderValue();
        }

        public void RemoveItem(OrderItem item)
        {
            if (!item.IsValid()) return;

            var existentItem = OrderItem.FirstOrDefault(p => p.ProductId == item.ProductId);

            if (existentItem == null) throw new DomainException("Item doesn't belong to order");
            _orderItems.Remove(existentItem);

            CalculateOrderValue();
        }

        public void UpdateItem(OrderItem item)
        {
            if (!item.IsValid()) return;
            item.AssociateOrder(Id);

            var existentItem = OrderItem.FirstOrDefault(p => p.ProductId == item.ProductId);

            if (existentItem == null) throw new DomainException("Item doesn't belong to order");

            _orderItems.Remove(existentItem);
            _orderItems.Add(item);

            CalculateOrderValue();
        }

        public void UpdateUnits(OrderItem item, int units)
        {
            item.UpdateUnits(units);
            UpdateItem(item);
        }

        public void BecomeDraft()
        {
            OrderStatus = OrderStatus.Draft;
        }

        public void StartOrder()
        {
            OrderStatus = OrderStatus.Started;
        }
        public void FinishOrder()
        {
            OrderStatus = OrderStatus.Paid;
        }
        public void CancelOrder()
        {
            OrderStatus = OrderStatus.Canceled;
        }

        // I've used a factory just when a client want to put items on shop cart, creating a draft order
        public static class OrderFactory
        {
            public static Order NewOrderDraft(Guid clientId)
            {
                var order =  new Order
                {
                    ClientId = clientId
                };

                order.BecomeDraft();
                return order;
            }
        }
    }
}
