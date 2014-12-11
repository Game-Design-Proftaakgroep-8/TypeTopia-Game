using UnityEngine;
using System.Collections;

public class Customer : MonoBehaviour {

	public LevelManager level;
	
	private int nr;

	// Use this for initialization
	void Start ()
	{
		nr = level.GetNextCustomerNr();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public int GetNumber()
	{
		return this.nr;
	}

	public void NumberMin()
	{
		this.nr = nr--;
	}

	public void MoveToPosition(Vector2 newPosition)
	{
		Vector2 currentPos = this.transform.position;		
		this.transform.position = Vector2.Lerp (currentPos, newPosition, 0.5f * Time.fixedDeltaTime);
	}

	public void Leave()
	{
		Vector2 currentPos = this.transform.position;
		Vector2 newPosition = new Vector2 (-8f, -3.5f);
		this.transform.position = Vector2.Lerp (currentPos, newPosition, 0.5f * Time.fixedDeltaTime);
	}
}
