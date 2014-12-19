using UnityEngine;
using System.Collections;

public class CounterManager : MonoBehaviour {

	public GameObject hand;
	private HandMovement handMovement;

	public GameObject eenEuro;
	public GameObject tweeEuro;
	public GameObject vijfEuro;

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
		priceBread = Random.Range (1, 10);
		demandCustomer = Random.Range (1, 5);
		cost = priceBread * demandCustomer;
		moneyCustomer = Random.Range (cost + 2, 60);
		answer = moneyCustomer - cost;

		generateCustomerMoney ();
	}

	private void generateCustomerMoney() {

		
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
