using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;
    public Music[] musics;

    private readonly Dictionary<SoundName, Sound> _soundsMap = new();
    private readonly Dictionary<MusicName, Music> _musicMap = new();

    private MusicName _currentMusic;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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

        _currentMusic = name;
        
        _musicMap[name].Play();
    }

    private void StopAllOnInstance()
    {
        foreach (var music in musics)
            music.Stop();
    }

    public void PlayMusicOnGameStart()
    {
        Play(MusicName.MainMenuTheme);
    }
    
    public void ChangeMusicOnLevelChange(string sceneName)
    {
        switch (sceneName)
        {
            case "Credits":
            case "Settings":
            case "Main Menu": 
                if (instance._currentMusic != MusicName.MainMenuTheme)
                {
                    StopAll();
                    Play(MusicName.MainMenuTheme);
                }
                break;
            case "Level 1":
                StopAll();
                Play(MusicName.Level1Theme);
                break;
            case "Level 2":
                StopAll();
                Play(MusicName.Level2Theme);
                break;
            case "Level 3":
                StopAll();
                Play(MusicName.Level3Theme);
                break;
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