using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LoginMenu : MonoBehaviour {

	private SavedData data;
	private Database database;

	private string menuScene;

	public Text txtName;
	public Text txtPassword;

	// Use this for initialization
	void Start ()
	{
		database = DatabaseHandler.Load ();
		data = SavedData.getInstance ();
		menuScene = "Menu";
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void Login()
	{
		string name = txtName.text;
		string password = txtPassword.text;

		if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(password))
		{
			bool exists = database.CheckLogin(name, password);

			if (exists)
			{
				data.login (name);
				Application.LoadLevel(menuScene);
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

		if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(password))
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
				Application.LoadLevel(menuScene);
			}
		}
	}

	public void Quit() {
		Application.Quit ();
	}
}
