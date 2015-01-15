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
	public Text topiansText;
	public Text wantedText;
	public Text productText;

	public AudioClip audioGood;
	public AudioClip audioBad;

	private SpriteManager spriteManager;
	private Recipe recipe;
	private int wanted;
	private float marge;
	private RecipeRow currentIngredient;
	private int currentAnswer;
	private int level;

	private SavedData savedData;
	private Database db;

	// Use this for initialization
	void Start () {
		this.savedData = SavedData.getInstance ();
		level = savedData.getLevel();
		this.spriteManager = GetComponent<SpriteManager> ();

		GenerateRecipe ();

		marge = 10f;
		recipeText.text = recipe.ToString();
		wantedText.text = wanted.ToString ();
		productText.text = recipe.product;
		topiansText.text = Convert.ToString(savedData.getTopians ());
		StartCoroutine (this.startFirstGame());
	}

	private void GenerateRecipe() {
		this.db = new Database ();
		SumInfo sumInfo = this.db.GetSumInfo ("volume", level);
		
		// load amount and recipe
		string product = "Brood";
		int amount = 0;
		wanted = 0;
		int amountFinished = 0;
		switch(level) {
		case 0:
			amount = 1;
			wanted = 1;
			amountFinished = 0;
			break;
		case 1:
			amount = 1;
			wanted = 2;
			amountFinished = 4;
			break;
		case 2:
			amount = 2;
			wanted = 1;
			amountFinished = 2;
			break;
		case 3:
			amount = 4;
			wanted = 3;
			amountFinished = 0;
			break;
		}
		
		recipe = new Recipe (product, amount);
		
		int enumSize = Enum.GetValues (typeof(Ingredients)).Length;
		for (int i = 0; i < enumSize; i++) {
			Ingredients ingredient = (Ingredients)UnityEngine.Random.Range(0, enumSize);
			while(recipe.FindIngredient(ingredient) != null) {
				ingredient = (Ingredients)UnityEngine.Random.Range(0, enumSize);
			}
			float ingredientAmount = UnityEngine.Random.Range(sumInfo.minRange, sumInfo.maxRange + 1);
			for(int j = 1; j <= sumInfo.sumCommas; j++) {
				if(ingredient != Ingredients.Water && ingredient != Ingredients.Melk) {
					ingredientAmount = 0;
				}
				ingredientAmount += (float) UnityEngine.Random.Range(1, 10) / (10 * j);
			}
			string unit = "g";
			if(ingredient == Ingredients.Water || ingredient == Ingredients.Melk) {
				unit = "l";
			} 
			UnitPrexixes unitPrefix = UnitPrexixes.no;
			switch(level){
				case 0:
					if(!unit.Equals("g")) {
						unitPrefix = UnitPrexixes.m;
					}
					break;
				case 1:
					if(!unit.Equals("g")) {
						unitPrefix = UnitPrexixes.m;
					}
					break;
				case 2:
					if(unit.Equals("g")) {
						unitPrefix = UnitPrexixes.k;
					} else {
						// ml cl
						int randomInt = UnityEngine.Random.Range(0, 2);
						int unitIndex = 1 * (int) Math.Pow(10, randomInt);
						if(unitIndex == 0) { unitIndex = 1; }
						unitPrefix = (UnitPrexixes)unitIndex;
					}
					break;
				case 3: 
					if(unit.Equals("g")) {
						unitPrefix = UnitPrexixes.k;
					} else {
						// ml cl dl
						int randomInt = UnityEngine.Random.Range(0, 3);
						int unitIndex = 1 * (int) Math.Pow(10, randomInt);
						if(unitIndex == 0) { unitIndex = 1; }
						unitPrefix = (UnitPrexixes)unitIndex;
					}
					break;
			}
			bool finished = false;
			if(i <= amountFinished - 1) { finished = true; }
			recipe.AddRecipeRow(ingredient, ingredientAmount, finished, unitPrefix, unit);
		}
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
			//winLoseText.text = "Einde van dit spel";
			StartCoroutine (this.backToOverview());
		}
	}

	public void CheckAssignment() {
		int given;
		float currentMarge;
		// check nog verbeteren?
		if(currentIngredient == null) { return; }
		if (currentIngredient.ingredient == Ingredients.Water || currentIngredient.ingredient == Ingredients.Melk) {
			if(!waterController.IsReadyToCheck()) { return; }
			given = waterController.GetMilliliters();
			currentMarge = marge;
			waterController.StopGame();
		} else {
			if(!balanceController.IsReadyToCheck()) { return; }
			given = balanceController.GetMilligrams ();
			currentMarge = marge * (int) balanceController.unitPrefix;
			balanceController.StopGame ();
		}
		print (currentAnswer);
		if(given >= currentAnswer - currentMarge && given <= currentAnswer + currentMarge) {
			//winLoseText.text = "GOED!!";
			audio.PlayOneShot(audioGood);
			savedData.increaseTopians(1);
			topiansText.text = Convert.ToString(savedData.getTopians ());
		} else {
			//winLoseText.text = "FOUT!!";
			audio.PlayOneShot(audioBad);
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

	public RecipeRow FindIngredient(Ingredients ingredient) {
		foreach (RecipeRow r in this.ingredients) {
			if(r.ingredient == ingredient) {
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
