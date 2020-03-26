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
        //Report win to GameManager.
    }

    public void OnLoss()
    {
        print("Loss");
        //Report loss to GameManager.
    }

    public void ReportScore()
    {
        
    }
}