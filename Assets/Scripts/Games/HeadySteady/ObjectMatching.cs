using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMatching : MonoBehaviour
{
    public AudioClip instructions1;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(Game());
    }

    private IEnumerator Game()
    {
        int numOfGames = gameManager.objectTrackingTestGameDatas.Count;

        for(int i = 0; i < numOfGames; i++)
        {
            
        }

        yield return null;
    }
}
