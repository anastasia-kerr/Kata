namespace Kata;

public class SimpleCart : ICart
{
    private readonly Dictionary<string, int> _items = [];

    public void AddItem(string item)
    {
        if (_items.ContainsKey(item))
        {
            _items[item]++;
        }
        else
        {
            _items[item] = 1;
        }
    }

    public Dictionary<string, int> GetItems()
    {
        return _items;
    }
}
