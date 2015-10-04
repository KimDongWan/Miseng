#pragma strict

var ball : GameObject;
var bunker_material : PhysicMaterial;
var lough_material : PhysicMaterial;
var bunker_particle : Transform;
static var is_bunker = false;

function  OnTriggerEnter(coll:Collider){
	var clone = Instantiate(bunker_particle,coll.transform.position,Quaternion.identity);
	Destroy(clone.transform.gameObject,2.0);
	ball.collider.material = bunker_material;
	ball.rigidbody.velocity.x=0;
	ball.rigidbody.velocity.z=0;
	ball.rigidbody.maxAngularVelocity = 0;
	is_bunker=true;
	AreaGUI_Changer.currentArea = 3;
}
function  OnTriggerExit(){
	ball.collider.material = lough_material;
	is_bunker=false;
	AreaGUI_Changer.currentArea = 1;
}