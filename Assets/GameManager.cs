using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
    
public class GameManager : MonoBehaviour
{

	public static GameManager instance = null;
	private int level = 1;
	public Image titleScreen;

	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    
		DontDestroyOnLoad(gameObject);
	}
	
	public void EndTitleScreen() {
		Debug.Log(titleScreen);
		titleScreen.gameObject.SetActive(false);
		// send event that game has started
		EventManager.TriggerEvent(Constants.gameStartedEvent);
	}
}