using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{
    public List<GameObject> unitPrefabs;

    [SerializeField] private BuildingTypeSO barracksSO;

    private void Start()
    {
        HP = barracksSO.buildingHP;
        name = barracksSO.name;
        isAProductionBuilding = barracksSO.isAProductionBuilding;
        isAResourceBuilding = barracksSO.isAResourceBuilding;
        isBuilding = barracksSO.isBuilding;
        isUnit = barracksSO.isUnit;
    }

}
