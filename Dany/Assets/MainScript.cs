using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour {
	public RedSquare redSquare;
	public GUIText score;
	public int points = 0;
	public bool paused = false;

	// Use this for initialization
	void Start () {
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

		if (carry3 > 0.5f && wait3 <= 0f) {
			carry3 = 0;
			f1--;
			if (f1 > 0) {
				wait3 -= Time.deltaTime * 1f;
			} else {
				RandomInstantiation(1,-0.0325695f);	
				f1 = 0;
			}
			
		}
		wait3 -= Time.deltaTime;
		carry3 += Time.deltaTime;

		if (carry2 > 0.30f && wait2 <= 0f) {
			carry2 = 0;
			f2--;
			if (f2 > 0) {
				wait2 -= Time.deltaTime * 1f;
			} else {
				RandomInstantiation(2,-1.733552f);	
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

		}
		wait -= Time.deltaTime;
		carry += Time.deltaTime;
	}

	void RandomInstantiation(int channel, float x) {
		redSquare.channelRunningIn = channel;
		redSquare.color = Random.Range (1, 4);
		Instantiate(redSquare, new Vector3(x,4.149223f+10f,0f), Quaternion.identity);
	}
}
