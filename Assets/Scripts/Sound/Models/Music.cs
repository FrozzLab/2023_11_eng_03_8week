using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Music : SoundBase<MusicName>
{
    public override void Init(GameObject gameObject, AudioMixerGroup mixerGroup)
    {
        base.Init(gameObject, mixerGroup);
        source.loop = true;
		source.spatialBlend = 0f;
    }
}