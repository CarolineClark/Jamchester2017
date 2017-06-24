using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
    public GameObject[] locations;
    public GameObject[] group1;
    public GameObject[] group2;
    public GameObject[] group3;
    private GameObject[][] groups;
    private GameObject[][] instantiatedCubes;
    private int currentIndex;
    private int currentLevel;

    void Start() {
        EventManager.StartListening(Constants.tetrisEvent, StartTetris);
        currentIndex = 0;
        currentLevel = -1;
        groups = new GameObject[][]{group1, group2, group3};
        instantiatedCubes = new GameObject[3][];
    }

    public void SpawnNext() {
        Debug.Log("spawn next hit");
        if (currentLevel == -1) {
            Debug.LogError("spawner level not set");
            return;
        }
        if (groups.Length >= currentLevel && NumberInGroup() > currentIndex) {
            GameObject block = Instantiate(groups[currentLevel - 1][currentIndex],
                    transform.position,
                    Quaternion.identity);
            instantiatedCubes[currentLevel - 1][currentIndex] = block;
            currentIndex++;
        } else {
            EventManager.TriggerEvent(Constants.platformerEvent);
        }
    }

    void StartTetris(Hashtable h) {
        currentIndex = 0;
        currentLevel = TetrisLevelMessage.GetLevelFromHashtable(h);
        if (instantiatedCubes[currentLevel - 1] != null) {
            foreach(GameObject g in instantiatedCubes[currentLevel - 1]) {
                Destroy(g);
            }
        }
        
        instantiatedCubes[currentLevel - 1] = new GameObject[NumberInGroup()];
        transform.position = locations[currentLevel - 1].transform.position;
        // spawn first block
        SpawnNext();
    }

    int NumberInGroup() {
        return groups[currentLevel - 1].Length;
    }

    void RestartTetris() {
        // TODO: wipe blocks and start again.
    }
}