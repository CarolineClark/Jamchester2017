using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
    public GameObject[] locations;
    public GameObject[] groups;
    private int currentIndex;

    void Start() {
        EventManager.StartListening(Constants.tetrisEvent, StartTetris);
        currentIndex = 0;
    }

    public void SpawnNext() {
        Debug.Log(groups.Length <= currentIndex);

        if (groups.Length > currentIndex) {
            Instantiate(groups[currentIndex],
                    transform.position,
                    Quaternion.identity);
            currentIndex++;
        } else {
            EventManager.TriggerEvent(Constants.platformerEvent);
        }
    }

    void StartTetris(Hashtable h) {
        int level = TetrisLevelMessage.GetLevelFromHashtable(h);
        transform.position = locations[level - 1].transform.position;
        // spawn first block
        SpawnNext();
    }

    void StopTetris(Hashtable h) {

    }
}