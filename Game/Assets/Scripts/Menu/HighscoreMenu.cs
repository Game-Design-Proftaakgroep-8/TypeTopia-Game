using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighscoreMenu : MonoBehaviour {

	private Database database;
	private SavedData data;

	private string menuScene;
	private string startScene;

	public Text score1;
	public Text score2;
	public Text score3;
	public Text score4;
	public Text score5;
	
	// Use this for initialization
	void Start ()
	{
		database = DatabaseHandler.Load ();
		menuScene = "Menu";
		startScene = "BakeryOverview";

		score1.text = "1. ";
		score2.text = "2. ";
		score3.text = "3. ";
		score4.text = "4. ";
		score5.text = "5. ";

		getScore ();
	}
	
	// Update is called once per frame
	void Update ()
	{	
	}

	public void getScore()
	{
		string[] scores = database.GetHighscore();

		if (scores[0] != null)
		{
			score1.text = "1. " + scores[0].ToString();
		}

		if (scores[1] != null)
		{
			score2.text = "2. " + scores[1].ToString();
		}

		if (scores[2] != null)
		{
			score3.text = "3. " + scores[2].ToString();
		}
		
		if (scores[3] != null)
		{
			score4.text = "4. " + scores[3].ToString();
		}

		if (scores[4] != null)
		{
			score5.text = "5. " + scores[4].ToString();
		}
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
}
