#pragma strict

static var time : float = -10;

function Update () {
	if(time > 0) {
		time -= Time.deltaTime;
		transform.GetChild(0).gameObject.SetActiveRecursively(true);
	}
	else if(time <= 0 && time > -5)
	{
		transform.GetChild(0).gameObject.SetActiveRecursively(false);
		time = -10;
	}
}