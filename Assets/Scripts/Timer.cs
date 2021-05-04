using UnityEngine;
using System.Collections;
using TMPro;
public class Timer : MonoBehaviour
{
    public int StartTime, MaxStartTime;
    private int Time;
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private Animator Clock;
    [SerializeField] private RentCollector Collector;
    public static Timer S { get; private set; }
    private void Awake() => S = this;
    public void StartTimer() 
    {
        AudioManager.S.PlaySound("TikTok"); 
        StartCoroutine(Second()); 
    }
    private IEnumerator Second()
    {
        Time = Mathf.Clamp(StartTime, 10, MaxStartTime);
        while (Time > 0)
        {
            Time--;
            Clock.speed = (StartTime - Time) / 60f;
            Text.text = string.Format("{0}:{1}", Mathf.RoundToInt(Time / 60), Time % 60 < 10 ? "0" + (Time % 60).ToString() : (Time % 60).ToString());
            yield return new WaitForSeconds(1);
        }
        Collector.CollectRent();
    }
}