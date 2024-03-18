namespace Kata.Tests;

[TestFixture]
public class PricingRuleTests
{
    [Test]
    public void CalculatePrice_WithoutSpecialPrice_ReturnsCorrectTotal()
    {
        var rule = new PricingRule(50, null);

        var total = rule.CalculatePrice(2);

        Assert.That(total, Is.EqualTo(100));
    }

    [Test]
    public void CalculatePrice_ExactSpecialPriceQuantity_ReturnsSpecialTotal()
    {
        var rule = new PricingRule(50, (3, 130));

        var total = rule.CalculatePrice(3);

        Assert.That(total, Is.EqualTo(130));
    }

    [Test]
    public void CalculatePrice_AboveSpecialPriceQuantityWithRemainder_ReturnsCorrectTotal()
    {
        var rule = new PricingRule(50, (3, 130));

        var total = rule.CalculatePrice(4); // 3 for special price + 1 at unit price

        Assert.That(total, Is.EqualTo(180)); // 130 + 50
    }

    [Test]
    public void CalculatePrice_MultipleSetsOfSpecialPriceQuantity_ReturnsCorrectTotal()
    {
        var rule = new PricingRule(50, (3, 130));

        var total = rule.CalculatePrice(6); // 2 sets of 3

        Assert.That(total, Is.EqualTo(260)); // 130 * 2
    }

    [Test]
    public void CalculatePrice_MultipleSetsPlusRemainder_ReturnsCorrectTotal()
    {
        var rule = new PricingRule(50, (3, 130));

        var total = rule.CalculatePrice(7); // 2 sets of 3 + 1 remainder

        Assert.That(total, Is.EqualTo(310)); // (130 * 2) + 50
    }

    [Test]
    public void CalculatePrice_SpecialPriceNotApplicableBecauseQuantityTooLow_ReturnsUnitPriceTotal()
    {
        var rule = new PricingRule(50, (3, 130));

        var total = rule.CalculatePrice(1); // Below the special price quantity threshold

        Assert.That(total, Is.EqualTo(50)); // Unit price only
    }
}
