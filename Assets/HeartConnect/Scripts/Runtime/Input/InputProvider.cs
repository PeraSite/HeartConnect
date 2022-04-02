using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class InputProvider : SerializedScriptableObject, IInputProvider {
	[OdinSerialize]
	private readonly List<IInputMiddleware> _middlewares = new List<IInputMiddleware>();

	public InputState GetState() {
		var state = InputState.DEFAULT;
		foreach (var middleware in _middlewares) {
			state = middleware.Process(state);
		}

		return state;
	}

	public void AddMiddleware(IInputMiddleware middleware) {
		for (var i = 0; i < _middlewares.Count; i++) {
			if (middleware.Order() >= _middlewares[i].Order()) continue;
			_middlewares.Insert(i, middleware);
			return;
		}

		_middlewares.Add(middleware);
	}

	public void RemoveMiddleware(IInputMiddleware middleware) {
		_middlewares.Remove(middleware);
	}

	[Button]
	public void Clear() {
		_middlewares.Clear();
	}
}
