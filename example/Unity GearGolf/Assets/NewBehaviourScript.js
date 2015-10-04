#pragma strict

var ball : GameObject;
var power = 500;
var speed = 5;
function Update () {
   var amtMove = speed * Time.deltaTime;
   var ver = Input.GetAxis("Vertical");
   var hor = Input.GetAxis("Horizontal");
   
   transform.Translate(Vector3.forward * ver * amtMove);
   transform.Translate(Vector3.right * hor * amtMove);
   
   if(Input.GetButtonDown("Fire1"))
      ball.rigidbody.AddForce(Vector3(0,1,1) * power);
}