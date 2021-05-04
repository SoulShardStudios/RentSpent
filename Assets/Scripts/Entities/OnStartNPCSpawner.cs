using UnityEngine;
public class OnStartNPCSpawner : MonoBehaviour, IResetable
{
    public GameObject[] NPCS;
    private void Start()
    {
        AddToSingleton();
        ResetIt();
    }
    public void AddToSingleton() => RentCollector.Resetables.Add(GetComponent<IResetable>());
    public void ResetIt() => Instantiate(NPCS[Random.Range(0, NPCS.Length)], transform.position, Quaternion.identity);
}