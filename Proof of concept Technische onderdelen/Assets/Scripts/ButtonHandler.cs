using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonHandler : MonoBehaviour {

	private FileHandler file;
	public Text countText;

	private float blockCount;

	// Use this for initialization
	void Start () {
		file = GetComponent<FileHandler> ();
		file.Load ();

		blockCount = file.GetBlockCount ();

		UpdateText ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Increase(){
		blockCount += 1;
		UpdateText ();
	}

	public void Decrease(){
		blockCount -= 1;
		UpdateText ();
	}

	public void Ok(){
		file.SetBlockCount (blockCount);
		file.Save ();
	}

	private void UpdateText(){
		countText.text = "BlockCount: " + blockCount;
	}
}
