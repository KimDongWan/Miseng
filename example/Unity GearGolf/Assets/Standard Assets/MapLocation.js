#pragma strict

var ball : Transform;

function Update () {
	var minimap_x;
	var minimap_y;
	
	minimap_x = (1052.51 - ball.position.z)/1053.92 - 0.02;
	minimap_y = (ball.position.x - 963.85)/1878.3 + 0.02;
	
	transform.localPosition.x = minimap_x;
	transform.localPosition.y = minimap_y;
}