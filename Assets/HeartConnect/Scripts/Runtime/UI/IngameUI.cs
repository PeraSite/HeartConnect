using System.Collections.Generic;
using DG.Tweening;
using Sirenix.Utilities;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour {
	[Header("Events")]
	public IntEvent HealthChangedEvent;

	public IntEvent ScoreChangedEvent;

	public IntEvent FeverChangedEvent;

	public BoolEvent IsFeverTimeChangedEvent;

	[Header("UI Elements")]
	public List<GameObject> HealthIcons;

	public TextMeshProUGUI ScoreText;

	public Image FeverGauge;

	public CanvasGroup FeverTimeIcon;

	[Header("Animations")]
	public float GaugeAnimationTime = 0.3f;

	public float FeverTimeIconAnimationTime = 0.3f;
	public float FeverTimeIconWaitTime = 1f;

	private void OnEnable() {
		HealthChangedEvent.Register(OnHealthChanged);
		ScoreChangedEvent.Register(OnScoreChanged);
		FeverChangedEvent.Register(OnFeverChanged);
		IsFeverTimeChangedEvent.Register(OnFeverTimeChanged);
	}


	private void OnDisable() {
		HealthChangedEvent.Unregister(OnHealthChanged);
		ScoreChangedEvent.Unregister(OnScoreChanged);
		FeverChangedEvent.Unregister(OnFeverChanged);
		IsFeverTimeChangedEvent.Unregister(OnFeverTimeChanged);

		FeverTimeIcon.DOKill();
		FeverGauge.DOKill();
	}

	private void OnFeverTimeChanged(bool isFeverTime) {
		if (isFeverTime) {
			DOTween.Sequence(FeverTimeIcon)
				.Append(FeverTimeIcon.DOFade(1f, FeverTimeIconAnimationTime))
				.AppendInterval(FeverTimeIconWaitTime)
				.Append(FeverTimeIcon.DOFade(0f, FeverTimeIconAnimationTime));
		}
	}


	private void OnFeverChanged(int fever) {
		FeverGauge.DOKill();
		FeverGauge.DOFillAmount(fever / 100f, GaugeAnimationTime);
	}

	private void OnHealthChanged(int health) {
		HealthIcons.ForEach((icon, index) => { icon.SetActive(index < health); });
	}

	private void OnScoreChanged(int score) {
		ScoreText.text = score.ToString();
	}
}
