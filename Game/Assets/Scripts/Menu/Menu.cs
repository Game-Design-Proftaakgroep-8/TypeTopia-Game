using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	private string startScene;
	private string highscoreScene;

	// Use this for initialization
	void Start ()
	{
		startScene = "BakeryOverview";
		highscoreScene = "Highscore";
	}
	
	// Update is called once per frame
	void Update ()
	{	
	}

	public void StartGame()
	{
		Application.LoadLevel(startScene);
	}

	public void ShowHighscore()
	{
		Application.LoadLevel(highscoreScene);
	}
}
