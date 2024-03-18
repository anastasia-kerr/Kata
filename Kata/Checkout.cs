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
        var total = 0;
        foreach (var item in items)
        {
            var pricingRule = _pricingRules[item.Key];
            var itemTotalPrice = 0;
            itemTotalPrice =  pricingRule.CalculatePrice(item.Value);
            total += itemTotalPrice;
        }
        return total;
    }

    public void Scan(string item) => _cart.AddItem(item);
}