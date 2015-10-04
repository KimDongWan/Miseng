#pragma strict

var club : GameObject;
var club_change_sound : AudioSource;

static var currentNum = 0;

static var ang = 0.0;
static var power = 0.0;

function Start() {
	SwitchClub(currentNum);
}

function Update() {
	if(Input.GetKeyDown(KeyCode.UpArrow)){
		currentNum++;
		club_change_sound.Play();
	}
	if(Input.GetKeyDown(KeyCode.DownArrow)){
		currentNum--;
		club_change_sound.Play();
	}
	if(currentNum > 4) currentNum -= 5;
	if(currentNum < 0) currentNum += 5;
	SwitchClub(currentNum);
}

function SwitchClub(index : int) {
	for(var i = 0; i < 5; i++)
	{
		if(i == index)
		{	
			switch(i){
				case 0 : ang = Driver.angle;	power = Driver.power;	break;
				case 1 : ang = Wood.angle;		power = Wood.power;		break;
				case 2 : ang = Iron.angle;		power = Iron.power;		break;
				case 3 : ang = S_W.angle;		power = S_W.power;		break;
				case 4 : ang = Putter.angle;	power = Putter.power;	break;
				default : ang = Driver.angle;	power = Driver.power;	break;
			}
					
			club.transform.GetChild(i).gameObject.SetActiveRecursively(true);
			transform.GetChild(i).gameObject.SetActiveRecursively(true);
		}
		else
		{
			club.transform.GetChild(i).gameObject.SetActiveRecursively(false);
			transform.GetChild(i).gameObject.SetActiveRecursively(false);
		}
	}
}