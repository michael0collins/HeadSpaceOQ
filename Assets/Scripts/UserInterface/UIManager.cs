using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject inGame;
    public GameObject scoreScreen;

    private void OnLevelWasLoaded()
    {
        if(SceneManager.GetActiveScene().name == "ScoreScreen")
        {
            inGame.SetActive(false);
            scoreScreen.SetActive(true);
        }
    }
}
