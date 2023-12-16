using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public Sound[] sounds;
    public Music[] musics;

    private readonly Dictionary<SoundName, Sound> _soundsMap = new();
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

    private void Play(SoundName name)
    {
        if (!_soundsMap.ContainsKey(name)) throw new SoundException($"Sound {name} not found. Add it to the {nameof(AudioManager)} before using it!!");
        _soundsMap[name].Play();
    }

    private void Play(MusicName name)
    {
        if (!_musicMap.ContainsKey(name)) throw new SoundException($"Music {name} not found. Add it to the {nameof(AudioManager)} before using it!!");

        foreach (var music in musics)
            music.Stop();

        _currentMusic = name;

        _musicMap[name].Play();
    }

    private void StopAll()
    {
        foreach (var music in musics)
            music.Stop();

        foreach (var sound in sounds)
            sound.Stop();
    }

    public static void PlayMusicOnGameStart()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        var name = GetMusicName(sceneName);
        instance.Play(name);
    }

    public static void ChangeMusicOnLevelChange(string sceneName)
    {
        var name = GetMusicName(sceneName);

        if (instance._currentMusic != name)
        {
            instance.StopAll();
            instance.Play(name);
        }
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

    private static MusicName GetMusicName(string sceneName)
    {
        return sceneName switch
        {
            "Credits" or "Settings" or "Main Menu" => MusicName.MainMenuTheme,
            "Level 1" => MusicName.Level1Theme,
            "Level 2" => MusicName.Level2Theme,
            "Level 3" => MusicName.Level3Theme,
            _ => MusicName.MainMenuTheme,
        };
    }
}