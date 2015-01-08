using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CounterManager : MonoBehaviour {

	public GameObject hand;
	private HandMovement handMovement;

	public Object eenEuro;
	public Object tweeEuro;
	public Object vijfEuro;

	public Text priceText;
	public Text demandText;
	public Text winText;

	private bool gameOver;

	private float moneyCustomer;
	private float demandCustomer;
	private float priceBread;
	private float cost;
	private float answer;


	// Use this for initialization
	void Start () {
		handMovement = hand.GetComponent<HandMovement> ();
		gameOver = false;

		setQuestion ();
		handMovement.MoveIn ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void setQuestion() {
		priceBread = Mathf.Round(Random.Range (1, 10));
		demandCustomer = Mathf.Round(Random.Range (1, 5));
		cost = priceBread * demandCustomer;
		moneyCustomer = Mathf.Round(Random.Range (cost + 2, cost + 10));
		answer = moneyCustomer - cost;

		print ("priceBread: " + priceBread);
		print ("demandCustomer: " + demandCustomer);
		print ("cost: " + cost);
		print ("moneyCustomer: " + moneyCustomer);
		print ("answer: " + answer);

		updateText ();
		generateCustomerMoney ();
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


		if(moneyCustomer >= 5) {
			money = (GameObject)Instantiate(vijfEuro);
			money.transform.position = inHand;
			moneyCustomer -= 5;
        }
		else if(moneyCustomer >= 2) {
			money = (GameObject)Instantiate(tweeEuro);
			money.transform.position = inHand;
			moneyCustomer -= 2;
        }
		else if(moneyCustomer >= 1) {
			money = (GameObject)Instantiate(eenEuro);
			money.transform.position = inHand;
			moneyCustomer -= 1;
		}

		if (moneyCustomer > 0)
			generateCustomerMoney();
	}

	public void addCustomerMoney(float money) {
		moneyCustomer += money;
		print (moneyCustomer);
	}

	public void confirm() {
		gameOver = true;

		if(-cost > moneyCustomer) {
			winText.text = "Je hebt te weinig gegeven";
		}
		else if(-cost < moneyCustomer) {
			winText.text = "Je hebt te veel gegeven";
		}
		else if(-cost == moneyCustomer) {
			winText.text = "Goed Gedaan!";
		}
		StartCoroutine (returnToOverview ());
	}

	public IEnumerator returnToOverview() {
		yield return new WaitForSeconds(3);
		Application.LoadLevel ("BakeryOverview");
	}
}
