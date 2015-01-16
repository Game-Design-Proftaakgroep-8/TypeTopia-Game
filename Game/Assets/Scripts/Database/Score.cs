using UnityEngine;
using System.Collections;
using System;

public class Score : IComparable<Score>
{
	public string name {get; private set;}
	public int score {get; private set;}

	public Score(string name, int score)
	{
		this.name = name;
		this.score = score;
	}

	public int CompareTo(Score other) {
		return this.score.CompareTo (other.score);
	}
}
