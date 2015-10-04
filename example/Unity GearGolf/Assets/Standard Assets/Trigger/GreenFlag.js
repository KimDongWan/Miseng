#pragma strict

var flag : GameObject;

function  OnTriggerEnter(){
	flag.gameObject.SetActiveRecursively(false);
}
function  OnTriggerExit(){
	flag.gameObject.SetActiveRecursively(true);
}