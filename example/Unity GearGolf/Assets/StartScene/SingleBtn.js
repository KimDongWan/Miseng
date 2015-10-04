#pragma strict

var time : float = -10;

function Update () {
	if(time > 0) {
		time -= Time.deltaTime;
		transform.position.x += 0.02;
	}
	else if(time <= 0 && time > -5)
	{
		Application.LoadLevel("asset");
	}
}
function OnMouseDown () {
	transform.GetChild(0).gameObject.SetActiveRecursively(false);
	transform.GetChild(1).gameObject.SetActiveRecursively(true);
}
function OnMouseUp () {
	time = 1;
}