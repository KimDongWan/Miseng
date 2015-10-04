using UnityEngine;
using System.Collections;
using System;

public class MissengPlugin : MonoBehaviour
{
    private AndroidJavaObject androidJavaObject;
    public object manager; /*°ÔÀÓÀ» ÃÑ°ýÇÏ´Â object = ClassType*/
    public bool is_called_startFlag = false;    
    private bool is_call_response = false;
    void Awake()
    {
        AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        this.androidJavaObject = ajc.GetStatic<AndroidJavaObject>("currentActivity");        
        is_called_startFlag = false;        
    }

    void Start()
    {        
    }

    void Update()
    {        
        this.androidJavaObject.Call("SendToGearMSG", "used");
    }

    public void Send_To_Gear_MSG(string strSend)
    {
        this.androidJavaObject.Call("RecieveFromUnityMSG", strSend);
    }
    public void recieve_Data(string msg)
    {
        switch (msg)
        {
            case "Message":
               //TODO                
                break;
            case "Response":
                //ToDO
                is_call_response = true;
                break;
            default:
                break;
        }
    }
    void OnGUI()
    {
        if (!is_call_response)
        {
            Send_To_Gear_MSG("GearMessage");
        }


    }
}
