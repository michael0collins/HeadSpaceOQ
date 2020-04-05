using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenDelete : MonoBehaviour
{
    public Canvas endCanvas;
    public int gamesComplete = 0;

    public void AddGame()
    {
        gamesComplete++;
        if(gamesComplete == 3)
        {
            endCanvas.enabled = true;
        }
    }
}
