using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupContainer : MonoBehaviour
{
    private CupGame cupGame;

    private void Start()
    {
        cupGame = FindObjectOfType<CupGame>();
    }

    public void TriggerSpawnBall()
    {
        cupGame.SpawnBall();
    }

    public void TriggerDeleteBall()
    {
        cupGame.Deleteball();
    }

    public void TriggerSetSpeed()
    {
        cupGame.SetAnimSpeed();
    }

    public void TriggerAnimFinished()
    {
        cupGame.SetAnimFinished();
    }
}
