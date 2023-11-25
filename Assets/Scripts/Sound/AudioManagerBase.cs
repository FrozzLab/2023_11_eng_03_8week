using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;

public abstract class AudioManagerBase<TName> : MonoBehaviour where TName : Enum
{
    public Sound<TName>[] sounds;

    private readonly Dictionary<TName, Sound<TName>> _soundsMap = new();

    private void Awake()
    {
        foreach (var sound in sounds)
        {
            sound.Init(gameObject);
            _soundsMap.Add(sound.name, sound);
        }

        Validate(sounds);
    }

    public void Play(TName name)
    {
        if (!_soundsMap.ContainsKey(name)) throw new SoundException($"Sound {name} not found. Add it to the {nameof(AudioManager)} before using it!!");
        _soundsMap[name].Play();
    }

	public void PlayIfNotPlaying(TName name)
    {
        if (!_soundsMap.ContainsKey(name)) throw new SoundException($"Sound {name} not found. Add it to the {nameof(AudioManager)} before using it!!");
        if(_soundsMap[name].IsPlaying) return;
        _soundsMap[name].Play();
    }

    private void Validate(Sound<TName>[] sounds)
    {
        var duplicates = sounds.GroupBy(s => s.name).Where(s => s.Count() > 1);
        if (duplicates.Any())
        {
            var names = duplicates.SelectMany(s => s.Select(s => s.name.ToString()).ToArray());
            var joinedNames = string.Join(", ", names);
            throw new SoundException($"{GetType().Name} contains duplicated sounds: {joinedNames}. Delete the duplicates!!");
        }
    }
}