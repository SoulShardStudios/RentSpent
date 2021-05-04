using UnityEngine;
using TMPro;
public class DissapearAndDestory : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public float ModBy;
    void Update()
    {
        Text.color = new Color(Text.color.r, Text.color.r, Text.color.b, Text.color.a - ModBy);
        if (Text.color.a < 0)
            Destroy(gameObject);
    }
}