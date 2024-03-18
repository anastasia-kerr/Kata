namespace Kata;
public record PricingRule(int UnitPrice, (int Quantity, int Price)? SpecialPrice);
