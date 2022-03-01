using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Deployable : MonoBehaviour
{
    // Start is called before the first frame update
    public int HP { get; set; }
    public string name;
    public bool isBuilding;
    public bool isUnit;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
