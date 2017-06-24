using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EventTest : MonoBehaviour {

    private UnityAction<Hashtable> someListener;

    void Awake ()
    {
        someListener = new UnityAction<Hashtable> (SomeFunction);
    }

    void OnEnable ()
    {
        EventManager.StartListening ("test", someListener);
        EventManager.StartListening ("Spawn", SomeOtherFunction);
        EventManager.StartListening ("Destroy", SomeThirdFunction);
    }

    void OnDisable ()
    {
        EventManager.StopListening ("test", someListener);
        EventManager.StopListening ("Spawn", SomeOtherFunction);
        EventManager.StopListening ("Destroy", SomeThirdFunction);
    }

    void SomeFunction (Hashtable h)
    {
        Debug.Log ("Some Function was called!");
    }
    
    void SomeOtherFunction (Hashtable h)
    {
        Debug.Log ("Some Other Function was called!");
    }
    
    void SomeThirdFunction (Hashtable h)
    {
        Debug.Log ("Some Third Function was called!");
    }
}