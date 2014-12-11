using UnityEngine;
using System.Collections;

public class InputDetection : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public static GameObject CheckTouch(Vector3 touchPosition)
	{
		Vector2 pos = Camera.main.ScreenToWorldPoint (touchPosition);
		Vector2 touchPos = new Vector2 (pos.x, pos.y);
		Collider2D hit = Physics2D.OverlapPoint (touchPos);

		if (hit != null)
		{
			return hit.gameObject;
		}

		return null;
	}
}
