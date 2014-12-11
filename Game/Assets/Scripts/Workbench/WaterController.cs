using UnityEngine;
using System.Collections;
using System;

public class WaterController : MonoBehaviour {
	public Transform measuringCup;

	private bool gameOver;
	private bool increasing = false;
	private bool decreasing = false;
	private float maxHeight;
	private float minHeight;
	private float minRotation;
	private float maxRotation;
	private float smoothRotate;

	// Use this for initialization
	void Start () {
		this.gameOver = false;
		maxHeight = 2f;
		minHeight = 0f;
		minRotation = 0f;
		maxRotation = 0.9f;
		smoothRotate = 2.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if(!gameOver){
			print (this.GetMilliliters ());
			if(increasing) {
				this.changeWaterLevelTo(maxHeight);
			} else if (decreasing) {
				if(measuringCup.localRotation.z < maxRotation) {
					measuringCup.Rotate(0f, 0f, smoothRotate);
				} else {
					this.changeWaterLevelTo(minHeight);
				}
			} else if (!decreasing) {
				if(measuringCup.localRotation.z > minRotation) {
					measuringCup.Rotate(0f, 0f, -smoothRotate);
				} else {
					measuringCup.rotation = Quaternion.identity;
				}
			}
		}
	}

	private void changeWaterLevelTo(float height) {
		Vector3 localScale = this.transform.localScale;
		this.transform.localScale = new Vector3(localScale.x, Mathf.Lerp (localScale.y, height, 0.005f), localScale.z);
	}

	public int GetMilliliters() {
		return Convert.ToInt32(1000f * this.transform.localScale.y / 2f);
	}

	public void StartStopIncreasing() {
		if(!decreasing && measuringCup.localRotation.z <= minRotation) {
			increasing = !increasing;
		}
	}

	public void StartStopDecreasing() {
		if(!increasing) {
			decreasing = !decreasing;
		}
	}

	public void GameOver() {
		// next Game
		this.gameOver = true;
	}
}
