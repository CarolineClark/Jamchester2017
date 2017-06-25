using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnds : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		EventManager.TriggerEvent(Constants.gameEndedEvent);
	}
}
