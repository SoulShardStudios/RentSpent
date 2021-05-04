public class MaininventorySlot : Slot
{
    public MaininventorySlot(PointerItem pitem) => Pitem = pitem;
    public override void Transfer(ref PointerItem otherpitem, string Button)
    {
        if (otherpitem.Item != null || Pitem.Item != null)
        {
            if (Button == "Left")
            {
                if (otherpitem.Item != Pitem.Item)
                    QuickSwap(ref otherpitem);
                else
                {
                    if (otherpitem.Amount + Pitem.Amount <= Pitem.Item.MaxStackAmount)
                    {
                        Pitem = new PointerItem(Pitem.Item, otherpitem.Amount + Pitem.Amount);
                        otherpitem = new PointerItem();
                        OnItemUpdated?.Invoke(Pitem);
                    }
                    else if (otherpitem.Amount < otherpitem.Item.MaxStackAmount && Pitem.Amount < Pitem.Item.MaxStackAmount)
                    {
                        otherpitem = new PointerItem(Pitem.Item, (otherpitem.Amount + Pitem.Amount) - otherpitem.Item.MaxStackAmount);
                        Pitem = new PointerItem(Pitem.Item, Pitem.Item.MaxStackAmount);
                        OnItemUpdated?.Invoke(Pitem);
                    }
                    else
                        QuickSwap(ref otherpitem);
                }
            }
            if (Button == "Right")
            {
                if (otherpitem.IsEmpty() && Pitem.Amount > 1)
                {
                    int DV2 = Pitem.Amount / 2;
                    otherpitem = new PointerItem(Pitem.Item, DV2);
                    Pitem = new PointerItem(Pitem.Item, DV2 + Pitem.Amount % 2);
                    OnItemUpdated?.Invoke(Pitem);
                }
                if ((Pitem.IsEmpty() || Pitem.Item == otherpitem.Item) && !otherpitem.IsEmpty())
                {
                    if (otherpitem.Amount > 1)
                    {
                        otherpitem.Amount--;
                        Pitem = new PointerItem(otherpitem.Item, Pitem.Amount + 1);
                        OnItemUpdated?.Invoke(Pitem);
                    }
                }
            }
        }

    }
}