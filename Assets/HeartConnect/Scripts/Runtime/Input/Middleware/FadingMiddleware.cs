using UnityAtoms.BaseAtoms;
using UnityEngine;

#pragma warning disable CS0067

public class FadingMiddleware : ScriptableObject, IInputMiddleware {
	public int Order() => 2;

	public BoolVariable IsFading;

	public InputState Process(InputState previous) {
		return IsFading.Value ? InputState.DEFAULT : previous;
	}
}
