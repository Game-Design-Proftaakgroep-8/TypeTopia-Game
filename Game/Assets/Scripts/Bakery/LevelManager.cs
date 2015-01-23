using UnityEngine;
using UnityEngine.UI;
//using System;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public SavedData data;

	public int nrOfCustomers;
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
	private string scoreScene;

	private int nextCustomerNr = 1;
	private int leftCount = 0;

	private float customerTimer;

	private GameObject hit; 
	private Touch touch;
	private bool pauze;
	private bool stopped;

	private int counterPlayed = 0;
	private int maxCounter;
	public GameObject counterGlow;
	private int workbenchPlayed = 0;
	private int maxWorkbench;
	public GameObject workbenchGlow;
	private int ovenPlayed = 0;
	private int maxOven;
	public GameObject ovenGlow;

	public Text geslaagd;
	public Text topiansText;
	public AudioClip audioGood;

	// Use this for initialization
	void Start ()
	{
		//Get values from SavedData
		data = SavedData.getInstance ();

		if (data.getLevel() == 0)
		{
			nrOfCustomers = 1;
			maxCounter = 1;
			maxWorkbench = 1;
			maxOven = 1;
		}
		else if(data.getLevel() == 1)
		{
			nrOfCustomers = 2;
			maxCounter = 2;
			maxWorkbench = 1;
			maxOven = 1;
		}
		else if(data.getLevel() == 2)
		{
			nrOfCustomers = 3;
			maxCounter = 3;
			maxWorkbench = 2;
			maxOven = 2;
		}
		else if(data.getLevel() == 3)
		{
			nrOfCustomers = 4;
			maxCounter = 4;
			maxWorkbench = 2;
			maxOven = 2;
		}

		customers = new GameObject[nrOfCustomers];

		//Get saved customers from SavedData
		customers = data.getCustomers ();
		//topiansText.text = Convert.ToString (data.getTopians());
		topiansText.text = "Topians: " + data.getTopians ().ToString ();

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
		stopped = false;

		ovenScene = "Oven";
		counterScene = "Counter";
		workbenchScene = "Workbench";
		scoreScene = "Score";

		customerTimer = Time.time + 1f;

		StartCoroutine(showObjectToTouch());
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (stopped == false && counterPlayed == maxCounter && workbenchPlayed == maxWorkbench && ovenPlayed == maxOven)
		{
			stopped = true;
			audio.PlayOneShot(audioGood);
			geslaagd.text = "Goed gedaan!";
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
				if ((Time.time >= customerTimer + 4f) || nextCustomerNr == 1)
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
		yield return new WaitForSeconds (0);

		this.pauze = true;

		//Set new value of customers to SavedData
		data.updateCustomers(this.customers, this.nextCustomerNr, this.leftCount);

		//start game
		if (hit == workbench && workbenchPlayed < maxWorkbench)
		{
			if ((ovenPlayed == 0 && workbenchPlayed == 0 && counterPlayed == 0) 
			    || (workbenchPlayed == 1 && ovenPlayed == 1 && counterPlayed == 2))
			{
				workbenchPlayed++;
				data.setWorkbenchPlayed(workbenchPlayed);
				CustomersToBackground();
				
				Application.LoadLevel(workbenchScene);
			}
		}
		else if (hit == oven && ovenPlayed < maxOven)
		{
			if ((workbenchPlayed == 1 && ovenPlayed == 0 && counterPlayed == 0) 
			    || (workbenchPlayed == 2 && ovenPlayed == 1))
			{
				ovenPlayed++;
				data.setOvenPlayed(ovenPlayed);
				CustomersToBackground();

				Application.LoadLevel(ovenScene);
			}
		}
		else if (hit == counter && counterPlayed < maxCounter)
		{
			if ((workbenchPlayed == 1 && ovenPlayed == 1 && counterPlayed < 2) 
			    || (workbenchPlayed == 2 && ovenPlayed == 2))
			{
				counterPlayed++;
				data.setCounterPlayed(counterPlayed);
				CustomersToBackground();
				this.Leave();

				Application.LoadLevel(counterScene);
			}
		}
	}

	public IEnumerator showObjectToTouch()
	{
		if (data.getLevel() == 0)
		{
			if (workbenchPlayed == 0)
			{
				for (int i = 0; i < 3; i++)
				{
					workbenchGlow.renderer.sortingOrder = 5;
					yield return new WaitForSeconds (1);
					workbenchGlow.renderer.sortingOrder = -2;
					yield return new WaitForSeconds (1);
				}
			}

			if (workbenchPlayed == 1 && ovenPlayed == 0)
			{
				for (int i = 0; i < 3; i++)
				{
					ovenGlow.renderer.sortingOrder = 5;
					yield return new WaitForSeconds (1);
					ovenGlow.renderer.sortingOrder = -2;
					yield return new WaitForSeconds (1);
				}
			}

			if (workbenchPlayed == 1 && ovenPlayed == 1 && counterPlayed == 0)
			{
				for (int i = 0; i < 3; i++)
				{
					counterGlow.renderer.sortingOrder = 5;
					yield return new WaitForSeconds (1);
					counterGlow.renderer.sortingOrder = -2;
					yield return new WaitForSeconds (1);
				}
			}
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

	public void stop()
	{
		data.reset ();
		data.resetTopians ();
		
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

	public IEnumerator StopGame()
	{
		yield return new WaitForSeconds (5);
		data.reset ();

		//Remove customers
		foreach (GameObject g in this.customers)
		{
			if (g != null)
			{
				Destroy(g);
			}
		}

		Application.LoadLevel(scoreScene);
	}
}
