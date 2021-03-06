﻿using UnityEngine;
using System.Collections;

public class DragManager : MonoBehaviour {

	private CounterManager manager;

	private GameObject lastTouched;

	private bool handGlowShown;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("Manager").GetComponent<CounterManager> ();

		lastTouched = null;
		handGlowShown = false;
	}
	
	// Update is called once per frame
	void Update() {
		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch(0);
			GameObject hit = InputDetection.CheckTouch(touch.position);
			GameObject money = (GameObject)manager.eenEuro;

			if(touch.phase == TouchPhase.Began)
				lastTouched = hit;
			else if(touch.phase == TouchPhase.Ended)
				lastTouched = null;

			Vector2 touchPosition = Vector2.zero;
			
			if(lastTouched != null) {
				if(lastTouched.tag == "money") {
					touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

					lastTouched.transform.position = touchPosition;
				}
				else {
					if(lastTouched.tag == "5cent" && touch.phase == TouchPhase.Began) {
						money = (GameObject)Instantiate(manager.vijfCent);
					}
					else if(lastTouched.tag == "10cent" && touch.phase == TouchPhase.Began) {
						money = (GameObject)Instantiate(manager.tienCent);
					}
					else if(lastTouched.tag == "20cent" && touch.phase == TouchPhase.Began) {
						money = (GameObject)Instantiate(manager.twintigCent);
					}
					else if(lastTouched.tag == "50cent" && touch.phase == TouchPhase.Began) {
						money = (GameObject)Instantiate(manager.vijftigCent);
					}
					else if(lastTouched.tag == "1euro" && touch.phase == TouchPhase.Began) {
						money = (GameObject)Instantiate(manager.eenEuro);
					}
					else if(lastTouched.tag == "2euro" && touch.phase == TouchPhase.Began) {
						money = (GameObject)Instantiate(manager.tweeEuro);
					}
					else if(lastTouched.tag == "5euro" && touch.phase == TouchPhase.Began) {
						money = (GameObject)Instantiate(manager.vijfEuro);
					}
					else if(lastTouched.tag == "10euro" && touch.phase == TouchPhase.Began) {
						money = (GameObject)Instantiate(manager.tienEuro);
					}
					else if(lastTouched.tag == "20euro" && touch.phase == TouchPhase.Began) {
						money = (GameObject)Instantiate(manager.twintigEuro);
					}
					else if(lastTouched.tag == "50euro" && touch.phase == TouchPhase.Began) {
						money = (GameObject)Instantiate(manager.vijftigEuro);
					}

					money.transform.position = hit.transform.position;
					lastTouched = InputDetection.CheckTouch(touch.position);

					if(!handGlowShown) {
						handGlowShown = true;
						StartCoroutine(manager.ShowHandGlow());
					}
				}
            }
        }
    }
}
