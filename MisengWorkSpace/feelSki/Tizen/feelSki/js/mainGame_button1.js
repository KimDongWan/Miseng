document.getElementById("button1").addEventListener("click",function(e){
  navigator.vibrate(1000);
  sendData("pause");
  window.location.replace("pause.html");
});
