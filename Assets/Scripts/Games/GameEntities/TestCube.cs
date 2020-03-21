using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour, IInteractable
{
    public void OnInteracted()
    {
        Destroy(gameObject);
    }
}
