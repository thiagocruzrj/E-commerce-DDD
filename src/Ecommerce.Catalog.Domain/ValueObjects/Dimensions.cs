using Ecommerce.Core.DomainObjects;

namespace Ecommerce.Catalog.Domain.ValueObjects
{
    public class Dimensions
    {
        public decimal Height { get; private set; }
        public decimal Width { get; private set; }
        public decimal Depth { get; private set; }
        public Dimensions(decimal height, decimal width, decimal depth)
        {
            AssertionConcern.ValidateIfLessThanMin(height, 1, "Heigth field cannot be less or equals than 0");
            AssertionConcern.ValidateIfLessThanMin(width, 1, "Width field cannot be less or equals than 0");
            AssertionConcern.ValidateIfLessThanMin(depth, 1, "Depth field cannot be less or equals than 0");

            Height = height;
            Width = width;
            Depth = depth;
        }

        public string FormattedDescription()
        {
            return $"HxWxD: {Height} x {Width} x {Depth}";
        }

        public override string ToString()
        {
            return FormattedDescription();
        }
    }
}
