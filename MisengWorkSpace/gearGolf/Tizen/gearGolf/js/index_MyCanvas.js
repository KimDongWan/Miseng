 window.addEventListener("devicemotion", function(e) {
	ax = e.accelerationIncludingGravity.x;
	ay = -e.accelerationIncludingGravity.y;
	az = -e.accelerationIncludingGravity.z;
	list_ax.push(ax);
	list_ay.push(ay);
	list_az.push(az);
	temp_ax = ax;
	temp_ay = ay;
	temp_az = az;
	if(list_ax.length>50){ list_ax = new Array(); list_ay= new Array(); list_az= new Array();}
	  if (is_golfshot()
  ) {
    navigator.vibrate(3000);
    sendData("swing");
  }

});
