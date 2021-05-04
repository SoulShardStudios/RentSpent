using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager S { get; private set; }
    public Sound[] sounds;
    private void Awake()
    {
        S = this;
        foreach (Sound S in sounds)
            InitializeAudioClip(S);
    }
    private void InitializeAudioClip(Sound sound)
    {
        sound.Source = gameObject.AddComponent<AudioSource>();
        sound.Source.clip = sound.ToPlay;
        sound.Source.playOnAwake = sound.PlayOnAwake;
        sound.Source.loop = sound.Loop;
        sound.Source.volume = sound.Volume;
        sound.Source.pitch = sound.Pitch;
        if (sound.PlayOnAwake)
            sound.Source.Play();
    }
    public void PlaySound(Sound sound) => sound.Source.Play();
    public void StopSound(Sound sound) => sound.Source.Stop();
    public void StopSound(string SoundName) => StopSound(ConvertSoundNameToSound(SoundName));
    public void PlaySound(string SoundName) => PlaySound(ConvertSoundNameToSound(SoundName));
    private Sound ConvertSoundNameToSound(string SoundName)
    {
        foreach (Sound S in sounds)
            if (S.Name == SoundName)
                return S;
        return null;
    }
}