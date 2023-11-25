using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Sound<TName> : SoundBase<TName> where TName : Enum
{
    public override void Init(GameObject gameObject)
    {
        base.Init(gameObject);
        source.loop = false;
		source.spatialBlend = 1f;
		source.outputAudioMixerGroup = AudioManager.soundGroup;
    }
}