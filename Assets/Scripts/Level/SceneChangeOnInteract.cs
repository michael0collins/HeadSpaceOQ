using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SceneChanger))]
public class SceneChangeOnInteract : MonoBehaviour, IInteractable
{
    public string scene;

    public void OnInteracted()
    {
        GetComponent<SceneChanger>().ChangeScene(scene);
    }
}
