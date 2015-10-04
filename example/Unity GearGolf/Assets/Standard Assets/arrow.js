#pragma strict

public var BGTexture : Texture;
static var rotation_angle = 0.0;
function OnGUI ()
{
  var position_x = Screen.width;
  var position_y = Screen.height;
  var texture_width_size = Screen.width/15;
  var texture_height_size = Screen.height/10;
  
  position_x -= texture_width_size*2.148;
  position_y -= texture_height_size*2.64;
  
  GUIUtility.RotateAroundPivot (rotation_angle, Vector2(position_x+(texture_width_size/2), position_y+(texture_height_size/2)));
  GUI.DrawTexture(Rect(position_x, position_y, texture_width_size, texture_height_size), BGTexture);
}