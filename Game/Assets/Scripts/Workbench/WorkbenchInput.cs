using UnityEngine;
using System.Collections;

public class WorkbenchInput : MonoBehaviour {
	public WaterController waterController;
	public BalanceController balanceController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch(0);
			GameObject hit = InputDetection.CheckTouch(touch.position);
			if(hit != null && touch.phase == TouchPhase.Began) {
				if(hit.CompareTag("tap")) {
					waterController.StartStopIncreasing();
				} else if (hit.CompareTag("measuringCup")) {
					waterController.StartStopDecreasing();
				} else if (hit.CompareTag("stockContainer")) {
					balanceController.StartStopIncreasingWeight();
				}
			}
		}
	}
}
