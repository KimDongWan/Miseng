#pragma strict

static var wind_direction = 90.0;
static var wind_power = 0.1;
public var ball : GameObject;
public var wind_text : GUIText;
static var is_ball_onair = false;

function Start () {
	wind_direction = Random.Range(0.0,360.0);
	wind_power = Random.Range(0.0,1.4);
	wind_text.guiText.text = Mathf.Round(wind_power * 100)/10.0+ 'm/s';
}

function Update () {
	
	if(is_ball_onair){
		var theta = (Mathf.PI/180)* wind_direction;
		var x = Mathf.Cos(theta);
		var z = Mathf.Sin(theta);
		ball.rigidbody.AddForce(Vector3(x,0.0,z) * wind_power);
	}
	
}