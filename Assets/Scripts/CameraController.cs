using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	private enum Mode {followPlayer, tetris};
	private Mode currentMode;
	public GameObject[] tetrisLocations;
	private GameObject player;
	private Vector3 offset;

	void Start () {
		EventManager.StartListening(Constants.tetrisEvent, TetrisMode);	
		EventManager.StartListening(Constants.platformerEvent, PlayerMode);	
		currentMode = Mode.followPlayer;
		player = GameObject.FindGameObjectWithTag(Constants.playerTag);
		offset = new Vector3(0, 0, -10);
	}
	
	void LateUpdate () {
		if (currentMode == Mode.followPlayer) {
			transform.position = GetCameraPositionInPlayerMode();
		}
	}

	Vector3 GetCameraPositionInPlayerMode() {
		return player.transform.position + offset;
	}

	void TetrisMode(Hashtable h) {
		currentMode = Mode.tetris;
		// get level, and go to appropriate camera tetris point.
		int level = TetrisLevelMessage.GetLevelFromHashtable(h);
		Debug.Log("level = " + level);

		if (level == -1) {
			Debug.LogError("no location specified!");
		}
		// this should change to lerp.
		if (level > tetrisLocations.Length) {
			Debug.LogError("not enough tetris locations specified");
		}
		transform.position = tetrisLocations[level-1].transform.position;
	}

	void PlayerMode(Hashtable h) {
		currentMode = Mode.followPlayer;
		// lerp camera position to GetCameraPositionInPlayerMode()
	}
}
