using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class RentCollector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] TextsToFade;
    [SerializeField] private Image[] ImagesToFade;
    public float ModBy;
    private float Alpha;
    [SerializeField] private TextMeshProUGUI MoneyText, GameOverText;
    [SerializeField] private Image Background;
    public static List<IResetable> Resetables { get; private set; }
    private void Awake()
    {
        SetAlphas(0);
        Resetables = new List<IResetable>(0);
    }
    public void CollectRent() => StartCoroutine(CollectRentAnimation());
    private IEnumerator CollectRentAnimation()
    {
        AudioManager.S.StopSound("TikTok");
        AudioManager.S.StopSound("BGMusic");
        Time.timeScale = 0;
        MoneyText.text = MoneyManager.S.Money.ToString();
        while (Alpha < 1)
        {
            ModAlphas(ModBy);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSecondsRealtime(2);
        MoneyManager.S.UpdateRent();
        MoneyText.text = MoneyManager.S.Money.ToString();
        AudioManager.S.PlaySound("Rent");
        yield return new WaitForSecondsRealtime(2);
        if (MoneyManager.S.Money <= 0 || InventoryUIManager.S.MainInventory.IsEmpty())
        {
            SetAlphas(0);
            Background.color = Color.black;
            GameOverText.color = Color.white;
            while (GameOverText.color.a < 1)
            {
                GameOverText.color = new Color(GameOverText.color.r, GameOverText.color.g, GameOverText.color.b, GameOverText.color.a + ModBy);
                yield return new WaitForEndOfFrame();
            }
            AudioManager.S.PlaySound("GameOver");
            yield return new WaitForSecondsRealtime(2);
            Application.Quit();
        }
        else
        {
            StartCoroutine(SetupNextDay());
            while (Alpha > 0)
            {
                ModAlphas(-ModBy);
                yield return new WaitForEndOfFrame();
            }
        }
    }
    private void SetAlphas(float SetTo)
    {
        foreach (Image I in ImagesToFade)
            I.color = new Color(I.color.r, I.color.g, I.color.b, SetTo);
        foreach (TextMeshProUGUI T in TextsToFade)
            T.color = new Color(T.color.r, T.color.g, T.color.b, SetTo);
        Alpha = ImagesToFade[0].color.a;
    }
    private void ModAlphas(float ModBy)
    {
        foreach (Image I in ImagesToFade)
            I.color = new Color(I.color.r, I.color.g, I.color.b, I.color.a + ModBy);
        foreach (TextMeshProUGUI T in TextsToFade)
            T.color = new Color(T.color.r, T.color.g, T.color.b, T.color.a + ModBy);
        Alpha = ImagesToFade[0].color.a;
    }
    private IEnumerator SetupNextDay()
    {
        yield return new WaitForEndOfFrame();
        Timer.S?.StartTimer();
        Time.timeScale = 1;
        AudioManager.S?.PlaySound("TikTok");
        AudioManager.S?.PlaySound("BGMusic");
        List<IResetable> Resets = new List<IResetable>(0);
        Resets.AddRange(Resetables);
        while (Resets.Count > 0)
        {
            try
            {
                if (Resets[0] != null)
                    Resets[0].ResetIt();
            }
            catch (MissingReferenceException) { }

            Resets.RemoveAt(0);
        }
    }
}
