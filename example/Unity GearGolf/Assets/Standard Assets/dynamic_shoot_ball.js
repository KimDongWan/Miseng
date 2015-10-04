#pragma strict

public var ball : GameObject;
public var shot_direction_view : Transform;
public var target_camera : GameObject;
public var hole_cup : Transform;
public var club : GameObject;
static var power : float = 300;
public var wind_text : GUIText;
public var swing_sound : AudioSource;
public var putting_sound : AudioSource;
public static var is_shot_ok = true;
wind_text.fontSize = 20;

public var target_round : GUIText;
public var target_score : GUIText;

//ball의 스핀
//public var hspin_direction = "left";
static var hspin_power = 0.0;
public var vspin_direction = "top";
public var vspin_power = 0.01;

public static var round = 1;
public static var score = -4;
private var add_power = false;

public static var ball_x=0.0;
public static var ball_y=0.0;
public static var ball_z=0.0;

public var Ball_GUI : GUITexture;
public var SpingPoint_GUI : GUITexture;
public var ClubSelect_GUI : GUITexture;
public var ScoreBoard_GUI : GUITexture;
public var MiniMap_GUI : GUITexture;
public var Area_GUI : GUITexture;

public static var shot = 0;

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

//2014 - 08 - 20 
// 지형에 따른 마찰 okok But, 그린, 페어, 벙커가 평평해야됨.
function OnGUI () {
	if(Input.GetKey(KeyCode.Space)){
		print("space called");
		SmoothFollow.hit_ball = true;			
	}
	//if(GUI.Button(new Rect(Screen.width/3, Screen.height*5/6, Screen.width/3, Screen.height/10), "Shot!")){
	//if(GUI.Button(new Rect(520,610,300,50), "Shot!")){
		//SmoothFollow.hit_ball = true;		
		//AddShoot();
	//}
}

public function AddShoot(){
	is_shot_ok = true;
	if(shot == 0) {
		if(ClubChanger.currentNum!=4)	swing_sound.Play();
		else							putting_sound.Play();
		
		Time.timeScale = 2;
		shot = 1;
		Ball_GUI.transform.GetChild(0).gameObject.SetActiveRecursively(true);
		SpingPoint_GUI.transform.localPosition.x = hspin_power*0.8;
		ClubSelect_GUI.gameObject.SetActiveRecursively(false);
		ScoreBoard_GUI.gameObject.SetActiveRecursively(false);
		MiniMap_GUI.gameObject.SetActiveRecursively(false);
		club.gameObject.SetActiveRecursively(false);
		Area_GUI.gameObject.SetActiveRecursively(false);

		rigidbody.maxAngularVelocity = 100;
		
		ball_x = ball.transform.position.x;
		ball_y = ball.transform.position.y;
		ball_z = ball.transform.position.z;
		
		//공의 축을 초기화
		ball.transform.rotation.x = 0.0;
		ball.transform.rotation.y = 0.0;
		ball.transform.rotation.z = 0.0;

		var y1 = shot_direction_view.transform.rotation.eulerAngles.y -90.0;	
		
		var theta = (Mathf.PI/180)* y1;

		//화면이 보여지는 부분으로 공을 쏨.
		
		//ball.rigidbody.AddForce(Vector3(Mathf.Cos(theta),1,-1*Mathf.Sin(theta)) * power);
		ball.rigidbody.AddForce(Vector3(Mathf.Cos(theta),(Mathf.PI/180)*ClubChanger.ang,-1*Mathf.Sin(theta)) * power * ClubChanger.power);
		
		//공의 스핀
		var rot_v = (power)/10;
		var torqueSpeed = Mathf.Pow(2,rot_v);
		var hspin_vector = Vector3(Mathf.Cos(theta),0.0,Mathf.Sin(theta)*(-1));//left : *1 , right : *(-1)
		var vspin_vector = Vector3(Mathf.Cos(theta-(Mathf.PI/2))*(-1),0.0,Mathf.Sin(theta-(Mathf.PI/2))); // top: *1 , back : *(-1)
		
		//if(hspin_direction=='right') hspin_vector *= (-1);
		//if(vspin_direction=='bottom') vspin_vector *= (-1);
		hspin_vector *= (-1);
		
		ball.rigidbody.AddTorque(	((hspin_vector * hspin_power) + (vspin_vector * vspin_power)) * torqueSpeed);
	}
}

function if_lower_velocity_then_breakStart(){
	is_start_move();
	if(ball.rigidbody.velocity.magnitude < 0.3 && add_power){
		add_power= false;
		rigidbody.maxAngularVelocity = 0;
		return true;
	}
	return false;
}
function is_start_move(){
	if(ball.rigidbody.velocity.magnitude>0.5)	add_power = true;	
}

function Update () {
	if(!is_shot_ok){
		AddShoot();
		is_shot_ok = true;
	}
	
	if(SmoothFollow.hit_ball)	
		AddShoot();
	if(ball.rigidbody.velocity.magnitude < 0.8 && add_power){
		print("upupupup!!");
		Time.timeScale = 6;
	}
	if(if_lower_velocity_then_breakStart()){
		shot = 0;
		Time.timeScale = 1;
		Ball_GUI.transform.GetChild(0).gameObject.SetActiveRecursively(false);
		ClubSelect_GUI.gameObject.SetActiveRecursively(true);
		ScoreBoard_GUI.gameObject.SetActiveRecursively(true);
		MiniMap_GUI.gameObject.SetActiveRecursively(true);
		club.gameObject.SetActiveRecursively(true);
		Area_GUI.gameObject.SetActiveRecursively(true);
		
		print("break Ok");
		
		SetCamera();
		wind.wind_direction = Random.Range(0.0,360.0);
		wind.wind_power = Random.Range(0.0,1.4);
		wind_text.guiText.text = Mathf.Round(wind.wind_power * 100)/10.0 + 'm/s';
		
		SmoothFollow.hit_ball = false;
		
		round++;
		score++;
		target_round.guiText.text = round.ToString();
		target_score.guiText.text = score.ToString();
		
		var dist_ball_hole = Vector2.Distance(Vector2(ball.transform.position.x,ball.transform.position.z),Vector2(hole_cup.position.x,hole_cup.position.z));
		if(!Green.is_green && !Bunker.is_bunker){
         	if(dist_ball_hole > 550.0)		ClubChanger.currentNum = 0;
         	else if(dist_ball_hole > 250)	ClubChanger.currentNum = 1;
         	else		                    ClubChanger.currentNum = 2;
      	} else{
        	if(Bunker.is_bunker)         	ClubChanger.currentNum = 3;
         	if(Green.is_green)            	ClubChanger.currentNum = 4;
      	}
	}
	
	if(!wind.is_ball_onair && ball.transform.position.y >60.0){
		print("on air : true");
		wind.is_ball_onair = true;
	}
}

function SetCamera(){
	print("SetCamera()");
			
	//club의 위치 잡기
	club.transform.position.x = ball.transform.position.x;
	club.transform.position.y = ball.transform.position.y;
	club.transform.position.z = ball.transform.position.z;
	
	var rot_theta;
	
	if((ball.transform.position.x-hole_cup.position.x)>0.0){
		print("z>0");
		rot_theta =  Mathf.Atan((ball.transform.position.z-hole_cup.position.z)/(ball.transform.position.x-hole_cup.position.x));
	}
	else{
		print("z<0");
		rot_theta =  Mathf.Atan((ball.transform.position.z-hole_cup.position.z)/(ball.transform.position.x-hole_cup.position.x))+Mathf.PI;
	}
	//var rot_theta = Mathf.Atan((hole_cup.position.z-ball.transform.position.z)/(hole_cup.position.x-ball.transform.position.x))+Mathf.PI;
	//var l = Vector2.Distance(Vector2(hole_cup.position.z,hole_cup.transform.position.x),Vector2(ball.transform.position.z,ball.transform.position.x));
	//var rot_theta = Mathf.Acos(l/(ball.transform.position.x-hole_cup.position.x))+Mathf.PI;
	Look.theta = rot_theta;
}

function OnCollisionEnter(coll:Collision){
	print("공이 충돌!!");
	wind.is_ball_onair = false;
	print("on air : false");
}

