#pragma strict

var target : GUIText;

function OnMouseDown() {
	var pos = Input.mousePosition;
	target.guiText.text = pos.x.ToString();
}