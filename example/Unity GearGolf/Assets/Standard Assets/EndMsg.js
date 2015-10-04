#pragma strict

var ball : GameObject;
var club : GameObject;
var Flag : GameObject;
public var target_round : GUIText;
public var target_score : GUIText;
public var standard : GUISkin;

function OnGUI () {
	GUI.skin = standard;
	if(GUI.Button(new Rect(Screen.width/3.3, Screen.height/1.9,Screen.width/6, Screen.height/7), "Yes")){
		gameObject.SetActiveRecursively(false);
		Flag.gameObject.SetActiveRecursively(true);
		
		Look.theta = 3.141592;
		Look.club_theta = 0.0;
		Look.rot = 0.0;
		
		ball.transform.position.x = 226;
		ball.transform.position.y = 35.47359;
		ball.transform.position.z = 946;
		
		dynamic_shoot_ball.round = 1;
		dynamic_shoot_ball.score = -4;
		target_round.guiText.text = dynamic_shoot_ball.round.ToString();
		target_score.guiText.text = dynamic_shoot_ball.score.ToString();
		
		club.transform.position.x = 225.9;
		club.transform.position.y = 35.64635;
		club.transform.position.z = 946;
		
		club.transform.rotation.x = 0;
		club.transform.rotation.y = 0;
		club.transform.rotation.z = 0;
		
		Green.is_green = false;
		ClubChanger.currentNum = 0;
	}
	if(GUI.Button(new Rect(Screen.width/1.9, Screen.height/1.9,Screen.width/6, Screen.height/7), "Quit")){
		Application.Quit();
	}
}