#pragma strict

var hole : Transform;

function Update () {
	var minimap_x;
	var minimap_y;
	
	minimap_x = (1052.51 - hole.position.z)/1053.92;
	minimap_y = (hole.position.x - 963.85)/1878.3 + 0.03;
	
	transform.localPosition.x = minimap_x;
	transform.localPosition.y = minimap_y;
}