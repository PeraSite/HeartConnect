using Sirenix.OdinInspector;
using UnityEngine;

public class ParallaxElement : MonoBehaviour {
	public float EffectAmount = 1f;
	public float Multiplier = 1f;
	public bool Inverted;

	private Transform _transform;
	private float _startPosition;

	[SerializeField]
	private float _length;

	private void Start() {
		_transform = GetComponent<Transform>();
		_startPosition = _transform.position.y;
	}

	private void Update() {
		var effectAmount = EffectAmount * Multiplier * Time.deltaTime;
		if (Inverted) effectAmount *= -1;

		var newY = _transform.position.y + effectAmount;

		if (newY >= _startPosition + _length || newY <= _startPosition - _length) {
			_transform.position = new Vector3(0, _startPosition);
		} else {
			_transform.position = new Vector3(0f, newY, 0f);
		}
	}

	[Button]
	private Vector3 Test() {
		var sr = GetComponent<SpriteRenderer>();
		return sr.bounds.size;
	}
}
