using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class TetrisSwitch : MonoBehaviour {
	private bool playerInRange;
	private bool canBeFlicked;
	public int level;
	public Sprite on;
	public Sprite off;
	private SpriteRenderer spriteRenderer;
	private GameObject showE;
	private string childString = "Press E";

	[SerializeField]	private AudioClip lever;
	private AudioSource audioSource;

	private void Awake()
	{
	Assert.IsNotNull(lever, "Fool of a Took! You don't have the lever sound attached to this gameobject");
	}

	void Start () {
		audioSource = GetComponent<AudioSource>();
		playerInRange = false;
		canBeFlicked = true;
		EventManager.StartListening(Constants.platformerEvent, Reactivated);
		spriteRenderer = GetComponent<SpriteRenderer>();
		showE = transform.Find(childString).gameObject;
		showE.SetActive(false);
	}

	void OnTriggerStay(Collider other) {
		if (canBeFlicked && other.gameObject.tag == Constants.playerTag) {
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
			audioSource.PlayOneShot(lever);
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
	}
}
