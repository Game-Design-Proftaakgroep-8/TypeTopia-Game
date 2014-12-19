using UnityEngine;
using System.Collections;

public class HighscoreMenu : MonoBehaviour {

	private string menuScene;
	private string startScene;
	
	// Use this for initialization
	void Start ()
	{
		menuScene = "Menu";
		startScene = "BakeryOverview";
	}
	
	// Update is called once per frame
	void Update ()
	{	
	}

	public void StartGame()
	{
		Application.LoadLevel(startScene);
	}
	
	public void ShowMenu()
	{
		Application.LoadLevel(menuScene);
	}
}
