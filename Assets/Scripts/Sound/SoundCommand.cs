using UnityEngine;
public class SoundCommand : MonoBehaviour
{
    public void PlaySoundCommand(string SoundName) => AudioManager.S.PlaySound(SoundName);
}