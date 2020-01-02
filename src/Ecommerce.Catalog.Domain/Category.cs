using Ecommerce.Core.DomainObjects;

namespace Ecommerce.Catalog.Domain
{
    public class Category : Entity
    {
        public string Name { get; private set; }
        public int Code { get; private set; }
        public Category(string name, int code)
        {
            Name = name;
            Code = code;
        }

        public override string ToString()
        {
            return $"{Name} - {Code}";
        }
    }
}
