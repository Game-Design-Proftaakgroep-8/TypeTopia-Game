﻿using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public SavedData data;

	public static int nrOfCustomers = 4;
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
	private bool pauze;

	private int btnCount;

	// Use this for initialization
	void Start ()
	{
		customers = new GameObject[nrOfCustomers];
		nextCustomerNr = 1;
		leftCount = 0;

		//Get saved customers from SavedData
		data = SavedData.getInstance ();
		customers = data.getCustomers ();
		
		foreach (GameObject g in this.customers)
		{
			if (g != null)
			{
				g.GetComponent<Customer>().SetVisible();
				nextCustomerNr = data.getNextCustomerNr();
				leftCount = data.getLeftCount();
				print(g.transform.position);
			}
		}

		pauze = false;

		ovenScene = "Oven";
		counterScene = "Counter";
		workbenchScene = "Workbench";

		customerTimer = Time.time + 1f;

		btnCount = 1;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!pauze)
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

			//Set new value of customers to SavedData
			data.updateCustomers(this.customers, this.nextCustomerNr, this.leftCount);
		}
	}

	public void Leave() //when button pressed
	{
		print ("leave");

		if (leftCount < nrOfCustomers)
		{
			GameObject c = customers[leftCount];
			print (leftCount);

			if (c != null && c.transform.position.y <= (GetCustomerPosition(1).y + 2f))
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

		this.pauze = true;

		//Set new value of customers to SavedData
		data.updateCustomers(this.customers, this.nextCustomerNr, this.leftCount);

		//Put customers on background
		foreach (GameObject g in this.customers)
		{
			if (g != null)
			{
				g.GetComponent<Customer>().SetInvisible();
			}
		}

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

	public void setPauze(bool pauze)
	{
		this.pauze = pauze;
	}

	public void buttonPauze()
	{
		if (btnCount%2 == 1)
		{
			this.pauze = true;
		}
		else
		{
			this.pauze = false;
		}

		btnCount++;
	}
}
