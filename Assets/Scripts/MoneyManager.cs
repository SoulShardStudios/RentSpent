using UnityEngine;
using TMPro;
public class MoneyManager : MonoBehaviour
{
    public static MoneyManager S { get; private set; }
    public int StartingRent, IncreaseBy, IncreaseTimeBy;
    [HideInInspector] public int Money, Rent, NumberOfRents;
    public TextMeshProUGUI MoneyText, RentText, DaysText;
    private void OnEnable()
    {
        S = this;
        Rent = StartingRent;
        MoneyText.text = "0";
        RentText.text = string.Format("RENT:{0}", Rent);
        DaysText.text = string.Format("Day:{0}", 1);
    }
    public void ModifyFunds(int ModBy)
    {
        Money += ModBy;
        MoneyText.text = Money.ToString();
    }
    public void UpdateRent()
    {
        ModifyFunds(-Rent);
        NumberOfRents++;
        Rent += IncreaseBy * NumberOfRents;
        Timer.S.StartTime += IncreaseTimeBy * NumberOfRents;
        RentText.text = string.Format("RENT: {0}", Rent);
        DaysText.text = string.Format("Day: {0}", NumberOfRents + 1);
    }
}