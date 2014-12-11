using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OvenManager : MonoBehaviour {

	public Text timerText;
	public Text timeNeededText;
	public Text winText;
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
	private float answer;

	private bool gameOver;

	// Use this for initialization
	void Start () {
		timeNowH = 6;
		timeNowM = 40;

		timeThenH = 6;
		timeThenM = 20;

		timeNeeded = 45;
		timeTimer = 0;

		answer = timeNeeded - (timeNowH - timeThenH) - (timeNowM - timeThenM);
		print (answer);

		gameOver = false;

		updateTimer ();
		setTimeNow ();
		setTimeThen ();
		setTimeNeeded ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setTimeNow() {
		ClockNowH.transform.Rotate (Vector3.forward * (timeNowH * -30));
		ClockNowM.transform.Rotate (Vector3.forward * ((timeNowM/5) * -30));
	}

	void setTimeThen() {
		ClockThenH.transform.Rotate (Vector3.forward * (timeThenH * -30));
		ClockThenM.transform.Rotate (Vector3.forward * ((timeThenM/5) * -30));
	}

	private void setTimeNeeded() {
		timeNeededText.text = "Time Needed: " + timeNeeded + " min";
	}

	private void updateTimer() {
		if(timeTimer < 10)
			timerText.text = "0";
		else
			timerText.text = "";

		timerText.text += timeTimer + " min";
	}

	public void increaseTimerTime() {
		if(timeTimer < 95 && !gameOver) {
			timeTimer += 5;
			updateTimer ();
		}
	}

	public void decreaseTimerTime() {
		if(timeTimer > 0 && !gameOver) {
			timeTimer -= 5;
			updateTimer ();
		}
	}

	public void confirm() {
		print (timeTimer);
		if(timeTimer == answer) {
			winText.text = "You Won!";
			gameOver = true;
		}
		else {
			winText.text = "You Lost!";
			gameOver = true;
		}
	}

}
