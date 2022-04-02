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

	private CancellationToken Token;

	protected void Awake() {
		if (GameObject.FindGameObjectsWithTag("TransitionManager").Length > 1) {
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
		Token = this.GetCancellationTokenOnDestroy();
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
		await FadeImage.DOFade(1f, 0.5f).WithCancellation(Token);
		await SceneManager.LoadSceneAsync(scene).ToUniTask(cancellationToken: Token);
		await FadeImage.DOFade(0f, 0.5f).WithCancellation(Token);
	}
}
