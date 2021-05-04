using System;
public class Slot
{
    public PointerItem Pitem;
    public Action<PointerItem> OnItemUpdated;
    public Slot() => Pitem = new PointerItem();
    public Slot(PointerItem pitem) => Pitem = pitem;
    public virtual void Transfer(ref PointerItem otherpitem, string Button) => QuickSwap(ref otherpitem);
    public virtual void SetSlotItem(PointerItem pitem)
    {
        Pitem = pitem;
        OnItemUpdated?.Invoke(Pitem);
    }
    public virtual void QuickSwap(ref PointerItem otherpitem)
    {
        PointerItem Opitem = otherpitem;
        otherpitem = Pitem;
        Pitem = Opitem;
        OnItemUpdated?.Invoke(Pitem);
    }
}