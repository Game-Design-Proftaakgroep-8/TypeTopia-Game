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
		Vector3 pos = Camera.main.ScreenToWorldPoint (touchPosition);
		Vector3 touchPos = new Vector3 (pos.x, pos.y, 0f);
		Collider[] hits = Physics.OverlapSphere (touchPos, 0f);
		
		if (hits.Length > 0)
		{
			return hits [0].gameObject;
		}
		
		return null;
	}
}
