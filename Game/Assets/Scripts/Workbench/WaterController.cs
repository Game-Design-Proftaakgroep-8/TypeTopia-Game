using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class WaterController : MonoBehaviour {
	public Transform measuringCup;
	public Transform rain;
	public Text millilitersText;
	/// <summary>
	/// MeasuringCup is 1 [unitPrefix]l
	/// </summary>
	public UnitPrexixes unitPrefix;

	private bool playing;
	private float minPosX;
	private float maxPosX;
	private float smoothMove;
	private bool increasing = false;
	private bool decreasing = false;
	private float maxHeight;
	private float minHeight;
	private float maxHeightRain;
	private float minRotation;
	private float maxRotation;
	private float smoothRotate;
	private float visible;
	private float invisible;

	// Use this for initialization
	void Start () {
		playing = false;
		minPosX = 6.64f;
		maxPosX = 14f;
		smoothMove = 0.1f;
		maxHeight = 2f;
		minHeight = 0f;
		maxHeightRain = 4f;
		minRotation = 0f;
		maxRotation = 0.9f;
		smoothRotate = 2.5f;
		visible = 0f;
		invisible = -10f;
	}
	
	// Update is called once per frame
	void Update () {
		if(playing){
			if(measuringCup.position.x > minPosX + smoothMove) {
				this.MoveCupTo(minPosX);
			} else {
				millilitersText.text = this.GetMilliliters ().ToString() + " ml";
				if(increasing) {
					this.ChangeWaterLevelTo(this.transform, maxHeight);
					//this.ChangeWaterLevelTo(rain, maxHeightRain);
				} else if (decreasing) {
					if(measuringCup.localRotation.z < maxRotation) {
						measuringCup.Rotate(0f, 0f, smoothRotate);
					} else {
						this.ChangeWaterLevelTo(this.transform, minHeight);
						//this.ChangeWaterLevelTo(rain, minHeight);
					}
				} else if (!decreasing) {
					if(measuringCup.localRotation.z > minRotation) {
						measuringCup.Rotate(0f, 0f, -smoothRotate);
					} else {
						measuringCup.rotation = Quaternion.identity;
					}
				}
			}
		} else {
			if(measuringCup.position.x < maxPosX - smoothMove) {
				this.MoveCupTo(maxPosX);
			} else {
				Vector3 localScale = this.transform.localScale;
				this.transform.localScale = new Vector3(localScale.x, 0f, localScale.z);
			}
		}
	}

	private void MoveCupTo(float posX) {
		Vector3 pos = measuringCup.position;
		measuringCup.position = new Vector3(Mathf.Lerp(pos.x, posX, smoothMove), pos.y, pos.z);
	}

	private void ChangeWaterLevelTo(Transform waterTransform, float height) {
		Vector3 localScale = waterTransform.localScale;
		waterTransform.localScale = new Vector3(localScale.x, Mathf.Lerp (localScale.y, height, 0.005f), localScale.z);
	}

	public int GetMilliliters() {
		return Convert.ToInt32((int)unitPrefix * this.transform.localScale.y / 2f);
	}

	public void StartStopIncreasing() {
		if(!decreasing && measuringCup.localRotation.z <= minRotation && measuringCup.position.x < minPosX + smoothMove) {
			increasing = !increasing;
		}
		Vector3 pos = rain.position;
		if (increasing) {
			rain.position = new Vector3(pos.x, pos.y, visible);
		} else {
			rain.position = new Vector3(pos.x, pos.y, invisible);
		}
	}

	public void StartStopDecreasing() {
		if(!increasing && measuringCup.position.x < minPosX + smoothMove) {
			decreasing = !decreasing;
		}
	}

	public void StartGame() {
		if(!playing) {
			this.playing = true;
		}
	}

	public void StopGame() {
		if(playing) {
			this.playing = false;
			millilitersText.text = string.Empty;
		}
	}
}
