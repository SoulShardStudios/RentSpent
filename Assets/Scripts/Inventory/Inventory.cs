public class Inventory
{
    public MaininventorySlot[] Slots;
    public bool IsFull;
    public Inventory(PointerItem[] Contents)
    {
        Slots = new MaininventorySlot[Contents.Length];
        for (int i = 0; i < Contents.Length; i++)
            Slots[i] = new MaininventorySlot(Contents[i] ?? new PointerItem());
    }
    public virtual bool AddItem(PointerItem ToBeAdded)
    {
        if (ToBeAdded.Item != null)
        {
            if (!CheckIfIsFull())
            {
                for (int i = 0; i < Slots.Length; i++)
                {
                    if (Slots[i].Pitem.Item == ToBeAdded.Item)
                    {
                        if (Slots[i].Pitem.Amount + ToBeAdded.Amount < Slots[i].Pitem.Item.MaxStackAmount)
                        {
                            Slots[i].SetSlotItem(new PointerItem(ToBeAdded.Item, Slots[i].Pitem.Amount + ToBeAdded.Amount));
                            return true;
                        }
                    }
                    else if (Slots[i].Pitem.IsEmpty())
                    {
                        Slots[i].SetSlotItem(ToBeAdded);
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public virtual bool CheckIfIsFull()
    {
        bool Full = true;
        foreach(MaininventorySlot S in Slots)
        {
            if (S.Pitem.Item == null)
                Full = false;
        }
        IsFull = Full;
        return IsFull;
    }
    public virtual bool IsEmpty()
    {
        foreach (Slot slot in Slots)
            if (slot.Pitem.Item != null)
                return false;
        return true;
    }
}