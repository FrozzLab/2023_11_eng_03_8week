using System;
using UnityEngine;

[Serializable]
public class Sound : SoundBase
{
    public SoundName name;

    public override void Init(GameObject gameObject)
    {
        base.Init(gameObject);
        source.loop = false;
    }
}