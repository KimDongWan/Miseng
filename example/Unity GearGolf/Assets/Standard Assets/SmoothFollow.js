var target : Transform;
var flag : Transform;
var distance = 10.0;
var height = 5.0;
var heightDamping = 2.0;
var rotationDamping = 3.0;
static var hit_ball :boolean  = false;
// Place the script in the Camera-Control group in the component menu
@script AddComponentMenu("Camera-Control/Smooth Follow")
function move_camera(){
	// Early out if we don't have a target
	if (!target)
		return;
	
	// Calculate the current rotation angles
	var wantedRotationAngle = target.eulerAngles.y+90.0;
	if((target.position.x-flag.position.x)>0.0){
		wantedRotationAngle += 180.0;
		print("Over the Flag!!!");
	}
	var wantedHeight = target.position.y + height;
		
	var currentRotationAngle = transform.eulerAngles.y;
	var currentHeight = transform.position.y;
	
	// Damp the rotation around the y-axis
	currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

	// Damp the height
	currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);

	// Convert the angle into a rotation
	var currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
	
	// Set the position of the camera on the x-z plane to:
	// distance meters behind the target
	transform.position = target.position;
	transform.position -= currentRotation * Vector3.forward * distance;

	// Set the height of the camera
	transform.position.y = currentHeight;
	
	// Always look at the target
	transform.LookAt (target);
}

function LateUpdate () {
	if(hit_ball == true) move_camera();
	//if(Input.GetButtonDown("Jump"))	hit_ball = !hit_ball;
}