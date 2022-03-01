using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant : Building
{

    public BuildingTypeSO powerPlantSO;


    private void Start()
    {
        HP = powerPlantSO.buildingHP;
        name = powerPlantSO.name;
        isAProductionBuilding = powerPlantSO.isAProductionBuilding;
        isAResourceBuilding = powerPlantSO.isAResourceBuilding;
        isBuilding = powerPlantSO.isBuilding;
        isUnit = powerPlantSO.isUnit;
    }


}
