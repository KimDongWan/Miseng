#pragma strict

public var BGTexture : Texture;
public var Minimap : GUITexture;
public var shot_direction_view : Transform;
//var a=9.0;
private var rotation_angle = 0.0;
function OnGUI ()
{
  var position_x = Screen.width;
  var position_y = Screen.height;
  
  var club_value = ClubChanger.currentNum;
  var arrow_height = 0.0;
  
  switch(club_value){
  	case 0 : arrow_height = 7.0;	break;
  	case 1 : arrow_height = 10.0;	break;
  	case 2 : arrow_height = 20.0;	break;
  	case 3 : arrow_height = 40.0;	break;
  	default : arrow_height = 10000.0;	break;
  }
  
  var texture_width_size = Screen.width/arrow_height;
  var texture_height_size = Screen.height/arrow_height;
  
  var ball_r = (Screen.width * ( Minimap.transform.localScale.x * transform.localScale.x ))/2;
  var ball_position_x = (Screen.width * transform.position.x) + ball_r;
  var ball_position_y = (Screen.height - Screen.height * transform.position.y)-(ball_r*2) - texture_height_size;
  
  var ball_center_position_x = (Screen.width * transform.position.x)-ball_r;
  var ball_center_position_y = (Screen.height - Screen.height * transform.position.y);
 
  rotation_angle = (shot_direction_view.rotation.eulerAngles.y -90.0);
  
 
    
  GUIUtility.RotateAroundPivot (rotation_angle-4.0, Vector2(ball_center_position_x,ball_center_position_y));
  GUI.DrawTexture(Rect(ball_position_x-(texture_width_size/2.1), ball_position_y + (texture_height_size/6), texture_width_size, texture_height_size), BGTexture);
}