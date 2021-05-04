using UnityEngine;
using UnityEngine.UI;
public class TradeUISlot : UISlot
{
    public Image Sihlouette;
    private PointerItem TradePItem;
    public void SetOutputSihlouette(PointerItem PItem)
    {
        if (PItem != null)
        {
            TradePItem = PItem;
            if (ReferenceSlot.Pitem.IsEmpty())
            {
                Sihlouette.sprite = PItem.Item?.DisplayImage;
                ItemAmountText.text = PItem.Amount.ToString();
                ItemImage.color = Color.clear;
            }
        }
    }
    protected override void UpdateUI(PointerItem Pitem)
    {
        if (Pitem != null)
        {
            if (Pitem.Item != null)
                base.UpdateUI(Pitem);
            else
                SetOutputSihlouette(TradePItem);
        }
    }
}