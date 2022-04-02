using System;
using CarterGames.Assets.AudioManager;
using DG.Tweening;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cat : FallingObject {
	public int Score;

	public IntVariable ScoreVariable;
	public IntVariable FeverVariable;
	public BoolVariable IsFeverTime;

	public Sprite HappyFace;
	public Sprite SadFace;

	public float SadY;

	private const int FeverAmount = 5;

	private bool _isAnimating;
	private SpriteRenderer _sr;

	protected override void Awake() {
		base.Awake();
		_sr = GetComponent<SpriteRenderer>();
	}

	private void OnDisable() {
		_sr.DOKill();
	}

	protected override void Update() {
		base.Update();
		if (_isAnimating) return;

		if (transform.position.y <= SadY) {
			_sr.sprite = SadFace;
		}
	}

	protected override void OnPlayerHit() {
		if (_isAnimating) return;

		var score = IsFeverTime.Value ? Score * 2 : Score;
		ScoreVariable.Add(score);
		if (!IsFeverTime.Value)
			FeverVariable.Value = Mathf.Clamp(FeverVariable.Value + FeverAmount, 0, 100);

		AudioManager.instance.Play(Random.value >= 0.5f ? "SFX_Cat1" : "SFX_Cat2", 0.5f);

		DOTween.Sequence(_sr)
			.AppendCallback(() => {
				_isAnimating = true;
				_sr.sprite = HappyFace;
			})
			.Append(_sr.transform.DOScale(.15f, 0.5f).SetEase(Ease.OutExpo))
			.Append(_sr.DOColor(new Color(1f, 1f, 1f, 0f), 0.3f).SetEase(Ease.OutExpo))
			.AppendCallback(() => {
				_isAnimating = false;
				Destroy(gameObject);
			});
	}
}
