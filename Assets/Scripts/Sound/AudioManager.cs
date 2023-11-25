using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;

public partial class AudioManager : AudioManagerBase<MenuSoundName>
{
    protected static AudioManager instance;
	public static AudioMixerGroup soundGroup;
	public static AudioMixerGroup musicGroup;

    public Music[] musics;
    private readonly Dictionary<MusicName, Music> _musicMap = new();
    private MusicName _currentMusic;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (var music in musics)
        {
            music.Init(gameObject);
            _musicMap.Add(music.name, music);
        }

        Validate(musics);
    }

    public void Play(MusicName name)
    {
        if (!_musicMap.ContainsKey(name)) throw new SoundException($"Music {name} not found. Add it to the {nameof(AudioManager)} before using it!!");

        foreach (var music in musics)
            music.Stop();

        _currentMusic = name;
        _musicMap[name].Play();
    }

    public void StopAll()
    {
        foreach (var music in musics)
            music.Stop();

        foreach (var sound in sounds)
            sound.Stop();
    }

	public static void ChangeVolumeOfSounds(float volume) => ChangeVolumeOfMixerGroup(volume, "SoundsVolume");

    public static void ChangeVolumeOfMusic(float volume) => ChangeVolumeOfMixerGroup(volume, "MusicVolume");

    private static void ChangeVolumeOfMixerGroup(float volume, string name)
    {
        if (volume < 0 || volume > 1) throw new SoundException($"volume has to be between 0 and 1. Provided value: {volume}. You want to go deaf?!");
        musicGroup.audioMixer.SetFloat(name, volume * Mathf.Log10(volume) * 20);
    }

    private void Validate(Music[] sounds)
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