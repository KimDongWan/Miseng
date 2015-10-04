#pragma strict

var ball : GameObject;
var fair_material : PhysicMaterial;
var lough_material : PhysicMaterial;
static var is_fairway = false;

function  OnTriggerEnter(){
	ball.collider.material = fair_material;
	is_fairway = true;
	AreaGUI_Changer.currentArea = 0;
}
function  OnTriggerExit(){
	ball.collider.material = lough_material;
	is_fairway = false;
	AreaGUI_Changer.currentArea = 1;
}