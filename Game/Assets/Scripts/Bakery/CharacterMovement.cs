using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	public LevelManager level;

	private GameObject hit; 
	private Touch touch;

	private float countDown;

	// Use this for initialization
	void Start ()
	{
		hit = null;
		countDown = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (hit != null)
		{
			LerpCharacter(hit);
		}

		if (countDown != 0)
		{
			if (Time.time > countDown + 2f)
			{
				level.StartGame(hit);
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
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		countDown = Time.time;
	}

	public void LerpCharacter(GameObject touchObject)
	{
		Vector2 currentPos = this.transform.position;
		Vector2 endPos = touchObject.transform.position;
		
		this.transform.position = Vector2.Lerp (currentPos, endPos, 0.5f * Time.fixedDeltaTime);
	}
}
