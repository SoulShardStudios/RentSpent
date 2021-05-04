using UnityEngine;
using System.Collections;
public class NPCSpawner : MonoBehaviour
{
    public GameObject[] NPCS;
    public RangeFloat TimeDelay;
    private bool IsVisible;
    [HideInInspector] public bool STOP;
    private void OnBecameVisible() => IsVisible = true;
    private void OnBecameInvisible() => IsVisible = false;
    private void OnEnable() => StartCoroutine(SpawnNPC());
    private IEnumerator SpawnNPC()
    {
        while (!STOP)
        {
            if (!IsVisible)
            {
                yield return new WaitForSeconds(TimeDelay.GetRandom());
                Instantiate(NPCS[Random.Range(0, NPCS.Length)], transform.position, Quaternion.identity);
            }
        }
    }
}