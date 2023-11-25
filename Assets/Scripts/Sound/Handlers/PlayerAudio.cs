public class PlayerSoundsHandler : AudioManagerBase<PlayerSoundName> { }

public enum PlayerSoundName
{
    TakeDamage, 
	Death, 
	Run, 
	Jump,
	Throw, 
	Melee,
}