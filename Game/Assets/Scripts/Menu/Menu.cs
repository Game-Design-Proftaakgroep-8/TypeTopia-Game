using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	private SavedData data;

	public Text txtUser;

	private string startScene;
	private string highscoreScene;
	private string loginScene;

	private int level;
	public Text txtLevel;

	// Use this for initialization
	void Start ()
	{
		data = SavedData.getInstance ();
		data.resetTopians ();
		startScene = "BakeryOverview";
		highscoreScene = "Highscore";
		loginScene = "Login";

		level = 0;
		txtUser.text = "Hallo, " + data.getUser();
	}
	
	// Update is called once per frame
	void Update ()
	{	
	}

	public void Signout()
	{
		data.login ("");
		Application.LoadLevel(loginScene);
	}

	public void StartGame()
	{
		data.setLevel (level);
		data.reset ();
		Application.LoadLevel(startScene);
	}

	public void ShowHighscore()
	{
		Application.LoadLevel(highscoreScene);
	}

	public void LevelPlus()
	{
		if (level < 3)
		{
			level++;
			txtLevel.text = level.ToString();
		}
	}

	public void LevelMin()
	{
		if (level > 0)
		{
			level--;

			if (level == 0)
			{
				txtLevel.text = "test";
			}
			else
			{
				txtLevel.text = level.ToString();
			}
		}
	}
}
