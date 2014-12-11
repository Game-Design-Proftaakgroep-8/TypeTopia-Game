using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	
	private GameObject hit; 
	private Touch touch;

	private float countDown;

	// Use this for initialization
	void Start ()
	{
		hit = null;
		countDown = 0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (countDown != 0f)
		{
			if (Time.time > countDown + 1f)
			{
				//LevelManager.StartGame(hit);
			}
		}
		else
		{
			//Touch input
			if(Input.touchCount == 1)
			{
				touch = Input.touches[0];
				GameObject o = InputDetection.CheckTouch(touch.position);

				if (o != null)
				{
					hit = o;
				}
			}

			if (hit != null)
			{
				//LevelManager.LerpCharacter(hit);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		countDown = Time.time;
	}
}
