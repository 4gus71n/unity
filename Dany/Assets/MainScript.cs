using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainScript : MonoBehaviour {
	public RedSquare redSquare;
	public DeadZoneScript pathA;
	public DeadZoneScript pathB;
	public DeadZoneScript pathC;
	public GUIText score;
	public int points = 0;
	public bool paused = false;
	public List<RedSquare> squaresInChannelOne = new List<RedSquare>();
	public List<RedSquare> squaresInChannelTwo = new List<RedSquare>();   
	public List<RedSquare> squaresInChannelThree = new List<RedSquare>();   


	// Use this for initialization
	void Start () {
	}

	void removeAllFromWithColor(List<RedSquare> rsq, int color) {
		foreach(RedSquare iter in rsq) {
			if (iter.color == color) {
				iter.status = RedSquare.DEAD;
			}
		}
	}

	public void eraseAllSquaresIn(int channel, int color) {
		switch (channel) {
		case 1:
			Debug.Log("removing all from channel one with color:"+color);
			removeAllFromWithColor(squaresInChannelOne, color);
			break;
		case 2:
			Debug.Log("removing all from channel two with color:"+color);
			removeAllFromWithColor(squaresInChannelTwo, color);
			break;
		case 3:
			Debug.Log("removing all from channel three with color:"+color);
			removeAllFromWithColor(squaresInChannelThree, color);
			break;
		}
	}

	public void TooglePause () {
		paused = !paused;
	}

	public int GetPoints() {
		return points;
	}

	public void IncPoints(){
		points++;
	}

	float carry = 0, carry2 = 0,  carry3 = 0;
	float wait = 0, wait2 = 0, wait3 = 0;
	int f3 = 0, f2 = 0, f1 = 0;
	int last = -1;
	int n = 3;
	int nrepeats = 0;
	// Update is called once per frame
	void Update () {
		if (paused)
			return;
		if (f3 == 0) {
			f3 = Random.Range(1,5);
		}
		if (f2 == 0) {
			f2 = Random.Range(1,5);
		}
		if (f1 == 0) {
			f1 = Random.Range(1,5);
		}

		if (carry3 > 0.30f && wait3 <= 0f) {
			carry3 = 0;
			f1--;
			if (f1 > 0) {
				wait3 -= Time.deltaTime * 1f;
			} else {
				RandomInstantiation(1,-1.733552f);	
				f1 = 0;
			}
			
		}
		wait3 -= Time.deltaTime;
		carry3 += Time.deltaTime;

		/*if (carry2 > 0.5f && wait2 <= 0f) {
			carry2 = 0;
			f2--;
			if (f2 > 0) {
				wait2 -= Time.deltaTime * 1f;
			} else {
				RandomInstantiation(2,-0.0325695f);	
				f2 = 0;
			}
			
		}
		wait2 -= Time.deltaTime;
		carry2 += Time.deltaTime;

		if (carry > 0.15f && wait <= 0f) {
			carry = 0;
			f3--;
			if (f3 > 0) {
				wait -= Time.deltaTime * 1f;
			} else {
				RandomInstantiation(3,1.475035f);	
				f3 = 0;
			}

		}*/
		wait -= Time.deltaTime;
		carry += Time.deltaTime;
	}

	void RandomInstantiation(int channel, float x) {
		redSquare.channelRunningIn = channel;
		redSquare.color = Random.Range (1, 4);
		Debug.Log ("channel:" + channel + "color:" + redSquare.color);
		//If the color of the square is the same as the channel, spawn it dead.
		if (channel != 666 && (getPathFromChannel (channel).currentColor == redSquare.color)) {
			redSquare.renderer.enabled = false;
			redSquare.blueSquare.renderer.enabled = false;
			redSquare.greenSquare.renderer.enabled = false;
			redSquare.status = RedSquare.DEAD;
			Debug.Log("not spawning!!!");
		} else {
			switch (redSquare.color) {
			case 1:
				redSquare.renderer.enabled = true;
				redSquare.blueSquare.renderer.enabled = false;
				redSquare.greenSquare.renderer.enabled = false;
				break;
			case 2:
				redSquare.renderer.enabled = false;
				redSquare.blueSquare.renderer.enabled = true;
				redSquare.greenSquare.renderer.enabled = false;
				break;
			case 3:
				redSquare.renderer.enabled = false;
				redSquare.blueSquare.renderer.enabled = false;
				redSquare.greenSquare.renderer.enabled = true;
				break;
			}
			redSquare.status = RedSquare.ALIVE;
		}
		addToChannel (channel, redSquare);
		Instantiate(redSquare, new Vector3(x,4.149223f+10f,0f), Quaternion.identity);
	}

	DeadZoneScript getPathFromChannel (int channel) {
		switch (channel) {
		case 1:
			return pathA;
			break;
		case 2:
			return pathB;
			break;
		case 3:
			return pathC;
			break;
		}
		return null;
	}

	void addToChannel(int channel, RedSquare rs) {
		switch (channel) {
		case 1:
			squaresInChannelOne.Add(rs);
			break;
		case 2:
			squaresInChannelTwo.Add(rs);
			break;
		case 3:
			squaresInChannelThree.Add(rs);
			break;
		}
		return;
	}
}
