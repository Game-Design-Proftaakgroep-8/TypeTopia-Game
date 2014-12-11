using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	public CharacterController controller;
	public GameObject oven;
	public GameObject counter;
	public GameObject workbench;

	private int count = 0;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (count == 0)
		{
			count = 1;
			Touch touch = Input.GetTouch(1);
			GetTouchPosition(touch);
			count = 0;
		}
	}

	public void GetTouchPosition(Touch touch)
	{
		Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);
		Vector2 touchPos = new Vector2(position.x, position.y);
		RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);

		if(hit)
		{                                
			GameObject tObject = GameObject.Find(hit.transform.gameObject.name);	
			Vector2 currentPos = this.transform.position;
			Vector2 endPos = tObject.transform.position;
			this.transform.position = Vector2.Lerp (currentPos, endPos, 1.0f * Time.fixedDeltaTime);
		}
	}
}
