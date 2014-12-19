using UnityEngine;
using System.Collections;

public class ContainerManager : MonoBehaviour {
	public float minPosX;

	private float maxPosX;
	private float smoothMove;
	private float minRotation;
	private float maxRotation;
	private float smoothRotate;

	private bool playing = false;
	private bool decreasing = false;

	// Use this for initialization
	void Start () {
		maxPosX = 14f;
		smoothMove = 0.1f;
		minRotation = 0f;
		maxRotation = 0.9f;
		smoothRotate = 2.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if(playing){
			if(transform.position.x > minPosX + smoothMove) {
				this.MoveContainerTo(minPosX);
			} else {
				if (decreasing) {
					if(transform.localRotation.z < maxRotation) {
						// rotate
						transform.Rotate(0f, 0f, smoothRotate);
					}
				} else if (!decreasing) {
					if(transform.localRotation.z > minRotation) {
						// rotate back
						transform.Rotate(0f, 0f, -smoothRotate);
					} else {
						transform.rotation = Quaternion.identity;
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
		transform.position = new Vector3(Mathf.Lerp(pos.x, posX, smoothMove), pos.y, pos.z);
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
