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
		database = new Database ();
		menuScene = "Menu";
		startScene = "BakeryOverview";

		getScore ();
	}
	
	// Update is called once per frame
	void Update ()
	{	
	}

	public void getScore()
	{
		string[] scores = database.GetHighscore();
		score1.text = "1. " + scores[0].ToString() + " Topians";
		score2.text = "2. " + scores[1].ToString() + " Topians";
		score3.text = "3. " + scores[2].ToString() + " Topians";
		score4.text = "4. " + scores[3].ToString() + " Topians";
		score5.text = "5. " + scores[4].ToString() + " Topians";
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
