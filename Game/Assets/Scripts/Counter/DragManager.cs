using UnityEngine;
using System.Collections;

public class DragManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch(0);
			GameObject hit = InputDetection.CheckTouch(touch.position);
			Vector3 touchPosition = Vector3.zero;

            if(hit != null && touch.phase == TouchPhase.Stationary) {
				touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
				if (touchPosition.x < transform.position.x) {
					transform.Translate(Vector3.left * 10 * Time.deltaTime);
				}
				else if (touchPosition.x > transform.position.x) {
					transform.Translate(Vector3.right * 10 * Time.deltaTime);
				}

				if (touchPosition.y < transform.position.y) {
					transform.Translate(Vector3.down * 10 * Time.deltaTime);
				}
				else if (touchPosition.y > transform.position.y) {
					transform.Translate(Vector3.up * 10 * Time.deltaTime);
                }
			}
		}
	}
}
