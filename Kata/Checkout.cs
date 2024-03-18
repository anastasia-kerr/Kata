namespace Kata;

public class Checkout : ICheckout
{
    private readonly ICart _cart;

    public Checkout(ICart cart)
    {
        _cart = cart;
    }

    public int GetTotalPrice()
    {
        throw new NotImplementedException();
    }

    public void Scan(string item)
    {
        _cart.AddItem(item);
    }
}