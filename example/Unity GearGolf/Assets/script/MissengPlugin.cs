using UnityEngine;
using System.Collections;
using System;

public class MissengPlugin : MonoBehaviour{
	public GameObject Club;
	public GameObject Ball;
	public GUITexture SpinPoint_GUI;
	private string[] words;
	public bool is_called_startFlag = false;    
	private bool is_call_response = false;

	private AndroidJavaObject androidJavaObject;
	//public GameObject dynamic_ball;


	void Awake () {
		AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		this.androidJavaObject = ajc.GetStatic<AndroidJavaObject>("currentActivity");
		is_called_startFlag = false;
	}
	
	void Start() {

	}
	
	void Update () {
		this.androidJavaObject.Call("SendToGearMSG", "used");
	}
	public void Send_To_Gear_MSG(string strSend)
	{
		this.androidJavaObject.Call("RecieveFromUnityMSG", strSend);
	}
	public void recieve_Data(string msg)
	{



			//dynamic_shoot_ball.power *= 11;

			//dynamic_shoot_ball.hspin_power = 0;
			/*if (this.words[2].ToString() == "1") {
				dynamic_shoot_ball.hspin_power = -0.3f;
			}
			else if (this.words[2].ToString() == "2") {
				dynamic_shoot_ball.hspin_power = 0;
			}
			else if (this.words[2].ToString() == "3"){
				dynamic_shoot_ball.hspin_power = 0.3f;
			}*/
		if (msg == "swing") {
			dynamic_shoot_ball.is_shot_ok = false;
			//dynamic_shoot_ball.AddShoot();
		}
		if (msg == "ready") {

		}
		if (msg == "swing") {
			SmoothFollow.hit_ball = true;
		}


	}
	
	void OnGUI(){

	}
}
