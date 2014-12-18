using UnityEngine;
using System.Collections;

public class HandMovement : MonoBehaviour {

	private int moveDirection;
	private float handSpeed;

	// Use this for initialization
	void Start () {
		moveDirection = 0;
		handSpeed = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		switch (moveDirection) {
		case 0:
			break;
		case 1:
			transform.Translate (new Vector3(0, handSpeed, 0));
			break;
		case -1:
			transform.Translate (new Vector3(0, -handSpeed, 0));
			break;
		}

		if(transform.position.y <= 2.5f || transform.position.y >= 7.5f) {
			StopMoving();
		}
	}

	public void MoveIn() {
		moveDirection = -1;
	}

	public void MoveOut() {
		moveDirection = 1;
	}

	public void StopMoving() {
		moveDirection = 0;
	}
}
