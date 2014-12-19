using UnityEngine;
using System.Collections;

public class ScoreMenu : MonoBehaviour {

	private string menuScene;
	private string highscoreScene;
	private string startScene;

	// Use this for initialization
	void Start ()
	{
		menuScene = "Menu";
		highscoreScene = "Highscore";
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

	public void ShowHighscore()
	{
		Application.LoadLevel(highscoreScene);
	}
}
