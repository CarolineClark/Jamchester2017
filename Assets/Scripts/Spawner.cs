using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
    public GameObject[] locations;
    public GameObject[] group1;
    public GameObject[] group2;
    public GameObject[] group3;
    private GameObject[][] groups;
    private int currentIndex;
    private int currentLevel;

    void Start() {
        EventManager.StartListening(Constants.tetrisEvent, StartTetris);
        currentIndex = 0;
        currentLevel = -1;
        groups = new GameObject[][]{group1, group2, group3};
    }

    public void SpawnNext() {
        Debug.Log("spawn next hit");
        if (currentLevel == -1) {
            Debug.LogError("spawner level not set");
            return;
        }
        if (groups.Length >= currentLevel && groups[currentLevel - 1].Length > currentIndex) {
            Instantiate(groups[currentLevel - 1][currentIndex],
                    transform.position,
                    Quaternion.identity);
            currentIndex++;
        } else {
            EventManager.TriggerEvent(Constants.platformerEvent);
        }
    }

    void StartTetris(Hashtable h) {
        Debug.Log("tetris started");
        currentLevel = TetrisLevelMessage.GetLevelFromHashtable(h);
        transform.position = locations[currentLevel - 1].transform.position;
        // spawn first block
        SpawnNext();
    }

    void RestartTetris() {
        // TODO: wipe blocks and start again.
    }
}