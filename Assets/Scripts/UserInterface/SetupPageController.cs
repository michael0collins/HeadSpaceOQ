using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupPageController : MonoBehaviour
{
    public GameObject listObjectPrefab;
    public Transform container;

    public void AddListObject()
    {
        GameObject listItem = Instantiate(listObjectPrefab, container);
    }
}
