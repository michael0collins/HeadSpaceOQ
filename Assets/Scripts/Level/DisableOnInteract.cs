using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnInteract : MonoBehaviour, IInteractable
{
    public GameObject go;

    public void OnInteracted()
    {
        go.SetActive(false);
    }
}
