using UnityEngine.SceneManagement;

public partial class AudioManager
{
    public static void PlayMusicOnGameStart()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        var name = GetMusicName(LevelNameExtensions.GetLevelName(sceneName));
        instance.Play(name);
    }

    public static void ChangeMusicOnLevelChange(LevelName sceneName)
    {
        var name = GetMusicName(sceneName);

        if (instance._currentMusic != name)
        {
            instance.Play(name);
        }
    }

    private static MusicName GetMusicName(LevelName sceneName)
    {
        return sceneName switch
        {
            LevelName.Menu => MusicName.MainMenu,
            LevelName.Level1 => MusicName.Level1,
            LevelName.Level2 => MusicName.Level2,
            LevelName.Level3 => MusicName.Level3,
            _ => MusicName.MainMenu,
        };
    }
}

public enum MusicName
{
    MainMenu, 
	Level1, 
	Level2, 
	Level3
}

public enum MenuSoundName
{
    ClickButton
}