using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    //for each list item create a new struct and load it into the games array
    public BalanceTestGameData[] balanceTestGameDatas;
    public ObjectTrackingTestGameData[] objectTrackingTestGameDatas;
    public MemoryTestGameData[] memoryTestGameDatas;

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
         public int incorrectAnswersToLose;
         public int roundDuration;
         public int numberOfCups;
     }

     public struct MemoryTestGameData
     {
         public int incorrectAnswersToLose;
         public int roundDuration;
         public int numberOfShapes;
         public float objectShowDuration;
     }
}
