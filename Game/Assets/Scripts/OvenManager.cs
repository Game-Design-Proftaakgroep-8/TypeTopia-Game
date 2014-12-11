using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OvenManager : MonoBehaviour {

	public Text timer;
	public GameObject ClockNowH;
	public GameObject ClockNowM;
	public GameObject ClockThenH;
	public GameObject ClockThenM;


	private float timeNowH;
	private float timeNowM;
	private float timeThenH;
	private float timeThenM;
	private float timeNeeded;
	private float timeTimer;

	// Use this for initialization
	void Start () {
		timeNowH = 6;
		timeNowM = 40;

		timeThenH = 6;
		timeThenM = 20;

		timeNeeded = 45;

		timeTimer = 0;

		updateTimer ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setTimeNow() {
		ClockNowH.transform.Rotate (Vector3.forward * (timeNowH * -30));
		ClockNowM.transform.Rotate (Vector3.forward * ((timeNowM/5) * -30));
	}

	void setTimeThen() {

	}

	private void updateTimer() {
		if(timeTimer < 10)
			timer.text = "0";
		else
			timer.text = "";

		timer.text += timeTimer + " min";
	}

	public void increaseTimerTime() {
		timeTimer += 5;
		updateTimer ();
	}

	public void decreaseTimerTime() {
		if(timeTimer > 0) {
			timeTimer -= 5;
			updateTimer ();
		}
	}

}
