using System;
using DG.Tweening;
using UnityEngine;

public class TouchCat : MonoBehaviour {
	public Canvas Canvas;
	public RectTransform Icon;
	public CanvasGroup IconGroup;

	private void OnDisable() {
		Icon.DOKill();
		IconGroup.DOKill();
	}

	private void Update() {
		if (!Input.GetMouseButtonDown(0)) return;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(
			(RectTransform) Canvas.transform,
			Input.mousePosition,
			null,
			out var point
		);
		var result = Canvas.transform.TransformPoint(point);
		Icon.position = result;
		Icon.localScale = Vector3.zero;
		IconGroup.alpha = 0f;
		Icon.DOKill();

		DOTween.Sequence(Icon)
			.Append(Icon.DOScale(Vector3.one, 0.3f))
			.Join(IconGroup.DOFade(1f, 0.15f))
			.Join(Icon.DOShakeRotation(0.3f,new Vector3(0f, 0f, 30f)))
			.Append(Icon.DOScale(Vector3.zero, 0.3f));
	}
}
