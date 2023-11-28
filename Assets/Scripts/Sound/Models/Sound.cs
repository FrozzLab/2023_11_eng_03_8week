using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Sound<TName> : SoundBase<TName> where TName : Enum
{
    public override void Init(GameObject gameObject, AudioMixerGroup mixerGroup)
    {
        base.Init(gameObject, mixerGroup);
        source.loop = false;
		source.spatialBlend = 1f;
    }
}