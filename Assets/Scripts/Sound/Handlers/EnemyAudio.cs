public class EnemySoundsHandler : AudioManagerBase<EnemySoundName> { }

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