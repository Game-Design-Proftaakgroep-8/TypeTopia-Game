using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CounterManager : MonoBehaviour {

	public GameObject hand;
	private HandMovement handMovement;
	private SavedData data;
	private Database db;
	private SumInfo info;

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

	private float moneyCustomer;
	private float demandCustomer;
	private float priceBread;
	private float cost;
	private float answer;


	// Use this for initialization
	void Start () {
		handMovement = hand.GetComponent<HandMovement> ();
		data = SavedData.getInstance ();
		db = DatabaseHandler.Load ();
		gameOver = false;

		info = db.GetSumInfo ("money", 1);
		moneyCustomer = 0;
		setTopianText ();
		setQuestion ();
		handMovement.MoveIn ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void setQuestion() {
		priceBread = Mathf.Round(Random.Range (info.minRange, info.maxRange + 1));
		demandCustomer = Mathf.Round(Random.Range (info.minRange, info.maxRange + 1));
		cost = priceBread * demandCustomer;

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
		priceText.text = "1 brood kost €" + priceBread;

		if(demandCustomer > 1)
			demandText.text = "Ik wil " + demandCustomer + " broden";
		else
			demandText.text = "Ik wil " + demandCustomer + " brood";
	}

	private void generateCustomerMoney() {
		GameObject money = (GameObject)eenEuro;
		Vector2 inHand = new Vector2(hand.transform.position.x, hand.transform.position.y);

		if(cost < 1) {
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

	public void addCustomerMoney(float money) {
		moneyCustomer += money;
		print (moneyCustomer);
	}

	public void confirm() {
		gameOver = true;

		if(answer > moneyCustomer) {
			winText.text = "Je hebt te weinig gegeven";
		}
		else if(answer < moneyCustomer) {
			winText.text = "Je hebt te veel gegeven";
		}
		else if(answer == moneyCustomer) {
			winText.text = "Goed Gedaan!";
			data.increaseTopians(1);
			setTopianText();
		}
		StartCoroutine (returnToOverview ());
	}

	public IEnumerator returnToOverview() {
		yield return new WaitForSeconds(5);
		Application.LoadLevel ("BakeryOverview");
	}
}
