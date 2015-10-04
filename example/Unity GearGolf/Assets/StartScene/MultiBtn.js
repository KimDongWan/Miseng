#pragma strict

function OnMouseDown () {
	transform.GetChild(0).gameObject.SetActiveRecursively(false);
	transform.GetChild(1).gameObject.SetActiveRecursively(true);
}
function OnMouseUp () {
	transform.GetChild(1).gameObject.SetActiveRecursively(false);
	transform.GetChild(0).gameObject.SetActiveRecursively(true);
}