using UnityEngine;
using System.Collections;

public class CounterManager : MonoBehaviour {

	public GameObject hand;
	private HandMovement handMovement;

	private bool start;
	private bool gameOver;

	// Use this for initialization
	void Start () {
		handMovement = hand.GetComponent<HandMovement> ();
		start = true;
		gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(start) {
			handMovement.MoveIn();
			start = false;
		}

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
