using UnityEngine;
using System.Collections;

public class DeadZoneScript : MonoBehaviour {

	public MainScript main;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		main.TooglePause ();
	}
		

}
