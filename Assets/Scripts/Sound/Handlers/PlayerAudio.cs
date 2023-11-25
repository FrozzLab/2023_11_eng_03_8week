public class PlayerAudio : AudioManagerBase<PlayerSoundName> 
{
	public void PlayTakeDamageSound() => Play(PlayerSoundName.TakeDamage);
	public void PlayDeathSound() => Play(PlayerSoundName.Death);
	public void PlayRunSound() => Play(PlayerSoundName.Run);
	public void PlayJumpSound() => Play(PlayerSoundName.Jump);
	public void PlayLandSound() => Play(PlayerSoundName.Land);
	public void PlayThrowDamageSound() => Play(PlayerSoundName.Throw);
	public void PlayMeleeSound() => Play(PlayerSoundName.Melee);
	public void PlayHideSound() => Play(PlayerSoundName.Hide);
}

public enum PlayerSoundName
{
    TakeDamage, 
	Death, 
	Run, 
	Jump,
	Land,
	Throw, 
	Melee,
	Hide,
}