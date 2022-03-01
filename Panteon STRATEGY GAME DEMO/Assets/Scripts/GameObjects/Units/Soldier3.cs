using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier3 : Soldier
{
    public SoldierTypeSO soldierSO;

    private void Start()
    {
        HP = soldierSO.unitHP;
        soldierDPS = soldierSO.soldierDPS;
        isBuilding = soldierSO.isBuilding;
        isUnit = soldierSO.isUnit;
    }
}
