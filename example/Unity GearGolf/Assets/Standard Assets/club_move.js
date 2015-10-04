#pragma strict

var club : GameObject;
var power = 500;
var r = 1;
static var delta_x : float = 0.2;
var fireRate : float = 0.1;
private var nextFire : float = 0.0;

function Update () {
	if (Input.GetButton("Fire1") && Time.time > nextFire) {
	
			nextFire = Time.time + fireRate;
			club.transform.Rotate(Vector3(0.0,0.0,-delta_x));

	}
	
	if (Input.GetButton("Fire2") && Time.time > nextFire) {
	
			var x=1;
			
			x*= delta_x;
			nextFire = Time.time + fireRate;
			club.transform.Rotate(Vector3(0.0,0.0,x));

	}
}

