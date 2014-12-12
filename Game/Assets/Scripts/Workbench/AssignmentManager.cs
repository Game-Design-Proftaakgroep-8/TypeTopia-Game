using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class AssignmentManager : MonoBehaviour {
	public WaterController waterController;
	public Text winLoseText;
	public Text recipeText;
	public Text scoreText;
	public Text wantedText;
	public Text ProductText;
	
	private Recipe recipe;
	private int wanted;
	private int marge;
	private RecipeRow currentIngredient;
	private int currentAnswer;

	// Use this for initialization
	void Start () {
		// load amount and recipe
		string product = "Brood";
		int amount = 4;
		recipe = new Recipe (product, amount);
		recipe.AddRecipeRow (Ingredients.Bloem, 2, true, UnitPrexixes.k, "g");
		recipe.AddRecipeRow (Ingredients.Water, 800, false, UnitPrexixes.m, "l");
		recipe.AddRecipeRow (Ingredients.Water, 0.4f, false, UnitPrexixes.no, "l");
		recipe.AddRecipeRow (Ingredients.Water, 3, false, UnitPrexixes.d, "l");
		wanted = 3;
		marge = 25;
		recipeText.text = recipe.ToString();
		wantedText.text = wanted.ToString ();
		ProductText.text = recipe.product;
		StartCoroutine (this.startFirstGame());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void NextGame() {
		currentIngredient = recipe.GetNext();
		if (currentIngredient != null) {
			currentAnswer = Convert.ToInt32((currentIngredient.amount * (int)currentIngredient.unitPrefix) / recipe.amount * wanted);
			switch(currentIngredient.ingredient) {
				case Ingredients.Water:
					waterController.StartGame();
					break;
			}
		} else {
			winLoseText.text = "Einde van dit spel";
			StartCoroutine (this.backToOverview());
		}
	}

	public void CheckAssignment() {
		switch(currentIngredient.ingredient) {
			case Ingredients.Water:
				int given = waterController.GetMilliliters();
				waterController.StopGame();
				if(given >= currentAnswer - marge && given <= currentAnswer + marge) {
					winLoseText.text = "GOED!!";
					// score points?
				} else {
					winLoseText.text = "FOUT!!";
				}
				break;
		}
		StartCoroutine (this.startNextGame ());
		recipe.CheckFirst();
		recipeText.text = recipe.ToString ();
	}

	private IEnumerator startFirstGame() {
		yield return new WaitForSeconds (1);
		NextGame ();
	}

	private IEnumerator startNextGame() {
		yield return new WaitForSeconds (3);
		winLoseText.text = string.Empty;
		NextGame ();
	}

	private IEnumerator backToOverview() {
		yield return new WaitForSeconds (3);
		Application.LoadLevel ("BakeryOverview");
	}
}

public class Recipe {
	public List<RecipeRow> ingredients { get; private set; }
	public string product { get; private set; }
	public int amount { get; private set; }

	public Recipe(string product, int amount) {
		this.product = product;
		this.amount = amount;
		this.ingredients = new List<RecipeRow>();
	}

	public void AddRecipeRow(Ingredients ingredient, float amount, bool finished, UnitPrexixes unitPrefix, string unit) {
		this.ingredients.Add (new RecipeRow(ingredient, amount, finished, unitPrefix, unit));
	}

	public RecipeRow GetNext() {
		foreach(RecipeRow r in ingredients) {
			if(!r.finished) {
				return r;
			}
		}
		return null;
	}

	public void CheckFirst() {
		RecipeRow ingredient = this.GetNext ();
		ingredient.finished = true;
	}

	public override string ToString ()
	{
		string text = string.Format ("Recept voor {0} ({1}x) \n", this.product, this.amount);
		foreach (RecipeRow r in this.ingredients) {
			text += r.ToString() + "\n";
		}
		return text;
	}
}

public class RecipeRow {
	public bool finished { get; set; }
	public Ingredients ingredient { get; private set; }
	public float amount { get; private set; }
	public UnitPrexixes unitPrefix { get; private set; }
	public string unit { get; private set; }

	public RecipeRow(Ingredients ingredient, float amount, bool finished, UnitPrexixes unitPrefix, string unit) {
		this.ingredient = ingredient;
		this.amount = amount;
		this.finished = finished;
		this.unitPrefix = unitPrefix;
		this.unit = unit;
	}

	public override string ToString() {
		string unitPrefixText = unitPrefix.ToString ();
		if(unitPrefix == UnitPrexixes.no) {
			unitPrefixText = "";
		}
		string text = string.Format ("{0} \t\t\t {1} {2}{3}", ingredient.ToString(), amount, unitPrefixText, unit);
		if(finished) {
			text = "[x] " + text;
		} else {
			text = "[-] " + text;
		}
		return text;
	}
}

public enum Ingredients {
	Bloem,
	Water
}

public enum UnitPrexixes {
	m = 1,
	c = 10,
	d = 100,
	no = 1000,
	da = 10000,
	h = 100000,
	k = 1000000
}
