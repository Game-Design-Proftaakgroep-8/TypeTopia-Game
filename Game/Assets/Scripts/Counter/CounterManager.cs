using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CounterManager : MonoBehaviour {

	public GameObject hand;
	public GameObject handGlow;
	public GameObject registerGlow;
	private HandMovement handMovement;
	private SavedData data;
	private Database db;
	private SumInfo info;

	public AudioClip audioGood;
	public AudioClip audioBad;

	public Object vijfCent;
	public Object tienCent;
	public Object twintigCent;
	public Object vijftigCent;
	public Object eenEuro;
	public Object tweeEuro;
	public Object vijfEuro;
	public Object tienEuro;
	public Object twintigEuro;
	public Object vijftigEuro;

	public Text priceText;
	public Text demandText;
	public Text winText;
	public Text topiansText;

	private bool gameOver;

	private double moneyCustomer;
	private int demandCustomer;
	private double priceBread1;
	private double priceBread2;
	private double priceBread;
	private double cost;
	private double answer;

	private bool handGlowShown;
	private bool registerGlowShown;


	// Use this for initialization
	void Start () {
		handMovement = hand.GetComponent<HandMovement> ();
		data = SavedData.getInstance ();
		db = DatabaseHandler.Load ();
		gameOver = false;
		handGlowShown = false;
		registerGlowShown = false;

		info = db.GetSumInfo ("money", data.getLevel ());
		moneyCustomer = 0;
		setTopianText ();
		setQuestion ();
		handMovement.MoveIn ();
	}
	
	// Update is called once per frame
	void Update () {
		if(hand.transform.position.y <= 3f || hand.transform.position.y >= 9f) {
			if(!handGlowShown) {
				handGlowShown = true;
				StartCoroutine(ShowHandGlow());
			}
		}
	}

	private void setQuestion() {
		priceBread1 = Mathf.Round(Random.Range (info.minRange, info.maxRange + 1));
		if(priceBread1 != info.maxRange) {
			print("sumCommas: " + info.sumCommas);
			switch (info.sumCommas) {
			case 0:
				priceBread2 = 0;
				break;
			case 1:
				priceBread2 = Random.Range(0, 10);
				priceBread2 = priceBread2 / 10;
				break;
			case 2:
				float commaOptions = info.commaOptions.Count;
				priceBread2 = info.commaOptions[(int)Mathf.Round (Random.Range (0, commaOptions -1))];
				break;
			}
		}
		print ("priceBread1: " + priceBread1 + " - priceBread2: " + priceBread2);
		priceBread = priceBread1 + priceBread2;

		demandCustomer = (int)Mathf.Round(Random.Range (info.minRange + 1, info.maxRange + 1));
		cost = priceBread * demandCustomer;

		if (cost == 100)
			setQuestion ();

		print ("priceBread: " + priceBread);
		print ("demandCustomer: " + demandCustomer);
		print ("cost: " + cost);

		updateText ();
		generateCustomerMoney ();
	}

	private void setTopianText() {
		topiansText.text = "Topians: " + data.getTopians();
	}

	private void updateText (){
		priceText.text = "1 brood kost €" + priceBread1 + ",";
		if(priceBread2 > 0) {
			if(priceBread2.ToString().Length < 4)
				priceText.text += (10 * priceBread2) + "0";
			else
				priceText.text += (100 * priceBread2);
		}
		else
			priceText.text += "-";

		demandText.text = "Ik wil " + demandCustomer;
		if(demandCustomer > 1)
			demandText.text += " broden";
		else
			demandText.text += " brood";
	}

	private void generateCustomerMoney() {
		GameObject money = (GameObject)eenEuro;
		Vector2 inHand = new Vector2 (hand.transform.position.x, hand.transform.position.y);

		if(cost < 0.05) {
			money = (GameObject)Instantiate(vijfCent);
		}
		else if(cost < 0.10) {
			money = (GameObject)Instantiate(tienCent);
		}
		else if(cost < 0.20) {
			money = (GameObject)Instantiate(twintigCent);
		}
		else if(cost < 0.50) {
			money = (GameObject)Instantiate(vijftigCent);
		}
		else if(cost < 1) {
			money = (GameObject)Instantiate(eenEuro);
        }
		else if(cost < 2) {
			money = (GameObject)Instantiate(tweeEuro);
        }
		else if(cost < 5) {
			money = (GameObject)Instantiate(vijfEuro);
        }
		else if(cost < 10) {
			money = (GameObject)Instantiate(tienEuro);
        }
		else if(cost < 20) {
			money = (GameObject)Instantiate(twintigEuro);
        }
        else if(cost < 50) {
			money = (GameObject)Instantiate(vijftigEuro);
        }
		else {
			money = (GameObject)Instantiate(vijftigEuro);
		}

		money.GetComponent<MoneyManager>().setStartMoney(true);
		moneyCustomer += money.GetComponent<MoneyManager>().getAmount();
		money.transform.position = inHand;

		if(moneyCustomer < cost) {
			generateCustomerMoney();
		}

		answer = moneyCustomer - cost;

		print ("moneyCustomer: " + moneyCustomer);
		print ("answer: " + answer);
	}

	public void addCustomerMoney(double money) {
		moneyCustomer += money;
		moneyCustomer = (double)Mathf.Round((float)moneyCustomer * 100f) / 100f;
		print (moneyCustomer);

		if(!registerGlowShown) {
			registerGlowShown = true;
			StartCoroutine(ShowRegisterGlow());
		}
	}

	public void confirm() {
		if(!gameOver) {
			gameOver = true;
			if(answer > moneyCustomer) {
				winText.text = "Te weinig!";
				audio.PlayOneShot(audioBad);
			}
			else if(answer < moneyCustomer) {
				winText.text = "Te veel!";
				audio.PlayOneShot(audioBad);
			}
			else if(answer == moneyCustomer) {
				winText.text = "Goed gedaan!";
				data.increaseTopians(1);
				setTopianText();
				audio.PlayOneShot(audioGood);
			}
			StartCoroutine (returnToOverview ());
		}
	}

	public IEnumerator returnToOverview() {
		yield return new WaitForSeconds(5);
		Application.LoadLevel ("BakeryOverview");
	}

	public IEnumerator ShowHandGlow() {
		if(info.sumLevel == 0) {
			for (int i = 0; i < 3 && handGlow != null; i++) {
				handGlow.renderer.sortingOrder = 0;
				yield return new WaitForSeconds(1);
				if(handGlow != null) {
					handGlow.renderer.sortingOrder = -1;
				}
				yield return new WaitForSeconds(1);
			}
		}
	}

	public IEnumerator ShowRegisterGlow() {
		if(info.sumLevel == 0) {
			for (int i = 0; i < 3 && registerGlow != null; i++) {
				registerGlow.renderer.sortingOrder = 0;
				yield return new WaitForSeconds(1);
				if(handGlow != null) {
					registerGlow.renderer.sortingOrder = -1;
				}
				yield return new WaitForSeconds(1);
			}
		}
	}
}
