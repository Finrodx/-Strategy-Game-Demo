using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class SoldierTypeSO : ScriptableObject
{
    public Transform prefab;
    public Sprite buttonSprite;
    public int unitHP;
    public int soldierDPS;
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
