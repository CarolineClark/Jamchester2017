using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private CharacterController characterController;
	public SpriteRenderer spriteRenderer;
	private readonly float defaultRunningSpeed = 10;
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
	public Animator animator;
	private bool flipped;
	private bool canMove;

	void Start () {
		canMove = false;
		tetrisMode = false;
		friction = defaultFriction;
		jumpSpeed = defaultJumpSpeed;
		runningSpeed = defaultRunningSpeed;
		characterController = GetComponent<CharacterController>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		EventManager.StartListening(Constants.tetrisEvent, StopMoving);
		EventManager.StartListening(Constants.cameraFollowingPlayerEvent, StartMoving);
		EventManager.StartListening(Constants.gameStartedEvent, StartMoving);
	}
	
	void Update() {
		if (canMove) {
			moveDirection.x = GetXVelocity();
			flipped = (moveDirection.x < 0 || (flipped && moveDirection.x <= 0));
			if (characterController.isGrounded) {
				if (Input.GetButton("Jump")) {
					moveDirection.y = jumpSpeed;
				}
			} else {
				moveDirection.y -= gravity * Time.deltaTime;
			}
		} else {
			moveDirection = Vector3.zero;
		}
	}

	void FixedUpdate () {
		characterController.Move(moveDirection * Time.deltaTime);
		animator.SetBool("move", moveDirection.x != 0);
		spriteRenderer.flipX = flipped;
	}

	float GetXVelocity() {
		if (friction == 1) {
			return GetHorizontalAxis() * runningSpeed;
		} else {
			return Mathf.Lerp(moveDirection.x, GetHorizontalAxis() * runningSpeed, friction * Time.deltaTime);
		}
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
		canMove = false;
	}

	void StartMoving(Hashtable h) {
		canMove = true;
	}
}
