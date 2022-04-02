using CarterGames.Assets.AudioManager;
using UnityAtoms.BaseAtoms;

public class Obstacle : FallingObject {
	public BoolVariable IsFeverTime;
	public IntVariable HealthVariable;

	protected override void OnPlayerHit() {
		base.OnPlayerHit();

		if (IsFeverTime.Value) return;

		HealthVariable.Subtract(1);
		AudioManager.instance.Play("SFX_Hit", 0.4f);
	}
}
