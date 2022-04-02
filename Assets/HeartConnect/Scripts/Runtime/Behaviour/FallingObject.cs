using System;
using UnityEngine;

public abstract class FallingObject : MonoBehaviour {
	public float MoveSpeed;

	public float KillY;

	private Rigidbody2D _rb;

	protected virtual void Awake() {
		_rb = GetComponent<Rigidbody2D>();
	}

	private void OnTriggerEnter2D(Collider2D col) {
		if (col.CompareTag("Player")) {
			OnPlayerHit();
		}
	}
	protected virtual void OnPlayerHit() {
		Destroy(gameObject);
	}

	protected virtual void Update() {
		_rb.velocity = new Vector2(0, -MoveSpeed);

		if (transform.position.y <= KillY) {
			Destroy(gameObject);
		}
	}
}
