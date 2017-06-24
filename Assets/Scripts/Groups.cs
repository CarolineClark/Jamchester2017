using UnityEngine;

public class Groups : MonoBehaviour { 
    float lastFall;
    readonly float TIME_TO_FALL = 10;
    private Spawner spawner;
    private bool hitGround = false;
    private float speedOfFalling;

    void Start() {
        Debug.Log("starting");
        lastFall = Time.time;
        spawner = FindObjectOfType<Spawner>();
        speedOfFalling = 3.0f;
    }

    void Update() {
        if (enabled) {
            ControlBlock();
        }
    }

    void FixedUpdate() {
        // RaycastHit hitInfo;
        // hitGround = Physics.Raycast(raycastDetecter.transform.position, new Vector3(0, -1, 0), out hitInfo, 0.1f);
        // if (hitGround) {
        //     Debug.Log("hit ground");
        // }
    }

    void OnCollisionEnter(Collision collision)
    {
        hitGround = true;
        // Debug.Log("OnColliderEnter hit");
    }

    void ControlBlock() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            transform.position += new Vector3(-1, 0, 0);
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            transform.position += new Vector3(1, 0, 0);
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            transform.Rotate(0, 0, -90);
        } else if (hitGround) {
            spawner.SpawnNext();
            enabled = false;
        }
        transform.position += new Vector3(0, -1, 0) * Time.deltaTime * speedOfFalling;
    }
}