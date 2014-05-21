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


	float timeCarry = 0;
	// Update is called once per frame
	void Update () {
		if (main.IsPaused()) {
						timeCarry += Time.deltaTime;
						if (timeCarry >= 2f  ) {
								timeCarry = 0;
								main.SetPaused(false);
								main.ChangeDifficulty (channel, true);
								main.score.color = Color.white;
						}
				}
	}



	void OnMouseDown() {
		if (!main.IsPaused ()) {
			main.SetPaused(true);
			main.score.color = Color.black;
		}

	}
		

}
