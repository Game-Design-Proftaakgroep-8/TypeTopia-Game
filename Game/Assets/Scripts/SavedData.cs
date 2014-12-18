using UnityEngine;
using System.Collections;

public class SavedData {

	private static SavedData instance = null;
	private GameObject[] customers;
	private int maxCustomers = 0;
	private int nextCustomerNr = 1;
	private int leftCount = 0;

	private SavedData()
	{
		maxCustomers = LevelManager.nrOfCustomers;
		nextCustomerNr = 1;
		leftCount = 0;
		customers = new GameObject[maxCustomers];
	}

	public static SavedData getInstance()
	{
		if (instance == null)
		{
			instance = new SavedData();
		}

		return instance;
	}

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
}
