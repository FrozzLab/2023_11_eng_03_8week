using System;
using UnityEngine;

[Serializable]
public class Sound : SoundBase<SoundName>
{
    public override void Init(GameObject gameObject)
    {
        base.Init(gameObject);
        source.loop = false;
    }
}