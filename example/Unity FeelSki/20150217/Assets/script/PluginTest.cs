using UnityEngine;
using System.Collections;
using System;

public class PluginTest : MonoBehaviour {
	private AndroidJavaObject androidJavaObject;
	private string[] words;
	public string recieve_from_Gear_MSG;
	public GameManager manager;
	public bool is_called_startFlag = false;   
	private int cnt = 10;
	private bool is_call_response = false;
	void Awake () {
		AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		this.androidJavaObject = ajc.GetStatic<AndroidJavaObject>("currentActivity");
        is_called_startFlag = false;
        recieve_from_Gear_MSG = "";		
		manager.is_gameOver = false;
	}
	
	void Start() {
		manager.is_gameOver = false;
	}
	
	void Update () {
		// ToJava : unity에서 호출할 java 함수
		this.androidJavaObject.Call("SendToGearMSG", "used");   
	}
	
	public void Send_To_Gear_MSG(string strSend)
	{
		this.androidJavaObject.Call("RecieveFromUnityMSG", strSend);
	}
	public void recieve_Data(string msg)
	{
		this.recieve_from_Gear_MSG = msg;

		switch (msg) {
		case "left":
			manager.EventGet (KeyCode.LeftArrow);
			break;
		case "right":
			manager.EventGet (KeyCode.RightArrow);
			break;
		case "accel":
			manager.EventGet (KeyCode.DownArrow);
			break;
		case "break":
			manager.EventGet (KeyCode.UpArrow);
			break;
		case "jump":
			manager.EventGet (KeyCode.Space);
			break;
		case "start":
			manager.StartButtonEvent();
			break;
		case "resume":
			manager.GameResume ();
			break;
		case "quit":
			manager.Quit ();
			break;
		case "pause":
			manager.GamePause ();
			break;
		case "responseReady" :
			is_call_response = true;
			break;
		case "responseInit":
			is_call_response = true;
			break;
		default:
			break;
		}
	} 
	void OnGUI(){      
		if (manager.gameStartFlag && !is_call_response)
		{		
			Send_To_Gear_MSG("ready");
		}
		if (manager.is_gameOver && !is_call_response) {
			Send_To_Gear_MSG ("init");
			manager.gameStartFlag = false;
			manager.is_gameOver = false;
		}


	}
}
