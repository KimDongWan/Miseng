$(document).ready(function(e){
	$("body").on("newMessageFromUnity",function(event){
		if(RecieveData=='ready'){
		  sendData("responseReady");
  navigator.vibrate(3000);
  window.location.replace("mainGame.html");
}
});});