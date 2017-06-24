using System.Collections;
using UnityEngine;

public class TetrisLevelMessage {
    private static readonly string LEVEL_KEY = "level";

    public static Hashtable CreateHashtable(int level) {
        Hashtable h = new Hashtable();
        h.Add(LEVEL_KEY, level);
        return h;
    }

    public static int GetLevelFromHashtable(Hashtable h) {
		Debug.Log(h);
        if (h != null && h.ContainsKey(LEVEL_KEY)) {
            return (int) h[LEVEL_KEY];
        }
        return -1;
    }
} 