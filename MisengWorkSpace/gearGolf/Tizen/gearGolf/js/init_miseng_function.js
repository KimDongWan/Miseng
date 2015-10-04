var mouse_X = 0.0;
var mouse_Y = 0.0;

var ax,ay,az;
var list_ax = new Array();
var list_ay = new Array();
var list_az = new Array();

var temp_az=0;
var temp_ax=0;
var temp_ay=0;

var up=0;
var temp_up=0;
var down = 0;
var temp_down=0;
var right = 0;
var temp_right=0;
var left = 0;
var temp_left=0;

var flag = 0;


window.onload = function(e) {
   document.getElementById("MyCanvas").addEventListener("click",
         function(e) {
            mouse_X = e.x;
            mouse_Y = e.y;
         });
}

function is_horizon(){
      
      
      if((temp_ax<3)&&(temp_ax>-3)&&(temp_ay<3)&&(temp_ay>-3))
      {
         return true;
      }
      else
      {
         return false;
      }
   }


   function is_boardjump(){

     if((list_az.length>15)&&(Math.max.apply(null,list_az)-Math.min.apply(null,list_az)>20))
     { 
      temp_az=Math.max.apply(null,list_az)-Math.min.apply(null,list_az);
      list_ax = new Array();
      list_ay = new Array();
      list_az = new Array();   
      return true;  
     }
     else
     {
      return false;
     }

   }

   function is_snowboard_turn_left(){
    

    if(temp_ax>3)
    {   
     return true;
    } 
    else
    {
     return false;
    }
   }


   function is_snowboard_turn_right(){
    
    if(temp_ax<-3)
    {   
     return true; 
    } 
    else
    {
     return false;
     
    }
   }


   function is_snowboard_accel(){
    
    if(temp_ay>3)
    {   
     return true;
    } 
    else
    {
     return false;
    }  
   }


   function is_snowboard_break(){
    
    if(temp_ay<-3)
    {   
     return true;
    } 
    else
    {
     return false;
     
    }
   }




   function is_rotate_counterclockwise(){
    
    if(temp_ay>3)
    {   
     return true;
    } 
    else
    {
     return false;
    }  
   }

   function is_rotate_clockwise(){
    
    if(temp_ay<-3)
    {   
     return true;
    } 
    else
    {
     return false; 
    }
   }



   function is_up_motion(){
    
    if((list_az.length>15)&&(temp_ax>5)&&(temp_az>-3))
    {
      
     temp_up = Math.max.apply(null,list_az)-Math.min.apply(null,list_az)+Math.max.apply(null,list_ax)-Math.min.apply(null,list_ax);
     if((temp_up>=up)||temp_up>=25){
     up = temp_up;
     }
     list_az = new Array();
     list_ax = new Array();
     list_ay = new Array();
     return true;
    }
    else
    {
     return false;
    }
   }


   function is_down_motion(){

    if((list_az.length>15)&&(temp_ax<-5)&&(temp_az<3))
    { 

     temp_down = Math.max.apply(null,list_az)-Math.min.apply(null,list_az)+Math.max.apply(null,list_ax)-Math.min.apply(null,list_ax);
     if((temp_down>=down)||temp_down>=25){
     down = temp_down;
     }
     list_az = new Array();
     list_ax = new Array();
     list_ay = new Array();
     return true;  
    }
    else
    {
     return false;
    }
   }



   function is_right_motion(){


      temp_right = Math.max.apply(null,list_ay)-Math.min.apply(null,list_ay);   
       if((list_ay.length>15)&&(temp_ay<-6)&&(temp_right>15))
       { 

        if((temp_right>=right)||temp_right>=15){
        right = temp_right;
        }
        list_az = new Array();
        list_ax = new Array();
        return true;  
       }
       else
       {
        return false;
       }
   }

   function is_left_motion(){

      temp_left = Math.max.apply(null,list_ay)-Math.min.apply(null,list_ay);   
       if((list_ay.length>15)&&(temp_ay>6)&&(temp_left>15))
       { 

        if((temp_left>=left)||temp_left>=15){
           left = temp_left;
        }
        list_az = new Array();
        list_ax = new Array();
        return true;  
       }
       else
       {
        return false;
       }
   }



   function is_golfshot(){
      
      
       if((temp_ax<-6)&&(temp_ay>5)&&(temp_az>-1)){
          
          if(flag==0){
              navigator.vibrate(1000);
              sendData("ready");
          }
       flag = 1;
       }
      if(flag==1){
         if((temp_ax>=7)&&((temp_ay>=-5)&&(temp_ay<=2))&&(temp_az>=1)&&(Math.max.apply(null,list_ax)-Math.min.apply(null,list_ax)>30))
         {
            list_ax = new Array();   
             flag = 0;
            return true;
         }
         else
         {
            return false;
         }
      }
      
      
   }