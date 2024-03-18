namespace Kata;

public class Checkout : ICheckout
{
    private readonly ICart _cart;
    private readonly Dictionary<string, PricingRule> _pricingRules;

    public Checkout(ICart cart, Dictionary<string, PricingRule> pricingRules)
    {
        _cart = cart;
        _pricingRules = pricingRules;
    }

    public int GetTotalPrice()
    {
        var items = _cart.GetItems();

        return items.Sum(item => _pricingRules[item.Key].CalculatePrice(item.Value));
    }

    public void Scan(string item) => _cart.AddItem(item);
}