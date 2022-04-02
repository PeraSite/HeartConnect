using UnityEngine;

public class SafeArea : MonoBehaviour {
	private RectTransform rectTransform;
	private Rect safeArea;
	private Vector2 minAnchor;
	private Vector2 maxAnchor;

	private void Awake() {
		rectTransform = GetComponent<RectTransform>();
		safeArea = Screen.safeArea;
		minAnchor = safeArea.position;
		maxAnchor = minAnchor + safeArea.size;

		//인스펙터 프로퍼티에 집어 넣을수 있게 비율로 변환 및 할당
		minAnchor.x /= Screen.width;
		minAnchor.y /= Screen.height;
		maxAnchor.x /= Screen.width;
		maxAnchor.y /= Screen.height;

		rectTransform.anchorMin = minAnchor;
		rectTransform.anchorMax = maxAnchor;
	}
}
