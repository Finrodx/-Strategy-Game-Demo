using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{

    bool iscreated = false;
    bool isSelected = false;
    //public Transform building;
    [SerializeField] private BuildingTypeSO activeBuildingType;


    void Update()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Transform building;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            worldPosition.z = -11;

            //discontunied. setting if building can be placeble indicator at mouse position



            //TODO fix underlaying color
            /*
            if (isSelected && !iscreated)
            {
                building = Instantiate(activeBuildingType.prefab, worldPosition, Quaternion.identity);
                buildingColor = building.GetComponentInChildren<SpriteRenderer>().color;

                Debug.Log("isSelected");
                building.position = worldPosition;

                if (CanSpawnBuilding(activeBuildingType, worldPosition))
                {
                    building.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                    iscreated = true;
                    isSelected = false;
                }
                else
                {
                    building.GetComponentInChildren<SpriteRenderer>().color = Color.red;
                }
            }
            */

            if (CanSpawnBuilding(activeBuildingType, worldPosition))
            {
                if (!iscreated)
                {
                    Instantiate(activeBuildingType.prefab, worldPosition, Quaternion.identity);
                }
                iscreated = true;
            }
            

        }
    }

    //set active building depending on selected building from UI
    public void SetActiveBuildingType(BuildingTypeSO buildingTypeSO)
    {
        iscreated = false;
        isSelected = true;
        activeBuildingType = buildingTypeSO;

    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }

    //check if building can spawn at mouse location
    private bool CanSpawnBuilding(BuildingTypeSO buildingTypeSO, Vector3 position)
    {
        BoxCollider2D buildingPrefabCollider = buildingTypeSO.prefab.GetComponent<BoxCollider2D>();

        if (Physics2D.OverlapBox(position + (Vector3)buildingPrefabCollider.offset, buildingPrefabCollider.size, 0))
        {
            return false;
        }
        else
        {
            return true;
        }
            
    }

}
