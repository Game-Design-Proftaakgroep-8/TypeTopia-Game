﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OvenManager : MonoBehaviour {

	private SavedData data;
	private Database db;
	private SumInfo info;

	public Text timerText;
	public Text timeNeededText;
	public Text winText;
	public Text topiansText;

	public GameObject Clock;

	private int timeH;
    private int timeM;
	private int timeHalf;
    private int timeNeeded;
    private int timeTimerH;
	private int timeTimerM;
	private int givenAnswer;
    private int answer;

	private bool gameOver;

	// Use this for initialization
	void Start () {
		data = SavedData.getInstance ();
		info = db.GetSumInfo ("time", 1);
		setTopianText ();

		gameOver = false;

		setTime();
		updateTimer ();        
		setTimeNeeded ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void setTopianText() {
		topiansText.text = "Topians: " + data.getTopians();
	}

    void setTime() {
		timeH = Random.Range(info.minRange, info.maxRange);
		switch (info.sumCommas) {
		case 0:
			timeM = 0;
			timeNeeded = Random.Range (1, 4) * 60;
			break;
		default:		
			timeM = Random.Range(0, 4) * 15;
			if(timeM >= 30)
				timeHalf = -15;
			else
				timeHalf = 0;

			timeNeeded = Random.Range (1, 13) * 15;
			break;
		}

		int maximum = 24*60;
		int time = (timeH * 60) + timeM;
		answer = time + timeNeeded;
		if(answer >= maximum) {
			answer = timeNeeded - (maximum - time);
		}

		print ("answer: " + answer);
        
		Clock.transform.Find("ClockH").transform.Rotate (Vector3.forward * (timeH * -30 + timeHalf));
		Clock.transform.Find("ClockM").transform.Rotate (Vector3.forward * ((timeM/15) * -90));
    }

	private void setTimeNeeded() {
		timeNeededText.text = "Tijd in de oven: " + timeNeeded + " min";
	}

	private void setGivenAnswer() {
		givenAnswer = (timeTimerH * 60) + timeTimerM;
		print (givenAnswer);
	}

	private void updateTimer() {
		if(timeTimerH < 10)
			timerText.text = "0";
		else
			timerText.text = "";

		timerText.text += timeTimerH + ":";

		if(timeTimerM < 10)
			timerText.text += "0" + timeTimerM;
		else
			timerText.text += timeTimerM;
	}

	public void increaseHour() {
		if(!gameOver) {
			timeTimerH += 1;
			if(timeTimerH >= info.maxRange)
				timeTimerH = 0;

			updateTimer ();
		}
	}
	
	public void decreaseHour() {
		if(!gameOver) {
			timeTimerH -= 1;
			if(timeTimerH <= info.minRange)
				timeTimerH = 11;

			updateTimer ();
		}
	}

	public void increaseMinute() {
		if(!gameOver) {
			timeTimerM += 15;
			if(timeTimerM >= 60) {
				timeTimerM = 0;
			}
			updateTimer ();
		}
	}

	public void decreaseMinute() {
		if(!gameOver) {
			timeTimerM -= 15;
			if(timeTimerM <= 0) {
				timeTimerM = 55;
			}
			updateTimer ();
		}
	}

	public void confirm() {
		setGivenAnswer ();
		if(givenAnswer == answer) {
			winText.text = "Goed Gedaan!";
			data.increaseTopians(1);
			setTopianText();
			gameOver = true;
		}
		else if(givenAnswer < answer) {
			winText.text = "De tijd was te kort!";
			gameOver = true;
		}
		else if(givenAnswer > answer) {
			winText.text = "Je hebt het aan laten branden!";
			gameOver = true;
		}
		StartCoroutine (returnToOverview());
	}

	private IEnumerator returnToOverview() {
		yield return new WaitForSeconds(5);
		Application.LoadLevel ("BakeryOverview");
	}

}
