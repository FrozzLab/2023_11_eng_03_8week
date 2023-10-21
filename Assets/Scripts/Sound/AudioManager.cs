using UnityEngine;
using System;
using UnityEngine.Audio;
using System.Linq;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Music[] musics;
    public static AudioManager instance;

    private readonly Dictionary<SoundName, Sound> _soundsMap;
    private readonly Dictionary<MusicName, Music> _musicMap;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this;
        else throw new SoundException($"there can be only one {nameof(AudioManager)}. Delete duplicates!!");
    }

    private void Start()
    {
        Validate(sounds);
        Validate(musics);

        foreach (var sound in sounds)
        {
            sound.Init(gameObject);
            _soundsMap.Add(sound.name, sound);
        }

        foreach (var sound in musics)
        {
            sound.Init(gameObject);
            _musicMap.Add(sound.name, sound);
        }
    }

    public static void Play(SoundName name) => instance.PlayOnInstance(name);

    public static void Play(MusicName name) => instance.PlayOnInstance(name);

    public static void StopAll() => instance.StopAllOnInstance();

    private void PlayOnInstance(SoundName name)
    {
        if(!_soundsMap.ContainsKey(name)) throw new SoundException($"Sound {name} not found. Add it to the {nameof(AudioManager)} before using it!!");
        _soundsMap[name].Play();
    }

    private void PlayOnInstance(MusicName name)
    {
        if (!_musicMap.ContainsKey(name)) throw new SoundException($"Sound {name} not found. Add it to the {nameof(AudioManager)} before using it!!");

        foreach (var music in musics) 
            music.Stop();

        _musicMap[name].Play();
    }

    private void StopAllOnInstance()
    {
        foreach (var music in musics)
            music.Stop();
    }

    private static void Validate(Sound[] sounds)
    {
        var duplicates = sounds.GroupBy(s => s.name).Where(s => s.Count() > 1);
        if (duplicates.Any())
        {
            var names = duplicates.SelectMany(s => s.Select(s => s.name.ToString()).ToArray());
            var joinedNames = string.Join(", ", names);
            throw new SoundException($"{nameof(AudioManager)} contains duplicated sounds: {joinedNames}. Delete the duplicates!!");
        }
    }

    private static void Validate(Music[] sounds)
    {
        var duplicates = sounds.GroupBy(s => s.name).Where(s => s.Count() > 1);
        if (duplicates.Any())
        {
            var names = duplicates.SelectMany(s => s.Select(s => s.name.ToString()).ToArray());
            var joinedNames = string.Join(", ", names);
            throw new SoundException($"{nameof(AudioManager)} contains duplicated musics: {joinedNames}. Delete the duplicates!!");
        }
    }
}