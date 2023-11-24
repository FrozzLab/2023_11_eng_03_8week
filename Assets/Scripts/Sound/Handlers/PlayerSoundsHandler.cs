public partial class AudioManager
{
    public static void PlayPlayerDeathSound()
    {
        instance.Play(SoundName.PlayerDeath);
    }

    public static void PlayPlayerJumpSound()
    {
        instance.Play(SoundName.PlayerJump);
    }

    public static void PlayPlayerMeleeSound()
    {
        instance.Play(SoundName.PlayerMelee);
    }

    public static void PlayPlayerRunSound()
    {
        instance.Play(SoundName.PlayerRun);
    }

    public static void PlayPlayerTakeDamageSound()
    {
        instance.Play(SoundName.PlayerTakeDamage);
    }

    public static void PlayPlayerThrowSound()
    {
        instance.Play(SoundName.PlayerThrow);
    }
}