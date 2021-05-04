using UnityEngine;
public class OutputSlot : Slot
{
    public override void Transfer(ref PointerItem otherpitem, string Button)
    {
        if (otherpitem.Item == null)
            QuickSwap(ref otherpitem);
        else if (otherpitem.Item == Pitem.Item)
        {
            otherpitem = new PointerItem(Pitem.Item, otherpitem.Amount + Pitem.Amount);
            SetSlotItem(new PointerItem());
        }

    }
}