using UnityEngine;
using System.Collections;

public class WaterController : MonoBehaviour {
	public Transform measuringCup;
	private bool increasing = false;
	private bool decreasing = false;
	private float maxHeight;
	private float minHeight;
	private float minRotation;
	private float maxRotation;
	private float smooth = 0.005f;

	// Use this for initialization
	void Start () {
		maxHeight = 2f;
		minHeight = 0f;
		minRotation = 0f;
		maxRotation = 0.25f;
	}
	
	// Update is called once per frame
	void Update () {
		if(increasing) {
			this.changeWaterLevelTo(maxHeight);
			this.rotateCupTo(minRotation);
		} else if (decreasing) {
			this.changeWaterLevelTo(minHeight);
			this.rotateCupTo(maxRotation);
		}
	}

	private void changeWaterLevelTo(float height) {
		Vector3 localScale = this.transform.localScale;
		this.transform.localScale = new Vector3(localScale.x, Mathf.Lerp (localScale.y, height, smooth), localScale.z);
	}

	private void rotateCupTo(float rotation) {
		print (measuringCup.localRotation.z);
		measuringCup.Rotate (0f, 0f, Mathf.Lerp (measuringCup.localRotation.z, rotation, smooth));
	}

	public void StartStopIncreasing() {
		increasing = !increasing;
	}

	public void StartStopDecreasing() {
		decreasing = !decreasing;
	}
}
