using Moq;

namespace Kata.Tests;

[TestFixture]
public class CheckoutTests
{
    [Test]
    public void Scan_WhenItemsScanned_AddsItemsToCart()
    {

        var mockCart = new Mock<ICart>();

        ICheckout checkout = new Checkout(mockCart.Object);

        checkout.Scan("A");
        checkout.Scan("B");
        checkout.Scan("A");
        checkout.Scan("B");
        checkout.Scan("A");

        mockCart.Verify(m => m.AddItem("A"), Times.Exactly(3));
        mockCart.Verify(m => m.AddItem("B"), Times.Exactly(2));
    }

    [Test]
    public void GetTotalPrice_WhenItemsScanned_AddItemsToCart()
    {

        var mockCart = new Mock<ICart>();

        ICheckout checkout = new Checkout(mockCart.Object);

        checkout.Scan("A");
        checkout.Scan("B");
        checkout.Scan("A");
        checkout.Scan("B");
        checkout.Scan("A");

        mockCart.Verify(m => m.AddItem("A"), Times.Exactly(3));
        mockCart.Verify(m => m.AddItem("B"), Times.Exactly(2));
    }
}
