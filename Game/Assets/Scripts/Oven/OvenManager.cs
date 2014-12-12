using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OvenManager : MonoBehaviour {

	public Text timerText;
	public Text timeNeededText;
	public Text winText;

	public GameObject ClockNow;
	public GameObject ClockThen;


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
		timeNowH = 7;
		timeNowM = 5;

		timeThenH = 6;
		timeThenM = 50;

		timeNeeded = 45;
		timeTimer = 0;

		answer = timeNeeded - ((timeNowH * 60) - (timeThenH * 60)) - (timeNowM - timeThenM);
		print (answer);

		gameOver = false;

		updateTimer ();
		setTimeNow ();
		setTimeThen ();
		setTimeNeeded ();
	}
	
	// Update is called once per frame
	void Update () {
		if(ClockThen.transform.position.y > 0) {
			ClockThen.transform.Translate(0, -0.1f, 0);
		}

		if(ClockThen.transform.position.y < 1 && ClockNow.transform.position.y > 3) {
			ClockNow.transform.Translate (0, -0.1f, 0);
		}
	}

	void setTimeNow() {
		ClockNow.transform.Find("ClockH").transform.Rotate (Vector3.forward * (timeNowH * -30));
		ClockNow.transform.Find("ClockM").transform.Rotate (Vector3.forward * ((timeNowM/5) * -30));
	}

	void setTimeThen() {
		ClockThen.transform.Find("ClockH").transform.Rotate (Vector3.forward * (timeThenH * -30));
		ClockThen.transform.Find("ClockM").transform.Rotate (Vector3.forward * ((timeThenM/5) * -30));
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
		StartCoroutine (returnToOverview());
	}

	private IEnumerator returnToOverview() {
		yield return new WaitForSeconds(3);
		Application.LoadLevel ("BakeryOverview");
	}

}
