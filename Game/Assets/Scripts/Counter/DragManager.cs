using UnityEngine;
using System.Collections;

public class DragManager : MonoBehaviour {

	private float dragSpeed;

	// Use this for initialization
	void Start () {
		dragSpeed = 10f;
	}
	
	// Update is called once per frame
	void Update () {

    }

	void FixedUpdate() {
		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch(0);
			GameObject hit = InputDetection.CheckTouch(touch.position);
			Vector3 touchPosition = Vector3.zero;
			
			if(hit != null && touch.phase == TouchPhase.Stationary) {
				touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
				
				if(touchPosition.x != transform.position.x || touchPosition.y != transform.position.y) {
					if (touchPosition.x < transform.position.x) {
						transform.Translate(Vector3.left * dragSpeed * Time.deltaTime);
					}
					else if (touchPosition.x > transform.position.x) {
						transform.Translate(Vector3.right * dragSpeed * Time.deltaTime);
					}
					
					if (touchPosition.y < transform.position.y) {
						transform.Translate(Vector3.down * dragSpeed * Time.deltaTime);
					}
					else if (touchPosition.y > transform.position.y) {
                        transform.Translate(Vector3.up * dragSpeed * Time.deltaTime);
                    }
                }
            }
        }
    }
}
