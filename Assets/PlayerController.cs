using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private CharacterController characterController;
	private float maxSpeed = 10;
	private float jumpSpeed = 10;
	private Vector3 moveDirection = Vector3.zero;
	private float gravity = 9.8F;

	void Start () {
		Debug.Log("start");
		characterController = GetComponent<CharacterController>();
	}
	
	void Update() {
		moveDirection.y -= gravity * Time.deltaTime;
		moveDirection.x = Input.GetAxis("Horizontal") * maxSpeed;
		if (characterController.isGrounded) {
			if (Input.GetButton("Jump")) {
				moveDirection.y = jumpSpeed;
			}
		}
	}

	void FixedUpdate () {
		characterController.Move(moveDirection * Time.deltaTime);
	}
}
