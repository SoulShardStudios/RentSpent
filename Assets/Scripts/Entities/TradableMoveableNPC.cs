using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TradableMoveableNPC : Character, ITradeable, IResetable
{
    private readonly Vector2[] Directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right, new Vector2(-.75f,-.75f), new Vector2(.75f, -.75f), new Vector2(-.75f, .75f), new Vector2(.75f, .75f) };
    public SpriteRenderer Renderer;
    public float OutlineSize;
    public Pair[] TradeTables;
    public AudioSource WalkSource;
    private Trade Trade = null;
    [SerializeField] protected GameObject WantedItemDisplay, DoneWithTradingIcon;
    [SerializeField] protected SpriteRenderer WantedItemImage;
    private bool TradesAreComplete, MovementCooldown, IsTrading;
    private Vector2 CurrentDirection;
    public static List<TradableMoveableNPC> Instances { get; private set; }
    private void Start()
    {
        StartCoroutine(DeleteAfter4Min());
        if (Instances == null)
            Instances = new List<TradableMoveableNPC>(0);
        Instances.Add(this);
        AddToSingleton();
        while (Trade == null)
        {
            Pair temp = TradeTables[Random.Range(0, TradeTables.Length)];
            if (temp.Chance / 100 > Random.Range(0f, 1f))
                Trade = temp.Trades.GenerateRandomTrade();
        }
        WantedItemDisplay.SetActive(true);
        WantedItemImage.sprite = Trade.WantedItem.Item.DisplayImage;
        DoneWithTradingIcon.SetActive(false);
    }
    private void OnMouseExit() => Renderer.material.SetFloat("OffsetAmount", 0);
    private void OnMouseEnter() => Renderer.material.SetFloat("OffsetAmount", OutlineSize);
    private void OnMouseDown()
    {
        if (!TradesAreComplete && !IsTrading && Player.S != null)
            if (Player.S.IsInInteractionRange(transform.position))
            {
                WantedItemDisplay.SetActive(false);
                TradeManager.S.ShowTrade(GetComponent<ITradeable>());
                IsTrading = true;
            }
    }
    private void Update()
    {
        UpdateMoveDirection();
        Move(WantedItemDisplay.activeSelf ? CurrentDirection : Vector2.zero);
        UpdateAnimator(CurrentDirection);
        bool range = false;
        if (Player.S != null)
            range = Player.S.IsInInteractionRange(transform.position);
        if (!range && IsTrading)
        {
            IsTrading = false;
            TradeManager.S?.CloseTrade();
        }
    }
    public void DeactivateTrade()
    {
        WantedItemDisplay.SetActive(true);
        IsTrading = false;
    }
    public void UsedUpTrades()
    {
        DoneWithTradingIcon.SetActive(true);
        TradesAreComplete = true;
    }
    public Trade GetTrade() { return Trade; }
    private IEnumerator MoveIEnumerator()
    {
        MovementCooldown = true;
        yield return new WaitForSeconds(Random.Range(.5f, 1f));
        MovementCooldown = false;
    }
    private void UpdateMoveDirection()
    {
        if (WantedItemDisplay.activeSelf)
        {
            if (!MovementCooldown)
            {
                CurrentDirection = Random.Range(0f, 1f) > .5f ? Vector2.zero : Directions[Random.Range(0, Directions.Length)];
                StartCoroutine(MoveIEnumerator());
            }
        }
        else
            CurrentDirection = Player.S.gameObject.transform.position - transform.position;
    }
    public override void UpdateAnimator(Vector2 Dir)
    {
        if (WantedItemDisplay.activeSelf)
            base.UpdateAnimator(Dir);
        else
        {
            Functions.SetXYForAnimator(BodyAnimator, Functions.BlendVector2(CurrentDirection*10));
            Functions.SetAnimatorLayer(BodyAnimator, "Idle");
        }  
    }
    private void OnBecameInvisible()
    {
        if (TradesAreComplete)
            Destroy(gameObject);
    }
    public void PlayWalkSound() => WalkSource.Play();
    public void ResetIt()
    {
        try
        {
            Destroy(gameObject);
        }
        catch (MissingReferenceException) { }
    }
    public void AddToSingleton() => RentCollector.Resetables.Add(GetComponent<IResetable>());

    private IEnumerator DeleteAfter4Min()
    {
        yield return new WaitForSeconds(60 * 4);
        Destroy(gameObject);
    }

}
[System.Serializable]
public class Pair
{
    public TradeTable Trades;
    public float Chance;
}