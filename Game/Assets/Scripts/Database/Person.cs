using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Person
{
	public string name {get; private set;}
	public string password {get; private set;}

	public Person(string name, string password)
	{
		this.name = name;
		this.password = password;
	}
}
