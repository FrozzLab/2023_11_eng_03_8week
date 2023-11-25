public class EnemySoundsHandler : AudioManagerBase<EnemySoundName> 
{ 
	public void PlayTakeDamageSound() => Play(EnemySoundName.TakeDamage);
	public void PlayDeathSound() => Play(EnemySoundName.Death);
	public void PlayWalkSound() => Play(EnemySoundName.Walk);
	public void PlayRunSound() => Play(EnemySoundName.Run);
	public void PlayStartChasingSound() => Play(EnemySoundName.StartChasing);
	public void PlayJumpSound() => Play(EnemySoundName.Jump);
	public void PlayShootDamageSound() => Play(EnemySoundName.Shoot);
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