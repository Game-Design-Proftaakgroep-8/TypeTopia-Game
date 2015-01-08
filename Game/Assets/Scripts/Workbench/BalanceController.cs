﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class BalanceController : MonoBehaviour {
	public ContainerManager stockContainer;
	public ContainerManager mixingBowl;
	public Transform stockRain;
	public Text milligramsText;
	/// <summary>
	/// MeasuringCup is 1 [unitPrefix]l
	/// </summary>
	public UnitPrexixes unitPrefix;

	private float currentWeight;

	private bool playing;
	private bool increasingWeight = false;
	private bool decreasingWeight = false;

	private float visible;
	private float invisible;

	// Use this for initialization
	void Start () {
		playing = false;
		currentWeight = 0;

		// Stock Container
		stockContainer.SetValues (1f, true);

		// Mixing Bowl
		mixingBowl.SetValues(-2.95f, false);

		// Stock Rain
		visible = 1f;
		invisible = -10f;
	}
	
	// Update is called once per frame
	void Update () {
		if (playing) {
			if(stockContainer.OnMinPosX () && mixingBowl.OnMinPosX()) {
				String unitPrefixText = unitPrefix.ToString();
				if(unitPrefix == UnitPrexixes.no) {
					unitPrefixText = "";
				}
				milligramsText.text = currentWeight + " " + unitPrefixText + "g";
				if(increasingWeight && stockContainer.OnMaxRotation()) {
					// increase weight
					Vector3 pos = stockRain.position;
					stockRain.position = new Vector3(pos.x, pos.y, visible);

					currentWeight++;
				} else if (!increasingWeight) {
					Vector3 pos = stockRain.position;
					stockRain.position = new Vector3(pos.x, pos.y, invisible);
				}
				if (decreasingWeight && mixingBowl.OnMaxRotation ()) {
					// decrease weight
					if(currentWeight > 0) {
						currentWeight--;
					}
				}
			}
		} else {
			// not playing?
		}
	}

	public int GetMilligrams() {
		return Convert.ToInt32((int)unitPrefix * currentWeight);
	}

	public void StartStopIncreasingWeight() {
		if (!decreasingWeight && stockContainer.OnMinPosX () && mixingBowl.OnMinPosX() && mixingBowl.OnMinRotation ()) {
			increasingWeight = !increasingWeight;
			stockContainer.StartStopDecreasing ();
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

	public void StartGame() {
		if(!playing) {
			this.playing = true;
			stockContainer.StartGame();
			mixingBowl.StartGame ();
		}
	}
	
	public void StopGame() {
		if(playing) {
			this.playing = false;
			milligramsText.text = string.Empty;
			currentWeight = 0;
			stockContainer.StopGame();
			mixingBowl.StopGame ();
		}
	}
}