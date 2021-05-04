using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName ="ScriptableObjects/TradeTable")]
public class TradeTable : ScriptableObject
{
    public RangeInt MaxTradeRange;
    public RangeInt PriceFluctuationRange;
    public Item[] WantedItems;
    public Item[] TradedItems;
    public Trade GenerateRandomTrade()
    {
        Trade ToReturn = new Trade();
        ToReturn.MaxNumberOfTrades = MaxTradeRange.GetRandom();
        Item wanted, traded;
        wanted = WantedItems[Random.Range(0, WantedItems.Length)];
        traded = TradedItems[Random.Range(0, TradedItems.Length)];
        int tradedamount = 1;
        int wantedamount = 1;
        int pricefluctuation = PriceFluctuationRange.GetRandom();
        int PriceFlucAdjusted = Mathf.RoundToInt(Mathf.Log(wanted.BaseSellPrice > traded.BaseSellPrice ? (wanted.BaseSellPrice + Mathf.Abs(pricefluctuation))
            / traded.BaseSellPrice : (traded.BaseSellPrice + Mathf.Abs(pricefluctuation)) / wanted.BaseSellPrice, 2));

        if (wanted.BaseSellPrice > traded.BaseSellPrice)
            tradedamount = (wanted.BaseSellPrice / traded.BaseSellPrice) + PriceFlucAdjusted;
        else
            wantedamount = (traded.BaseSellPrice / wanted.BaseSellPrice) + PriceFlucAdjusted;

        ToReturn.WantedItem = new PointerItem(wanted, wantedamount);
        ToReturn.BaseTradedItem = new PointerItem(traded, tradedamount);
        return ToReturn;
    }
    public Trade GenerateRandomMissingTrade()
    {
        Trade ToReturn = new Trade();
        ToReturn.MaxNumberOfTrades = MaxTradeRange.GetRandom();
        Item wanted, traded = null;
        int tradedamount = 1;
        int wantedamount = 1;

        List<Item> HasItems = new List<Item>(0);
        foreach (Slot S in InventoryUIManager.S.MainInventory.Slots)
            if (S.Pitem.Item != null)
                HasItems.Add(S.Pitem.Item);
        wanted = HasItems[Random.Range(0, HasItems.Count)];

        HasItems = new List<Item>(0);
        foreach (TradableMoveableNPC T in TradableMoveableNPC.Instances)
            if (T.GetTrade().WantedItem.Item != null)
                HasItems.Add(T.GetTrade().WantedItem.Item);
        while (traded == null)
        {
            Item T = HasItems[Random.Range(0, HasItems.Count)];
            if (T.name != wanted.name)
                traded = T;
        }
        int pricefluctuation = PriceFluctuationRange.GetRandom();
        int PriceFlucAdjusted = Mathf.RoundToInt(Mathf.Log(wanted.BaseSellPrice > traded.BaseSellPrice ? (wanted.BaseSellPrice + Mathf.Abs(pricefluctuation))
            / traded.BaseSellPrice : (traded.BaseSellPrice + Mathf.Abs(pricefluctuation)) / wanted.BaseSellPrice, 2));

        if (wanted.BaseSellPrice > traded.BaseSellPrice)
            tradedamount = (wanted.BaseSellPrice / traded.BaseSellPrice) + PriceFlucAdjusted;
        else
            wantedamount = (traded.BaseSellPrice / wanted.BaseSellPrice) + PriceFlucAdjusted;

        ToReturn.WantedItem = new PointerItem(wanted, wantedamount);
        ToReturn.BaseTradedItem = new PointerItem(traded, tradedamount);
        return ToReturn;
    }
}