using UnityEngine;
using System.Collections;

public class RedSquare : MonoBehaviour {
	public const int DEAD = 0;
	public const int ALIVE = 1;

	public const int RED_BRICK = 1;
	public const int BLUE_BRICK = 2;
	public const int GREEN_BRICK = 3;

	public GameObject blueSquare;
	public GameObject greenSquare;
	public MainScript main;

	public int status = ALIVE;

	public int channelRunningIn;
	public int color = 1; //1 red, 2 blue, 3 green

	// Use this for initialization
	void Start () {
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
		color = BLUE_BRICK;
		renderer.enabled = false;
		blueSquare.renderer.enabled = true;
		greenSquare.renderer.enabled = false;
	}

	void changeToGreen() {
		color = GREEN_BRICK;
		renderer.enabled = false;
		blueSquare.renderer.enabled = false;
		greenSquare.renderer.enabled = true;
	}

	void changeToRed() {
		color = RED_BRICK;
		renderer.enabled = true;
		blueSquare.renderer.enabled = false;
		greenSquare.renderer.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if ((this.transform.position.y <= -5.539416) && !main.isMenuScene && (this.channelRunningIn != 666)) {
			RedSquare rsq = 
				main.brickBag.Find (rs => rs.gameObject.GetInstanceID () == gameObject.GetInstanceID());
			if (rsq != null) {
				main.ChangeDifficulty(rsq.channelRunningIn, false);
				main.brickBag.Remove(rsq);
				Destroy(rsq.gameObject);
				Handheld.Vibrate ();
			}
		} else if (!main.paused) {
						transform.position = 
				Vector3.MoveTowards (this.transform.position,
				                    new Vector3 (this.transform.position.x,
				             this.transform.position.y - MainScript.Y_SQUARE_START_OFFSET,
				            this.transform.position.z), 
				                    Time.deltaTime * main.GetSpeedForChannel(channelRunningIn));	
		}
	}
}
