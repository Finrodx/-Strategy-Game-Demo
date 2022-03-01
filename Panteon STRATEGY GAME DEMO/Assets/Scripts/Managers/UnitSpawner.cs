using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public GameObject unit;
    public GameObject building;
    public GameObject gameScreen;
    ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    //Spawn selected unit in game screen from Object Pool
    public void SpawnUnit()
    {
        Debug.Log(unit.name);
        Vector3 buildingSpawnPos = building.transform.Find("Spawn Point").position;
        Vector3 unitSpawnPos = new Vector3(buildingSpawnPos.x, buildingSpawnPos.y, -5);
        GameObject newUnit = objectPooler.SpawnFromPool(unit.name, unitSpawnPos);
        newUnit.transform.position = unitSpawnPos;

    }
}
