namespace Kata;

public interface ICart
{
    void AddItem(string item);
    Dictionary<string, int> GetItems();
}
