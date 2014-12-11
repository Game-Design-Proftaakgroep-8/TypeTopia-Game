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

		customerTimer = Time.time + 1f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		foreach (GameObject c in customers)
		{
			if (c != null)
			{
				//Get customer number
				int number = c.GetComponent<Customer>().GetNumber();

				//Move to position
				if (c.transform.position != GetCustomerPosition(number))
				{
					c.GetComponent<Customer>().MoveToPosition(GetCustomerPosition(number));
				}
			}
		}

		//Touch input
		if(Input.touchCount == 1)
		{
			touch = Input.touches[0];
			GameObject o = InputDetection.CheckTouch(touch.position);

			//If check sprite is touched
			if (o != null && hit.tag.Equals("button"))
			{
				hit = o;

				//Let first person leave
				foreach (GameObject c in customers)
				{
					if (c != null)
					{
						int number = c.GetComponent<Customer>().GetNumber();

						//Let customer with number 1 leave
						if (number == 1)
						{
							c.GetComponent<Customer>().Leave();
						}
					}
				}

				//Move other customers forward
				foreach (GameObject c in customers)
				{
					if (c != null)
					{
						//Number of customer - 1
						c.GetComponent<Customer>().NumberMin();
					}
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
					//Vector3 pos = GetCustomerPosition(nextCustomerNr);
					Vector3 pos =  new Vector3(-1, 6, 0);
					GameObject cus = (GameObject)ScriptableObject.Instantiate(CustomerPrefab, pos, Quaternion.identity);
					cus.GetComponent<Customer>().level = this;
					customers[nextCustomerNr - 1] = cus;

					if (nextCustomerNr < nrOfCustomers)
					{
						customerTimer = Time.time;
					}
					else if (nextCustomerNr == nrOfCustomers)
					{
						customerTimer = 0;
					}
				}				
			}
		}
	}

	public int GetNextCustomerNr()
	{
		int nr = this.nextCustomerNr;
		this.nextCustomerNr++;
		return nr;
	}

	public Vector3 GetCustomerPosition(int number)
	{
		int nr = number - 1;
		float y = -3.5f + (nr * 2);
		Vector3 vec = new Vector3 (-1, y, 0);
		return vec;
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
}
