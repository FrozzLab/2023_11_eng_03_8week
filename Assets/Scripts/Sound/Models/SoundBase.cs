using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public abstract class SoundBase<T> where T : Enum
{
    protected AudioSource source;
    public T name;
    public AudioClip clip;
    [Range(0, 5f)]
    public float volume = 1f;

    public virtual void Init(GameObject gameObject, AudioMixerGroup mixerGroup)
    {
        if (clip == null) throw new SoundException($"{nameof(AudioClip)} cannot be empty. Add audio reference to {name}!!");
        source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
		source.outputAudioMixerGroup = mixerGroup;
    }
    
    public bool IsPlaying => source.isPlaying;
    public void Play() => source.Play();
    public void Stop() => source.Stop();
}