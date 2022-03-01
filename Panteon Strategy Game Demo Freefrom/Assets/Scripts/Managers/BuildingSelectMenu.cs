using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelectMenu : MonoBehaviour
{

    [SerializeField] private List<BuildingTypeSO> buildingTypeSOList;
    [SerializeField] private BuildingManager buildingManager;
    [SerializeField] private Transform buildingBtnTemp;

    private Dictionary<BuildingTypeSO, Transform> buildingBtnDictionary;

    private void Awake()
    {
        buildingBtnTemp.gameObject.SetActive(false);

        buildingBtnDictionary = new Dictionary<BuildingTypeSO, Transform>();
        foreach (BuildingTypeSO buildingTypeSO in buildingTypeSOList)
        {
            Transform buildingBtnTransform = Instantiate(buildingBtnTemp, transform);
            buildingBtnTransform.gameObject.SetActive(true);

        
            buildingBtnTransform.GetComponentInChildren<Image>().sprite = buildingTypeSO.buttonSprite;

            buildingBtnTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                buildingManager.SetActiveBuildingType(buildingTypeSO);
            });

            //pairing UI button and building
            buildingBtnDictionary[buildingTypeSO] = buildingBtnTransform;
        }
    }

    private void UpdateSelectedButtonVisual()
    {
        BuildingTypeSO buildingTypeSO = buildingManager.GetActiveBuildingType();
        Transform selectedButton = buildingBtnDictionary[buildingTypeSO];
        selectedButton.GetComponentInChildren<Image>().color = Color.red;
    }

    // Start is called before the first frame update
    void Start()
    {
        //UpdateSelectedButtonVisual();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
 