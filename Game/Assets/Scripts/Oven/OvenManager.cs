using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OvenManager : MonoBehaviour {

	public Text timerText;
	public Text timeNeededText;
	public Text winText;

	public GameObject ClockNow;
	public GameObject ClockThen;


	private int timeNowH;
    private int timeNowM;
    private int timeThenH;
    private int timeThenM;
    private int timeDifference;
    private int timeNeeded;
    private int timeTimer;
    private int answer;

	private bool gameOver;

	// Use this for initialization
	void Start () {
        setTime();

		gameOver = false;

		updateTimer ();
        
		setTimeNeeded ();
	}
	
	// Update is called once per frame
	void Update () {
		if(ClockThen.transform.position.y > 0) {
			ClockThen.transform.Translate(0, -0.1f, 0);
		}

		if(ClockThen.transform.position.y < 0.5f && ClockNow.transform.position.y > 3) {
			ClockNow.transform.Translate (0, -0.1f, 0);
		}
	}

    void setTime() {
        setTimeThen ();
        setTimeNow ();
        
        timeDifference = ((timeNowH * 60) - (timeThenH * 60)) - (timeNowM - timeThenM);
        timeNeeded = Random.Range(timeDifference/5, 13) *5;
        timeTimer = 0;
        
        answer = timeNeeded - ((timeNowH * 60) - (timeThenH * 60)) - (timeNowM - timeThenM);
        print("difference: " + timeDifference);
        print("needed: " + timeNeeded);
        print("answer: " + answer);

        if(answer <= 0 || answer > 95) {
            setTime();
        }
    }

	void setTimeNow() {
        timeNowH = Random.Range(timeThenH, timeThenH + 3);
        if(timeNowH <= timeThenH) {
            timeNowM = Random.Range(timeThenM/5 + 1, 13) * 5;
        }
        else {
            timeNowM = Random.Range(1, 13) * 5;
        }

		ClockNow.transform.Find("ClockH").transform.Rotate (Vector3.forward * (timeNowH * -30));
		ClockNow.transform.Find("ClockM").transform.Rotate (Vector3.forward * ((timeNowM/5) * -30));
	}

	void setTimeThen() {
        timeThenH = Random.Range(1, 12);
        timeThenM = Random.Range(1, 12) * 5;

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
