using UnityEngine;
using System.Collections;

public class DeadZoneScript : MonoBehaviour {
	public GameObject red;
	public GameObject blue;
	public GameObject green;

	public MainScript main;
	public int channel;
	public int currentColor = 0; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		++currentColor;
		currentColor = (currentColor == 4) ? (1) : (currentColor);
		switch (currentColor) {
		case 1:
			renderer.enabled = false;
			red.renderer.enabled = true;
			blue.renderer.enabled = false;
			green.renderer.enabled = false;
			break;
		case 2:
			renderer.enabled = false;
			red.renderer.enabled = false;
			blue.renderer.enabled = true;
			green.renderer.enabled = false;
			break;
		case 3:
			renderer.enabled = false;
			red.renderer.enabled = false;
			blue.renderer.enabled = false;
			green.renderer.enabled = true;
			break;
		}
		main.eraseAllSquaresIn(channel, currentColor);
	}
		

}
