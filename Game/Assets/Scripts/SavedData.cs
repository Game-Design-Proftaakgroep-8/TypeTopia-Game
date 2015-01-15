using UnityEngine;
using System.Collections;

public class SavedData {

	private static SavedData instance = null;
	private GameObject[] customers;
	private int maxCustomers = 0;
	private int nextCustomerNr = 1;
	private int leftCount = 0;

	private int counterPlayed;
	private int workbenchPlayed;
	private int ovenPlayed;

	private int topians = 0;
	private int level = 0;
	private string user;

	private SavedData()
	{
		switch (this.level)
		{
		case 0:
			maxCustomers = 1;
			break;
		case 1:
			maxCustomers = 2;
			break;
		case 2:
			maxCustomers = 3;
			break;
		case 3:
			maxCustomers = 4;
			break;
		}

		nextCustomerNr = 1;
		leftCount = 0;
		customers = new GameObject[maxCustomers];

		counterPlayed = 0;
		workbenchPlayed = 0;
		ovenPlayed = 0;
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

	public void reset()
	{
		switch (this.level)
		{
		case 0:
			maxCustomers = 1;
			break;
		case 1:
			maxCustomers = 2;
			break;
		case 2:
			maxCustomers = 3;
			break;
		case 3:
			maxCustomers = 4;
			break;
		}

		nextCustomerNr = 1;
		leftCount = 0;
		customers = new GameObject[maxCustomers];
		
		counterPlayed = 0;
		workbenchPlayed = 0;
		ovenPlayed = 0;
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
	public int getCounterPlayed()
	{
		return this.counterPlayed;
	}
	
	public int getWorkBenchPlayed()
	{
		return this.workbenchPlayed;
	}
	
	public int getOvenPlayed()
	{
		return this.ovenPlayed;
	}
	
	public void setCounterPlayed(int played)
	{
		this.counterPlayed = played;
	}
	
	public void setWorkbenchPlayed(int played)
	{
		this.workbenchPlayed = played;
	}
	
	public void setOvenPlayed(int played)
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
