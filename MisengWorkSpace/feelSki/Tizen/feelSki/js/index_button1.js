document.getElementById("button1").addEventListener("click",function(e){
  navigator.vibrate(1000);
  sendData("start");
  window.location.replace("readyScreen.html");
});
