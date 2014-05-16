using UnityEngine;
using System.Collections;

public class RedSquare : MonoBehaviour {

	public GameObject blueSquare;
	public GameObject greenSquare;
	public MainScript main;
	public int status = ALIVE;
	public GUIText scoreGui;

	public const int DEAD = 0;
	public const int ALIVE = 1;

	public int channelRunningIn;
	public float speed = 1.133032f;
	public int color = 1; //1 red, 2 blue, 3 green

	// Use this for initialization
	void Start () {
		switch (channelRunningIn) {
		case 1:
			speed = 1.133032f * 3f;
			break;
		case 2:
			speed = 1.133032f * 2f;
			break;
		case 3:
			speed = 1.133032f * 6f;
			break;
		}
		switch (color) {
		case 1:
			changeToRed();
			break;
		case 2:
			changeToBlue();
			break;
		case 3:
			changeToGreen();
			break;
		}
	}

	void changeToBlue() {
		color = 2;
		renderer.enabled = false;
		blueSquare.renderer.enabled = true;
		greenSquare.renderer.enabled = false;
	}

	void changeToGreen() {
		color = 3;
		renderer.enabled = false;
		blueSquare.renderer.enabled = false;
		greenSquare.renderer.enabled = true;
	}

	void changeToRed() {
		color = 1;
		renderer.enabled = true;
		blueSquare.renderer.enabled = false;
		greenSquare.renderer.enabled = false;
	}

	protected int clickCounter = 0;

	void OnMouseDown() {
		if (status == DEAD) return;
		main.IncPoints ();
		scoreGui.text = "Points:" + main.GetPoints ();
		switch (color) {
		case 1:
				changeToBlue ();
				break;
		case 2:
				changeToGreen ();
				break;
		case 3:
				changeToRed ();
				break;
		default:
			changeToRed();
			break;
		}			
	}

	// Update is called once per frame
	void Update () {
		if (status == DEAD) {
			Debug.Log("Funeral coming!");
			renderer.enabled = false;
			blueSquare.renderer.enabled = false;
			greenSquare.renderer.enabled = false;
		} else if (!main.paused && transform.position.y > -8f) {
			transform.position  = 
				Vector3.MoveTowards(this.transform.position,
				                    new Vector3(this.transform.position.x,
				            this.transform.position.y-10,
				            this.transform.position.z), 
				                    Time.deltaTime * speed);
		}
	}
}
