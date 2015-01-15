using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreMenu : MonoBehaviour {

	private Database database;
	private SavedData data;

	public Text score;

	private string menuScene;
	private string highscoreScene;
	private string startScene;

	// Use this for initialization
	void Start ()
	{
		database = new Database();
		data = SavedData.getInstance ();

		menuScene = "Menu";
		highscoreScene = "Highscore";
		startScene = "BakeryOverview";

		score.text = "Score: " + data.getTopians() + " Topians";

		SaveScore ();
	}
	
	// Update is called once per frame
	void Update ()
	{		
	}

	public void SaveScore()
	{
		database.AddScore (data.getUser (), data.getTopians ());
		data.resetTopians();
	}

	public void StartGame()
	{
		data.reset ();
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
