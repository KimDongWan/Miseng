#pragma strict
var ball : GameObject;
var ball_capsule : GameObject;

function Update () {
	ball_capsule.transform.position=ball.transform.position;
}