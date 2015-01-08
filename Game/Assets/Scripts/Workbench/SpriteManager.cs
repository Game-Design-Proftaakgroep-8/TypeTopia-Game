using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteManager : MonoBehaviour {
	// weight
	public Transform stockRain;
	public Transform stockContainer;

	// liquid
	public Transform rain;
	public Transform water;

	// sprites
	public GameObject gistRain;
	public GameObject gistContainer;
	public GameObject meelRain;
	public GameObject meelContainer;
	public GameObject melkRain;
	public GameObject melkContainer;
	public GameObject suikerRain;
	public GameObject suikerContainer;
	public GameObject waterRain;
	public GameObject waterContainer;
	public GameObject zoutRain;
	public GameObject zoutContainer;

	public List<SpriteSet> spriteSets;

	private GameObject rainSprite;
	private GameObject containerSprite;

	// Use this for initialization
	void Start () {
		spriteSets = new List<SpriteSet> ();
		spriteSets.Add (new SpriteSet (meelRain, meelContainer));
		spriteSets.Add (new SpriteSet (gistRain, gistContainer));
		spriteSets.Add (new SpriteSet (zoutRain, zoutContainer));
		spriteSets.Add (new SpriteSet (waterRain, waterContainer));
		spriteSets.Add (new SpriteSet (suikerRain, suikerContainer));
		spriteSets.Add (new SpriteSet (melkRain, melkContainer));

		rainSprite = null;
		containerSprite = null;
	}

	public void SetSprites(Ingredients ingredient) {
		if (rainSprite != null && containerSprite != null) {
			Destroy (rainSprite);
			Destroy (containerSprite);
		}
		rainSprite = (GameObject) Instantiate (spriteSets [(int)ingredient].rain);
		containerSprite = (GameObject) Instantiate(spriteSets [(int)ingredient].container);
		if (ingredient == Ingredients.Water || ingredient == Ingredients.Melk) {
			rainSprite.transform.SetParent(rain);
			containerSprite.transform.SetParent(water);
		} else {
			rainSprite.transform.SetParent (stockRain);
			containerSprite.transform.SetParent(stockContainer);
		}
		resetTransform (rainSprite.transform);
		resetTransform (containerSprite.transform);
	}

	
	private void resetTransform(Transform spriteTransform) {
		spriteTransform.localPosition = new Vector3 (0, 0, 0);
		spriteTransform.localScale = new Vector3(1, 1, 1);
		spriteTransform.localRotation = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public class SpriteSet {
	public GameObject rain { get; private set; }
	public GameObject container { get; private set; }

	public SpriteSet(GameObject rain, GameObject container) {
		this.rain = rain;
		this.container = container;
	}
}
