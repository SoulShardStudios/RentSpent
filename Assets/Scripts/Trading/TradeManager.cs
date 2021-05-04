using UnityEngine;
public class TradeManager : MonoBehaviour, IResetable
{
    public GameObject Background;
    public TradeUISlot OutputSlot, InputSlot;
    private Trade CurrentTrade;
    private int TradesLeft;
    private ITradeable CurrentlyBeingTradedWith;
    public static TradeManager S { get; private set; }
    private void Start()
    {
        S = this;
        OutputSlot.Initialize(new OutputSlot());
        InputSlot.Initialize(new InputSlot());
        AddToSingleton();
    }
    private void Update()
    {
        if (CurrentTrade != null)
        {
            int RemainingItems = InputSlot.ReferenceSlot.Pitem.Amount - CurrentTrade.WantedItem.Amount;
            if (RemainingItems >= 0)
            {
                if (OutputSlot.ReferenceSlot.Pitem.IsEmpty())
                {
                    OutputSlot.ReferenceSlot.SetSlotItem(OutputSlot.ReferenceSlot.Pitem.Amount == 0 ? CurrentTrade.BaseTradedItem : new PointerItem(CurrentTrade.BaseTradedItem.Item, (CurrentTrade.BaseTradedItem.Amount) + OutputSlot.ReferenceSlot.Pitem.Amount));
                    if (RemainingItems > 0)
                        InputSlot.ReferenceSlot.SetSlotItem(new PointerItem(InputSlot.ReferenceSlot.Pitem.Item, InputSlot.ReferenceSlot.Pitem.Amount - CurrentTrade.WantedItem.Amount));
                    else if (RemainingItems == 0)
                        InputSlot.ReferenceSlot.SetSlotItem(new PointerItem());
                }
            }
            else
            {
                InventoryUIManager.S.MainInventory.AddItem(OutputSlot.ReferenceSlot.Pitem);
                OutputSlot.ReferenceSlot.SetSlotItem(new PointerItem());
            }
        }
    }
    public void ShowTrade(ITradeable Tradeable)
    {
        AudioManager.S.PlaySound("TradeComplete");
        if (CurrentTrade != null)
            RemoveItemsFromTradeUI();
        CurrentlyBeingTradedWith = Tradeable;
        CurrentTrade = CurrentlyBeingTradedWith.GetTrade();
        TradesLeft = CurrentTrade.MaxNumberOfTrades;
        OutputSlot.Initialize(new OutputSlot());
        InputSlot.Initialize(new InputSlot());
        Background.SetActive(true);
        InputSlot.SetOutputSihlouette(CurrentTrade.WantedItem);
        InputSlot S = (InputSlot)InputSlot.ReferenceSlot;
        S.AcceptedItem = CurrentTrade.WantedItem;
        OutputSlot.SetOutputSihlouette(CurrentTrade.BaseTradedItem);
    }
    public void CloseTrade()
    {
        CurrentTrade = null;
        AudioManager.S.PlaySound("CloseTrade");
        Background.SetActive(false);
        RemoveItemsFromTradeUI();
    }
    private void RemoveItemsFromTradeUI()
    {
        CurrentlyBeingTradedWith?.DeactivateTrade();
        if (!InputSlot.ReferenceSlot.Pitem.IsEmpty())
            InventoryUIManager.S.MainInventory.AddItem(InputSlot.ReferenceSlot.Pitem);
        if (!OutputSlot.ReferenceSlot.Pitem.IsEmpty())
            InventoryUIManager.S.MainInventory.AddItem(OutputSlot.ReferenceSlot.Pitem);
    }
    public void ResetIt() => CloseTrade();
    public void AddToSingleton() => RentCollector.Resetables.Add(GetComponent<IResetable>());
}