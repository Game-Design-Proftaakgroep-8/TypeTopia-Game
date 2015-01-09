﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public SavedData data;

	public static int nrOfCustomers = 4;
	private GameObject[] customers;

	public GameObject CustomerPrefab1;
	public GameObject CustomerPrefab2;
	public GameObject CustomerPrefab3;
	public GameObject CustomerPrefab4;

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

	private bool counterPlayed;
	private bool workbenchPlayed;
	private bool ovenPlayed;

	public Text geslaagd;
	public Text topiansText;

	// Use this for initialization
	void Start ()
	{
		customers = new GameObject[nrOfCustomers];
		nextCustomerNr = 1;
		leftCount = 0;

		//Get saved customers from SavedData
		data = SavedData.getInstance ();
		customers = data.getCustomers ();
		topiansText.text = Convert.ToString (data.getTopians());

		counterPlayed = data.getCounterPlayed();
		workbenchPlayed = data.getWorkBenchPlayed();
		ovenPlayed = data.getOvenPlayed();
		geslaagd.text = " ";
		
		foreach (GameObject g in this.customers)
		{
			if (g != null)
			{
				g.GetComponent<Customer>().SetVisible();
				nextCustomerNr = data.getNextCustomerNr();
				leftCount = data.getLeftCount();
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
		if (counterPlayed && workbenchPlayed && ovenPlayed)
		{
			geslaagd.text = "Geslaagd";
			StartCoroutine(StopGame());
		}

		if (!pauze)
		{
			foreach (GameObject c in customers)
			{
				if (c != null)
				{
					c.GetComponent<Customer>().MoveToPosition();

					//c.transform.position.x == -8f && c.transform.position.y == -3.5f
					if (c.transform.position.x <= -7.8f && c.transform.position.y <= -3.3f)
					{
						Destroy(c.gameObject, 2);
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
						GameObject cus = (GameObject)ScriptableObject.Instantiate(this.getRandomCustomer(), pos, Quaternion.identity);
						cus.GetComponent<Customer>().level = this;

						//set order in layer so that the next customer shows up behind the one in front of him
						cus.renderer.sortingOrder = nrOfCustomers - nextCustomerNr + 1;

						int nr = nextCustomerNr - leftCount;
						cus.GetComponent<Customer>().SetNewPosition(GetCustomerPosition(nr));
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
		if (leftCount < nrOfCustomers)
		{
			GameObject c = customers[leftCount];

			if (c != null && c.transform.position.y <= (GetCustomerPosition(1).y + 1f))
			{
				c.GetComponent<Customer>().SetNewPosition(new Vector2 (-8f, -3.5f));

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

	//Select prefab
	public GameObject getRandomCustomer()
	{
		System.Random r = new System.Random ();
		int random = r.Next (0, 4);
		GameObject prefab = null;
		
		switch (random)
		{
		case 0:
			prefab = CustomerPrefab1;
			break;
		case 1:
			prefab = CustomerPrefab2;
			break;
		case 2:
			prefab = CustomerPrefab3;
			break;
		case 3:
			prefab = CustomerPrefab4;
			break;
		}
		
		return prefab;
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

		//start game
		if (hit == oven && !ovenPlayed)
		{
			print("Start game oven");
			CustomersToBackground();
			data.setOvenPlayed(true);
			Application.LoadLevel(ovenScene);
		}
		else if (hit == counter && !counterPlayed)
		{
			print("Start game counter");
			CustomersToBackground();
			this.Leave();
			data.setCounterPlayed(true);
			Application.LoadLevel(counterScene);
		}
		else if (hit == workbench && !workbenchPlayed)
		{
			print("Start game workbench");
			CustomersToBackground();
			data.setWorkbenchPlayed(true);
			Application.LoadLevel(workbenchScene);
		}
	}

	public void CustomersToBackground()
	{
		//Put customers on background
		foreach (GameObject g in this.customers)
		{
			if (g != null)
			{
				g.GetComponent<Customer>().SetInvisible();
			}
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

	public IEnumerator StopGame()
	{
		yield return new WaitForSeconds (3);
		data.deleteInstance ();

		//Remove customers
		foreach (GameObject g in this.customers)
		{
			if (g != null)
			{
				Destroy(g);
			}
		}

		Application.LoadLevel("Menu");
	}
}
