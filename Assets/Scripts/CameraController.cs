using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	private enum Mode {followPlayer, tetris, transitionToPlayer, transitionToTetris};
	private Mode currentMode;
	public GameObject[] tetrisLocations;
	private GameObject player;
	private Vector3 offset;
	private float transitionStartTime;
	private float timeToTransitionToTetris = 2;
	private float timeToTransitionToPlayer = 3;
	private Vector3 endPosition;
	int currentLevel;

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
		} else if (currentMode == Mode.transitionToTetris) {
			float progress = (Time.time - transitionStartTime)/timeToTransitionToTetris;
			if (progress < 1) {
				transform.position = Vector3.Lerp(transform.position, endPosition, (Time.time - transitionStartTime)/timeToTransitionToTetris);
			} else {
				currentMode = Mode.tetris;
				EventManager.TriggerEvent(Constants.cameraWatchingTetrisEvent, TetrisLevelMessage.CreateHashtable(currentLevel));
			}
		} else if (currentMode == Mode.transitionToPlayer) {
			float progress = (Time.time - transitionStartTime)/timeToTransitionToTetris;
			if (progress < 1) {
				transform.position = Vector3.Lerp(transform.position, endPosition, (Time.time - transitionStartTime)/timeToTransitionToPlayer);
			} else {
				currentMode = Mode.followPlayer;
				EventManager.TriggerEvent(Constants.cameraFollowingPlayerEvent);
			}
		}
	}

	Vector3 GetCameraPositionInPlayerMode() {
		return player.transform.position + offset;
	}

	void TetrisMode(Hashtable h) {
		currentMode = Mode.transitionToTetris;
		transitionStartTime = Time.time;
		currentLevel = TetrisLevelMessage.GetLevelFromHashtable(h);

		if (currentLevel == -1) {
			Debug.LogError("no location specified!");
		}
		if (currentLevel > tetrisLocations.Length) {
			Debug.LogError("not enough tetris locations specified");
		}
		// TODO lerp into tetris mode
		endPosition = tetrisLocations[currentLevel-1].transform.position;
	}

	void PlayerMode(Hashtable h) {
		currentMode = Mode.transitionToPlayer;
		endPosition = GetCameraPositionInPlayerMode();
		transitionStartTime = Time.time;
	}
}
