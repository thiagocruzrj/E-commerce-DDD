using Ecommerce.Catalog.Domain.ValueObjects;
using Ecommerce.Core.DomainObjects;
using System;

namespace Ecommerce.Catalog.Domain.Entities
{
    public class Product : Entity, IAggregateRoot
    {
        public Guid CategoryId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
        public decimal Value { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public string Image { get; private set; }
        public int StockQuantity { get; private set; }
        public Category Category { get; private set; }
        public Dimensions Dimensions { get; private set; }

        public Product(string name, string description, bool active, decimal value, Guid categoryId, DateTime registerDate, string image, Dimensions dimensions)
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Active = active;
            Value = value;
            RegisterDate = registerDate;
            Image = image;
            Dimensions = dimensions;
            Validate();
        }

        public void Activate() => Active = true;
        public void Deactive() => Active = false;

        public void ChangeCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }

        public void ChangeDescription(string description)
        {
            AssertionConcern.ValidateIfEmpty(description, "Product Description field cannot be empty");
            Description = description;
        }

        public void DebitStock(int quantity)
        {
            if (quantity < 0) quantity *= -1;
            if (!HasStock(quantity)) throw new DomainException("Insuficient stock");
            StockQuantity -= quantity;
        }

        public void ReplenishStock(int quantity)
        {
            StockQuantity += quantity;
        }

        public bool HasStock(int quantity)
        {
            return StockQuantity >= quantity;
        }

        public void Validate()
        {
            AssertionConcern.ValidateIfEmpty(Name, "Product Name field cannot be empty");
            AssertionConcern.ValidateIfEmpty(Description, "Product Description field cannot be empty");
            AssertionConcern.ValidateIfEqual(CategoryId, Guid.Empty, "Product CategoryId field cannot be empty");
            AssertionConcern.ValidateIfLessThanMin(Value, 1, "Product Value field cannot be less than 0");
            AssertionConcern.ValidateIfEmpty(Image, "Product Image field cannot be empty");
        }
    }
}
