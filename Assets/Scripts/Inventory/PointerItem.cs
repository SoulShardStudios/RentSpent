[System.Serializable]
public class PointerItem
{
    public Item Item;
    public int Amount;
    public PointerItem()
    {
        Item = null;
        Amount = 0;
    }
    public PointerItem(Item item, int amount)
    {
        Item = item;
        Amount = amount;
    }
    public bool IsEmpty() { return Item == null; }
}