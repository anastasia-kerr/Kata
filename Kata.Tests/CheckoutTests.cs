using Moq;

namespace Kata.Tests;

[TestFixture]
public class CheckoutTests
{
    readonly Dictionary<string, PricingRule> pricingRules = new()
    {
            { "A", new PricingRule(50, (3, 130)) },
            { "B", new PricingRule(30, (2, 45)) },
            { "C", new PricingRule(30, null) },
            { "D", new PricingRule(30, null )}
        };

    [Test]
    public void Scan_WhenItemsScanned_AddsItemsToCart()
    {

        var mockCart = new Mock<ICart>();

        ICheckout checkout = new Checkout( mockCart.Object, pricingRules);

        checkout.Scan("A");
        checkout.Scan("B");
        checkout.Scan("A");
        checkout.Scan("B");
        checkout.Scan("A");

        mockCart.Verify(m => m.AddItem("A"), Times.Exactly(3));
        mockCart.Verify(m => m.AddItem("B"), Times.Exactly(2));
    }

    [Test]
    public void GetTotalPrice_WhenMultipleItemsScanned_CalculatesSpecialPriceCorrectly()
    {

       // var mockCart = new Mock<ICart>();
        var checkout = new Checkout(new SimpleCart(), pricingRules);
        checkout.Scan("B");
        checkout.Scan("A");
        checkout.Scan("B");

        Assert.That(checkout.GetTotalPrice(), Is.EqualTo(95));
    }
}
