using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


//managing production and and information menu
public class SideMenuManager : MonoBehaviour
{
    public static SideMenuManager instance;

    [SerializeField] private GameObject productionMenu;
    [SerializeField] private GameObject informationMenu;
    [SerializeField] private Transform iconTemp;
    private List<Transform> tempIcons = new List<Transform>();


    [SerializeField] private List<Transform> soldierButtonList = new List<Transform>();

    private bool isBarrackActivated = false;
    private void Awake()
    {
        instance = this;
    }

    //closing side menus at start
    void Start()
    {
        productionMenu.SetActive(false);
        informationMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
           
            //if a building clicked open side menus
            if (hit != null && hit.collider != null)
            {
                if (hit.collider.name == "Barracks(Clone)")
                {
                    //fill information menu areas
                    GameObject barracks = hit.collider.gameObject;
                    informationMenu.transform.Find("Selected Building Name Text").GetComponent<Text>().text = barracks.GetComponent<Barracks>().name;
                    informationMenu.transform.Find("Selected Building Image").GetComponent<Image>().sprite = barracks.GetComponentInChildren<SpriteRenderer>().sprite;

                    Transform unitContainer = informationMenu.transform.Find("ProductionContainer");
                    unitContainer.Find("Header").GetComponent<Text>().text = "PRODUCTION";

                    Transform unitImages = unitContainer.Find("Unit Images");
                    
                    //fill soldier icons that building can produce
                    foreach (GameObject soldierPrefab in barracks.GetComponent<Barracks>().unitPrefabs)
                    {
                        if (isBarrackActivated)
                        {
                            break;
                        }

                        iconTemp.gameObject.SetActive(false);
                        Transform unitIcon = Instantiate(iconTemp, unitImages.transform);
                        unitIcon.GetComponent<Image>().sprite = soldierPrefab.GetComponentInChildren<SpriteRenderer>().sprite;
                        unitIcon.gameObject.SetActive(true);

                        tempIcons.Add(unitIcon);
                    }

                    //set which building unit is produce from
                    foreach (Transform btn in soldierButtonList)
                    {
                        btn.GetComponent<UnitSpawner>().building = barracks;
                    }

                    if (barracks.GetComponent<Barracks>().isAProductionBuilding)
                    {
                        iconTemp.gameObject.SetActive(false);
                        productionMenu.SetActive(true);
                    }
                    informationMenu.SetActive(true);
                    isBarrackActivated = true;
                }

                //set side menu for power plant building
                else if (hit.collider.name == "Power Plant(Clone)")
                {
                    iconTemp.gameObject.SetActive(false);
                    productionMenu.SetActive(false);
                    GameObject powerPlant = hit.collider.gameObject;
                    informationMenu.transform.Find("Selected Building Name Text").GetComponent<Text>().text = powerPlant.GetComponent<PowerPlant>().name;
                    informationMenu.transform.Find("Selected Building Image").GetComponent<Image>().sprite = powerPlant.GetComponentInChildren<SpriteRenderer>().sprite;
                    Debug.Log(hit.collider.name);
                    Debug.Log(hit.collider.gameObject.GetComponent<PowerPlant>().name);


                    if (powerPlant.GetComponent<PowerPlant>().isAProductionBuilding)
                    {
                        productionMenu.SetActive(true);
                    }
                    else
                    {
                        Transform unitContainer = informationMenu.transform.Find("ProductionContainer");
                        unitContainer.Find("Header").GetComponent<Text>().text = "";
                    }

                    informationMenu.SetActive(true);
                }

                

            }
            else
            {
                CloseUI();
            }
        }
    }

    private void CloseUI()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            informationMenu.SetActive(false);
            productionMenu.SetActive(false);
        }

    }
}
