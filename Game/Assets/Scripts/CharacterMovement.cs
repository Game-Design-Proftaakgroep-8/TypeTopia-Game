﻿using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	
	public GameObject oven;
	public GameObject counter;
	public GameObject workbench;

	private GameObject hit; 
	private Touch touch;

	// Use this for initialization
	void Start ()
	{
		hit = null;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Touch input
		if(Input.touchCount == 1)
		{
			touch = Input.touches[0];
			hit = InputDetection.CheckTouch(touch.position);
		}

		if (hit != null)
		{
			LerpCharacter(hit);
		}
	}

	public void LerpCharacter(GameObject touchObject)
	{
		Vector2 currentPos = this.transform.position;
		Vector2 endPos = touchObject.transform.position;

		this.transform.position = Vector2.Lerp (currentPos, endPos, 0.5f * Time.fixedDeltaTime);
	}
}