using UnityEngine;
using System.Collections;

public class DragManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update() {
		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch(0);
			GameObject hit = InputDetection.CheckTouch(touch.position);
			Vector2 touchPosition = Vector2.zero;
			
			if(hit != null && touch.phase == TouchPhase.Moved) {
				touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
				
				if(touchPosition.x != transform.position.x || touchPosition.y != transform.position.y) {
					transform.position = touchPosition;
                }
            }
        }
    }
}
