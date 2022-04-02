using UnityEngine;
using UnityEngine.InputSystem;

#pragma warning disable CS0067

public class NewInputSystemMiddleware : MonoBehaviour, IInputMiddleware {
	public int Order() => 0;

	private Vector2 movementDirection;

	public InputState Process(InputState previous) {
		return new InputState {
			movementDirection = movementDirection
		};
	}

	public void OnMove(InputValue value) {
		movementDirection = value.Get<Vector2>();
	}
}
