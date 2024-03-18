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
        int total = 0;
        foreach (var item in items)
        {
            var pricingRule = _pricingRules[item.Key];
            int itemTotalPrice = 0;
            if (pricingRule.SpecialPrice.HasValue && item.Value >= pricingRule.SpecialPrice.Value.Quantity)
            {
                int sets = item.Value / pricingRule.SpecialPrice.Value.Quantity;
                int remainder = item.Value % pricingRule.SpecialPrice.Value.Quantity;
                itemTotalPrice = sets * pricingRule.SpecialPrice.Value.Price + remainder * pricingRule.UnitPrice;
            }
            else
            {
                itemTotalPrice = item.Value * pricingRule.UnitPrice;
            }
            total += itemTotalPrice;
        }
        return total;
    }

    public void Scan(string item)
    {
        _cart.AddItem(item);
    }
}