using UnityEngine;
using System.Collections;

public class DragManager : MonoBehaviour {

	public GameObject eenEuro;
	public GameObject tweeEuro;
	public GameObject vijfEuro;

	private GameObject lastTouched;

	// Use this for initialization
	void Start () {
		lastTouched = null;
	}
	
	// Update is called once per frame
	void Update() {
		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch(0);
			GameObject hit = InputDetection.CheckTouch(touch.position);
			lastTouched = hit;
			Vector2 touchPosition = Vector2.zero;
			
			if(hit != null) {
				if(hit.tag == "money") {
					touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
					
					if(touchPosition.x != transform.position.x || touchPosition.y != transform.position.y) {
						hit.transform.position = touchPosition;
	                }
				}
				else if(hit.tag == "1euro") {
					GameObject geld = eenEuro;
					geld = (GameObject)Instantiate(eenEuro);

					geld.transform.position = hit.transform.position;
				}
				else if(hit.tag == "2euro") {
					GameObject geld = tweeEuro;
					geld = (GameObject)Instantiate(tweeEuro);

					geld.transform.position = hit.transform.position;
				}
				else if(hit.tag == "5euro") {
					GameObject geld = vijfEuro;
					geld = (GameObject)Instantiate(vijfEuro);
					
					geld.transform.position = hit.transform.position;
				}
            }
        }
    }
}
