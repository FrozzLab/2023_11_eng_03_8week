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

[Serializable]
public class SoundBase
{
    protected AudioSource source;
    public AudioClip clip;
    [Range(0, 1f)]
    public float volume = .5f;

    public virtual void Init(GameObject gameObject)
    {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
    }

    public void Play() => source.Play();
    public void Stop() => source.Stop();
}

public enum SoundName
{
    PlayerTakeDamage, PlayerDeath, PlayerWalk, PlayerJump, PlayerThrow, PlayerMelee,
    EnemyTakeDamage, EnemyDeath, EnemyWalk, EnemyJump, EnemyShoot, EnemyMelee, //another sound for dark night, dark magician, dark knight etc?
}

public enum MusicName
{
    MainMenuTheme, Level1Theme, Level2Theme, Level3Theme
}