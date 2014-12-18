using UnityEngine;
using System.Collections;

public class DragManager : MonoBehaviour {

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
            }
        }
    }

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "hand") {
			lastTouched.transform.SetParent(col.gameObject.transform);
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.tag == "hand") {
			lastTouched.transform.SetParent(null);
		}
	}
}
