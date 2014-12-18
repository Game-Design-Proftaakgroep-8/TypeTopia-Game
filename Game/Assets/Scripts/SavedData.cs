using UnityEngine;
using System.Collections;

public class SavedData {

	private static SavedData instance = null;
	private GameObject[] customers;

	private SavedData()
	{
		customers = new GameObject[4];
	}

	public static SavedData getInstance()
	{
		if (instance == null)
		{
			instance = new SavedData();
		}

		return instance;
	}

	public void updateCustomers(GameObject[] cus)
	{
		int count = 0;

		foreach (GameObject g in cus)
		{
			if (g != null)
			{
				customers[count] = g;
				Debug.Log(customers[count]);
				count++;
			}
			else
			{
				//Debug.Log(null);
			}
		}
	}

	public GameObject[] getCustomers()
	{
		return customers;
	}
}
