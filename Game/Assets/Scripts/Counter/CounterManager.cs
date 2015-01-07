using UnityEngine;
using System.Collections;

public class CounterManager : MonoBehaviour {

	public GameObject hand;
	private HandMovement handMovement;

	public Object eenEuro;
	public Object tweeEuro;
	public Object vijfEuro;

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
		moneyCustomer = Mathf.Round(Random.Range (cost + 2, 60));
		answer = moneyCustomer - cost;

		print ("priceBread: " + priceBread);
		print ("demandCustomer: " + demandCustomer);
		print ("cost: " + cost);
		print ("moneyCustomer: " + moneyCustomer);

		generateCustomerMoney ();
	}

	private void generateCustomerMoney() {
		int randomMoney = Random.Range (0, 3);
		GameObject money = (GameObject)eenEuro;
		Vector2 inHand = new Vector2(hand.transform.position.x, hand.transform.position.y);

		switch (randomMoney) {
		case 0:
			if(moneyCustomer < 1) {
				break;
			}
			else {
				money = (GameObject)Instantiate(eenEuro);
				money.transform.position = inHand;
				moneyCustomer -= 1;
				break;
			}
		case 1:
			if(moneyCustomer < 2) {
				break;
			}
			else {
				money = (GameObject)Instantiate(tweeEuro);
				money.transform.position = inHand;
				moneyCustomer -= 2;
				break;
			}
		case 2:
			if(moneyCustomer < 5) {
				break;
			}
			else {
				money = (GameObject)Instantiate(vijfEuro);
				money.transform.position = inHand;
				moneyCustomer -= 5;
				break;
			}
		}

		if (moneyCustomer > 0)
			generateCustomerMoney();
	}


	public void confirm() {
		gameOver = true;
		StartCoroutine (returnToOverview ());
	}

	public IEnumerator returnToOverview() {
		yield return new WaitForSeconds(3);
		Application.LoadLevel ("BakeryOverview");
	}
}
