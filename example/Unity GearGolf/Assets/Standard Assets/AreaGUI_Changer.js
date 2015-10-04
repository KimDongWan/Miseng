#pragma strict

static var currentArea = 0;

function Update() {
	AreaText(currentArea);
}

function AreaText(index : int) {
	for(var i = 0; i < 4; i++)
	{
		if(i == index)
			transform.GetChild(i).gameObject.SetActiveRecursively(true);
		else
			transform.GetChild(i).gameObject.SetActiveRecursively(false);
	}
}