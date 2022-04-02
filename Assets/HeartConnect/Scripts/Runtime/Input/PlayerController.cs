using DG.Tweening;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
	public FloatVariable MoveSpeed;
	public InputProvider Provider;

	public Sprite NormalFace;
	public Sprite HappyFace;
	public Sprite SadFace;

	private Rigidbody2D _rb;
	private SpriteRenderer _sr;

	private void Awake() {
		_rb = GetComponent<Rigidbody2D>();
		_sr = GetComponent<SpriteRenderer>();
	}

	private void OnEnable() {
		if (Accelerometer.current != null)
			UnityEngine.InputSystem.InputSystem.EnableDevice(Accelerometer.current);
	}

	private void OnDisable() {
		if (Accelerometer.current != null)
			UnityEngine.InputSystem.InputSystem.DisableDevice(Accelerometer.current);
		_sr.DOKill();
	}

	private void Update() {
		Move();
	}

	private void Move() {
		var movementDirection = Provider.GetState().movementDirection;
		_rb.velocity = movementDirection * MoveSpeed.Value;
	}

	private void OnTriggerEnter2D(Collider2D col) {
		if (col.CompareTag("Cat")) {
			_sr.DOKill();
			DOTween.Sequence(_sr)
				.AppendCallback(() => _sr.sprite = HappyFace)
				.AppendInterval(.5f)
				.AppendCallback(() => _sr.sprite = NormalFace);
		} else if (col.CompareTag("Obstacle")) {
			_sr.DOKill();
			DOTween.Sequence(_sr)
				.AppendCallback(() => _sr.sprite = SadFace)
				.AppendInterval(.5f)
				.AppendCallback(() => _sr.sprite = NormalFace);
		}
	}
}
