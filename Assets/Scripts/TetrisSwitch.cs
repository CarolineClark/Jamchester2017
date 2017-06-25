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
	private GameObject showE;
	private string childString = "Press E";

	void Start () {
		playerInRange = false;
		canBeFlicked = true;
		EventManager.StartListening(Constants.platformerEvent, Reactivated);
		spriteRenderer = GetComponent<SpriteRenderer>();
		showE = transform.Find(childString).gameObject;
		showE.SetActive(false);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == Constants.playerTag) {
			playerInRange = true;
			showE.SetActive(true);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == Constants.playerTag) {
			playerInRange = false;
			showE.SetActive(false);
		}
	}
	
	void Update () {
		if (FlickedSwitch()) {
			EventManager.TriggerEvent(Constants.tetrisEvent, TetrisLevelMessage.CreateHashtable(level));
			canBeFlicked = false;
			spriteRenderer.sprite = on;
			showE.SetActive(false);
		}	
	}

	bool FlickedSwitch() {
		return canBeFlicked && playerInRange && Input.GetKeyDown(KeyCode.E);
	}

	void Reactivated(Hashtable h) {
		canBeFlicked = true;
		spriteRenderer.sprite = off;
		showE.SetActive(true);
	}
}
