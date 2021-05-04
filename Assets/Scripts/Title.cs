using UnityEngine;
public class Title : MonoBehaviour
{
    public GameObject TUI;
    public GameObject[] ToEnable;
    private void OnEnable() => Time.timeScale = 0;
    public void Play()
    {
        TUI.SetActive(false);
        foreach (GameObject G in ToEnable)
            G.SetActive(true);
        Timer.S.StartTimer();
        Time.timeScale = 1;
    }
}