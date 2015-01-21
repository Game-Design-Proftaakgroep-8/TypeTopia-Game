﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using System;

public class BalanceController : MonoBehaviour {
	public ContainerManager stockContainer;
	public ContainerManager mixingBowl;
	public GameObject mixingBowlGlow;
	public Transform stockRain;
	public Text milligramsText;
	/// <summary>
	/// unit is [unitPrefix]g
	/// </summary>
	public UnitPrexixes unitPrefix;

	private float currentWeight;

	private bool playing;
	private bool glowContainer = false;
	private bool increasingWeight = false;
	private bool decreasingWeight = false;

	private float visible;
	private float invisible;

	private int speedPerSecond = 60;
	private int level;

	private GameObject stockContainerGlow;
	private int containerGlowed;
	private int mixingBowlGlowed;

	// Use this for initialization
	void Start () {
		playing = false;
		currentWeight = 0;

		// Stock Container
		stockContainer.SetValues (1.85f, true);

		// Mixing Bowl
		mixingBowl.SetValues(-1.19f, false);

		// Stock Rain
		visible = 1f;
		invisible = -10f;
	}
	
	// Update is called once per frame
	void Update () {
		if (playing) {
			if(stockContainer.OnMinPosX () && mixingBowl.OnMinPosX()) {
				string unitPrefixText = unitPrefix.ToString();
				if(unitPrefix == UnitPrexixes.no) {
					unitPrefixText = "";
				}
				milligramsText.text = currentWeight + " " + unitPrefixText + "g";
				if(level == 0 && glowContainer && containerGlowed < 2) {
					StartCoroutine(this.ShowGlow(this.stockContainerGlow));
					containerGlowed++;
					glowContainer = false;
				}
				if(increasingWeight && stockContainer.OnMaxRotation()) {
					// increase weight
					Vector3 pos = stockRain.position;
					stockRain.position = new Vector3(pos.x, pos.y, visible);

					currentWeight += Mathf.Round(Time.deltaTime * speedPerSecond);
					if(currentWeight > 100 && level == 0 && containerGlowed < 2) {
						glowContainer = true;
					}
				} else if (!increasingWeight) {
					Vector3 pos = stockRain.position;
					stockRain.position = new Vector3(pos.x, pos.y, invisible);
				}
				if (decreasingWeight && mixingBowl.OnMaxRotation ()) {
					// decrease weight
					if(currentWeight > 0) {
						currentWeight -= Mathf.Round(Time.deltaTime * speedPerSecond);
					} else if (level == 0 && mixingBowlGlowed < 2) {
						StartCoroutine(this.ShowGlow(this.mixingBowlGlow));
						mixingBowlGlowed++;
					}
				}
			}
		} else {
			// not playing?
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

	public int GetMilligrams() {
		return (int) ((int)unitPrefix * currentWeight);
	}

	public void StartStopIncreasingWeight() {
		if (!decreasingWeight && stockContainer.OnMinPosX () && mixingBowl.OnMinPosX() && mixingBowl.OnMinRotation ()) {
			increasingWeight = !increasingWeight;
			stockContainer.StartStopDecreasing ();
		}
		if (!increasingWeight && level == 0 && currentWeight > 100 && mixingBowlGlowed < 2) {
			StartCoroutine(this.ShowGlow(this.mixingBowlGlow));
			this.mixingBowlGlowed++;
		}
	}

	public void StartStopDecreasingWeight() {
		if(!increasingWeight && stockContainer.OnMinPosX() && mixingBowl.OnMinPosX () && stockContainer.OnMinRotation()) {
			decreasingWeight = !decreasingWeight;
			mixingBowl.StartStopDecreasing ();
		}
	}

	public bool IsReadyToCheck() {
		return !decreasingWeight && !increasingWeight && mixingBowl.OnMinRotation () && mixingBowl.OnMinPosX () && stockContainer.OnMinRotation () && stockContainer.OnMinPosX ();
	}

	public void StartGame(int level) {
		if(!playing) {
			this.playing = true;
			this.level = level;
			this.containerGlowed = 0;
			this.mixingBowlGlowed = 0;
			this.stockContainerGlow = stockContainer.gameObject.transform.GetChild(0).FindChild("glow3").gameObject;
			this.glowContainer = true;
			stockContainer.StartGame();
			mixingBowl.StartGame ();
		}
	}
	
	public void StopGame() {
		if(playing) {
			this.playing = false;
			milligramsText.text = string.Empty;
			currentWeight = 0;
			this.glowContainer = false;
			this.level = -1;
			this.stockContainerGlow = null;
			stockContainer.StopGame();
			mixingBowl.StopGame ();
		}
	}
}
