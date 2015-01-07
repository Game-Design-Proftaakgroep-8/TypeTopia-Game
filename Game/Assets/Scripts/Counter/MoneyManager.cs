using UnityEngine;
using System.Collections;

public class MoneyManager : MonoBehaviour {
	private GameObject manager;
	private CounterManager counterManager;

	public float amount;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("Manager");
		counterManager = manager.GetComponent<CounterManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "hand") {
			transform.SetParent(col.gameObject.transform);
			counterManager.addCustomerMoney(amount);
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		if(col.tag == "hand") {
			transform.SetParent(null);
			counterManager.addCustomerMoney(-amount);
		}
	}
}
