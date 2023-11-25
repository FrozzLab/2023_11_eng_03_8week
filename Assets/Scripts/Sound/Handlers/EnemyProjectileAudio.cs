public class EnemyProjectileAudio : AudioManagerBase<EnemyProjectileSoundName> 
{ 
	public void PlayHitSound() => Play(EnemyProjectileSoundName.Hit);
}

public enum EnemyProjectileSoundName
{
    Hit,
}