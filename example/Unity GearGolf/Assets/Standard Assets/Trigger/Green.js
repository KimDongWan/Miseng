#pragma strict

var ball : GameObject;
var green_material : PhysicMaterial;
var lough_material : PhysicMaterial;
static var is_green = false;

function  OnTriggerEnter(){
	ball.collider.material = green_material;
	is_green = true;
	AreaGUI_Changer.currentArea = 2;
}
function  OnTriggerExit(){
	ball.collider.material = lough_material;
	is_green = false;
	AreaGUI_Changer.currentArea = 0;
}