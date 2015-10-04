#pragma strict
public var sensitivityX = 30.0;
public var minimumX = -360.0;
public var maximumX = 360.0 ;

public var clubs : Transform;
public var ball : Transform;
private var e_Speed = 45.0;
static var theta = 0.0;
static var club_theta = 0.0;
static var rot = 0.0;


function Start () {
	if (rigidbody)
			rigidbody.freezeRotation = true;
			
	theta = Mathf.PI;
}

function Update () {
	//var r = Vector2.Distance(Vector2(transform.position.x,transform.position.z),Vector2(ball.position.x,ball.position.z));
	var r = 16.1727;
	//r : camera_to_ball_Distance
			
	//camera 이동 + 클럽의 회전
	if(!SmoothFollow.hit_ball){
	if(Input.GetKey(KeyCode.LeftArrow)){
		theta += (Mathf.PI/180)*e_Speed*Time.deltaTime;
		rot = (-1)*e_Speed*Time.deltaTime;
	}
	if(Input.GetKey(KeyCode.RightArrow)){
		theta -= (Mathf.PI/180)*e_Speed*Time.deltaTime;				
		rot = e_Speed*Time.deltaTime;
	}
	
	clubs.Rotate(Vector3(0.0,rot,0.0));
	transform.position.x = (r*(Mathf.Cos(theta))+ball.position.x);
	transform.position.y = ball.position.y+16.0;
	transform.position.z = (r*Mathf.Sin(theta)+ball.position.z);
	transform.LookAt(Vector3(ball.position.x,ball.position.y+8,ball.position.z));
	clubs.transform.eulerAngles.y=transform.eulerAngles.y-90.0;
	}
	arrow.rotation_angle=(360.0-wind.wind_direction)+(90.0-transform.eulerAngles.y);
}