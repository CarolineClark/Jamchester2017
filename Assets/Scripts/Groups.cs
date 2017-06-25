using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Groups : MonoBehaviour { 
    float lastFall;
    readonly float TIME_TO_FALL = 10;
    private Spawner spawner;
    private bool hitGround = false;
    private float speedOfFalling = 3.0f;
    private GameObject triggerColliders;
    private readonly string triggerChild = "TriggerColliders";
    private Rigidbody rb;

    void Start() {
        Debug.Log("starting");
        lastFall = Time.time;
        spawner = FindObjectOfType<Spawner>();
        triggerColliders = transform.Find(triggerChild).gameObject;
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            transform.position += new Vector3(-1, 0, 0);
        } else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            transform.position += new Vector3(1, 0, 0);
        } else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
            transform.Rotate(0, 0, -90);
        } else if (hitGround) {
            rb.isKinematic = true;
            spawner.SpawnNext();
            MoveTriggerCollidersUp();
            enabled = false;
        }
        transform.position += new Vector3(0, -1, 0) * Time.deltaTime * speedOfFalling;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != Constants.backgroundTag) {
            hitGround = true;
        }
    }

    void MoveTriggerCollidersUp() {
        triggerColliders.transform.position += new Vector3(0, 1, 0);
    }
}