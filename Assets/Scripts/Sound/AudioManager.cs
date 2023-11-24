using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public partial class AudioManager : MonoBehaviour
{
    protected static AudioManager instance;
    public Sound[] sounds;
    public Music[] musics;

    private readonly Dictionary<SoundName, Sound> _soundsMap = new();
    private readonly Dictionary<MusicName, Music> _musicMap = new();

    private MusicName _currentMusic;

    private void Awake()
    {
		Debug.Log("test");
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

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

        Validate(sounds);
        Validate(musics);
    }

    protected void Play(SoundName name)
    {
        if (!_soundsMap.ContainsKey(name)) throw new SoundException($"Sound {name} not found. Add it to the {nameof(AudioManager)} before using it!!");
        _soundsMap[name].Play();
    }

    protected void Play(MusicName name)
    {
        if (!_musicMap.ContainsKey(name)) throw new SoundException($"Music {name} not found. Add it to the {nameof(AudioManager)} before using it!!");

        foreach (var music in musics)
            music.Stop();

        _currentMusic = name;

        _musicMap[name].Play();
    }

    protected void StopAll()
    {
        foreach (var music in musics)
            music.Stop();

        foreach (var sound in sounds)
            sound.Stop();
    }

    public static void ChangeVolumeOfMusic(float volume)
    {
        if (volume < 0 || volume > 2) throw new SoundException($"music volume has to be between 0 and 2. Provided value: {volume}. You want to go deaf?!");
        foreach (var music in instance.musics)
        {
            music.ChangeVolume(volume);
        }
    }

    public static void ChangeVolumeOfSounds(float volume)
    {
        if (volume < 0 || volume > 2) throw new SoundException($"sound volume has to be between 0 and 2. Provided value: {volume}. You want to go deaf?!");
        foreach (var sound in instance.sounds)
        {
            sound.ChangeVolume(volume);
        }
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