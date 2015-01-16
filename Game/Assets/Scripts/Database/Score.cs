using UnityEngine;
using System.Collections;

public class Score
{
	public string name {get; private set;}
	public int score {get; private set;}

	public Score(string name, int score)
	{
		this.name = name;
		this.score = score;
	}
}
