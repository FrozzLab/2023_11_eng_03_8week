using System;
using UnityEngine;

[Serializable]
public class Music : SoundBase
{
    public MusicName name;

    public override void Init(GameObject gameObject)
    {
        base.Init(gameObject);
        source.loop = true;
    }
}