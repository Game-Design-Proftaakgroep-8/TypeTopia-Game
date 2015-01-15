using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class WaterController : MonoBehaviour {
	public ContainerManager measuringCup;
	public Transform rain;
	public Text millilitersText;
	/// <summary>
	/// MeasuringCup is 1 [unitPrefix]l
	/// </summary>
	public UnitPrexixes unitPrefix;

	private bool playing;
	private bool increasing = false;
	private bool decreasing = false;
	private float maxHeight;
	private float minHeight;
	private float visible;
	private float invisible;

	// Use this for initialization
	void Start () {
		playing = false;
		// Measuring Cup
		measuringCup.SetValues (7.34f, true); 

		// Water
		maxHeight = 16.8f;
		minHeight = 0f;

		// Rain
		visible = 0f;
		invisible = -10f;
	}
	
	// Update is called once per frame
	void Update () {
		if(playing){
			if(measuringCup.OnMinPosX()) {
				//millilitersText.text = this.GetMilliliters ().ToString() + " ml";
				if(increasing) {
					this.ChangeWaterLevelTo(maxHeight);
				} else if (decreasing && measuringCup.OnMaxRotation ()) {
					this.ChangeWaterLevelTo(minHeight);
				}
			}
		} else {
			if(measuringCup.OnMaxPosX()) {
				Vector3 localScale = this.transform.localScale;
				this.transform.localScale = new Vector3(localScale.x, 0f, localScale.z);
			}
		}
	}

	private void ChangeWaterLevelTo(float height) {
		Vector3 localScale = transform.localScale;
		transform.localScale = new Vector3(localScale.x, Mathf.Lerp (localScale.y, height, 0.005f), localScale.z);
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

	public bool IsReadyToCheck() {
		return !decreasing && !increasing && measuringCup.OnMinRotation () && measuringCup.OnMinPosX ();
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
