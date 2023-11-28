public class EnemyAudio : AudioManagerBase<EnemySoundName> 
{ 
	public void PlayTakeDamageSound() => Play(EnemySoundName.TakeDamage);
	public void PlayDeathSound() => Play(EnemySoundName.Death);
	public void PlayWalkSound() => PlayIfNotPlaying(EnemySoundName.Walk);
	public void PlayRunSound() => PlayIfNotPlaying(EnemySoundName.Run);
	public void PlayStartChasingSound() => Play(EnemySoundName.StartChasing);
	public void PlayJumpSound() => Play(EnemySoundName.Jump);
	public void PlayShootSound() => Play(EnemySoundName.Shoot);
	public void PlayMeleeSound() => Play(EnemySoundName.Melee);
}

public enum EnemySoundName
{
    TakeDamage, 
	Death, 
	Walk, 
	Run, 
	StartChasing, 
	Jump, 
	Shoot, 
	Melee, 
}