#pragma strict

var ball : GameObject;
var main_frame : GameObject;
var target_camera : GameObject;
var hole_cup : Transform;
var club : GameObject;
var target_round : GUIText;
var target_score : GUIText;
var rotSpeed = 120;
var power = 1000;
var speed = 30;
//var break_flag = false;
var add_power = false;
var hspin_direction = "left";
var hspin_power = 0.5;
var vspin_direction = "top";
var vspin_power = 0.5;
var round = 1;
var score = -4;


//바운스, 바람,  클럽의 종류에 따른 샷, 클럽의 에니메이션, 


// 2014 - 08 - 14 (공의 스핀)
//회전은 들어가는데, 좌,우,센터에 대한 회전과 파워에 따른 스핀의 정도량, 공의 축이 흔들림에 따른 카메라무빙 떨림 보정.
//	=> 파워에 따른 스핀 정도량 해결!!!!!!
//  => 공의 축의 흔들립에 따른 카메라 무빙 해결함 (ball_stoker).
//	* 스핀에 따른 값이처리됨 (왼쪽, 오른쪽, 센터)
//	* 샷을 할 때 방향을 능동적으로 줘야됨.(축의 변화량에 따른)

//2014 - 08 - 16 (공의 스핀 +)
// 공의 방향에 따라서 left,right,front,back 스핀을 전부 세세하게 줄수 있음.

//2014 - 08 - 18 (카메라의 이동 , 바람)
// 카메라의 화면을 키보드 방향으로 움직이고 샷 터치시 볼이 나감. (이에 따른 카메라 이동도 마침)
//
function OnGUI () {	
	if(GUI.Button(new Rect(300,350,300,50), "Shot!")){
		SmoothFollow.hit_ball = true;		
		AddShoot();
	}
}

function AddShoot(){
		rigidbody.maxAngularVelocity = 100;
		
		//공의 축을 초기화
		ball.transform.rotation.x = 0.0;
		ball.transform.rotation.y = 0.0;
		ball.transform.rotation.z = 0.0;

		var y1 = main_frame.transform.rotation.eulerAngles.y -90.0;	
		print("y1 : " + y1);
		var theta = (Mathf.PI/180)* y1;

		print("shoot x : " + Mathf.Cos(theta));
		//print("shoot y : " + 1);
		print("shoot z : " + Mathf.Sin(theta));
		
		//화면이 보여지는 부분으로 공을 쏨.
		
		ball.rigidbody.AddForce(Vector3(Mathf.Cos(theta),1,-1*Mathf.Sin(theta)) * power);
		print("theta : " + theta);
		print("Mathf.Cos(theta) : " + Mathf.Cos(theta));
		print("Mathf.Sin(theta) : " + Mathf.Sin(theta));
		//공의 스핀
		var rot_v = (power)/100;
		var torqueSpeed = Mathf.Pow(2,rot_v);
		var hspin_vector = Vector3(Mathf.Cos(theta),0.0,Mathf.Sin(theta)*(-1));//left : *1 , right : *(-1)
		var vspin_vector = Vector3(Mathf.Cos(theta-(Mathf.PI/2))*(-1),0.0,Mathf.Sin(theta-(Mathf.PI/2))); // top: *1 , back : *(-1)
		
		if(hspin_direction.Equals("right")) hspin_vector * (-1);
		if(vspin_direction.Equals("bottom")) vspin_vector * (-1);
		
		ball.rigidbody.AddTorque(	((hspin_vector * hspin_power) + (vspin_vector * vspin_power))	* torqueSpeed);
}

function Update () {	
	var ang = Input.GetAxis("Horizontal");	
	if( ang !=0) print("ang : " + ang);
	
	if(ball.rigidbody.velocity.magnitude>0.5){
		add_power = true;
	}
	
	if(ball.rigidbody.velocity.magnitude < 0.3 && add_power){
		print("break Ok");
		add_power = false;
		
		rigidbody.maxAngularVelocity=0;
		
		
		
		
		SmoothFollow.hit_ball = false;
		SetCamera();
		round++;
		score++;
		target_round.guiText.text = round.ToString();
		target_score.guiText.text = score.ToString();
		
		
	}
}

function SetCamera(){
	print("SetCamera()");
	
	
	//camera의 위치 잡기
	target_camera.transform.position.x = (ball.transform.position.x - 15.0);
	target_camera.transform.position.y = (ball.transform.position.y + 8.0);
	target_camera.transform.position.z = (ball.transform.position.z);
	
	//club의 위치 잡기
	club.transform.position.x = (ball.transform.position.x-2.0);
	club.transform.position.y = (ball.transform.position.y + 14.0);
	club.transform.position.z = (ball.transform.position.z + 6.0);
	
	//camera의 앵글 잡기
	target_camera.transform.LookAt(hole_cup);
	
	
	
	print("x : " + target_camera.transform.localRotation.x);
	print("y : " + target_camera.transform.localRotation.y);
	print("z : " +target_camera.transform.localRotation.z);
}
