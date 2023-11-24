public partial class AudioManager
{
    public static void PlayEnemyDeathSound()
    {
        instance.Play(SoundName.EnemyDeath);
    }

    public static void PlayEnemyJumpSound()
    {
        instance.Play(SoundName.EnemyJump);
    }

    public static void PlayEnemyMeleeSound()
    {
        instance.Play(SoundName.EnemyMelee);
    }

    public static void PlayEnemyShootSound()
    {
        instance.Play(SoundName.EnemyShoot);
    }

    public static void PlayEnemyTakeDamageSound()
    {
        instance.Play(SoundName.EnemyTakeDamage);
    }

    public static void PlayEnemyWalkSound()
    {
        instance.Play(SoundName.EnemyWalk);
    }
}