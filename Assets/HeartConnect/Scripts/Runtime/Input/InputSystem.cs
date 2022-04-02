using System.Collections.Generic;
using Sirenix.OdinInspector;

public class InputSystem : SerializedMonoBehaviour {
	public InputProvider Provider;

	public List<IInputMiddleware> Middlewares = new List<IInputMiddleware>();

	private void OnEnable() {
		foreach (var middleware in Middlewares) {
			Provider.AddMiddleware(middleware);
		}
	}

	private void OnDisable() {
		Provider.Clear();
	}
}
