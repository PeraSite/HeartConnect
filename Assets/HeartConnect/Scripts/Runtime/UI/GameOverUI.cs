﻿using System;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {
	public IntVariable Score;
	public IntVariable BestScore;

	public TextMeshProUGUI ScoreText;
	public TextMeshProUGUI BestScoreText;

	private void Start() {
		ScoreText.text = Score.Value.ToString();
		BestScoreText.text = BestScore.Value.ToString();
	}

	public void ReturnMain() {
		SceneManager.LoadScene("MainMenu");
	}
}