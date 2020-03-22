using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGameOnInteract : MonoBehaviour, IInteractable
{
    //TODO find the game manager and stop any other games, resetting them first.

    public GameObject game;

    public void OnInteracted()
    {
        game.SetActive(true);
    }
}
