using UnityEngine;
using System.Collections;

public class ContainerManager : MonoBehaviour {
	private float minPosX;
	private bool rotateMiddle;

	private float maxPosX;
	private float smoothMove;
	private float smoothMovePerSecond;
	private float minRotation;
	private float maxRotation;
	private float smoothRotate;
	private float posY;

	private bool playing = false;
	private bool decreasing = false;

	private Vector3 rotationPoint = Vector3.zero;
	private Vector3 oldPosition = Vector3.zero;

	// Use this for initialization
	void Start () {
		maxPosX = 15f;
		smoothMove = 0.1f;
		smoothMovePerSecond = smoothMove * 60;
		minRotation = 0f;
		maxRotation = 0.9f;
		smoothRotate = 2.5f * 60;
		posY = transform.position.y;
	}

	public void SetValues(float minPosX, bool rotateMiddle) {
		this.minPosX = minPosX;
		this.rotateMiddle = rotateMiddle;
	}
	
	// Update is called once per frame
	void Update () {
		if(playing){
			if(transform.position.x > minPosX + smoothMove) {
				this.MoveContainerTo(minPosX);
			} else {
				if(oldPosition == Vector3.zero && rotationPoint == Vector3.zero) {
					rotationPoint = transform.position;
					oldPosition = transform.position;
					if(!rotateMiddle) {
						Vector2 size = transform.gameObject.GetComponent<BoxCollider2D>().bounds.size / 2;
						rotationPoint = new Vector3 (minPosX - (size.x / 2) - 1, posY - (size.y / 2), 0);
					}
				}
				if (decreasing) {
					if(transform.localRotation.z < maxRotation) {
						// rotate
						transform.RotateAround(rotationPoint, new Vector3(0f, 0f, smoothRotate * Time.deltaTime), 4f);
					}
				} else if (!decreasing) {
					if(transform.localRotation.z > minRotation) {
						// rotate back
						transform.RotateAround(rotationPoint, new Vector3(0f, 0f, -smoothRotate * Time.deltaTime), 4f);
					} else {
						transform.rotation = Quaternion.identity;
						transform.position = oldPosition;
					}
				}
			}
		} else {
			if(transform.position.x < maxPosX - smoothMove) {
				this.MoveContainerTo(maxPosX);
			}
		}
	}

	private void MoveContainerTo(float posX) {
		Vector3 pos = transform.position;
		transform.position = new Vector3(Mathf.Lerp(pos.x, posX, smoothMovePerSecond * Time.deltaTime), pos.y, pos.z);
	}

	public bool OnMinPosX() {
		return transform.position.x <= minPosX + smoothMove;
	}

	public bool OnMaxPosX() {
		return transform.position.x >= maxPosX - smoothMove;
	}

	public bool OnMaxRotation() {
		return transform.localRotation.z >= maxRotation;
	}

	public bool OnMinRotation() {
		return transform.localRotation.z <= minRotation;
	}

	public void StartGame() {
		if(!playing) {
			this.playing = true;
		}
	}
	
	public void StopGame() {
		if(playing) {
			this.playing = false;
		}
	}

	public void StartStopDecreasing() {
		if(transform.position.x < minPosX + smoothMove) {
			decreasing = !decreasing;
		}
	}
}
