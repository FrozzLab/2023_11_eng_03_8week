public class PlayerProjectileAudio : AudioManagerBase<PlayerProjectileSoundName> 
{
	public void PlayChargeSound() => Play(PlayerProjectileSoundName.Charge);
	public void PlayFullyChargedSound() => Play(PlayerProjectileSoundName.FullyCharged);
	public void PlayExplodeSound() => Play(PlayerProjectileSoundName.Explode);
	public void PlayAbsorbSound() => Play(PlayerProjectileSoundName.Absorb);
}

public enum PlayerProjectileSoundName
{
	Charge,
	FullyCharged,
    Explode,
	Absorb,
}