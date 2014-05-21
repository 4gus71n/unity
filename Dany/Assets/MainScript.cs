using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainScript : MonoBehaviour {

	public RedSquare redSquare;
	public RedSquare redSquare2;
	public RedSquare redSquare3;


	public DeadZoneScript pathA;
	public DeadZoneScript pathB;
	public DeadZoneScript pathC;
	public GUIText score;
	public int timeSeconds = 30;
	public int timeMiliSeconds = 60;

	public bool paused = false;

	public bool isMenuScene;

	public List<RedSquare> brickBag = new List<RedSquare>();

	public void SetPaused(bool b) {
		paused = b;
	}

	public bool IsPaused() {
		return paused;
	}

	public void TooglePause () {
		paused = !paused;
	}

	public const float MAX_SPEED = 7f;
	public const float MIN_SPEED = 1f;
	public const float INCREASE_SPEED_RATE = 0.25f;
	public const float DECREASE_SPEED_RATE = 0.5f;

	public void GameOver() {
		score.text = "GameOver: "+timeSeconds+":"+timeMiliSeconds;
		paused = true;
	}

	public void ChangeDifficulty(int channel, bool type) {
		switch (channel) {
		case 1:
			if (ch1speed >= MAX_SPEED && type) return;
			if (ch1speed <= MIN_SPEED && !type) GameOver();
			ch1speed += (type)?(INCREASE_SPEED_RATE):(-DECREASE_SPEED_RATE);
			break;
		case 2:
			if (ch2speed >= MAX_SPEED && type) return;
			if (ch2speed <= MIN_SPEED && !type) GameOver();
			ch2speed += (type)?(INCREASE_SPEED_RATE):(-DECREASE_SPEED_RATE);
			break;
		case 3:
			if (ch3speed >= MAX_SPEED && type) return;
			if (ch3speed <= MIN_SPEED && !type) GameOver();
			ch3speed += (type)?(INCREASE_SPEED_RATE):(-DECREASE_SPEED_RATE);
			break;
		}
	}

	float ch1speed = 2f, ch2speed = 2f, ch3speed = 2f;

	public float GetSpeedForChannel(int channel) {
		switch (channel) {
		case 1:
			return ch1speed;
			break;
		case 2:
			return ch2speed;
			break;
		case 3:
			return ch3speed;
			break;
		}
		return 0f;
	}

	float getChannelOneSpeed() {
		return ch1speed;
	}

	float getChannelTwoSpeed() {
		return ch2speed;
	}

	float getChannelThreeSpeed() {
		return ch3speed;
	}

	public const float CHANNEL_1_X_AXIS = -1.733552f;
	public const float CHANNEL_2_X_AXIS = -0.0325695f;
	public const float CHANNEL_3_X_AXIS = 1.475035f;
	public const float SQUARE_HEIGHT = 2.053983f;
	public const float INCREASE_DIFF_AT_SECONDS = 3;

	public const float Y_SQUARE_START_OFFSET = 10f;

	float wait = 0, wait2 = 0, wait3 = 0;
	int f1 = 0, f2 = 0, f3 = 0;
	int last = -1;
	int n = 3;
	int nrepeats = 0;
	float difficultCarry = 0f;

	public const int SINGLE_BRICK = 1;
	public const int DOUBLE_BRICK = 2;
	public const int TRIPLE_BRICK = 3;

	class SpawnStrategy {
		public int brickSize;
		public int brickColor;
		public float brickYOffset;
	}

	int lastColor = 0;

	void BuildBrickStrategy() {
		for (int x = 0; x < 3; x++) {
			for (int y = 199; y > 0; y--) {
				int brickSize = Random.Range(0,4);
				int currBrickSize = 0;
				if (y+1 < 200) {
					currBrickSize += flow[x, y+1].brickSize;
				}
				if (y+2 < 200) {
					currBrickSize += flow[x, y+2].brickSize;
				}
				if (y+3 < 200) {
					currBrickSize += flow[x, y+2].brickSize;
				}
				if (currBrickSize < 3) {
					brickSize = Random.Range(1,4-currBrickSize);
				} else {
					brickSize = 0;
				}

				SpawnStrategy currentStrategy = new SpawnStrategy();
				currentStrategy.brickSize = brickSize;
				if (brickSize == 2) {
					currentStrategy.brickYOffset = 0.5f;
				} else {
					currentStrategy.brickYOffset = 0f;
				}

				if (brickSize != 0) {
					do {
						currentStrategy.brickColor = Random.Range(1,4);
					} while (currentStrategy.brickColor == lastColor);
					lastColor = currentStrategy.brickColor;
				}


				flow[x, y] = currentStrategy;
			}
		}
	}

	SpawnStrategy[,] flow = new SpawnStrategy[3,200];
	void Start () {
		BuildBrickStrategy ();
		timeSeconds = 30;
		timeMiliSeconds = 60;
	}

	int xOffset1 = 199, xOffset2 = 199, xOffset3 = 199;

	float milisecondCarry = 0f; 

	// Update is called once per frame
	void Update () {
		milisecondCarry += Time.deltaTime;
		if (milisecondCarry > 1f/60f) {
			milisecondCarry = 0;
			timeMiliSeconds--;
		}
		if (timeMiliSeconds <= 0) {
			timeSeconds--;
			timeMiliSeconds = 60;
		}
		if (timeSeconds <= 0) {
			GameOver();
		}

		if (score != null) {
			score.text = timeSeconds + ":" + timeMiliSeconds;
		}
		if (Input.GetKeyDown(KeyCode.Escape) && isMenuScene) 
			Application.Quit();
		if (Input.GetKeyDown(KeyCode.Escape) && !isMenuScene) 
			Application.LoadLevel("MenuScene");
		if (paused)
			return;



		if (xOffset1 == 0 || xOffset2 == 0 || xOffset3 == 0) {
			BuildBrickStrategy();
			xOffset1 = 199;
			xOffset2 = 199;
			xOffset3 = 199;
		}

		difficultCarry += Time.deltaTime;
		if ((difficultCarry >= INCREASE_DIFF_AT_SECONDS) && !isMenuScene) {
			ChangeDifficulty(1, true);
			ChangeDifficulty(2, true);
			ChangeDifficulty(3, true);
			difficultCarry = 0;
		}

		if (wait <= 0f) {
			SpawnStrategy spwStr = flow[0, xOffset1--];
			if (spwStr.brickSize == 0) {
				wait = ((getHeightForBrick(1)) / getChannelOneSpeed());
			} else {
				wait = ((getHeightForBrick(spwStr.brickSize)) / getChannelOneSpeed());
			}
			if (spwStr.brickSize != 0) 
				RandomInstantiation(1,CHANNEL_1_X_AXIS,spwStr);	
		}
		wait -= Time.deltaTime * 1;	
		if (wait2 <= 0f) {
			SpawnStrategy spwStr = flow[1, xOffset2--];
			if (spwStr.brickSize == 0) {
				wait2 = ((getHeightForBrick(1)) / getChannelTwoSpeed());
			} else {
				wait2 = ((getHeightForBrick(spwStr.brickSize)) / getChannelTwoSpeed());
			}
			if (spwStr.brickSize != 0) 
				RandomInstantiation(2,CHANNEL_2_X_AXIS,spwStr);	
		}
		wait2 -= Time.deltaTime * 1;	
		if (wait3 <= 0f) {
			SpawnStrategy spwStr = flow[2, xOffset3--];
			if (spwStr.brickSize == 0) {
				wait3 = ((getHeightForBrick(1)) / getChannelThreeSpeed());
			} else {
				wait3 = ((getHeightForBrick(spwStr.brickSize)) / getChannelThreeSpeed());
			}
			if (spwStr.brickSize != 0) 
				RandomInstantiation(3,CHANNEL_3_X_AXIS,spwStr);	
		}
		wait3 -= Time.deltaTime * 1;	
	}

	float getHeightForBrick (int brickSize) {
		switch (brickSize) {
		case 1: 
			return redSquare.renderer.bounds.size.y;
			break;
		case 2: 
			return redSquare2.renderer.bounds.size.y;
			break;
		case 3: 
			return redSquare3.renderer.bounds.size.y;
			break;
		default:
			return 0f;
			Debug.Log("Fuck!");
			break;
		}
	}

	void RandomInstantiation(int channel, float x, SpawnStrategy spwStr) {
		RedSquare rs;
		int brickSize = spwStr.brickSize;
		switch (brickSize) {
		case 1: 
			rs = redSquare;
			break;
		case 2: 
			rs = redSquare2;
			break;
		case 3: 
			rs = redSquare3;
			break;
		default:
			rs = redSquare;
			Debug.Log("Fuck!");
			break;
		}
		rs.channelRunningIn = channel;
		rs.color = spwStr.brickColor;
		DeadZoneScript currentChannel = getPathFromChannel (channel);

		//If the color of the square is the same as the channel, spawn it dead.
		switch (rs.color) {
		case 1:
			rs.renderer.enabled = true;
			rs.blueSquare.renderer.enabled = false;
			rs.greenSquare.renderer.enabled = false;
			break;
		case 2:
			rs.renderer.enabled = false;
			rs.blueSquare.renderer.enabled = true;
			rs.greenSquare.renderer.enabled = false;
			break;
		case 3:
			rs.renderer.enabled = false;
			rs.blueSquare.renderer.enabled = false;
			rs.greenSquare.renderer.enabled = true;
			break;
		}
		rs.status = RedSquare.ALIVE;
		RedSquare copyOfRedSqr = Instantiate(rs, new Vector3(x,4.149223f + spwStr.brickYOffset,0f), Quaternion.identity) as RedSquare;
		brickBag.Add (copyOfRedSqr);
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
}
