using CarterGames.Assets.AudioManager;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class GameOverManager : MonoBehaviour {
	public IntEvent HealthChangedEvent;

	public StringEvent SceneChangeRequest;

	private void OnEnable() {
		HealthChangedEvent.Register(OnHealthChanged);
	}

	private void OnDisable() {
		HealthChangedEvent.Unregister(OnHealthChanged);
	}

	private void OnHealthChanged(int health) {
		if (health <= 0) {
			AudioManager.instance.Play("SFX_GameOver");
			SceneChangeRequest.Raise("GameOver");
		}
	}
}
