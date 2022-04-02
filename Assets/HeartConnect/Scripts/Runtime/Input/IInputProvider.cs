using UnityEngine;

public interface IInputProvider {

	public InputState GetState();

	public void AddMiddleware(IInputMiddleware middleware);
	public void RemoveMiddleware(IInputMiddleware middleware);
}

public struct InputState {
	public Vector2 movementDirection;

	public override string ToString() {
		return $"movementDirection:{movementDirection}";
	}

	public static InputState DEFAULT = new InputState {
		movementDirection = Vector2.zero
	};
}
