using Ecommerce.Catalog.Domain.Entities;
using Ecommerce.Catalog.Domain.ValueObjects;
using Ecommerce.Core.DomainObjects;
using System;
using Xunit;

namespace Ecommerce.Catalog.Domain.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Product_Validate_ValidationsMustReturnExceptions()
        {
            // Arrange & Act & Assert

            var ex = Assert.Throws<DomainException>(() =>
                new Product(string.Empty, "Description", false, 100, Guid.NewGuid(), DateTime.Now, "Image", new Dimensions(1, 1, 1))
            );

            Assert.Equal("Product Name field cannot be empty", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", string.Empty, false, 100, Guid.NewGuid(), DateTime.Now, "Image", new Dimensions(1, 1, 1))
            );

            Assert.Equal("Product Description field cannot be empty", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", false, 0, Guid.NewGuid(), DateTime.Now, "Image", new Dimensions(1, 1, 1))
            );

            Assert.Equal("Product Value field cannot be less than 0", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", false, 100, Guid.Empty, DateTime.Now, "Image", new Dimensions(1, 1, 1))
            );

            Assert.Equal("Product CategoryId field cannot be empty", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", false, 100, Guid.NewGuid(), DateTime.Now, string.Empty, new Dimensions(1, 1, 1))
            );

            Assert.Equal("Product Image field cannot be empty", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", false, 100, Guid.NewGuid(), DateTime.Now, "Image", new Dimensions(0, 1, 1))
            );

            Assert.Equal("Heigth field cannot be less or equals than 0", ex.Message);
        }
    }
}
