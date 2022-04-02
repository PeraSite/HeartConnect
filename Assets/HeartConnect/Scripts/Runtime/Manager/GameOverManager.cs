using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {
	public IntEvent HealthChangedEvent;

	private void OnEnable() {
		HealthChangedEvent.Register(OnHealthChanged);
	}

	private void OnDisable() {
		HealthChangedEvent.Unregister(OnHealthChanged);
	}

	private void OnHealthChanged(int health) {
		if (health <= 0) {
			SceneManager.LoadScene("GameOver");
		}
	}
}
