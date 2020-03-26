using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchingObject : MonoBehaviour, IInteractable
{
    public bool isGoalObject = false;

    void Start()
    {
        GetComponentInChildren<Text>().text = name;
    }

    public void OnInteracted()
    {

    }
}
