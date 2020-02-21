using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugConsole : MonoBehaviour
{
    public static string DebugMessage = "";

    void OnGUI ()
    {
        GUI.Box(new Rect(Screen.width/2-250,10,500,100), DebugMessage);
    }
}
