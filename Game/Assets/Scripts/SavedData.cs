using UnityEngine;
using System.Collections;

public class SavedData {

	private static SavedData instance = null;
	private GameObject[] customers;
	private int maxCustomers = 0;
	private int nextCustomerNr = 1;
	private int leftCount = 0;

	private bool counterPlayed;
	private bool workbenchPlayed;
	private bool ovenPlayed;

	private int topians = 0;
	private int level = 0;
	private string user;
	private int level = 0;

	private SavedData()
	{
		maxCustomers = LevelManager.nrOfCustomers;
		nextCustomerNr = 1;
		leftCount = 0;
		customers = new GameObject[maxCustomers];

		counterPlayed = false;
		workbenchPlayed = false;
		ovenPlayed = false;
	}

	public static SavedData getInstance()
	{
		if (instance == null)
		{
			instance = new SavedData();
		}

		return instance;
	}

	public void deleteInstance()
	{
		instance = null;
	}

	//Get and set Customer data
	public void updateCustomers(GameObject[] cus, int nextNr, int leftNr)
	{
		int count = 0;

		foreach (GameObject g in cus)
		{
			if (g != null)
			{
				customers[count] = g;
				count++;
			}
		}

		this.nextCustomerNr = nextNr;
		this.leftCount = leftNr;
	}

	public GameObject[] getCustomers()
	{
		return customers;
	}

	public int getNextCustomerNr()
	{
		return this.nextCustomerNr;
	}

	public int getLeftCount()
	{
		return this.leftCount;
	}

	//Get and set played games
	public bool getCounterPlayed()
	{
		return this.counterPlayed;
	}
	
	public bool getWorkBenchPlayed()
	{
		return this.workbenchPlayed;
	}
	
	public bool getOvenPlayed()
	{
		return this.ovenPlayed;
	}
	
	public void setCounterPlayed(bool played)
	{
		this.counterPlayed = played;
	}
	
	public void setWorkbenchPlayed(bool played)
	{
		this.workbenchPlayed = played;
	}
	
	public void setOvenPlayed(bool played)
	{
		this.ovenPlayed = played;
	}

	//Get and set score	
	public void increaseTopians(int topians)
	{
		this.topians += topians;
	}

	public int getTopians()
	{
		return this.topians;
	}

	public void resetTopians()
	{
		this.topians = 0;
	}

	public int getLevel() 
	{
		return this.level;
	}

	public void setLevel(int level) 
	{
		this.level = level;
	}

	public void login(string name)
	{
		this.user = name;
	}

	public string getUser()
	{
		return this.user;
	}
}
