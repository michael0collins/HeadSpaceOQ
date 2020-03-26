using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchingObject : MonoBehaviour, IInteractable
{
    public bool isGoalObject = false;
    private ObjectMatching objectMatching;

    void Start()
    {
        objectMatching = FindObjectOfType<ObjectMatching>();
        GetComponentInChildren<Text>().text = name.Substring(0, name.Length - 7);
    }

    public void OnInteracted()
    {
        if (isGoalObject)
            objectMatching.totalCorrect++;
        else
            objectMatching.incorrectGuess = true;

        //play sound

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if(isGoalObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(transform.position, Vector3.one / 3);
        }
    }
}
