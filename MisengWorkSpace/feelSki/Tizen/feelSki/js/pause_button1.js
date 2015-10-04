document.getElementById("button1").addEventListener("click",function(e){
  sendData("resume");
  navigator.vibrate(1000);
  window.location.replace("mainGame.html");
});
