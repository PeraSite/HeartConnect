using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour {
	public StringEvent SceneChangeRequested;
	public Image FadeImage;
	public BoolVariable IsFading;

	protected void Awake() {
		if (GameObject.FindGameObjectsWithTag("TransitionManager").Length > 1) {
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
	}

	protected void OnEnable() {
		SceneChangeRequested.Register(OnSceneChangeRequest);
	}

	private void OnDisable() {
		SceneChangeRequested.Unregister(OnSceneChangeRequest);
		FadeImage.DOKill();
	}

	private void OnSceneChangeRequest(string scene) => OnSceneChangeRequestAsync(scene).Forget();

	private async UniTaskVoid OnSceneChangeRequestAsync(string scene) {
		IsFading.Value = true;
		await FadeImage.DOFade(1f, 0.5f);
		await SceneManager.LoadSceneAsync(scene);
		await FadeImage.DOFade(0f, 0.5f);
		IsFading.Value = false;
	}
}
