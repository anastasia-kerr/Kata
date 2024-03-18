namespace Kata;
public interface ICheckout
{
    void Scan(string item);
    int GetTotalPrice(); // Price should be a decimal!
}
