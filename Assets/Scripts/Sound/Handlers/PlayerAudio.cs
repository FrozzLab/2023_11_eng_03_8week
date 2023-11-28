using UnityEngine;

public class PlayerAudio : AudioManagerBase<PlayerSoundName> 
{
	public void PlayTakeDamageSound() => Play(PlayerSoundName.TakeDamage);
	public void PlayDeathSound() => Play(PlayerSoundName.Death);
	public void PlayRunSound(float speed) 
	{
		if(Mathf.Abs(speed) > 0.1f) PlayIfNotPlaying(PlayerSoundName.Run);
		else Stop(PlayerSoundName.Run);
	}
	public void PlayJumpSound() => Play(PlayerSoundName.Jump);
	public void PlayLandSound() => Play(PlayerSoundName.Land);
	public void PlayThrowSound() => Play(PlayerSoundName.Throw);
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