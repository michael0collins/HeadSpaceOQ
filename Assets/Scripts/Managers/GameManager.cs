using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    public bool instructions = true;
    public enum VRDevice { Vive, Rift, Quest, Mobile, None };
    public VRDevice currentVRDevice = VRDevice.None;

    //for each list item create a new struct and load it into the games array
    public List<BalanceTestGameData> balanceTestGameDatas = new List<BalanceTestGameData>();
    public List<ObjectTrackingTestGameData> objectTrackingTestGameDatas = new List<ObjectTrackingTestGameData>();
    public List<MemoryTestGameData> memoryTestGameDatas = new List<MemoryTestGameData>();

    public int gamesComplete;

    void Awake()
    {
        DetectCurrentVRDevice();
    }

    void Start()
    {
        if(currentVRDevice == VRDevice.None)
        {
            SetDefaultTestDatas();
            DebugConsole.DebugMessage = "There does not seem to be a valid VR headset connected.";
        }

        if(currentVRDevice == VRDevice.Quest)
        {
            SetDefaultTestDatas();
            //SceneManager.LoadScene("MainMenu");
            DebugConsole.DebugMessage = "Quest detected.";
        }
    }

    public void DetectCurrentVRDevice()
    {
        if (Application.platform == RuntimePlatform.Android)
            currentVRDevice = VRDevice.Quest;
    }

    //Replace
    public void CheckGamesComplete()
    {
        if(gamesComplete == 3)
        {
            GameObject.Find("Timer").GetComponent<Text>().text = "Thank you for participating in our test. <br> Please consult your phsyician with any questions..";
        }
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
        defaultObjectTrackingTestDatas1.objectMovementSpeed = 1.25f;
        defaultObjectTrackingTestDatas1.numberOfCups = 4;            
        objectTrackingTestGameDatas.Add(defaultObjectTrackingTestDatas1);

        ObjectTrackingTestGameData defaultObjectTrackingTestDatas2 = new ObjectTrackingTestGameData();
        defaultObjectTrackingTestDatas2.roundDuration = 7;
        defaultObjectTrackingTestDatas2.objectMovementSpeed = 1.5f;
        defaultObjectTrackingTestDatas2.numberOfCups = 5;
        objectTrackingTestGameDatas.Add(defaultObjectTrackingTestDatas2);

        ObjectTrackingTestGameData defaultObjectTrackingTestDatas3 = new ObjectTrackingTestGameData();
        defaultObjectTrackingTestDatas3.roundDuration = 6;
        defaultObjectTrackingTestDatas3.objectMovementSpeed = 1.75f;
        defaultObjectTrackingTestDatas3.numberOfCups = 5;
        objectTrackingTestGameDatas.Add(defaultObjectTrackingTestDatas3);


        ObjectTrackingTestGameData defaultObjectTrackingTestDatas4 = new ObjectTrackingTestGameData();
        defaultObjectTrackingTestDatas4.roundDuration = 5;
        defaultObjectTrackingTestDatas4.objectMovementSpeed = 2f;
        defaultObjectTrackingTestDatas4.numberOfCups = 5;            
        objectTrackingTestGameDatas.Add(defaultObjectTrackingTestDatas4);

        ObjectTrackingTestGameData defaultObjectTrackingTestDatas5 = new ObjectTrackingTestGameData();
        defaultObjectTrackingTestDatas5.roundDuration = 4;
        defaultObjectTrackingTestDatas5.objectMovementSpeed = 2.25f;
        defaultObjectTrackingTestDatas5.numberOfCups = 5;
        objectTrackingTestGameDatas.Add(defaultObjectTrackingTestDatas5);

        ObjectTrackingTestGameData defaultObjectTrackingTestDatas6 = new ObjectTrackingTestGameData();
        defaultObjectTrackingTestDatas6.roundDuration = 4;
        defaultObjectTrackingTestDatas6.objectMovementSpeed = 2.5f;
        defaultObjectTrackingTestDatas6.numberOfCups = 5;
        objectTrackingTestGameDatas.Add(defaultObjectTrackingTestDatas6);

        ObjectTrackingTestGameData defaultObjectTrackingTestDatas7 = new ObjectTrackingTestGameData();
        defaultObjectTrackingTestDatas7.roundDuration = 4;
        defaultObjectTrackingTestDatas7.objectMovementSpeed = 2.75f;
        defaultObjectTrackingTestDatas7.numberOfCups = 5;
        objectTrackingTestGameDatas.Add(defaultObjectTrackingTestDatas7);

        ObjectTrackingTestGameData defaultObjectTrackingTestDatas8 = new ObjectTrackingTestGameData();
        defaultObjectTrackingTestDatas8.roundDuration = 4;
        defaultObjectTrackingTestDatas8.objectMovementSpeed = 3f;
        defaultObjectTrackingTestDatas8.numberOfCups = 5;
        objectTrackingTestGameDatas.Add(defaultObjectTrackingTestDatas8);

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
