using System;
using UnityEngine;

[Serializable]
public class Music : SoundBase<MusicName>
{
    public override void Init(GameObject gameObject)
    {
        base.Init(gameObject);
        source.loop = true;
    }
}