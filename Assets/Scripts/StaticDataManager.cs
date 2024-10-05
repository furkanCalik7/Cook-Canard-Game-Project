using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDataManager : MonoBehaviour
{
   private void Awake() {
    TrashCounter.ResetStaticData();
    CuttingCounter.ResetStaticData();
    BaseCounter.ResetStaticData();
   } 
}
