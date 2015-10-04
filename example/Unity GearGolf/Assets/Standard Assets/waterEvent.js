#pragma strict

var ball : GameObject;
var falling_water_sound : AudioSource;

function  OnTriggerEnter(){
	falling_water_sound.Play();
	OB_Show.time = 2.0;
	dynamic_shoot_ball.score++;
	
	ball.transform.position = Vector3(dynamic_shoot_ball.ball_x,dynamic_shoot_ball.ball_y,dynamic_shoot_ball.ball_z);
	
	ball.rigidbody.velocity.x=0.0;
	ball.rigidbody.velocity.y=0.0;
	ball.rigidbody.velocity.z=0.0;
	
	ball.rigidbody.maxAngularVelocity = 0;
}