using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;

public class VariableResetManager : SerializedMonoBehaviour {
	public List<IntVariable> Variables = new List<IntVariable>();

	private void Start() {
		foreach (var variable in Variables) {
			variable.SetValue(variable.InitialValue, true);
		}
	}
}
