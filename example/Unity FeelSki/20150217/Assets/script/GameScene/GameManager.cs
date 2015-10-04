using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject GameLayOut;
	GameObject startPanel;
	public bool gameStartFlag=false;
	private static GameManager gm;
	public ArrayList tweenscript;
	public bool is_gamePause = false;
	public bool is_gameOver =false;
	public static GameManager Instanace{
		get{
			return gm;
		}
	}

	// Use this for initialization
	void Start () {
		GameLayOut = this.gameObject;

		is_gameOver = false;
		is_gamePause = false;
		gm = this;
		tweenscript = new ArrayList ();
	}


	public void gamescene(){	
		
		timerLabel = GameLayOut.transform.GetChild (0).GetComponentInChildren<UILabel> ();
		startPanel=GameLayOut.transform.GetChild (2).gameObject;
		gameObjects = GameLayOut.transform.GetChild (3).gameObject;

		gameTimer = 0;
		Invoke ("FlashStart", 0.5f);

		return;
	}

	private void FlashStart(){
		int i;

		bool flag = startPanel.activeSelf;

		if (flag) {
			i=(++GameLayOut.GetComponentInChildren<GameStartPanel> ().i);
			if(i>3){
				Destroy(GameLayOut.GetComponentInChildren<GameStartPanel>().gameObject);
				startPanel.SetActive(false);
				gameTimer=0;
				timerStart();
				gameStart();
				return;
			}
			if(i==1)
			{
				Invoke ("initGame", 2f);
			}
			startPanel.SetActive(false);
			Invoke ("FlashStart", 1f);

		} else {
			Invoke ("FlashStart", 0.8f);
			startPanel.SetActive(true);
			return;
		}

	}


	GameObject gameObjects;
	private void initGame(){
		gameObjects.SetActive (true);
	}

	
	public int gameTimer;
	UILabel timerLabel;
	void timerStart(){
		InvokeRepeating("timer",1f,1f);
	}
	void timerStop(){
		CancelInvoke ("timer");
	}
	void timer(){
		gameTimer++;
		timerLabel.text =gameTimer.ToString();
	}


	//real game start point


	public void gameStart(){
		GameObject treeMaker = gameObjects.transform.GetChild (1).gameObject;
		treeMaker.gameObject.SetActive (true);
		treeMaker.GetComponent<TreeMaker> ().treeMakerStart ();

		GameObject flagMaker = gameObjects.transform.GetChild (2).gameObject;
		flagMaker.gameObject.SetActive (true);
		flagMaker.GetComponent<FlagMaker> ().flagMakerStart ();
		PauseButton.SetActive (true);
		gameStartFlag = true;
	}

	public BayMax baymax;
	

		public void EventGet(KeyCode k){
		
		switch(k)
		{
		case KeyCode.LeftArrow:
			baymax.MoveLeft();
			break;
		case KeyCode.RightArrow:
			baymax.MoveRight();
			break;
		case KeyCode.DownArrow:
			ChangeDuration(KeyCode.DownArrow);
			break;
		case KeyCode.UpArrow:
			ChangeDuration(KeyCode.UpArrow);
			break;
			
		case KeyCode.Space:
			baymax.Jump();
			break;
		default:
			break;
		}


	}

	public float mainDuration=10;
	public void ChangeDuration(KeyCode code){
		for (int i=0; i<tweenscript.Count; i++) {
			if(tweenscript[i]==null)
			{
				tweenscript.RemoveAt(i);
			}
		}
		switch(code)
		{
		case KeyCode.UpArrow:
			if(mainDuration>13)return;
			mainDuration+=0.1f;
			for (int i=0; i<tweenscript.Count; i++) {
				UITweener tw= tweenscript[i] as UITweener;
				
				tw.duration=tw.duration+0.1f;
			}
			break;
		case KeyCode.DownArrow:
			if(mainDuration<7)return;
			mainDuration-=0.1f;
			for (int i=0; i<tweenscript.Count; i++) {
				UITweener tw= tweenscript[i] as UITweener;
				tw.duration=tw.duration-0.1f;
			}
			break;
		}

	}
	public GameObject PauseButton;
	public GameObject PauseMenu;

	public void GamePause(){
		if (!gameStartFlag)
			return;
		is_gamePause = true;
		PauseMenu.SetActive (true);
		gameObjects.SetActive (false);
		timerStop ();
		PauseButton.SetActive (false);
	}
	public void GameResume(){
		if (!gameStartFlag)
			return;
		is_gamePause = false;
		PauseMenu.SetActive (false);
		gameObjects.SetActive (true);
		timerStart ();
		PauseButton.SetActive (true);
	}
	public void Quit(){
		Application.Quit ();
	}

	public GameObject VictoryObjects;
	public void GameWin(){
		PauseButton.SetActive (false);
		is_gameOver = true;
		PauseMenu.SetActive (false);
		gameObjects.SetActive (false);
		VictoryObjects.SetActive (true);
	}
	public void Restart(){
		Application.LoadLevel ("startScene");
	}
	void Update (){
		if (gameTimer >= 100)
			GameWin ();
	}

	
	public void StartButtonEvent(){
		this.GetComponent<TweenPosition> ().Toggle ();
		transform.parent.GetChild (1).GetComponent<TweenPosition> ().Toggle ();
	}
}
