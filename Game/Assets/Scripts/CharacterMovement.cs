using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	
	public GameObject oven;
	public GameObject counter;
	public GameObject workbench;

	private GameObject hit; 
	private Touch touch;

	private float countDown;

	private string ovenScene;
	private string counterScene;
	private string workbenchScene;

	// Use this for initialization
	void Start ()
	{
		hit = null;
		countDown = 0f;

		ovenScene = "Oven";
		counterScene = "Counter";
		workbenchScene = "Workbench";
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (countDown != 0f)
		{
			if (Time.time > countDown + 1f)
			{
				//start game
				if (hit == oven)
				{
					print("Start game oven");
					Application.LoadLevel(ovenScene);
				}
				else if (hit == counter)
				{
					print("Start game counter");
					Application.LoadLevel(counterScene);
				}
				else if (hit == workbench)
				{
					print("Start game workbench");
					Application.LoadLevel(workbenchScene);
				}
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
				LerpCharacter(hit);
			}
		}
	}

	public void LerpCharacter(GameObject touchObject)
	{
		Vector2 currentPos = this.transform.position;
		Vector2 endPos = touchObject.transform.position;

		this.transform.position = Vector2.Lerp (currentPos, endPos, 0.5f * Time.fixedDeltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == oven || other.gameObject == counter || other.gameObject == workbench)
		{
			countDown = Time.time;
		}
	}
}
