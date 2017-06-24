using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisSwitch : MonoBehaviour {
	private bool playerInRange;
	private bool canBeFlicked;
	public int level;

	void Start () {
		playerInRange = false;
		canBeFlicked = true;
		EventManager.StartListening(Constants.platformerEvent, Reactivated);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == Constants.playerTag) {
			playerInRange = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == Constants.playerTag) {
			playerInRange = false;
		}
	}
	
	void Update () {
		if (FlickedSwitch()) {
			EventManager.TriggerEvent(Constants.tetrisEvent, TetrisLevelMessage.CreateHashtable(level));
			canBeFlicked = false;
		}	
	}

	bool FlickedSwitch() {
		return canBeFlicked && playerInRange && Input.GetKeyDown(KeyCode.Q);
	}

	void Reactivated(Hashtable h) {
		canBeFlicked = true;
	}
}
