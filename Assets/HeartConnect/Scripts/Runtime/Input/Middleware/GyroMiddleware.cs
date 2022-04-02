using UnityEngine;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

#pragma warning disable CS0067

public class GyroMiddleware : ScriptableObject, IInputMiddleware {
	public int Order() => 999;

	private const float GYRO_MULTIPLIER = 2f;

	public InputState Process(InputState previous) {
		if (Accelerometer.current == null) return previous;
		if (!Accelerometer.current.enabled) return previous;
		var accel = Accelerometer.current.acceleration.ReadValue();
		var moveAmount = (Vector2) (accel * GYRO_MULTIPLIER);

		return new InputState {
			movementDirection = moveAmount
		};
	}
}
