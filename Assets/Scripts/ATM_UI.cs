using UnityEngine;
using TMPro;
public class ATM_UI : MonoBehaviour, IResetable
{
    [HideInInspector] public bool IsOpen;
    [SerializeField] private UISlot Slot;
    [SerializeField] private GameObject Background, MoneyNotification;
    private void Awake()
    {
        Slot.Initialize(new MaininventorySlot(new PointerItem()));
        AddToSingleton();
    }
    public void ToggleATM_UI()
    {
        IsOpen = !IsOpen;
        AudioManager.S.PlaySound(IsOpen ? "TradeComplete" : "CloseTrade");
        Background.SetActive(IsOpen);
        InventoryUIManager.S.MainInventory.AddItem(Slot.ReferenceSlot.Pitem);
        Slot.ReferenceSlot.SetSlotItem(new PointerItem());
    }
    public void SellCurrentItem()
    {
        if (!Slot.ReferenceSlot.Pitem.IsEmpty())
        {
            int Money = Slot.ReferenceSlot.Pitem.Item.BaseSellPrice * Slot.ReferenceSlot.Pitem.Amount;
            GameObject G = Instantiate(MoneyNotification, Background.transform);
            G.GetComponent<TextMeshProUGUI>().text = string.Format("+{0}$", Money);
            MoneyManager.S.ModifyFunds(Money);
            Slot.ReferenceSlot.SetSlotItem(new PointerItem());
        }
    }
    public void AddToSingleton() => RentCollector.Resetables.Add(GetComponent<IResetable>());
    public void ResetIt()
    {
        IsOpen = true;
        ToggleATM_UI();
    }
}