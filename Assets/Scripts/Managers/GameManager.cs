using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public enum VRDevice { Vive, Rift, Quest };
    public VRDevice currentVRDevice;

    //for each list item create a new struct and load it into the games array
    public List<BalanceTestGameData> balanceTestGameDatas = new List<BalanceTestGameData>();
    public List<ObjectTrackingTestGameData> objectTrackingTestGameDatas = new List<ObjectTrackingTestGameData>();
    public List<MemoryTestGameData> memoryTestGameDatas = new List<MemoryTestGameData>();

    public void DetectCurrentVRDevice()
    {
        //Get the currently connected device and set the VRDevice enum.
    }
    
    public void ClearGameDatas()
    {
        balanceTestGameDatas.Clear();
        objectTrackingTestGameDatas.Clear();
        memoryTestGameDatas.Clear();
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
