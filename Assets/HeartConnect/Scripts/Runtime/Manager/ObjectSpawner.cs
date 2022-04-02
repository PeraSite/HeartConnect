using System.Collections.Generic;
using PeraCore.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

public class ObjectSpawner : SerializedMonoBehaviour {
	public List<GameObject> Prefabs = new List<GameObject>();

	[MinMaxSlider(-2, 2f)]
	public Vector2 SpawnXRange = new Vector2(-2f, 2f);

	public float SpawnY;

	public float SpawnTime = 5f;

	private List<GameObject> _pool;

	private float _timer;

	private void Start() {
		_pool = new List<GameObject>();
	}

	[Button]
	public void SpawnObject() {
		var prefab = Prefabs.Random();
		var position = new Vector2(Random.Range(SpawnXRange.x, SpawnXRange.y), SpawnY);
		var created = Instantiate(prefab, position, Quaternion.identity);
		_pool.Add(created);
	}

	private void Update() {
		_timer += Time.deltaTime;
		if (_timer >= SpawnTime) {
			SpawnObject();
			_timer = 0f;
		}
	}
}
