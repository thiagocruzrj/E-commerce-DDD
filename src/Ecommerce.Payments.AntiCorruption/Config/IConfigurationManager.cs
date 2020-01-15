namespace Ecommerce.Payments.AntiCorruption.Config
{
    public interface IConfigurationManager
    {
        string GetValue(string node);
    }
}
