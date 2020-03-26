using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingObject : MonoBehaviour, IInteractable
{
    public bool isGoalObject = false;
    public enum ObjectType { A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z };
    public ObjectType objectType;

    [System.Obsolete]
    void Start()
    {
        GetComponentInChildren<MeshRenderer>().material.SetColor("", new Color(Random.RandomRange(0, 1), Random.RandomRange(0, 1), Random.RandomRange(0, 1)));
    }

    public void OnInteracted()
    {
        if(isGoalObject)
            FindObjectOfType<ObjectMatching>().totalCollected++;
        
        Destroy(gameObject);
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
