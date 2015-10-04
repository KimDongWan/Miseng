#pragma strict

var animator : Animator;
var motion = 0;

function OnMouseDown () {
	if(motion == 3) {
		animator.SetBool("Die",true);
	}
	else {
		motion++;
		animator.SetInteger("Click", motion);
	}
}