using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
    
public class GameManager : MonoBehaviour
{

	public static GameManager instance = null;
	private int level = 1;
	public Image titleScreen;
	public Image endScreen;

	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    
		DontDestroyOnLoad(gameObject);
	}

	void Start() {
		EventManager.StartListening(Constants.gameEndedEvent, ShowFinalScreen);
		endScreen.gameObject.SetActive(false);
	}
	
	public void EndTitleScreen() {
		Debug.Log("ending title screen");
		titleScreen.gameObject.SetActive(false);
		EventManager.TriggerEvent(Constants.gameStartedEvent);
	}

	private void ShowFinalScreen(Hashtable h) {
		Debug.Log("game ended");
		endScreen.gameObject.SetActive(true);
	}

	public void QuitGame() {
		Debug.Log("game quit");
		Application.Quit();
	}
}