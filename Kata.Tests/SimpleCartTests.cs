namespace Kata.Tests;

[TestFixture]
public class SimpleCartTests
{
    [Test]
    public void AddItem_WhenCalled_AddsItemsCorrectly()
    {
        // Arrange
        var cart = new SimpleCart();

        // Act
        cart.AddItem("A");
        cart.AddItem("B");
        cart.AddItem("A"); // Adding "A" a second time to test quantity increment

        var items = cart.GetItems();

        // Assert
        Assert.That(items.Count, Is.EqualTo(2), "Cart should contain two types of items.");
        Assert.IsTrue(items.ContainsKey("A") && items["A"] == 2, "Item 'A' should have a quantity of 2.");
        Assert.IsTrue(items.ContainsKey("B") && items["B"] == 1, "Item 'B' should have a quantity of 1.");
    }

    [Test]
    public void GetItems_WhenCartIsEmpty_ReturnsEmptyDictionary()
    {
        // Arrange
        var cart = new SimpleCart();

        // Act
        var items = cart.GetItems();

        // Assert
        Assert.IsNotNull(items, "GetItems should never return null.");
        Assert.That(items.Count, Is.EqualTo(0), "Newly created cart should be empty.");
    }
}