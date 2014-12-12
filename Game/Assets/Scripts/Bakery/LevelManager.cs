using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	private int nrOfCustomers;
	private GameObject[] customers;
	public GameObject CustomerPrefab;

	public GameObject oven;
	public GameObject counter;
	public GameObject workbench;

	private string ovenScene;
	private string counterScene;
	private string workbenchScene;

	private int nextCustomerNr;
	private int leftCount;

	private float customerTimer;

	private GameObject hit; 
	private Touch touch;

	// Use this for initialization
	void Start ()
	{
		nrOfCustomers = 4;
		customers = new GameObject[nrOfCustomers];

		ovenScene = "Oven";
		counterScene = "Counter";
		workbenchScene = "Workbench";

		nextCustomerNr = 1;
		leftCount = 0;

		customerTimer = Time.time + 1f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		foreach (GameObject c in customers)
		{
			if (c != null)
			{
				Vector3 newPos = c.GetComponent<Customer>().GetNewPosition();

				//Move to position
				if (c.transform.position != newPos)
				{
					c.GetComponent<Customer>().MoveToPosition();
				}
			}
		}

		//Place customer
		if (customerTimer != 0)
		{
			if (Time.time >= customerTimer + 4f)
			{
				if (nextCustomerNr <= nrOfCustomers)
				{
					Vector3 pos =  new Vector3(-1, 6, 0);
					GameObject cus = (GameObject)ScriptableObject.Instantiate(CustomerPrefab, pos, Quaternion.identity);
					cus.GetComponent<Customer>().level = this;

					//gaat goed?!
					int newNr = nextCustomerNr - leftCount;
					cus.GetComponent<Customer>().SetNewPosition(GetCustomerPosition(newNr));
					customers[nextCustomerNr - 1] = cus;

					if (nextCustomerNr < nrOfCustomers)
					{
						customerTimer = Time.time;
					}
					else if (nextCustomerNr >= nrOfCustomers)
					{
						customerTimer = 0;
					}
				}				
			}
		}
	}

	public void Leave() //when button pressed
	{
		if (leftCount < nrOfCustomers)
		{
			GameObject c = customers[leftCount];

			if (c != null && c.transform.position.y <= (GetCustomerPosition(1).y + 1f))
			{
				c.GetComponent<Customer>().SetNewPosition(new Vector2 (-8f, -3.5f));
				Destroy(c.gameObject, 5);
				leftCount ++;

				for (int i = leftCount; i < nextCustomerNr-1; i++)
				{
					//Move to the front
					Vector3 posFront = GetCustomerPosition(i - (leftCount - 1));
					customers[i].GetComponent<Customer>().SetNewPosition(posFront);
				}
			}
		}
		else
		{
			print("everyone left");
		}
	}

	public int GetNextCustomerNr()
	{
		int nr = this.nextCustomerNr;
		this.nextCustomerNr++;
		return nr;
	}

	//Get starting position of customer
	public Vector3 GetCustomerPosition(int number)
	{
		int nr = number - 1;
		float y = -3.5f + (nr * 2);
		Vector3 vec = new Vector3 (-1, y, 0);
		return vec;
	}

	public IEnumerator StartGame(GameObject hit)
	{
		yield return new WaitForSeconds (3);

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
