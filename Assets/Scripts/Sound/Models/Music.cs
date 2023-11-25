using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Music : SoundBase<MusicName>
{
    public override void Init(GameObject gameObject)
    {
        base.Init(gameObject);
        source.loop = true;
		source.spatialBlend = 0f;
		source.outputAudioMixerGroup = AudioManager.musicGroup;
    }
}