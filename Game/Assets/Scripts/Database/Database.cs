using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Database {
	private List<Person> persons;
	private List<Score> scores;
	private List<SumInfo> sums;

	public Database()
	{
		persons = new List<Person>();
		scores = new List<Score>();
		sums = new List<SumInfo>();
	}

	public void insertTestData()
	{
		persons.Add (new Person ("mel", "ww"));
		persons.Add (new Person ("alex", "123"));
		persons.Add (new Person ("bart", "bb"));

		scores.Add (new Score("mel", 1100));
		scores.Add (new Score("alex", 1450));
		scores.Add (new Score("bart", 760));

		sums.Add (new SumInfo ("money", 0, 1, 10, 0));
		sums.Add (new SumInfo ("money", 1, 0, 5, 1));
		sums.Add (new SumInfo ("money", 2, 0, 6, 2));
		sums [sums.Count - 1].AddCommaOption (0.25);
		sums [sums.Count - 1].AddCommaOption (0.50);
		sums [sums.Count - 1].AddCommaOption (0.75);
		sums.Add (new SumInfo ("money", 3, 0, 6, 2));
		sums.Add (new SumInfo ("time", 0, 0, 23, 0));
		sums.Add (new SumInfo ("time", 1, 0, 23, 2));
		sums [sums.Count - 1].AddCommaOption (0.15);
		sums [sums.Count - 1].AddCommaOption (0.30);
		sums [sums.Count - 1].AddCommaOption (0.45);
		sums.Add (new SumInfo ("time", 2, 0, 23, 2));
		sums.Add (new SumInfo ("time", 3, 0, 23, 2));
		sums.Add (new SumInfo ("volume", 0, 1, 100, 0));
		sums.Add (new SumInfo ("volume", 1, 1, 999, 0));
		sums.Add (new SumInfo ("volume", 2, 0, 100, 1));
		sums.Add (new SumInfo ("volume", 3, 0, 100, 1));
		DatabaseHandler.Save (this);
	}

	public bool CheckLogin(string name, string password)
	{
		Person pers = null;

		foreach (Person p in persons)
		{
			if (p.name.Equals(name) && p.password.Equals(password))
			{
				return true;
			}
		}

		return false;
	}

	public bool CheckUsernameExists(string name)
	{		
		foreach (Person p in persons)
		{
			if (p.name.Equals(name))
			{
				return true;
			}
		}
		
		return false;
	}

	public void SignIn(string name, string password)
	{		
		persons.Add (new Person(name, password));
		DatabaseHandler.Save (this);
	}

	public string[] GetHighscore()
	{

		return null;
//			MySqlDataReader reader = cmd.ExecuteReader ();
//			
//			while (reader.Read() && count < 5)
//			{
//				string name = reader.GetString("pName");
//				int score = reader.GetInt32("score");
//
//				string total = name + " - " + score;
//				scores[count] = total;
//
//				count++;
//			}
	}

	public void AddScore(string name, int score)
	{
		scores.Add (new Score (name, score));
		DatabaseHandler.Save (this);
	}

	public SumInfo GetSumInfo(string sumType, int sumLevel)
	{
		foreach (SumInfo s in this.sums) 
		{
			if(s.sumType.Equals(sumType) && s.sumLevel.Equals(sumLevel)) 
			{
				return s;
			}
		}
		return null;
	}
}
