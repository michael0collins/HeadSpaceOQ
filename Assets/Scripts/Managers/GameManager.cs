using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public enum VRDevice { Vive, Rift, Quest, Mobile, None };
    public VRDevice currentVRDevice = VRDevice.None;

    //for each list item create a new struct and load it into the games array
    public List<BalanceTestGameData> balanceTestGameDatas = new List<BalanceTestGameData>();
    public List<ObjectTrackingTestGameData> objectTrackingTestGameDatas = new List<ObjectTrackingTestGameData>();
    public List<MemoryTestGameData> memoryTestGameDatas = new List<MemoryTestGameData>();

    void Awake()
    {
        DetectCurrentVRDevice();
    }

    void Start()
    {
        if(currentVRDevice == VRDevice.Quest)
        {
            SetDefaultTestDatas();
            DebugConsole.DebugMessage = "Quest detected.";
        }
  
        SceneManager.LoadScene("MainMenu");
    }

    public void DetectCurrentVRDevice()
    {
        if (Application.platform == RuntimePlatform.Android)
            currentVRDevice = VRDevice.Quest;
    }
    
    public void ClearGameDatas()
    {
        balanceTestGameDatas.Clear();
        objectTrackingTestGameDatas.Clear();
        memoryTestGameDatas.Clear();
    }

    private void SetDefaultTestDatas()
    {
        BalanceTestGameData defaultBalanceTestDatas =  new BalanceTestGameData();
        defaultBalanceTestDatas.baselineTestDuration = 15;
        defaultBalanceTestDatas.balanceTestDuration = 15;
        defaultBalanceTestDatas.recordingGranularity = 1;
        defaultBalanceTestDatas.muteNaration = false;
        defaultBalanceTestDatas.useMusic = true;
        balanceTestGameDatas.Add(defaultBalanceTestDatas);
        
        //--Object Tracking--

        ObjectTrackingTestGameData defaultObjectTrackingTestDatas0 = new ObjectTrackingTestGameData();
        defaultObjectTrackingTestDatas0.roundDuration = 10;
        defaultObjectTrackingTestDatas0.objectMovementSpeed = 1.0f;
        defaultObjectTrackingTestDatas0.numberOfCups = 3;
        objectTrackingTestGameDatas.Add(defaultObjectTrackingTestDatas0);


        ObjectTrackingTestGameData defaultObjectTrackingTestDatas1 = new ObjectTrackingTestGameData();
        defaultObjectTrackingTestDatas1.roundDuration = 8;
        defaultObjectTrackingTestDatas1.objectMovementSpeed = 2.0f;
        defaultObjectTrackingTestDatas1.numberOfCups = 4;            
        objectTrackingTestGameDatas.Add(defaultObjectTrackingTestDatas1);

        ObjectTrackingTestGameData defaultObjectTrackingTestDatas2 = new ObjectTrackingTestGameData();
        defaultObjectTrackingTestDatas2.roundDuration = 8;
        defaultObjectTrackingTestDatas2.objectMovementSpeed = 2.0f;
        defaultObjectTrackingTestDatas2.numberOfCups = 4;
        objectTrackingTestGameDatas.Add(defaultObjectTrackingTestDatas2);

        //--Memory Test--

        MemoryTestGameData defaultMemoryTestData0 = new MemoryTestGameData();
        defaultMemoryTestData0.roundDuration = 10;
        defaultMemoryTestData0.objectShowDuration = 5.0f;
        defaultMemoryTestData0.numberOfShapes = 6;
        memoryTestGameDatas.Add(defaultMemoryTestData0);

        MemoryTestGameData defaultMemoryTestData1 = new MemoryTestGameData();
        defaultMemoryTestData1.roundDuration = 8;
        defaultMemoryTestData1.objectShowDuration = 3.0f;
        defaultMemoryTestData1.numberOfShapes = 8;
        memoryTestGameDatas.Add(defaultMemoryTestData1);

        MemoryTestGameData defaultMemoryTestData2 = new MemoryTestGameData();
        defaultMemoryTestData2.roundDuration = 5;
        defaultMemoryTestData2.objectShowDuration = 2.5f;
        defaultMemoryTestData2.numberOfShapes = 12;
        memoryTestGameDatas.Add(defaultMemoryTestData2);
    }
}

public struct BalanceTestGameData
{
     public int baselineTestDuration;
     public int balanceTestDuration;
     public int recordingGranularity;
     public bool muteNaration;
     public bool useMusic;
 }

 public struct ObjectTrackingTestGameData
 {
     public int roundDuration;
     public int numberOfCups;
     public float objectMovementSpeed;
 }

 public struct MemoryTestGameData
 {
     public int roundDuration;
     public int numberOfShapes;
     public float objectShowDuration;
 }
