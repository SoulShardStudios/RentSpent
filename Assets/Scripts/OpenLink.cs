using UnityEngine;
public class OpenLink : MonoBehaviour
{
    public void OpenALink(string LinkAdress) => Application.OpenURL(LinkAdress);
}