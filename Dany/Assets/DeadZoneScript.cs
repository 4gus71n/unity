using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public float maxTime = 0;
	float timeHolder;
	// Update is called once per frame
	void Update () {
		if (main.isMenuScene) {
			if (maxTime == 0) {
				maxTime = Random.Range(1,3);
			}
			timeHolder += Time.deltaTime;
			if (timeHolder >= maxTime) {
				OnMouseDown();
				timeHolder = 0;
				maxTime = 0;
			}
		}
	}

	void OnMouseDown() {
		if (main.paused) return;
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
		List<RedSquare> toRemove = 
			main.brickBag.FindAll(rs => rs.color == currentColor && rs.channelRunningIn == channel);
		main.brickBag.RemoveAll(rs => rs.color == currentColor && rs.channelRunningIn == channel);
		foreach (RedSquare rsq in toRemove) {
			if (rsq.gameObject != null) DestroyImmediate(rsq.gameObject);
		}
	}
		

}
