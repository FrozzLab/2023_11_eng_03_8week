public class PlayerProjectileAudio : AudioManagerBase<PlayerProjectileSoundName> 
{
	public void PlayChargeSound() 
	{
		StopAllSounds();
		Play(PlayerProjectileSoundName.Charge);
	} 
	public void PlayFullyChargedSound() 
	{
		StopAllSounds();
		Play(PlayerProjectileSoundName.FullyCharged);
	} 
	public void PlayExplodeSound() 
	{
		StopAllSounds();
		Play(PlayerProjectileSoundName.Explode);
	}
	public void PlayAbsorbSound()
	{
		StopAllSounds();
		Play(PlayerProjectileSoundName.Absorb);
	} 
}

public enum PlayerProjectileSoundName
{
	Charge,
	FullyCharged,
    Explode,
	Absorb,
}