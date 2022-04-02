using System;
using System.Collections.Generic;
using System.Threading;
using CarterGames.Assets.AudioManager;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class FeverTimeManager : MonoBehaviour {
	[Header("Variables")]
	public BoolVariable IsFeverTime;

	public IntVariable Fever;
	public IntEvent FeverChangedEvent;
	public FloatVariable MoveSpeed;

	[Header("Settings")]
	public float FeverTimeSeconds = 5f;

	[Header("Backgrounds")]
	public Sprite NormalBackground;

	public Sprite FeverBackground;

	public List<SpriteRenderer> Backgrounds;

	[Header("Audio")]
	public AudioClip FeverTimeMusic;

	public AudioClip NormalMusic;

	private CancellationToken _cts;

	private void Start() {
		_cts = this.GetCancellationTokenOnDestroy();
	}

	private void OnEnable() {
		FeverChangedEvent.Register(OnFeverChanged);
	}

	private void OnDisable() {
		FeverChangedEvent.Unregister(OnFeverChanged);
		Backgrounds.ForEach(background => background.DOKill());
	}

	private void OnFeverChanged(int fever) {
		if (IsFeverTime.Value) return;

		if (fever >= 100) {
			StartFeverTime().Forget();
		}
	}

	[Button]
	private void MakeFeverFull() {
		Fever.Value = 100;
	}

	private async UniTaskVoid StartFeverTime() {
		IsFeverTime.Value = true;
		MoveSpeed.Value = 10f;
		Backgrounds.ForEach(background => {
			DOTween.Sequence(background)
				.Append(background.DOColor(Color.black, 0.3f))
				.AppendCallback(() => background.sprite = FeverBackground)
				.Append(background.DOColor(Color.white, 0.3f));
		});
		MusicPlayer.instance.PlayTrack(FeverTimeMusic, TransitionType.CrossFade);

		await UniTask.Delay(TimeSpan.FromSeconds(FeverTimeSeconds), cancellationToken: _cts);
		StopFeverTime();
	}

	private void StopFeverTime() {
		IsFeverTime.Value = false;
		MoveSpeed.Value = 5f;
		Fever.Value = 0;

		MusicPlayer.instance.PlayTrack(NormalMusic, TransitionType.CrossFade);

		Backgrounds.ForEach(background => {
			DOTween.Sequence(background)
				.Append(background.DOColor(Color.black, 0.3f))
				.AppendCallback(() => background.sprite = NormalBackground)
				.Append(background.DOColor(Color.white, 0.3f));
		});
	}
}
