using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class BuildingTypeSO : ScriptableObject
{
    public Transform prefab;
    public Sprite buttonSprite;
    public int buildingHP;
    public List<GameObject> units;
    public bool isAProductionBuilding;
    public bool isAResourceBuilding;
    public bool isBuilding;
    public bool isUnit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
