﻿namespace Kata;
public record PricingRule(int UnitPrice, (int Quantity, int Price)? SpecialPrice)
{
    public int CalculatePrice(int quantity)
    {
        if (!HasSpecialPrice(quantity))
        {
            return quantity * UnitPrice;
        }

        int sets = quantity / SpecialPrice.Value.Quantity;
        int remainder = quantity % SpecialPrice.Value.Quantity;
        return sets * SpecialPrice.Value.Price + remainder * UnitPrice;
    }
    private bool HasSpecialPrice(int quantity) {

        return SpecialPrice.HasValue && quantity >= SpecialPrice.Value.Quantity;
    }
}