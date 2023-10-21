using System;
using UnityEngine;

[Serializable]
public class SoundBase
{
    protected AudioSource source;
    public AudioClip clip;
    [Range(0, 1f)]
    public float volume = .5f;

    public virtual void Init(GameObject gameObject)
    {
        if (clip == null) throw new SoundException($"{nameof(AudioClip)} cannot be empty. Add audio reference to name?");
        source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
    }

    public void Play() => source.Play();
    public void Stop() => source.Stop();
}