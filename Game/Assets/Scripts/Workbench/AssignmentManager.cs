using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class AssignmentManager : MonoBehaviour {
	public WaterController waterController;
	public BalanceController balanceController;
	public Text winLoseText;
	public Text recipeText;
	public Text scoreText;
	public Text wantedText;
	public Text ProductText;

	private SpriteManager spriteManager;
	private Recipe recipe;
	private int wanted;
	private float marge;
	private RecipeRow currentIngredient;
	private int currentAnswer;

	// Use this for initialization
	void Start () {
		this.spriteManager = GetComponent<SpriteManager> ();
		// load amount and recipe
		string product = "Brood";
		int amount = 4;
		recipe = new Recipe (product, amount);
		recipe.AddRecipeRow (Ingredients.Meel, 2, true, UnitPrexixes.k, "g");
		//recipe.AddRecipeRow (Ingredients.Water, 3, false, UnitPrexixes.d, "l");
		recipe.AddRecipeRow (Ingredients.Gist, 400, false, UnitPrexixes.no, "g");
		recipe.AddRecipeRow (Ingredients.Suiker, 4, false, UnitPrexixes.no, "g");
		recipe.AddRecipeRow (Ingredients.Melk, 25, false, UnitPrexixes.d, "l");
		recipe.AddRecipeRow (Ingredients.Zout, 1, false, UnitPrexixes.k, "g");
		wanted = 3;
		marge = 0.10f;
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
			spriteManager.SetSprites (currentIngredient.ingredient);
			currentAnswer = Convert.ToInt32((currentIngredient.amount * (int)currentIngredient.unitPrefix) / recipe.amount * wanted);
			if(currentIngredient.ingredient == Ingredients.Water || currentIngredient.ingredient == Ingredients.Melk) {
				// Ingredient meegeven
				waterController.StartGame();
			} else {
				balanceController.StartGame ();
			}
		} else {
			winLoseText.text = "Einde van dit spel";
			StartCoroutine (this.backToOverview());
		}
	}

	public void CheckAssignment() {
		int given;
		// check nog verbeteren?
		if(currentIngredient == null) { return; }
		if (currentIngredient.ingredient == Ingredients.Water || currentIngredient.ingredient == Ingredients.Melk) {
			if(!waterController.IsReadyToCheck()) { return; }
			given = waterController.GetMilliliters();
			waterController.StopGame();
		} else {
			if(!balanceController.IsReadyToCheck()) { return; }
			given = balanceController.GetMilligrams ();
			balanceController.StopGame ();
		}
		if(given >= currentAnswer - (currentAnswer * marge) && given <= currentAnswer + (currentAnswer * marge)) {
			winLoseText.text = "GOED!!";
			// score points?
		} else {
			winLoseText.text = "FOUT!!";
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
	Meel,
	Gist,
	Zout,
	Water,
	Suiker,
	Melk
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
