using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {
	public GameObject CreditPanel;
	public CanvasGroup CreditPanelGroup;

	public void StartGame() {
		SceneManager.LoadScene("Play");
	}

	public void QuitGame() {
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#else
	Application.Quit();
#endif
	}

	private void OnDisable() {
		CreditPanelGroup.DOKill();
	}

	public void OpenCreditPanel() {
		CreditPanelGroup.DOKill();
		CreditPanelGroup.alpha = 0f;
		CreditPanel.SetActive(true);
		CreditPanelGroup.DOFade(1f, 0.5f);
	}

	public void CloseCreditPanel() {
		CreditPanelGroup.DOKill();
		CreditPanelGroup.alpha = 1f;
		CreditPanelGroup.DOFade(0f, 0.5f)
			.OnComplete(() => CreditPanel.SetActive(false));
	}
}
