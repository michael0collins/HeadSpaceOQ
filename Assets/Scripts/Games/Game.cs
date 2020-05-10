using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour, IGame
{
    public void OnStart()
    {

    }
    
    public void OnWin()
    {
        print("Win");
    }

    public void OnLoss()
    {
        print("Loss");
    }

    public void ReportScore()
    {
        
    }
}