using UnityEngine;
public class InputSlot : Slot
{
    public PointerItem AcceptedItem;
    public override void Transfer(ref PointerItem otherpitem, string Button)
    {
        if (otherpitem.Item == AcceptedItem.Item || otherpitem.Item == null)
            QuickSwap(ref otherpitem);
    }
}