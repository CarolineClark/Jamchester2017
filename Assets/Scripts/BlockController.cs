using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {
	private Spawner spawner;

	void Start () {
		spawner = FindObjectOfType<Spawner>();
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			spawner.spawnNext();
		}
	}
}
