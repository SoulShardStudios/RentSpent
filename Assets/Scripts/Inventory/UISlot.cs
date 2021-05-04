using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class UISlot : MonoBehaviour, IPointerClickHandler
{
    public Slot ReferenceSlot;
    public Image ItemImage;
    public TextMeshProUGUI ItemAmountText;
    protected virtual void UpdateUI(PointerItem Pitem)
    {
        if (Pitem != null)
        {
            ItemImage.sprite = Pitem.Item?.DisplayImage;
            ItemImage.color = Pitem.Item == null ? Color.clear : Color.white;
            ItemAmountText.text = Pitem.Amount.ToString();
            ItemAmountText.color = Pitem.Amount < 1 ? Color.clear : Color.white;
        }
    }
    public virtual void Initialize(Slot referenceslot)
    {
        ReferenceSlot = referenceslot;
        ReferenceSlot.OnItemUpdated += UpdateUI;
        UpdateUI(ReferenceSlot.Pitem);
    }
    public virtual void OnPointerClick(PointerEventData eventData) => ReferenceSlot.Transfer(ref InventoryUIManager.S.HeldItem.PItem, eventData.button.ToString());
}