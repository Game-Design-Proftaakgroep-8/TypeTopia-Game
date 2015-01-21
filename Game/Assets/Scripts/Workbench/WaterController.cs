using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using System;

public class WaterController : MonoBehaviour {
	public ContainerManager measuringCup;
	public Transform rain;
	public Text millilitersText;
	public GameObject tapGlow;
	public GameObject measuringCupGlow;
	/// <summary>
	/// MeasuringCup is 1 [unitPrefix]l
	/// </summary>
	public UnitPrexixes unitPrefix;

	private bool playing;
	private bool increasing = false;
	private bool decreasing = false;
	private bool glow = false;
	private float maxHeight;
	private float minHeight;
	private float visible;
	private float invisible;

	private float speedPerSecond = 0.005f * 60;
	private int level;

	private int measuringCupGlowed;
	private int tapGlowed;

	// Use this for initialization
	void Start () {
		playing = false;
		// Measuring Cup
		measuringCup.SetValues (5.55f, true); 

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
				millilitersText.text = this.GetMilliliters ().ToString() + " ml";
				if(level == 0 && glow && tapGlowed < 2) {
					StartCoroutine(this.ShowGlow(this.tapGlow));
					tapGlowed++;
					glow = false;
				}
				if(increasing) {
					this.ChangeWaterLevelTo(maxHeight);
					if(this.GetMilliliters() > 900 && level == 0 && tapGlowed < 2) {
						glow = true;
					}
				} else if (decreasing && measuringCup.OnMaxRotation ()) {
					this.ChangeWaterLevelTo(minHeight);
					if(this.GetMilliliters() <= 1 && level == 0 && measuringCupGlowed < 2) {
						StartCoroutine(this.ShowGlow(this.measuringCupGlow));
						measuringCupGlowed++;
					}
				}
			}
		} else {
			if(measuringCup.OnMaxPosX()) {
				Vector3 localScale = this.transform.localScale;
				this.transform.localScale = new Vector3(localScale.x, 0f, localScale.z);
			}
		}
	}

	private IEnumerator ShowGlow(GameObject glow) {
		for (int i = 0; i < 3 && glow != null; i++) {
			glow.renderer.sortingOrder = 1;
			yield return new WaitForSeconds(1);
			glow.renderer.sortingOrder = -1;
			yield return new WaitForSeconds(1);
		}
	}

	private void ChangeWaterLevelTo(float height) {
		Vector3 localScale = transform.localScale;
		transform.localScale = new Vector3(localScale.x, Mathf.Lerp (localScale.y, height, speedPerSecond * Time.deltaTime), localScale.z);
	}

	public int GetMilliliters() {
		return (int) ((int)unitPrefix * this.transform.localScale.y / maxHeight);
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
			if(level == 0 && this.GetMilliliters() > 900 && measuringCupGlowed < 2) {
				StartCoroutine(this.ShowGlow(this.measuringCupGlow));
				this.measuringCupGlowed++;
			}
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

	public void StartGame(int level) {
		if(!playing) {
			this.playing = true;
			this.level = level;
			this.glow = true;
			this.measuringCupGlowed = 0;
			this.tapGlowed = 0;
			measuringCup.StartGame();
		}
	}

	public void StopGame() {
		if(playing) {
			this.playing = false;
			this.glow = false;
			this.level = -1;
			millilitersText.text = string.Empty;
			measuringCup.StopGame();
		}
	}
}
