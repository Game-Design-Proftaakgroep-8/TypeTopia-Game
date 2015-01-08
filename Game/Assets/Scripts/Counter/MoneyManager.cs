using UnityEngine;
using System.Collections;

public class MoneyManager : MonoBehaviour {
	private GameObject manager;
	private CounterManager counterManager;

	public float amount;
	private bool startMoney;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("Manager");
		counterManager = manager.GetComponent<CounterManager> ();

		startMoney = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float getAmount() {
		return amount;
	}

	public void setStartMoney(bool isStartMoney) {
		startMoney = isStartMoney;
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "hand") {
			transform.SetParent(col.gameObject.transform);

			if(!startMoney)
				counterManager.addCustomerMoney(amount);
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		if(col.tag == "hand") {
			transform.SetParent(null);
			counterManager.addCustomerMoney(-amount);

			if(startMoney)
				setStartMoney(false);
		}
	}
}
