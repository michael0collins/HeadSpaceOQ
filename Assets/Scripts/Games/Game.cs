using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour, IGame
{
    public GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    
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