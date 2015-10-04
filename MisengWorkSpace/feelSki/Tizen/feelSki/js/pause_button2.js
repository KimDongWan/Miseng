document.getElementById("button2").addEventListener("click",function(e){
  navigator.vibrate(3000);
  sendData("quit");
  window.location.replace("index.html");
});
