using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject oven;
	public GameObject counter;
	public GameObject workbench;

	private string ovenScene;
	private string counterScene;
	private string workbenchScene;

	private int nextCustomerNr;

	// Use this for initialization
	void Start ()
	{
		ovenScene = "Oven";
		counterScene = "Counter";
		workbenchScene = "Workbench";

		nextCustomerNr = 1;		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public int GetCostumerNr()
	{
		int nr = this.nextCustomerNr;
		this.nextCustomerNr++;
		return nr;
	}

	public void StartGame(GameObject hit)
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

	public void LerpCharacter(GameObject touchObject)
	{
		Vector2 currentPos = this.transform.position;
		Vector2 endPos = touchObject.transform.position;
		
		this.transform.position = Vector2.Lerp (currentPos, endPos, 0.5f * Time.fixedDeltaTime);
	}
}
