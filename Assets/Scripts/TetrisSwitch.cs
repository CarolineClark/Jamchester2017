using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetrisSwitch : MonoBehaviour {
	private bool playerInRange;
	private bool canBeFlicked;
	public int level;
	public Sprite on;
	public Sprite off;
	private SpriteRenderer spriteRenderer;

	void Start () {
		playerInRange = false;
		canBeFlicked = true;
		EventManager.StartListening(Constants.platformerEvent, Reactivated);
		spriteRenderer = GetComponent<SpriteRenderer>();
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
			Debug.Log("switch flicked");
			EventManager.TriggerEvent(Constants.tetrisEvent, TetrisLevelMessage.CreateHashtable(level));
			canBeFlicked = false;
			spriteRenderer.sprite = on;
		}	
	}

	bool FlickedSwitch() {
		return canBeFlicked && playerInRange && Input.GetKeyDown(KeyCode.Q);
	}

	void Reactivated(Hashtable h) {
		canBeFlicked = true;
		spriteRenderer.sprite = off;
	}
}
