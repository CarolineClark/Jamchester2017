using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private CharacterController characterController;
	private readonly float defaultRunningSpeed = 15;
	private readonly float defaultJumpSpeed = 8;
	private readonly float slowRunningSpeed = 3;
	private readonly float iceRunningSpeed = 50;
	private readonly float bigJumpSpeed = 15;
	private float jumpSpeed;
	private float runningSpeed;
	private Vector3 moveDirection = Vector3.zero;
	private float gravity = 9.8F;
	private bool invertedControls = false;
	private readonly float defaultFriction = 1f;
	private readonly float iceFriction = 0.1f;
	private float friction;
	private bool tetrisMode;

	void Start () {
		tetrisMode = false;
		friction = defaultFriction;
		jumpSpeed = defaultJumpSpeed;
		runningSpeed = defaultRunningSpeed;
		characterController = GetComponent<CharacterController>();
		EventManager.StartListening(Constants.tetrisEvent, StopMoving);
		EventManager.StartListening(Constants.platformerEvent, StartMoving);
	}
	
	void Update() {
		if (tetrisMode) {
			moveDirection = Vector3.zero;
		} else {
			float vel = GetHorizontalAxis() * runningSpeed;
			moveDirection.x = Mathf.Lerp(moveDirection.x, vel, friction * Time.deltaTime);
			if (characterController.isGrounded) {
				if (Input.GetButton("Jump")) {
					moveDirection.y = jumpSpeed;
				}
			} else {
				moveDirection.y -= gravity * Time.deltaTime;
			}
		}
	}

	void FixedUpdate () {
		characterController.Move(moveDirection * Time.deltaTime);
	}

	void OnTriggerStay(Collider other) {
		string tag = other.gameObject.tag;
		if (tag == Constants.slowBlockTag) {
			runningSpeed = slowRunningSpeed;
		}
		if (tag == Constants.bigJumpBlockTag) {
			jumpSpeed = bigJumpSpeed;
		}
		if (tag == Constants.invertedBlockTag) {
			invertedControls = true;
		}
		if (tag == Constants.iceBlockTag) {
			friction = iceFriction;
			runningSpeed = iceRunningSpeed;
		}
	}

	void OnTriggerExit(Collider other) {
		jumpSpeed = defaultJumpSpeed;
		runningSpeed = defaultRunningSpeed;
		invertedControls = false;
		friction = defaultFriction;
	}

	float GetHorizontalAxis() {
		float velocity = Input.GetAxis("Horizontal");
		return invertedControls ? -1 * velocity : velocity;
	}

	void StopMoving(Hashtable h) {
		tetrisMode = true;
	}

	void StartMoving(Hashtable h) {
		tetrisMode = false;
	}
}
