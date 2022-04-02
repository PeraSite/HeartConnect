using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class BestScoreManager : MonoBehaviour {
	public IntEvent ScoreChangedEvent;
	public IntVariable BestScore;

	private void OnEnable() {
		ScoreChangedEvent.Register(OnScoreChanged);
	}

	private void Start() {
		BestScore.SetValue(PlayerPrefs.GetInt("BestScore", 0), true);
	}

	private void OnDisable() {
		ScoreChangedEvent.Unregister(OnScoreChanged);
		PlayerPrefs.SetInt("BestScore", BestScore.Value);
	}

	private void OnScoreChanged(int score) {
		if (score > BestScore.Value) {
			BestScore.Value = score;
		}
	}
}
