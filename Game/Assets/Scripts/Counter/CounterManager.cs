using UnityEngine;
using System.Collections;

public class CounterManager : MonoBehaviour {

	public GameObject hand;
	private HandMovement handMovement;

	private bool start;

	// Use this for initialization
	void Start () {
		handMovement = hand.GetComponent<HandMovement> ();
		start = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(start) {
			handMovement.MoveIn();
			start = false;
		}

	}
}
