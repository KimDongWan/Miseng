#pragma strict

var ball : GameObject;
var exit_gui : GUITexture;
var hole_in_sound : AudioSource;
//ball
//x:226
//y:40.5
//z:946
function  OnTriggerEnter(){
	if(ball.rigidbody.velocity.magnitude<20.0){
		hole_in_sound.Play();
	
		ball.rigidbody.maxAngularVelocity = 0;
		ball.rigidbody.Sleep();
				
		ball.transform.position.y -= 0.9;
		ball.transform.position.x = transform.position.x;
		ball.transform.position.z = transform.position.z;		
		//ball.rigidbody.useGravity=false;
		
		exit_gui.gameObject.SetActiveRecursively(true);
	}
}