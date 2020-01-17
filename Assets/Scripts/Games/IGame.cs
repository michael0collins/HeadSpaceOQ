using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGame
{
   void OnStart();
   void OnWin();
   void OnLoss();
   void ReportScore();
}
