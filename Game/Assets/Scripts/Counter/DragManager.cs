using UnityEngine;
using System.Collections;

public class DragManager : MonoBehaviour {

	private CounterManager manager;
	private GameObject eenEuro;
	private GameObject tweeEuro;
	private GameObject vijfEuro;

	private GameObject lastTouched;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("Manager").GetComponent<CounterManager> ();

		lastTouched = null;
	}
	
	// Update is called once per frame
	void Update() {
		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch(0);
			GameObject hit = InputDetection.CheckTouch(touch.position);

			if(touch.phase == TouchPhase.Began)
				lastTouched = hit;
			else if(touch.phase == TouchPhase.Ended)
				lastTouched = null;

			Vector2 touchPosition = Vector2.zero;
			
			if(lastTouched != null) {
				if(lastTouched.tag == "money") {
					touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

					lastTouched.transform.position = touchPosition;
				}
				else if(lastTouched.tag == "1euro" && touch.phase == TouchPhase.Began) {
					GameObject geld = (GameObject)Instantiate(manager.eenEuro);

					geld.transform.position = hit.transform.position;
					lastTouched = InputDetection.CheckTouch(touch.position);
				}
				else if(lastTouched.tag == "2euro" && touch.phase == TouchPhase.Began) {
					GameObject geld = (GameObject)Instantiate(manager.tweeEuro);

					geld.transform.position = hit.transform.position;
					lastTouched = InputDetection.CheckTouch(touch.position);
				}
				else if(lastTouched.tag == "5euro" && touch.phase == TouchPhase.Began) {
					GameObject geld = (GameObject)Instantiate(manager.vijfEuro);
					
					geld.transform.position = hit.transform.position;
					lastTouched = InputDetection.CheckTouch(touch.position);
				}
            }
        }
    }
}
