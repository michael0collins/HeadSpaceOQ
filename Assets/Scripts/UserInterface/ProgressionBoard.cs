using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionBoard : MonoBehaviour
{
    public GameObject boardElementPrefab;
    private Transform _elementContainer;

    void Start()
    {
        _elementContainer = transform;
    }
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.A))
            //AddBoardElement("Test");
    }
    public void AddBoardElement(string text)
    {
        GameObject boardElementClone = Instantiate(boardElementPrefab, _elementContainer) as GameObject;
        boardElementClone.GetComponentInChildren<Text>().text = text;
    }   
}
