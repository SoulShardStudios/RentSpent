using UnityEngine;
public class ImobleTradeableNPC : MonoBehaviour, ITradeable, IResetable
{
    public SpriteRenderer Renderer;
    public float OutlineSize;
    public TradeTable SpecialTable;
    private Trade Trade = null;
    private bool IsTrading;
    private void Start() => AddToSingleton();
    private void OnMouseExit() => Renderer.material.SetFloat("OffsetAmount", 0);
    private void OnMouseEnter() => Renderer.material.SetFloat("OffsetAmount", OutlineSize);
    private void OnMouseDown()
    {
        if (!IsTrading)
            if (Player.S != null)
            {
                if (Player.S.IsInInteractionRange(transform.position))
                {
                    if (Trade == null)
                        Trade = SpecialTable.GenerateRandomMissingTrade();
                    TradeManager.S.ShowTrade(GetComponent<ITradeable>());
                    IsTrading = true;
                }
            }
    }
    private void Update()
    {
        bool range = false;
        if (Player.S != null)
            range = Player.S.IsInInteractionRange(transform.position);
        if (!range && IsTrading)
        {
            IsTrading = false;
            TradeManager.S?.CloseTrade();
        }
    }
    public void DeactivateTrade() => IsTrading = false;
    public void UsedUpTrades() => Debug.Log("get the dub");
    public Trade GetTrade() { return Trade; }
    public void ResetIt() => Trade = SpecialTable.GenerateRandomMissingTrade();
    public void AddToSingleton() => RentCollector.Resetables.Add(GetComponent<IResetable>());
}