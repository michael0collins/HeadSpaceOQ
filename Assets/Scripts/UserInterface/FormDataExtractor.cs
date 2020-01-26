using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FormDataExtractor : MonoBehaviour
{
    public GameObject balanceTestDataContainer;
    public GameObject objectTrackingTestDataContainer;
    public GameObject memoryTestDataContainer;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void SetBalanceTestDataObjects()
    {
        gameManager.balanceTestGameDatas.Clear();

        foreach(FormItem item in balanceTestDataContainer.GetComponentsInChildren<FormItem>())
        {
            BalanceTestGameData datas = new BalanceTestGameData();

            datas.baselineTestDuration = Convert.ToInt32(item.transform.Find("BaselineDuration").GetComponentInChildren<Text>().text);
            datas.balanceTestDuration = Convert.ToInt32(item.transform.Find("TestedDuration").GetComponentInChildren<Text>().text);
            datas.recordingGranularity = Convert.ToInt32(item.transform.Find("InputGranularity").GetComponentInChildren<Text>().text);
            datas.muteNaration = item.transform.Find("MuteNarationToggle").GetComponentInChildren<Toggle>().isOn;
            datas.muteNaration = item.transform.Find("UseMusicToggle").GetComponentInChildren<Toggle>().isOn;

            gameManager.balanceTestGameDatas.Add(datas);

        }   

        foreach(BalanceTestGameData data in gameManager.balanceTestGameDatas)
        {
            print("BALANCETEST: First test time: " + data.baselineTestDuration.ToString() + ", seconds test time: " + data.balanceTestDuration.ToString() + 
                ", granularity: " + data.recordingGranularity.ToString());
        }
    }

    private void SetMemoryTestObjects()
    {
        gameManager.memoryTestGameDatas.Clear();

        foreach(FormItem item in memoryTestDataContainer.GetComponentsInChildren<FormItem>())
        {
            MemoryTestGameData datas = new MemoryTestGameData();

            datas.roundDuration = Convert.ToInt32(item.transform.Find("RoundDuration").GetComponentInChildren<Text>().text);
            datas.numberOfShapes = Convert.ToInt32(item.transform.Find("NumberOfShapes").transform.Find("Label").GetComponentInChildren<Text>().text);
            datas.objectShowDuration = Convert.ToInt32(item.transform.Find("ShapeShowDuration").GetComponentInChildren<Text>().text);
        
            gameManager.memoryTestGameDatas.Add(datas);
        }

        foreach(MemoryTestGameData data in gameManager.memoryTestGameDatas)
        {
            print("MEMORYTEST: There are " + data.roundDuration.ToString() + " rounds, " + data.numberOfShapes.ToString() + " cups, and " + data.objectShowDuration.ToString() + " speed.");
        }
    }

    private void SetObjectTrackingDataObjects()
    {
        gameManager.objectTrackingTestGameDatas.Clear();

        foreach(FormItem item in objectTrackingTestDataContainer.GetComponentsInChildren<FormItem>())
        {
            ObjectTrackingTestGameData datas = new ObjectTrackingTestGameData();
            
            datas.roundDuration = Convert.ToInt32(item.transform.Find("RoundDurationInput").GetComponentInChildren<Text>().text);
            datas.numberOfCups = Convert.ToInt32(item.transform.Find("NumberOfCups").transform.Find("Label").GetComponent<Text>().text);
            datas.objectMovementSpeed = Convert.ToInt32(item.transform.Find("ObjectMovementSpeed").GetComponentInChildren<Text>().text);

            gameManager.objectTrackingTestGameDatas.Add(datas);
        }

        foreach(ObjectTrackingTestGameData data in gameManager.objectTrackingTestGameDatas)
        {
            print("OBJECTTRACKING: There are " + data.roundDuration.ToString() + " rounds, " + data.numberOfCups.ToString() + " cups, and " + data.objectMovementSpeed.ToString() + " speed.");
        }
    }

    public void ExtractData()
    {
        SetBalanceTestDataObjects();
        SetMemoryTestObjects();
        SetObjectTrackingDataObjects();
    }
}
