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
    public void Scan_WhenSingleItemScannedMultipleTimes_AddsItemToCartCorrectly()
    {
        var mockCart = new Mock<ICart>();
        ICheckout checkout = new Checkout(mockCart.Object, pricingRules);

        checkout.Scan("D");
        checkout.Scan("D");
        checkout.Scan("D");

        mockCart.Verify(m => m.AddItem("D"), Times.Exactly(3));
    }

    [Test]
    public void GetTotalPrice_WhenMultipleItemsScanned_CalculatesSpecialPriceCorrectly()
    {

        var mockCart = new Mock<ICart>();
        mockCart.Setup(m => m.GetItems()).Returns(new Dictionary<string, int>
            {
                { "A", 1 },
                { "B", 2 }
            });

        var checkout = new Checkout(mockCart.Object, pricingRules);

        Assert.That(checkout.GetTotalPrice(), Is.EqualTo(95));
    }

    [Test]
    public void GetTotalPrice_WhenItemsWithoutSpecialPricesScanned_CalculatesTotalCorrectly()
    {
        var mockCart = new Mock<ICart>();
        mockCart.Setup(m => m.GetItems()).Returns(new Dictionary<string, int>
    {
        { "C", 2 },
        { "D", 3 }
    });

        var checkout = new Checkout(mockCart.Object, pricingRules);

        Assert.That(checkout.GetTotalPrice(), Is.EqualTo(150)); // C: 2*30, D: 3*30
    }

    [Test]
    public void GetTotalPrice_WhenItemsWithSpecialPricesExceedingRequiredQuantityScanned_CalculatesTotalCorrectly()
    {
        var mockCart = new Mock<ICart>();
        mockCart.Setup(m => m.GetItems()).Returns(new Dictionary<string, int>
    {
        { "A", 7 }, // Special price for 3, so 2*130 for 6, plus 1*50 for the remaining 1
        { "B", 5 }  // Special price for 2, so 2*45 for 4, plus 1*30 for the remaining 1
    });

        var checkout = new Checkout(mockCart.Object, pricingRules);

        Assert.That(checkout.GetTotalPrice(), Is.EqualTo(130 * 2 + 50 + 45 * 2 + 30)); // 390
    }

}
