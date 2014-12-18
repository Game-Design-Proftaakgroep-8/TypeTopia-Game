using UnityEngine;
using System.Collections;

public class MoneyManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "hand") {
			transform.SetParent(col.gameObject.transform);
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		if(col.tag == "hand") {
			transform.SetParent(null);
		}
	}
}
