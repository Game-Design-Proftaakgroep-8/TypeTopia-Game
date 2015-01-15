using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LoginMenu : MonoBehaviour {

	private SavedData data;
	private Database database;

	private string startScene;

	public Text txtName;
	public Text txtPassword;

	// Use this for initialization
	void Start ()
	{
		startScene = "BakeryOverview";
	}
	
	// Update is called once per frame
	void Update ()
	{
		data = SavedData.getInstance ();
	}

	public void Login()
	{
		string name = txtName.text;
		string password = txtPassword.text;

		if (String.IsNullOrEmpty(name) && String.IsNullOrEmpty(password))
		{
			bool exists = database.CheckLogin(name, password);

			if (exists)
			{
				data.login (name);
				Application.LoadLevel(startScene);
			}
			else
			{
				print("Login failed");
			}
		}
	}

	public void Signin()
	{
		string name = txtName.text;
		string password = txtPassword.text;

		if (String.IsNullOrEmpty(name) && String.IsNullOrEmpty(password))
		{
			bool exists = database.CheckUsernameExists (name);

			if (exists)
			{
				print("Username already exists");
			}
			else
			{
				database.SignIn(name, password);
				data.login (name);
				Application.LoadLevel(startScene);
			}
		}
	}
}
