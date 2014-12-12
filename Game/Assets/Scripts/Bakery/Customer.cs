using UnityEngine;
using System.Collections;

public class Customer : MonoBehaviour {

	public LevelManager level;
	
	private int nr;
	private Vector2 newPosition;

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

	public Vector3 GetNewPosition()
	{
		return this.newPosition;
	}

	public void SetNewPosition(Vector2 newPosition)
	{
		this.newPosition = newPosition;
	}

	public void MoveToPosition()
	{
		this.transform.position = Vector2.Lerp (this.transform.position, newPosition, 0.5f * Time.fixedDeltaTime);
	}
}
