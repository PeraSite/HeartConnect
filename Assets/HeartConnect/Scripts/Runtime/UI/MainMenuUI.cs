using DG.Tweening;
using UnityAtoms.BaseAtoms;
using UnityEditor;
using UnityEngine;

public class MainMenuUI : MonoBehaviour {
	public GameObject CreditPanel;
	public CanvasGroup CreditPanelGroup;

	public StringEvent SceneChangeRequest;

	public void StartGame() {
		SceneChangeRequest.Raise("Play");
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
		if (DOTween.IsTweening(CreditPanelGroup)) {
			return;
		}
		CreditPanelGroup.DOKill();
		CreditPanelGroup.alpha = 1f;
		CreditPanelGroup.DOFade(0f, 0.5f)
			.OnComplete(() => CreditPanel.SetActive(false));
	}
}
