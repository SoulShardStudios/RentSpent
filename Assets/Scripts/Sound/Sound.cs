using UnityEngine;
using UnityEngine.Audio;
[System.Serializable]
public class Sound
{
    public AudioClip ToPlay;
    public string Name;
    public bool Loop, PlayOnAwake;
    [Range(0f, 1f)] public float Volume;
    [Range(.1f, 3f)] public float Pitch;
    [HideInInspector] public AudioSource Source;
}