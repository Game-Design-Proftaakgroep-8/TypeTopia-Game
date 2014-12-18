using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class WaterController : MonoBehaviour {
	public ContainerManager measuringCup;
	//public Transform measuringCup;
	public Transform rain;
	public Text millilitersText;
	/// <summary>
	/// MeasuringCup is 1 [unitPrefix]l
	/// </summary>
	public UnitPrexixes unitPrefix;

	private bool playing;
//	private float minPosX;
//	private float maxPosX;
//	private float smoothMove;
	private bool increasing = false;
	private bool decreasing = false;
	private float maxHeight;
	private float minHeight;
	//private float maxHeightRain;
//	private float minRotation;
//	private float maxRotation;
//	private float smoothRotate;
	private float visible;
	private float invisible;

	// Use this for initialization
	void Start () {
		playing = false;
		// Measuring Cup
		measuringCup.minPosX = 6.64f;
		measuringCup.maxPosX = 14f;

		// Water
		maxHeight = 10f;
		minHeight = 0f;
		//maxHeightRain = 4f;

		// Rain
		visible = 0f;
		invisible = -10f;
	}
	
	// Update is called once per frame
	void Update () {
		if(playing){
			if(measuringCup.OnMinPosX()) {
				millilitersText.text = this.GetMilliliters ().ToString() + " ml";
				if(increasing) {
					this.ChangeWaterLevelTo(this.transform, maxHeight);
					//this.ChangeWaterLevelTo(rain, maxHeightRain);
				} else if (decreasing && measuringCup.OnMaxRotation ()) {
					this.ChangeWaterLevelTo(this.transform, minHeight);
					//this.ChangeWaterLevelTo(rain, minHeight);
				}
			}
		} else {
			if(measuringCup.OnMaxPosX()) {
				Vector3 localScale = this.transform.localScale;
				this.transform.localScale = new Vector3(localScale.x, 0f, localScale.z);
			}
		}
	}

//	private void MoveCupTo(float posX) {
//		Vector3 pos = measuringCup.position;
//		measuringCup.position = new Vector3(Mathf.Lerp(pos.x, posX, smoothMove), pos.y, pos.z);
//	}

	private void ChangeWaterLevelTo(Transform waterTransform, float height) {
		Vector3 localScale = waterTransform.localScale;
		waterTransform.localScale = new Vector3(localScale.x, Mathf.Lerp (localScale.y, height, 0.005f), localScale.z);
	}

	public int GetMilliliters() {
		return Convert.ToInt32((int)unitPrefix * this.transform.localScale.y / maxHeight);
	}

	public void StartStopIncreasing() {
		if(!decreasing && measuringCup.OnMinRotation () && measuringCup.OnMinPosX()) {
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
		if(!increasing && measuringCup.OnMinPosX()) {
			decreasing = !decreasing;
			measuringCup.StartStopDecreasing();
		}
	}

	public void StartGame() {
		if(!playing) {
			this.playing = true;
			measuringCup.StartGame();
		}
	}

	public void StopGame() {
		if(playing) {
			this.playing = false;
			millilitersText.text = string.Empty;
			measuringCup.StopGame();
		}
	}
}
