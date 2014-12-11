using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssignmentManager : MonoBehaviour {
	public WaterController waterController;
	
	private Recipe recipe;
	private int wanted;
	private int marge;

	// Use this for initialization
	void Start () {
		// load amount and recipe
		string product = "Brood";
		int amount = 4;
		recipe = new Recipe (product, amount);
		recipe.AddRecipeRow (Ingredients.Bloem, 2, true);
		recipe.AddRecipeRow (Ingredients.Water, 800, false);
		wanted = 3;
		marge = 25;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CheckAssignment() {
		RecipeRow ingredient = recipe.GetNext ();
		if (ingredient != null) {
			int answer = ingredient.amount / recipe.amount * wanted;
			switch(ingredient.ingredient) {
				case Ingredients.Water:
					int given = waterController.GetMilliliters();
					waterController.GameOver();
					if(given >= answer - marge && given <= answer + marge) {
						print ("Winner");
					} else {
						print ("Loser");
					}
					break;
			}
			// finish last ingredient + if there is next do next, etc....

		}
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

	public void AddRecipeRow(Ingredients ingredient, int amount, bool finished) {
		this.ingredients.Add (new RecipeRow(ingredient, amount, finished));
	}

	public RecipeRow GetNext() {
		foreach(RecipeRow r in ingredients) {
			if(!r.finished) {
				return r;
			}
		}
		return null;
	}
}

public class RecipeRow {
	public bool finished { get; set; }
	public Ingredients ingredient { get; private set; }
	public int amount { get; private set; }
	//eenheid?

	public RecipeRow(Ingredients ingredient, int amount, bool finished) {
		this.ingredient = ingredient;
		this.amount = amount;
		this.finished = finished;
	}

	public override string ToString() {
		return ingredient.ToString() + "           " + amount;
	}
}

public enum Ingredients {
	Bloem,
	Water
}
